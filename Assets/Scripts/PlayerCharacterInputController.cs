using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(ThrowComponent))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerCharacterInputController : MonoBehaviour
{
    public PlayerMovement playerMovement { get; private set; }
    public ThrowComponent throwComponent { get; private set; }
    public PlayerHealth playerHealth { get; private set; }

    public PlayerCharacterActions playerCharacterActions { get; private set; }
    public Transform aimPivot;
    Vector3 moveDirection;
    bool mouseEnabled;
    bool applicationHasFocus = true;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        throwComponent = GetComponent<ThrowComponent>();
    }

    void Start()
    {
    }
    
    void FixedUpdate()
    {
        if(playerHealth.IsAlive()) // Only allow input when alive
        {
            HandleMove();
        }
    }

    void Update()
    {
        if(playerHealth.IsAlive()) // Only allow input when alive
        {
            //HandleAim();
            HandleAttack();
        }
    }

    void HandleMove()
    {
        // Read inputs
        float x = playerCharacterActions.Move.X;
        float y = playerCharacterActions.Move.Y;
        bool jump = playerCharacterActions.Jump.IsPressed;
        bool walk = playerCharacterActions.Walk.IsPressed;

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
    void HandleAttack()
    {
        // Read inputs
        bool primaryAttack = playerCharacterActions.PrimaryAttack.IsPressed;
        bool secondaryAttack = playerCharacterActions.SecondaryAttack.IsPressed;

        // Primary attack takes precedence
        if(primaryAttack)
        {
            // Attack if able
            weaponInventory.GetEquippedWeapon().UsePrimaryAttack(aimPivot.position, aimPivot.forward);
        }
        else if(secondaryAttack)
        {
            // Attack if able
            weaponInventory.GetEquippedWeapon().UseSecondaryAttack(aimPivot.position, aimPivot.forward);
        }
    }

    public void SetupPlayerActions(bool includeKeyboardActions, bool includeControllerActions)
    {
        playerCharacterActions = PlayerCharacterActions.GetDefaultBindings(includeKeyboardActions, includeControllerActions);
        mouseEnabled = includeKeyboardActions;
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