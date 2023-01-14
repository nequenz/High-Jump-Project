using System;

public interface IDefaultState
{
    public IStateMachine StateMachineTarget { get; }
    public bool IsUpdateEnabled { get; }
    public bool IsLateUpdateEnabled { get; }


    public void SetStateMachine(IStateMachine stateMachine);

    public void SetTarget(object target);

    public void EnableUpdate(bool isEnabled);

    public void EnableLateUpdate(bool isEnabled);

    public void Start();

    public void Update();

    public void LateUpdate();

    public void Exit();

    public void AttachTransition(Transition transition);

}

public interface IPhysicsState
{
    public bool IsFixedUpdateEnabled { get; }

    public void EnableFixedUpdate(bool isEnabled);

    public void FixedUpdate();
}

public interface IState : IDefaultState, IPhysicsState
{

}