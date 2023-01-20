
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

    private void Awake()
    {
        _move = GetComponent<PlayerMove>();

        AttachAction(MoveForwardActionID, _move.MoveForward, KeyMode.Hold, KeyCodes.PlayerForwardMove);
        AttachAction(MoveBackwardActionID, _move.MoveBackward, KeyMode.Hold, KeyCodes.PlayerBackwardMove);
        AttachAction(MoveLeftActionID, _move.MoveLeft, KeyMode.Hold, KeyCodes.PlayerLeftMove);
        AttachAction(MoveRightActionID, _move.MoveRight, KeyMode.Hold, KeyCodes.PlayerRightMove);
        AttachAction(JumpActionID, _move.GroundJump2, KeyMode.Hold, KeyCodes.PlayerJump);
        AttachAction(SprintOnActionID, _move.EnableSprintMode, KeyMode.Hold, KeyCodes.PlayerSprint);
        AttachAction(SprintOffActionID, _move.DisableSprintMode, KeyMode.Up, KeyCodes.PlayerSprint);
        AttachAction(SwingSprintOnActionID, _move.EnableSwingSprintMode, KeyMode.Hold, KeyCodes.PlayerSprint);
        AttachAction(SwingSpringOffActionID, _move.DisableSwingSprintMode, KeyMode.Up, KeyCodes.PlayerSprint);

        DisableAction(SwingSprintOnActionID);
        DisableAction(SwingSpringOffActionID);
    }

    private void Update() => HandleInput();

}
