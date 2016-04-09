using UnityEngine;
using System.Collections;
using InControl;

public class PlayerCharacterActions : PlayerActionSet
{
    public PlayerAction MoveLeft;
    public PlayerAction MoveRight;
    public PlayerAction MoveUp;
    public PlayerAction MoveDown;
    public PlayerAction AimLeft;
    public PlayerAction AimRight;
    public PlayerAction AimUp;
    public PlayerAction AimDown;
    public PlayerTwoAxisAction Aim;
    public PlayerTwoAxisAction Move;
    public PlayerAction Jump;
    public PlayerAction PickUp;
    public PlayerAction Shoot;
    public PlayerAction Dash;
    public PlayerAction PrimaryAttack;
    public PlayerAction SecondaryAttack;

    public PlayerCharacterActions()
    {
        // Movement
        MoveLeft = CreatePlayerAction("Move Left");
        MoveRight = CreatePlayerAction("Move Right");
        MoveUp = CreatePlayerAction("Move Up");
        MoveDown = CreatePlayerAction("Move Down");
        Move = CreateTwoAxisPlayerAction(MoveLeft, MoveRight, MoveDown, MoveUp);

        // Aiming
        AimLeft = CreatePlayerAction("Aim Left");
        AimRight = CreatePlayerAction("Aim Right");
        AimUp = CreatePlayerAction("Aim Up");
        AimDown = CreatePlayerAction("Aim Down");
        Aim = CreateTwoAxisPlayerAction(AimLeft, AimRight, AimDown, AimUp);

        // Actions
        Jump = CreatePlayerAction("Jump");
        PickUp = CreatePlayerAction("Crouch");
        Shoot = CreatePlayerAction("Walk");
        Dash = CreatePlayerAction("Dash");
        PrimaryAttack = CreatePlayerAction("Primary Attack");
        SecondaryAttack = CreatePlayerAction("Secondary Attack");
    }

    public static PlayerCharacterActions GetDefaultBindings(bool includeKeyboardActions, bool includeControllerActions)
    {
        var actions = new PlayerCharacterActions();

        // Keyboard actions
        if(includeKeyboardActions)
        {
            AddDefaultKeyboardBindings(actions);
        }

        // Controller actions
        if(includeControllerActions)
        {
            AddDefaultControllerBindings(actions);
        }

        return actions;
    }

    private static void AddDefaultKeyboardBindings(PlayerCharacterActions actions)
    {
        // Movement
        actions.MoveLeft.AddDefaultBinding( Key.A );
        actions.MoveLeft.AddDefaultBinding( Key.LeftArrow );
        actions.MoveRight.AddDefaultBinding( Key.D );
        actions.MoveRight.AddDefaultBinding( Key.RightArrow );
        actions.MoveUp.AddDefaultBinding( Key.W );
        actions.MoveUp.AddDefaultBinding( Key.UpArrow );
        actions.MoveDown.AddDefaultBinding( Key.S );
        actions.MoveDown.AddDefaultBinding( Key.DownArrow );
        
        // Aiming
        // TODO: Add mouse aiming using custom source

        // Actions
        actions.Jump.AddDefaultBinding( Key.Space );
        actions.PickUp.AddDefaultBinding( Key.C );
        actions.PickUp.AddDefaultBinding( Key.LeftControl );
        actions.Shoot.AddDefaultBinding( Key.LeftShift );
        actions.Dash.AddDefaultBinding( Mouse.MiddleButton );
        actions.PrimaryAttack.AddDefaultBinding( Mouse.LeftButton );
        actions.SecondaryAttack.AddDefaultBinding( Mouse.RightButton );
    }

    private static void AddDefaultControllerBindings(PlayerCharacterActions actions)
    {
        // Movement
        actions.MoveLeft.AddDefaultBinding( InputControlType.DPadLeft );
        actions.MoveLeft.AddDefaultBinding( InputControlType.LeftStickLeft );
        actions.MoveRight.AddDefaultBinding( InputControlType.DPadRight );
        actions.MoveRight.AddDefaultBinding( InputControlType.LeftStickRight );
        actions.MoveUp.AddDefaultBinding( InputControlType.DPadUp );
        actions.MoveUp.AddDefaultBinding( InputControlType.LeftStickUp );
        actions.MoveDown.AddDefaultBinding( InputControlType.DPadDown );
        actions.MoveDown.AddDefaultBinding( InputControlType.LeftStickDown );
        
        // Aiming
        actions.AimLeft.AddDefaultBinding( InputControlType.RightStickLeft );
        actions.AimRight.AddDefaultBinding( InputControlType.RightStickRight );
        actions.AimUp.AddDefaultBinding( InputControlType.RightStickUp );
        actions.AimDown.AddDefaultBinding( InputControlType.RightStickDown );
        
        // Actions
        actions.Jump.AddDefaultBinding( InputControlType.Action1 );
        actions.PickUp.AddDefaultBinding( InputControlType.Action2 );
        actions.Shoot.AddDefaultBinding( InputControlType.Action3 );
        actions.Dash.AddDefaultBinding( InputControlType.Action4 );
        actions.PrimaryAttack.AddDefaultBinding( InputControlType.LeftBumper );
        actions.SecondaryAttack.AddDefaultBinding( InputControlType.RightBumper );
    }

    // TODO: Implement custom source, reading the mouse position on screen and calculating a vector from the player to the mouse (see: https://www.youtube.com/watch?v=5wnQ9hSBB0I)
}