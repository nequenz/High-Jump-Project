using UnityEngine;

public class RopeShotModeInput : LocalInput
{
    public const int ModeUseActionID = 0;
    public const int ShootRopeActionID = 1;

    [SerializeField] private RopeShooter _shooter;
    [SerializeField] private SlowMotionEffector _slowMotion;
    [SerializeField] private PlayerInput _player;
    [SerializeField] private RopeJoinPhysics _ropeJoin;

    public override int ID => 1;

    private void Awake()
    {
        if (_shooter is null || _slowMotion is null || _ropeJoin is null)
            enabled = false;

        _ropeJoin.UsePhysics(false);

        AttachAction(ModeUseActionID, SwitchMode, KeyMode.Up, KeyCodes.RopeShotModeUse);
        AttachAction(ShootRopeActionID, UseRope, KeyMode.Up, KeyCodes.UseRope);
    }

    private void Update()
    {
        HandleInput();

        if (_ropeJoin.IsPhysicsUsing == false && _shooter.IsRopeEnabled())
            _shooter.RemoveRope();

    }

    private void SwitchMode()
    {
        _slowMotion.Switch();
        _player.SetEnableMode( !_player.IsEnabled );
    }

    private void UseRope()
    {
        if(_shooter.IsHitted && _ropeJoin.IsPhysicsUsing == false)
        {
            _shooter.ShootRope();
            _ropeJoin.SetHookTransform(_shooter.Hook);
            _ropeJoin.UsePhysics(true);
        }
        else if(_ropeJoin.IsPhysicsUsing)
        {
            _shooter.RemoveRope();
            _ropeJoin.UsePhysics(false);
        }
    }
}
