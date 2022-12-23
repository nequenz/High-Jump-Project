using System;

public interface IDefaultState
{
    public int ID { get; }
    public IStateMachine StateMachineTarget { get; }
    public bool IsUpdateEnabled { get; }
    public bool IsLateUpdateEnabled { get; }

    public event Action Started;
    public event Action Exited;

    public void SetStateMachine(IStateMachine stateMachine);

    public void SetTarget(object target);

    public void EnableUpdate(bool isEnabled);

    public void EnableLateUpdate(bool isEnabled);

    public void Start();

    public void Update();

    public void LateUpdate();

    public void Exit();

    public bool TryGoTo(int stateID);

    public void AttachTransition(Transition transition);

    public void RemoveTransition(int id);
}

public interface IPhysicsState
{
    public bool IsFixedUpdateEnabled { get; }

    public void SetEnableFixedUpdate(bool isEnabled);

    public void FixedUpdate();
}

public interface IState : IDefaultState, IPhysicsState
{

}