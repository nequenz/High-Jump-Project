using UnityEngine;


[RequireComponent(typeof(PlayerMove))]
public class PlayerInput : LocalInputHandler
{
    private PlayerMove _move;

    public override int ID => 0;

    private void Awake()
    {
        _move = GetComponent<PlayerMove>();

        AttachAction(InputActionIDs.MoveForward, _move.MoveForward, KeyMode.Hold, KeyCodes.PlayerForwardMove);
        AttachAction(InputActionIDs.MoveBackward, _move.MoveBackward, KeyMode.Hold, KeyCodes.PlayerBackwardMove);
        AttachAction(InputActionIDs.MoveLeft, _move.MoveLeft, KeyMode.Hold, KeyCodes.PlayerLeftMove);
        AttachAction(InputActionIDs.MoveRight, _move.MoveRight, KeyMode.Hold, KeyCodes.PlayerRightMove);
        AttachAction(InputActionIDs.Jump, _move.GroundJump2, KeyMode.Hold, KeyCodes.PlayerJump);
        AttachAction(InputActionIDs.SprintOn, _move.EnableSprintMode, KeyMode.Hold, KeyCodes.PlayerSprint);
        AttachAction(InputActionIDs.SprintOff, _move.DisableSprintMode, KeyMode.Up, KeyCodes.PlayerSprint);
        AttachAction(InputActionIDs.SwingSprintOn, _move.EnableSwingSprintMode, KeyMode.Hold, KeyCodes.PlayerSprint);
        AttachAction(InputActionIDs.SwingSpringOff, _move.DisableSwingSprintMode, KeyMode.Up, KeyCodes.PlayerSprint);

        DisableAction(InputActionIDs.SwingSpringOff);
        DisableAction(InputActionIDs.SwingSprintOn);
    }

    private void Update() => HandleInput();

}
