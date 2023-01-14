using System;

public delegate bool TransitionPredicate();
public delegate bool MachineStateEvent( IState state );

public enum TransitionType
{
    Update,
    FixedUpdate,
    LateUpdate
}
public struct Transition
{
    private Type _targetStatType;
    private TransitionPredicate _predicate;
    TransitionType _type;

    public Type TargetStateType => _targetStatType;
    public TransitionPredicate Predicate => _predicate;
    public TransitionType Type => _type;

    public Transition(Type stateTargetType, TransitionType type, TransitionPredicate transitionPredicate)
    {
        _targetStatType = stateTargetType;
        _predicate = transitionPredicate;
        _type = type;
    }

    public bool Invoke() => _predicate.Invoke();
}

public interface IStateMachine
{
    public event MachineStateEvent StateChanged;
    public event MachineStateEvent StateStarted;
    public event MachineStateEvent StateExited;

    public bool TryAddState(IState state);

    public void AddStates(IState[] states);

    public IState GetState<T>() where T : IState;

    public bool TryGoTo<T>() where T : IState;

    public bool TryGoTo(Type type);
}