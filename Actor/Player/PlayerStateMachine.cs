using System;
using UnityEngine;

[RequireComponent(typeof(RopeJoinPhysics))]
[RequireComponent(typeof(PlayerMove))]
public class PlayerStateMachine : StateMachine
{
    private PlayerMove _move;
    private RopeJoinPhysics _rope;

    private void Awake()
    {
        _move = GetComponent<PlayerMove>();
        _rope = GetComponent<RopeJoinPhysics>();
    }

    private void Start()
    {
        PlayerStateIdle idleState = new PlayerStateIdle(gameObject);
        PlayerStateFall fallState = new PlayerStateFall(gameObject);
        PlayerStateWalk walkState = new PlayerStateWalk(gameObject);
        PlayerStateRun runState = new PlayerStateRun(gameObject);
        PlayerStateSprint sprintState = new PlayerStateSprint(gameObject);
        PlayerStateSwing stateSwing = new PlayerStateSwing(gameObject);
        PlayerStateJump stateJump = new PlayerStateJump(gameObject);
        PlayerStateLanding stateLanding = new PlayerStateLanding(gameObject);

        TryAddState(idleState);
        TryAddState(fallState);
        TryAddState(walkState);
        TryAddState(runState);
        TryAddState(sprintState);
        TryAddState(stateSwing);
        TryAddState(stateJump);
        TryAddState(stateLanding);

        idleState.AttachTransition<PlayerStateFall>(TransitionType.FixedUpdate, CastState<PlayerStateIdle>().ToWalkState);
        idleState.AttachTransition<PlayerStateFall>(TransitionType.FixedUpdate, AnyToFallState);

        fallState.AttachTransition<PlayerStateLanding>(TransitionType.Update, CastState<PlayerStateFall>().ToLandingState);
        fallState.AttachTransition<PlayerStateIdle>(TransitionType.Update, CastState<PlayerStateFall>().ToIdleState);
        fallState.AttachTransition<PlayerStateWalk>(TransitionType.Update, CastState<PlayerStateFall>().ToAnyMoveState);
        fallState.AttachTransition<PlayerStateSwing>(TransitionType.Update, ToSwingState);
        fallState.AttachTransition<PlayerStateJump>(TransitionType.FixedUpdate, CastState<PlayerStateFall>().ToJumpState);

        walkState.AttachTransition<PlayerStateRun>(TransitionType.Update, CastState<PlayerStateWalk>().ToRunState);
        walkState.AttachTransition<PlayerStateIdle>(TransitionType.Update, AnyToIdleState);
        walkState.AttachTransition<PlayerStateFall>(TransitionType.Update, AnyToFallState);

        runState.AttachTransition<PlayerStateWalk>(TransitionType.Update, CastState<PlayerStateRun>().ToWalkState);
        runState.AttachTransition<PlayerStateSprint>(TransitionType.Update, CastState<PlayerStateRun>().ToSprintState);
        runState.AttachTransition<PlayerStateIdle>(TransitionType.Update, AnyToIdleState);
        runState.AttachTransition<PlayerStateFall>(TransitionType.Update, AnyToFallState);

        sprintState.AttachTransition<PlayerStateRun>(TransitionType.Update, CastState<PlayerStateSprint>().ToWalkState);
        sprintState.AttachTransition<PlayerStateIdle>(TransitionType.Update, AnyToIdleState);
        sprintState.AttachTransition<PlayerStateFall>(TransitionType.Update, AnyToFallState);

        stateSwing.AttachTransition<PlayerStateFall>(TransitionType.FixedUpdate, CastState<PlayerStateSwing>().ToFallState);
        stateSwing.AttachTransition<PlayerStateIdle>(TransitionType.FixedUpdate, CastState<PlayerStateSwing>().ToIdleState);
        stateSwing.AttachTransition<PlayerStateWalk>(TransitionType.FixedUpdate, CastState<PlayerStateSwing>().ToAnyMoveState);

        stateJump.AttachTransition<PlayerStateFall>(TransitionType.Update, CastState<PlayerStateJump>().ToFallState);
        stateJump.AttachTransition<PlayerStateWalk>(TransitionType.Update, CastState<PlayerStateJump>().ToAnyMoveMove);
        stateJump.AttachTransition<PlayerStateSwing>(TransitionType.Update, ToSwingState);
        stateJump.AttachTransition<PlayerStateIdle>(TransitionType.Update, AnyToIdleState);

        stateLanding.AttachTransition<PlayerStateIdle>(TransitionType.Update, CastState<PlayerStateLanding>().ToGroundState);

        TryGoTo<PlayerStateIdle>();
    }

    private T CastState<T>() where T : IState
    {
        IState neededState = GetState<T>();

        if (neededState is not null)
            return (T)neededState;

        throw new InvalidCastException("Can't cast null state to needed type.");
    }

    private bool AnyToFallState()
    {
        return _move.IsOnGroundChached == false;
    }

    private bool AnyToIdleState()
    {
        return _move.IsOnGroundChached
            && _move.VelocityXZValue <= ActorPhysics.NoVelocity;
    }

    private bool ToSwingState()
    {
        return _rope.IsPhysicsUsing;
    }
}
