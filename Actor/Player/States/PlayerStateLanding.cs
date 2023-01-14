using UnityEngine;


public class PlayerStateLanding : PlayerState
{
    private float _delay = 0.0f;

    public PlayerStateLanding(object target) : base(target)
    {

    }

    protected override void OnStart()
    {
        const float DelayLanding = 1.25f;

        View.SetBool(AnimationNames.ToLanding, true);
        PlayerEntry.GetComponent<PlayerInput>().enabled = false;
        _delay = DelayLanding;
    }

    protected override void OnUpdate()
    {
        if(_delay > 0.0f)
            _delay -= Time.deltaTime;
    }

    protected override void OnExit()
    {
        View.SetBool(AnimationNames.ToLanding, false);
        PlayerEntry.GetComponent<PlayerInput>().enabled = true;
    }

    public bool ToGroundState() => _delay <= 0.0f;
}