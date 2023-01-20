using UnityEngine;

public sealed class PlayerStateJump : PlayerState
{
    private float _maxDelay = 3.0f;
    private float _delay;
    private float _pitchValue = 0;
    private float _slerpValue = 0;
    public bool IsJumpingEnd => View.GetBool(AnimationNames.ToJumpEnd);

    public PlayerStateJump(object target) : base(target)
    {

    }

    protected override void OnStart()
    {
        const float MaxPitchValue = 720;

        View.SetBool(AnimationNames.ToJump, true);
        _delay = _maxDelay;
        _pitchValue = MaxPitchValue;
    }

    protected override void OnUpdate()
    {
        const float RedemptionRate = 0.15f;

        if (_delay > 0.0f)
            _delay -= Time.deltaTime;

        _pitchValue = Mathf.Lerp(_pitchValue, 0, Time.deltaTime * RedemptionRate);
        View.MeshTrasform.transform.localRotation *= Quaternion.AngleAxis(Time.deltaTime * _pitchValue, Vector3.right);

        if (_pitchValue < 550)
        {
            _slerpValue = Mathf.Lerp(_slerpValue, 1, Time.deltaTime);
            View.MeshTrasform.transform.localRotation = Quaternion.Slerp(View.MeshTrasform.transform.localRotation, Quaternion.identity, _slerpValue);
            _pitchValue = 0.0f;
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