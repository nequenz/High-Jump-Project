using System.Collections.Generic;

public delegate bool TransitionPredicate();

public enum TransitionType
{
    Update,
    FixedUpdate,
    LateUpdate
}
public struct Transition
{
    private int _id;
    private int _targetStateID;
    private TransitionPredicate _predicate;
    TransitionType _type;

    public int ID => _id;
    public int TargetStateID => _targetStateID;
    public TransitionPredicate Predicate => _predicate;
    public TransitionType Type => _type;

    public Transition(int idTransition, int targetStateID, TransitionType type, TransitionPredicate transitionPredicate)
    {
        _id = idTransition;
        _targetStateID = targetStateID;
        _predicate = transitionPredicate;
        _type = type;
    }

    public bool Invoke() => _predicate.Invoke();
}

public interface IStateMachine
{
    public bool TryAddState(IState state);

    public void AddStates(IState[] states);

    public IState GetState(int id);

    public bool TryGoTo(int id);
}