using UnityEngine;


[RequireComponent(typeof(PlayerEntry))]
public class PlayerInput : LocalInputHandler
{
    private PlayerEntry _player;

    public override string Name => "player";

    private void Awake()
    {
        _player = GetComponent<PlayerEntry>();

        AttachAction(
            (int)ActionIDs.MoveForward, 
            _player.MoveForward, 
            () => Input.GetKey(SKeyCodeMap.PlayerForwardMove));

        AttachAction(
            (int)ActionIDs.MoveBackward, 
            _player.MoveBackward, 
            () => Input.GetKey(SKeyCodeMap.PlayerBackwardMove));

        AttachAction(
            (int)ActionIDs.MoveLeft,
            _player.MoveLeft, 
            () => Input.GetKey(SKeyCodeMap.PlayerLeftMove));

        AttachAction(
            (int)ActionIDs.MoveRight, 
            _player.MoveRight,
            () => Input.GetKey(SKeyCodeMap.PlayerRightMove));

        AttachAction(
            (int)ActionIDs.Jump,
            _player.GroundJump2,
            () => Input.GetKey(SKeyCodeMap.PlayerJump));

        AttachAction(
            (int)ActionIDs.SprintOn, 
            _player.EnableSprintMode,
            () => Input.GetKey(SKeyCodeMap.PlayerSprint));

        AttachAction(
            (int)ActionIDs.SprintOff, 
            _player.DisableSprintMode,
            () => Input.GetKeyUp(SKeyCodeMap.PlayerSprint));
    }

    private void Update() => HandleInput();

}
