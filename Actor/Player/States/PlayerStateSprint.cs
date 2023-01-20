using UnityEngine;

public sealed class PlayerStateSprint : PlayerState
{
    public PlayerStateSprint(object target) : base(target)
    {

    }

    protected override void OnStart()
    {
        View.SetBool(AnimationNames.ToSprint,true);
    }

    protected override void OnExit()
    {
        View.SetBool(AnimationNames.ToSprint, false);
    }

    public bool ToWalkState()
    {
        return Move.IsOnGroundChached
            && Move.VelocityXZValue <= Move.RunMoveSet.MaxSpeed;
    }
}