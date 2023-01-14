using UnityEngine;

public sealed class PlayerStateJump : PlayerState
{
    private float _maxDelay = 3.0f;
    private float _delay;
    private float _animationValue = 0;
    private float _slerpValue = 0;
    public bool IsJumpingEnd => View.GetBool(AnimationNames.ToJumpEnd);

    public PlayerStateJump(object target) : base(target)
    {

    }

    protected override void OnStart()
    {
        View.SetBool(AnimationNames.ToJump, true);
        _delay = _maxDelay;
        _animationValue = 720;
    }

    protected override void OnUpdate()
    {
        if (_delay > 0.0f)
            _delay -= Time.deltaTime;

        _animationValue = Mathf.Lerp(_animationValue, 0, Time.deltaTime * 0.15f);
        View.MeshTrasform.transform.localRotation *= Quaternion.AngleAxis(Time.unscaledDeltaTime * _animationValue, Vector3.right);

        if (_animationValue < 550)
        {
            _slerpValue = Mathf.Lerp(_slerpValue, 1, Time.deltaTime);
            View.MeshTrasform.transform.localRotation = Quaternion.Slerp(View.MeshTrasform.transform.localRotation, Quaternion.identity, _slerpValue);
            _animationValue = 0.0f;
            View.SetBool(AnimationNames.ToJumpEnd, true);
        }
    }

    protected override void OnExit()
    {
        View.SetBool(AnimationNames.ToJumpEnd, false);
        View.SetBool(AnimationNames.ToJump, false);
    }

    public bool ToFallState() => _delay <= 0.0f;

    public bool ToAnyMoveMove()
    {
        return Move.IsOnGroundChached 
            && Move.VelocityXZValue >= ActorPhysics.NoVelocity;
    }

}