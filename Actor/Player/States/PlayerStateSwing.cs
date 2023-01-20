using UnityEngine;

public sealed class PlayerStateSwing : PlayerState
{
    private RopeJoinPhysics _rope;

    public PlayerStateSwing(object target) : base(target)
    {
        _rope = PlayerEntry.GetComponent<RopeJoinPhysics>();
    }

    protected override void OnStart()
    {
        View.SetBool(AnimationNames.ToSwing, true);
        Input.DisableAction(PlayerInput.SprintOffActionID);
        Input.DisableAction(PlayerInput.SprintOnActionID);
        Input.EnableAction(PlayerInput.SwingSprintOnActionID);
        Input.EnableAction(PlayerInput.SwingSpringOffActionID);
    }

    protected override void OnUpdate()
    {
        Vector3 normalToHook = PlayerEntry.transform.position.GetNormalTo(_rope.HookTransform.position);

        View.MeshTrasform.transform.rotation = Quaternion.LookRotation(normalToHook);
        View.MeshTrasform.transform.localRotation *= Quaternion.AngleAxis(90, Vector3.right);
    }

    protected override void OnFixedUpdate()
    {
        if (Move.IsOnGroundChached)
            _rope.UsePhysics(false);
    }

    protected override void OnExit()
    {
        View.MeshTrasform.transform.localRotation = Quaternion.identity;
        View.SetBool(AnimationNames.ToSwing, false);
        Input.DisableAction(PlayerInput.SwingSprintOnActionID);
        Input.DisableAction(PlayerInput.SwingSpringOffActionID);
        Input.EnableAction(PlayerInput.SprintOffActionID);
        Input.EnableAction(PlayerInput.SprintOnActionID);
    }

    public bool ToFallState() => _rope.IsPhysicsUsing == false;

    public bool ToIdleState()
    {
        return Move.IsOnGroundChached
            && _rope.IsPhysicsUsing == false
            && Move.VelocityXZValue < ActorPhysics.NoVelocity;
    }

    public bool ToAnyMoveState()
    {
        return Move.IsOnGroundChached
            && _rope.IsPhysicsUsing == false
            && Move.VelocityXZValue >= ActorPhysics.NoVelocity;
    }

}