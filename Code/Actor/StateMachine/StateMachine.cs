using System.Collections.Generic;
using UnityEngine;

public sealed class StateMachine : MonoBehaviour, IStateMachine
{
    [SerializeField] GameObject _target;
    private List<IState> _states = new();
    private IState _currentState;


    private void Update()
    {
        if (_currentState is not null && _currentState.IsUpdateEnabled)
            _currentState.Update();
    }

    private void LateUpdate()
    {
        if (_currentState is not null && _currentState.IsLateUpdateEnabled)
            _currentState.LateUpdate();
    }

    private void FixedUpdate()
    {
        if (_currentState is not null && _currentState.IsFixedUpdateEnabled)
            _currentState.FixedUpdate();
    }

    public IState GetState(int id) => _states.Find(state => state.ID == id);

    public IState GetState(States stateID) => GetState((int)stateID);

    public bool TryAddState(IState state)
    {
        if (state is not null 
            &&  _states.Exists(otherState => otherState.ID == state.ID))
            return false;

        state.SetStateMachine(this);
        _states.Add(state);

        return true;
    }

    public void AddStates(IState[] states)
    {
        for (int i = 0; i < states.Length; i++)
            TryAddState(states[i]);
    }

    public bool TryGoTo(int id) 
    {
        IState nextState = _states.Find(transition => transition.ID == id);

        if(nextState is not null)
        {
            nextState.Start();
            _currentState = nextState;

            return true;
        }

        return false;
    }

    public bool TryGoTo(States stateID) => TryGoTo((int)stateID);
}
