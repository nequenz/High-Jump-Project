public sealed class PlayerStateRun : PlayerState
{

    public PlayerStateRun(object target) : base(target)
    {

    }

    protected override void OnStart()
    {
        View.SetBool(AnimationNames.ToRun, true);
        Input.EnableAction(PlayerInput.SprintOnActionID);
        Input.EnableAction(PlayerInput.SprintOffActionID);
    }

    protected override void OnExit()
    {
        View.SetBool(AnimationNames.ToRun, false);
        Input.DisableAction(PlayerInput.SprintOnActionID);
        Input.DisableAction(PlayerInput.SprintOffActionID);
    }

    public bool ToWalkState()
    {
        return Move.IsOnGroundChached
            && Move.VelocityXZValue <= Move.WalkMoveSet.MaxSpeed;
    }

    public bool ToSprintState()
    {
        return Move.IsOnGroundChached
            && Move.VelocityXZValue > Move.RunMoveSet.MaxSpeed;
    }
}