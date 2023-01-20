using UnityEngine;

public sealed class PlayerStateFall : PlayerState
{
    private RopeJoinPhysics _rope;
    private float _slerpValue;

    public bool IsSwinging => _rope.IsPhysicsUsing;

    public PlayerStateFall(object target) : base(target)
    {
        _rope = PlayerEntry.GetComponent<RopeJoinPhysics>();
    }

    protected override void OnStart()
    {
        View.SetBool(AnimationNames.ToFall,true);
    }

    protected override void OnUpdate()
    {
        const float FastFallBorder = -10.0f;

        if(Move.Velocity.y <= FastFallBorder)
        {
            _slerpValue = Mathf.Lerp(_slerpValue, 1, Time.unscaledDeltaTime);
            View.SetBool(AnimationNames.ToFall, false);
            View.SetBool(AnimationNames.ToFastFall, true);
            View.MeshTrasform.rotation = Quaternion.Slerp(View.MeshTrasform.rotation, Quaternion.LookRotation(Move.Velocity), _slerpValue);
        }
    }

    protected override void OnExit()
    {
        View.MeshTrasform.transform.localRotation = Quaternion.identity;
        View.SetBool(AnimationNames.ToFastFall, false);
        View.SetBool(AnimationNames.ToFall, false);
    }

    public bool ToSwingState()
    {
        return _rope.IsPhysicsUsing;
    }

    public bool ToAnyMoveState()
    {
        return Move.IsOnGroundChached 
            && View.GetBool(AnimationNames.ToFall)
            && !View.GetBool(AnimationNames.ToFastFall) 
            && Move.VelocityXZValue >= ActorPhysics.NoVelocity;
    }

    public bool ToIdleState()
    {
        return Move.IsOnGroundChached && View.GetBool(AnimationNames.ToFall) 
            && !View.GetBool(AnimationNames.ToFastFall) 
            && Move.VelocityXZValue < ActorPhysics.NoVelocity;
    }

    public bool ToJumpState()
    {
        const float JumpVelocityValue = 10.0f;

        return Move.Velocity.y > JumpVelocityValue;
    }

    public bool ToLandingState()
    {
        return View.GetBool(AnimationNames.ToFastFall) 
            && !View.GetBool(AnimationNames.ToFall)
            && Move.IsOnGroundChached;
    }

}