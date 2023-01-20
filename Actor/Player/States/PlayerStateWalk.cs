
public sealed class PlayerStateWalk : PlayerState
{
    public PlayerStateWalk(object target) : base(target)
    {

    }

    protected override void OnStart()
    {
        View.SetBool(AnimationNames.ToWalk, true);
    }

    protected override void OnExit()
    {
        View.SetBool(AnimationNames.ToWalk, false);
    }

    public bool ToRunState()
    {
        return Move.IsOnGroundChached
            && Move.VelocityXZValue >= Move.WalkMoveSet.MaxSpeed;
    }

}
