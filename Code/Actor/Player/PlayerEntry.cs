using System;
using System.Collections.Generic;
using UnityEngine;

public enum StateIDs
{
    Default = 1,
    Idle,
    Walk,
    Run,
    Sprint,
    Jump,
    Jump2,
    Fall,
    FallFast,
    Landing,
    Landing2,
    WallRun,
    Swing
}

public struct State
{
    private StateIDs _stateID;
    private int _hashAnimationId;
    private bool _isConditionDone;
    private Action _action;
    private ActionKeyPredicate _condition;
    private bool _isSyncFixed;
    private bool _canPlay;

    public StateIDs StateID => _stateID;
    public int HashAnimationID => _hashAnimationId;
    public bool IsConditionDone => _isConditionDone;
    public bool IsSyncFixed => _isSyncFixed;
    public bool CanPlay => _canPlay;

    public State(StateIDs stateID, int hashAnimationId, ActionKeyPredicate stateCondition, Action actionState, bool isSyncWithFixed)
    {
        _stateID = stateID;
        _hashAnimationId = hashAnimationId;
        _condition = stateCondition;
        _action = actionState;
        _isConditionDone = false;
        _isSyncFixed = isSyncWithFixed;
        _canPlay = true;
    }

    public void InvokeAction() => _action?.Invoke();

    public void InvokeConditions()
    {
        _isConditionDone = _condition == null ? false : _condition.Invoke();
    }

    public void SetPlayFlag(bool canPlay) => _canPlay = canPlay;
}

[RequireComponent(typeof(ActorPhysics))]
[RequireComponent(typeof(ActorSmoothLook))]
[RequireComponent(typeof(PlayerView))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(StateMachine))]
public class PlayerEntry : MonoBehaviour
{
    [SerializeField] private Transform _meshTransform;
    [SerializeField] private CameraInput _cameraInput;
    private ActorPhysics _actorPhysics;
    private ActorSmoothLook _actorLook;
    private PlayerView _actorView;
    private PlayerInput _input;
    private StateMachine _stateMachine;

    private Vector3 _directionToMove = Vector3.zero;
    private MoveSet _walkMoveSet = new MoveSet(35,3,700,0.4f,Vector3.down,ActorPhysics.DefaultFrictionSupress);
    private MoveSet _runMoveSet = new MoveSet(100, 6, 700, 0.4f, Vector3.down, ActorPhysics.DefaultFrictionSupress);
    private MoveSet _sprintMoveSet = new MoveSet(100, 15, 700, 0.4f, Vector3.down, ActorPhysics.DefaultFrictionSupress);

    public MoveSet WalkMoveSet => _walkMoveSet;
    public MoveSet RunMoveSet => _runMoveSet;
    public MoveSet SprinntMoveSet => _sprintMoveSet;
    
    public CameraInput AttachedCameraInput => _cameraInput;
    public ActorPhysics AttachedActorPhysics => _actorPhysics;
    public ActorSmoothLook AttachedSmoothLook => _actorLook;
    public PlayerView AttachedActorView => _actorView;
    public PlayerInput Input => _input;


    private void Awake()
    {
        _actorPhysics = GetComponent<ActorPhysics>();
        _actorLook = GetComponent<ActorSmoothLook>();
        _actorView = GetComponent<PlayerView>();
        _input = GetComponent<PlayerInput>();
        _stateMachine = GetComponent<StateMachine>();

        InitStates();
    }

    private void Start()
    {
        _actorPhysics.SetMoveSet(_runMoveSet);

        /*
        AddState( 
            StateIDs.Fall, 
            SAnimations.OnFall, () =>
            _jump2Delay <= 0.0f
            && _actorPhysics.IsOnGroundChached == false,
            PlayFallState,false);

        AddState(
            StateIDs.Idle,
            SAnimations.OnIdle, () => 
            _actorPhysics.IsOnGroundChached == true 
            && _actorPhysics.VelocityValue < ActorPhysics.NoVelocity
            && _landing2Delay <= 0.0f
            && _jump2Delay <= 0.0f,
            PlayIdleState,false);

        AddState(
            StateIDs.Landing,
            SAnimations.OnLanding2, () => 
            _actorPhysics.IsOnGroundChached == true
            && _landing2Delay > 0.0f
            && _jump2Delay <= 0.0f,
            PlayLanding2State,true);

        AddState(
            StateIDs.Walk,
            SAnimations.OnWalk, () =>
            _actorPhysics.IsOnGroundChached == true
            && _jump2Delay <= 0.0f
            && _landing2Delay <= 0.0f
            && _actorPhysics.VelocityXZValue > 0.5f
            && _actorPhysics.VelocityXZValue <= 4.0f,
            PlayWalkState, false);

        AddState(
            StateIDs.Run,
            SAnimations.OnRun, () =>
            _actorPhysics.IsOnGroundChached == true
            && _landing2Delay <= 0.0f
            && _jump2Delay <= 0.0f
            && _actorPhysics.VelocityXZValue > 4.0f
            && _actorPhysics.VelocityXZValue <= 10.0f,
            PlayRunState, false);

        AddState(
            StateIDs.Sprint,
            SAnimations.OnSprint, () =>
            _actorPhysics.IsOnGroundChached == true
            && _jump2Delay <= 0.0f
            && _landing2Delay <= 0.0f
            && _actorPhysics.VelocityXZValue > 10.0f,
            PlaySprintState, false);

        AddState(
            StateIDs.Jump2,
            SAnimations.OnJump2, () =>
            _actorPhysics.IsOnGroundChached == false
            && _jump2Delay > 0.0f,
            PlayJump2State, false);

        */
    }

    private void Update()
    {
        //HandleStates();
        HandleMove();
    }

    private void InitStates()
    {
        const int TransitionsPerState = 64;

        int TransitionId (States state,int id) => (int)state * TransitionsPerState + id;

        _stateMachine.TryAddState(new StateIdle(States.Idle, this, new[]
        {

            new Transition(TransitionId(States.Idle,0),(int)States.Walk,TransitionType.Update,TransitIdleToWalk)

        }));

        _stateMachine.TryAddState(new StateWalk(States.Walk, this));
        //_stateMachine.TryAddState(new StateRun(States.Run, this));




        _stateMachine.TryGoTo(States.Idle);

    }    

    private void HandleMove()
    {
        Vector3 cameraForwardVector = _cameraInput.transform.position.Get2DNormalTo(transform.position);
        Vector3 moveDirection = Quaternion.LookRotation(cameraForwardVector) * _directionToMove;

        if (_directionToMove.magnitude > 0.0f)
        {
            _actorLook.SetLookTarget(moveDirection);
            _actorPhysics.Move(moveDirection);
            _directionToMove = Vector2.zero;
        }
    }

    public void MoveForward() => _directionToMove.z = 1;

    public void MoveBackward() => _directionToMove.z = -1;

    public void MoveLeft() => _directionToMove.x = -1;

    public void MoveRight() => _directionToMove.x = 1;

    public void GroundJump2()
    {
        if (_actorPhysics.IsOnGroundChached)
        {
            _actorPhysics.Jump();
        }
            
    }

    public void EnableSprintMode() => _actorPhysics.SetMoveSet(_sprintMoveSet);

    public void DisableSprintMode() => _actorPhysics.SetMoveSet(_runMoveSet);

    //trans

    private bool TransitIdleToWalk()
    {
        return _actorPhysics.VelocityXZValue > ActorPhysics.NoVelocity 
            && _actorPhysics.IsOnGroundChached;
    }

    //states


    private void PlayFallState()
    {
        /*
        FindObjectOfType<DebugInfo>().PublicText = "State:Fall";
        _landing2Delay = 3.0f;

        if(_pitch > 0.0f)
        {
            _pitch = _pitch % 360;
            _pitch = Mathf.MoveTowards(_pitch, 0, Time.deltaTime * 100);
            _meshTransform.rotation = transform.rotation * Quaternion.AngleAxis(_pitch, Vector3.right);
        }
        */
    }


    private void PlayJump2State()
    {
        /*
        float _angleAcceleration = Time.deltaTime * _actorPhysics.Velocity.y * _actorPhysics.Velocity.y * 1.85f;

        FindObjectOfType<DebugInfo>().PublicText = "State:Jump";

        _jump2Delay -= Time.deltaTime;

        if(_actorPhysics.Velocity.y > 0.0f)
            _pitch += _angleAcceleration;
        
        _meshTransform.rotation = transform.rotation * Quaternion.AngleAxis(_pitch, Vector3.right);

        if (_jump2Delay <= 0.0f)
        {
            //to-do
        }
        */
    }



}
