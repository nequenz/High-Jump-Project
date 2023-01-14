using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour, IStateMachine
{
    private List<IState> _states = new();
    private IState _currentState;

    public event MachineStateEvent StateChanged;
    public event MachineStateEvent StateStarted;
    public event MachineStateEvent StateExited;

    public IState CurrentState => _currentState;

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

    public IState GetState<T>() where T : IState => _states.Find(state => state is T);

    public bool TryAddState(IState state)
    {
        if (state is not null 
            &&  _states.Exists(otherState => otherState.GetType() == state.GetType()))
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

    public bool TryGoTo<T>() where T : IState
    {
        return TryToGo((state) => state is T);
    }

    public bool TryGoTo(Type type) => TryToGo( (state) => state.GetType() == type );

    private bool TryToGo( Predicate<IState> predicate )
    {
        IState nextState = _states.Find(predicate);

        if (nextState is not null)
        {
            StateStarted?.Invoke(nextState);
            nextState.Start();

            if(_currentState is not null)
            {
                StateExited?.Invoke(_currentState);
                _currentState.Exit();
            }
        
            StateChanged?.Invoke(nextState);
            _currentState = nextState;

            return true;
        }

        return false;
    }
}
