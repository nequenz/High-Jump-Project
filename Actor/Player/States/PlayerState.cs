using UnityEngine;


public abstract class PlayerState : GameState
{
    private GameObject _player;
    private PlayerView _view;
    private PlayerInput _input;
    private PlayerMove _move;

    protected GameObject PlayerEntry => _player;
    protected PlayerView View => _view;
    protected PlayerInput Input => _input;
    protected PlayerMove Move => _move;


    protected PlayerState(object target) : base(target)
    {
        Init();
    }

    protected PlayerState(object target, Transition[] transitions) : base(target, transitions)
    {
        Init();
    }

    private void Init()
    {
        _player = Target as GameObject;
        _view = _player.GetComponent<PlayerView>(); 
        _input = _player.GetComponent<PlayerInput>();
        _move = _player.GetComponent<PlayerMove>();
    }
}