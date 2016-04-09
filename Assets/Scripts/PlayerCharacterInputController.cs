using UnityEngine;
using System.Collections;
using InControl;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(ThrowComponent))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerCharacterInputController : MonoBehaviour
{
    public int PlayerID;

    public PlayerMovement playerMovement { get; private set; }
    public ThrowComponent throwComponent { get; private set; }
    public PlayerHealth playerHealth { get; private set; }
    public PlayerCharacterActions playerCharacterActions { get; private set; }

    public Transform aimPivot;
    Vector3 moveDirection;
    bool mouseEnabled;
    bool applicationHasFocus = true;

    InputDevice inputDevice;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        throwComponent = GetComponent<ThrowComponent>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Start()
    {
        if(playerCharacterActions == null)
        {
            playerCharacterActions = PlayerCharacterActions.GetDefaultBindings(true, true);
            AttachInputDevice(playerCharacterActions);
            playerCharacterActions.Device = inputDevice;
        }
    }

    void AttachInputDevice(PlayerCharacterActions actions){
        inputDevice = (InputManager.Devices.Count > PlayerID) ? InputManager.Devices[PlayerID] : null;
        actions.Device = inputDevice;
    }

    void Update()
    {
        if(inputDevice == null){
            ErrorHelper.DisplayError(ErrorMessage.PlayerDisconnected[PlayerID]);
            AttachInputDevice(playerCharacterActions);
        }

        if(playerHealth.IsAlive()) // Only allow input when alive
        {
            HandleMove(inputDevice);
        }

        if(playerHealth.IsAlive()) // Only allow input when alive
        {
            //HandleAim();
            HandleAttack(inputDevice);
        }
    }

    void HandleMove(InputDevice inputDevice)
    {
        // Read inputs
        float x = playerCharacterActions.Move.X;
        float y = playerCharacterActions.Move.Y;
        bool jump = playerCharacterActions.Jump.WasPressed;

        playerMovement.Move(x,y,jump);
    }
    /*
    void HandleAim()
    {
        // Read inputs
        Vector2 aimDirection = mouseEnabled ? GetMouseAimDirection() : playerCharacterActions.Aim.Value;

        // Update aim pivot if able
        if (aimPivot != null)
        {
            if (aimDirection != Vector2.zero) // If aiming using stick
            {
                var aimAngle = MathHelper.VectorToAngle(aimDirection);
                
                // HACK: Offsetting stick aim angle by 90 degrees to fix incorrect aim angle
                aimPivot.rotation = Quaternion.Euler(0, -aimAngle + 90, 0);
            }
            else // If not aiming using stick
            {
                // Use character facing direction for aim
                aimPivot.localRotation = Quaternion.identity;
            }
        }
    }
    */
    void HandleAttack(InputDevice inputDevice)
    {
        // Read inputs
        bool action1Pressed = playerCharacterActions.PickUp.IsPressed;
        bool action1Down = playerCharacterActions.PickUp.WasPressed;
		bool action1Up = playerCharacterActions.Shoot.WasReleased;

        bool action2Pressed = playerCharacterActions.Shoot.IsPressed;
        bool action2Down = playerCharacterActions.Shoot.WasPressed;
		bool action2Up = playerCharacterActions.Shoot.WasReleased;

		throwComponent.HandleAction1(action1Pressed, action1Down, action1Up);
		throwComponent.HandleAction2(action2Pressed, action2Down, action2Up);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        applicationHasFocus = hasFocus;
    }

    Vector2 GetMouseAimDirection()
    {
        Vector2 aimDirection = Vector2.zero;

        if(applicationHasFocus)
        {
            Vector3 aimPointWorldSpace = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            float maxDistance = 100f;
            
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                aimPointWorldSpace = hit.point;
            }
            
            Vector3 deltaPosition = aimPointWorldSpace - transform.position;
            aimDirection.Set(deltaPosition.x, deltaPosition.z);
            aimDirection.Normalize();
        }

        return aimDirection;
    }
}