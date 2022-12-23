using System;
using System.Collections.Generic;

public abstract class ActorState : IState
{
    public const int NoId = -1;

    private int _id = NoId;
    private IStateMachine _stateMachine;
    private object _target;
    private bool _isFixedUpdateEnabled = false;
    private bool _isLateUpdateEnabled = false;
    private bool _isUpdateEnabled = true;
    private List<Transition> _transitions = new();

    public int ID => _id;
    public States StateID => (States)ID;
    public IStateMachine StateMachineTarget => _stateMachine;
    public bool IsUpdateEnabled => _isUpdateEnabled;
    public bool IsLateUpdateEnabled => _isLateUpdateEnabled;
    public bool IsFixedUpdateEnabled => _isFixedUpdateEnabled;

    protected object Target => _target;

    public event Action Started;
    public event Action Exited;

    public ActorState(int id,  object target)
    {
        _id = id;
        _target = target;
    }

    public ActorState(States id, object target)
    {
        _id = (int)id;
        _target = target;
    }

    public ActorState(States id, object target, Transition[] transitions)
    {
        _id = (int)id;
        _target = target;
        _transitions.AddRange(transitions);
    }

    private void RunTransitions(TransitionType transitionType)
    {
        _transitions.ForEach(transition =>
        {
            if (transition.Type == transitionType && transition.Invoke())
            {
                Exit();
                TryGoTo(transition.TargetStateID);
            }
                
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

    public void RemoveTransition(int id)
    {
        const int NoIndex = -1;
        int index = _transitions.FindIndex(transition => transition.ID == id);

        if(index != NoIndex)
            _transitions.RemoveAt(index);
    }

    public void SetStateMachine(IStateMachine stateMachine) => _stateMachine = stateMachine;

    public void SetTarget(object target) => _target = target;

    public void Start()
    {
        OnStart();
        Started?.Invoke();
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
        Exited?.Invoke();
    }

    public bool TryGoTo(int id)
    {
        bool result = _stateMachine.TryGoTo(id);

        if (result)
            Exit();

        return _stateMachine.TryGoTo(id);
    }

    public bool TryGoTo(States stateID) => TryGoTo((States)stateID);

    public void EnableUpdate(bool isEnabled) => _isUpdateEnabled = isEnabled;

    public void EnableLateUpdate(bool isEnabled) => _isLateUpdateEnabled = isEnabled;

    public void SetEnableFixedUpdate(bool isEnabled) => _isFixedUpdateEnabled = isEnabled;

}
