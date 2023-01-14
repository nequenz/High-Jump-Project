using System;
using System.Collections.Generic;

public abstract class GameState : IState
{
    private IStateMachine _stateMachine;
    private object _target;
    private bool _isFixedUpdateEnabled = true;
    private bool _isLateUpdateEnabled = true;
    private bool _isUpdateEnabled = true;
    private List<Transition> _transitions = new();

    public IStateMachine StateMachineTarget => _stateMachine;
    public bool IsUpdateEnabled => _isUpdateEnabled;
    public bool IsLateUpdateEnabled => _isLateUpdateEnabled;
    public bool IsFixedUpdateEnabled => _isFixedUpdateEnabled;

    protected object Target => _target;


    public GameState(object target)
    {
        _target = target;
    }

    public GameState(object target, Transition[] transitions)
    {
        _target = target;
        _transitions.AddRange(transitions);
    }

    private void RunTransitions(TransitionType transitionType)
    {
        _transitions.ForEach(transition =>
        {
            if (transition.Type == transitionType && transition.Invoke())
                _stateMachine.TryGoTo(transition.TargetStateType);
        });
    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnUpdate()
    {

    }

    protected virtual void OnLateUpdate()
    {

    }

    protected virtual void OnFixedUpdate()
    {

    }

    protected virtual void OnExit()
    {

    }

    public void AttachTransition(Transition transition) => _transitions.Add(transition);

    public void SetStateMachine(IStateMachine stateMachine) => _stateMachine = stateMachine;

    public void SetTarget(object target) => _target = target;

    public void Start()
    {
        OnStart();
    }

    public void Update()
    {
        OnUpdate();
        RunTransitions(TransitionType.Update);
    }

    public void LateUpdate()
    {
        OnLateUpdate();
        RunTransitions(TransitionType.LateUpdate);
    }

    public void FixedUpdate()
    {
        OnFixedUpdate();
        RunTransitions(TransitionType.FixedUpdate);
    }

    public void Exit()
    {
        OnExit();
    }

    public void EnableUpdate(bool isEnabled) => _isUpdateEnabled = isEnabled;

    public void EnableLateUpdate(bool isEnabled) => _isLateUpdateEnabled = isEnabled;

    public void EnableFixedUpdate(bool isEnabled) => _isFixedUpdateEnabled = isEnabled;

}
