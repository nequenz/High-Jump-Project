using UnityEngine;

public class PlayerInput : LocalInput
{
    public const int MoveForwardActionID = 0;
    public const int MoveBackwardActionID = 1;
    public const int MoveLeftActionID = 2;
    public const int MoveRightActionID =3;
    public const int JumpActionID = 4;
    public const int SprintOnActionID = 5;
    public const int SprintOffActionID = 6;
    public const int SwingSprintOnActionID = 7;
    public const int SwingSpringOffActionID = 8;

    private PlayerMove _move;

    public override int ID => 0;
    public KeyCode ForwardMoveKey { get; private set; } = KeyCode.W;
    public KeyCode BackwardMoveKey { get; private set; } = KeyCode.S;
    public KeyCode LeftMoveKey { get; private set; } = KeyCode.A;
    public KeyCode RightMoveKey { get; private set; } = KeyCode.D;
    public KeyCode JumpKey { get; private set; } = KeyCode.Space;
    public KeyCode SprintKey { get; private set; } = KeyCode.LeftShift;

    private void Awake()
    {
        _move = GetComponent<PlayerMove>();

        AttachAction(MoveForwardActionID, _move.MoveForward, KeyMode.Hold, ForwardMoveKey);
        AttachAction(MoveBackwardActionID, _move.MoveBackward, KeyMode.Hold, BackwardMoveKey);
        AttachAction(MoveLeftActionID, _move.MoveLeft, KeyMode.Hold, LeftMoveKey);
        AttachAction(MoveRightActionID, _move.MoveRight, KeyMode.Hold, RightMoveKey);
        AttachAction(JumpActionID, _move.GroundJump2, KeyMode.Hold, JumpKey);
        AttachAction(SprintOnActionID, _move.EnableSprintMode, KeyMode.Hold, SprintKey);
        AttachAction(SprintOffActionID, _move.DisableSprintMode, KeyMode.Up, SprintKey);
        AttachAction(SwingSprintOnActionID, _move.EnableSwingSprintMode, KeyMode.Hold, SprintKey);
        AttachAction(SwingSpringOffActionID, _move.DisableSwingSprintMode, KeyMode.Up, SprintKey);

        DisableAction(SwingSprintOnActionID);
        DisableAction(SwingSpringOffActionID);
    }

    private void Update() => HandleInput();

}
