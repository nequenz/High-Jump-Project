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

        idleState.AttachTransition(new Transition(typeof(PlayerStateFall), TransitionType.FixedUpdate, CastState<PlayerStateIdle>().ToWalkState));
        idleState.AttachTransition(new Transition(typeof(PlayerStateFall), TransitionType.FixedUpdate, AnyToFallState));

        fallState.AttachTransition(new Transition(typeof(PlayerStateLanding), TransitionType.Update, CastState<PlayerStateFall>().ToLandingState));
        fallState.AttachTransition(new Transition(typeof(PlayerStateIdle), TransitionType.Update, CastState<PlayerStateFall>().ToIdleState));
        fallState.AttachTransition(new Transition(typeof(PlayerStateWalk), TransitionType.Update, CastState<PlayerStateFall>().ToAnyMoveState));
        fallState.AttachTransition(new Transition(typeof(PlayerStateSwing), TransitionType.Update, ToSwingState));
        fallState.AttachTransition(new Transition(typeof(PlayerStateJump), TransitionType.FixedUpdate, CastState<PlayerStateFall>().ToJumpState));

        walkState.AttachTransition(new Transition(typeof(PlayerStateRun), TransitionType.Update, CastState<PlayerStateWalk>().ToRunState));
        walkState.AttachTransition(new Transition(typeof(PlayerStateIdle), TransitionType.Update, AnyToIdleState));
        walkState.AttachTransition(new Transition(typeof(PlayerStateFall), TransitionType.Update, AnyToFallState));

        runState.AttachTransition(new Transition(typeof(PlayerStateWalk), TransitionType.Update, CastState<PlayerStateRun>().ToWalkState));
        runState.AttachTransition(new Transition(typeof(PlayerStateSprint), TransitionType.Update, CastState<PlayerStateRun>().ToSprintState));
        runState.AttachTransition(new Transition(typeof(PlayerStateIdle), TransitionType.Update, AnyToIdleState));
        runState.AttachTransition(new Transition(typeof(PlayerStateFall), TransitionType.Update, AnyToFallState));

        sprintState.AttachTransition(new Transition(typeof(PlayerStateRun),TransitionType.Update, CastState<PlayerStateSprint>().ToWalkState));
        sprintState.AttachTransition(new Transition(typeof(PlayerStateIdle),TransitionType.Update, AnyToIdleState));
        sprintState.AttachTransition(new Transition(typeof(PlayerStateFall), TransitionType.Update, AnyToFallState));

        stateSwing.AttachTransition(new Transition(typeof(PlayerStateFall), TransitionType.FixedUpdate, CastState<PlayerStateSwing>().ToFallState));
        stateSwing.AttachTransition(new Transition(typeof(PlayerStateIdle), TransitionType.FixedUpdate, CastState<PlayerStateSwing>().ToIdleState));
        stateSwing.AttachTransition(new Transition(typeof(PlayerStateWalk), TransitionType.FixedUpdate, CastState<PlayerStateSwing>().ToAnyMoveState));

        stateJump.AttachTransition(new Transition(typeof(PlayerStateFall), TransitionType.Update, CastState<PlayerStateJump>().ToFallState));
        stateJump.AttachTransition(new Transition(typeof(PlayerStateWalk), TransitionType.Update, CastState<PlayerStateJump>().ToAnyMoveMove));
        stateJump.AttachTransition(new Transition(typeof(PlayerStateSwing), TransitionType.Update, ToSwingState));
        stateJump.AttachTransition(new Transition(typeof(PlayerStateIdle), TransitionType.Update, AnyToIdleState));

        stateLanding.AttachTransition(new Transition(typeof(PlayerStateIdle), TransitionType.Update, CastState<PlayerStateLanding>().ToGroundState));

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

    public bool ToSwingState()
    {
        return _rope.IsPhysicsUsing;
    }
}
