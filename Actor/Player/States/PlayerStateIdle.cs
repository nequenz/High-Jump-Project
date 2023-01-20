
public sealed class PlayerStateIdle : PlayerState
{
    public PlayerStateIdle(object target) : base(target)
    {

    }

    protected override void OnStart()
    {
        View.SetBool(AnimationNames.ToIdle, true);
    }

    protected override void OnExit()
    {
        View.SetBool(AnimationNames.ToIdle, false);
    }

    public bool ToWalkState()
    {
        return Move.IsOnGroundChached
            && Move.VelocityXZValue > ActorPhysics.NoVelocity;
    }
}
