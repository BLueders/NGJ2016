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

    public Transform aimPivot;
    Vector3 moveDirection;
    bool mouseEnabled;
    bool applicationHasFocus = true;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        throwComponent = GetComponent<ThrowComponent>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Start()
    {

    }
    
    void Update()
    {
        var inputDevice = (InputManager.Devices.Count > PlayerID) ? InputManager.Devices[PlayerID] : null;

        if(inputDevice == null){
            ErrorHelper.DisplayError(ErrorMessage.PlayerDisconnected[PlayerID]);
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
        float x = inputDevice.Direction.X;
        float y = inputDevice.Direction.Y;
        bool jump = inputDevice.Action1.WasPressed;

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
        bool action1Pressed = inputDevice.Action2.IsPressed;;
        bool action1Down = !inputDevice.Action2.WasPressed;

        bool action2Pressed = inputDevice.Action3.IsPressed;
        bool action2Down = !inputDevice.Action3.WasPressed;

        throwComponent.HandleAction1(action1Pressed, action1Down);
        throwComponent.HandleAction2(action2Pressed, action2Down);
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