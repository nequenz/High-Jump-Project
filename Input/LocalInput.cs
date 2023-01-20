using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate bool UnityInputSignature(KeyCode key);

public enum KeyMode
{
    Hold,
    Down,
    Up,
}

public struct ActionBind
{
    private int _actionID;
    private Action _bindedAction;
    private bool _isEnabled;
    private KeyMode _modeId;
    private KeyCode _keyCode;

    public int ID => _actionID;
    public bool IsEnabled => _isEnabled;

    public ActionBind(int actionID, Action action, KeyMode mode, KeyCode key)
    {
        _actionID = actionID;
        _bindedAction = action;
        _modeId = mode;
        _keyCode = key;
        _isEnabled = true;
    }

    public void Invoke()
    {
        if (_bindedAction != null && LocalInput.BindedUnityInputMethods[(int)_modeId](_keyCode))
            _bindedAction();
    }

    public void SetEnableMode(bool isEnabled) => _isEnabled = isEnabled;

    public void ChangeKey(KeyCode key) => _keyCode = key;
}


public abstract class LocalInput : MonoBehaviour, IEnumerable<ActionBind>
{
    public static readonly UnityInputSignature[] BindedUnityInputMethods = {

        Input.GetKey,
        Input.GetKeyDown,
        Input.GetKeyUp,

    };

    private List<ActionBind> _actions = new List<ActionBind>();
    private bool _isEnabled = true;
    private bool _isKeyUpLoopEnabled = false;

    public bool IsEnabled => _isEnabled;
    public abstract int ID { get; }

    protected void HandleInput()
    {
        if (Input.anyKey && IsEnabled == true || _isKeyUpLoopEnabled)
        {
            foreach (ActionBind action in _actions)
            {
                if(action.IsEnabled)
                    action.Invoke();
            }

            _isKeyUpLoopEnabled = Input.anyKey;
        }
    }

    protected void AttachAction(int actionID, Action action, KeyMode mode, KeyCode key)
    {
        _actions.Add( new(actionID, action, mode, key) );
    }

    protected bool TryRemoveAction(int actionID)
    {
        return _actions.Remove( _actions.Find(action => action.ID == actionID) );
    }

    public void ChangeKeyCode(int actionID, KeyCode key)
    {
        int actionIndex = _actions.FindIndex((action) => action.ID == actionID);
        ActionBind currentAction;

        if(actionIndex != -1)
        {
            currentAction = _actions[actionIndex];
            currentAction.ChangeKey(key);
            _actions[actionIndex] = currentAction;
        } 
    }

    public void SetActionEnableMode( int actionID, bool isEnabled)
    {
        int resultIndex = _actions.FindIndex(0, _actions.Count - 1, actionBind => actionBind.ID == actionID);

        if (resultIndex != -1)
        {
            ActionBind action = _actions[resultIndex];
            action.SetEnableMode(isEnabled);
            _actions[resultIndex] = action;
        }
    }

    public void EnableAction(int id) => SetActionEnableMode(id,true);

    public void DisableAction(int id) => SetActionEnableMode(id,false);

    public void SetEnableMode(bool isEnabled) => _isEnabled = isEnabled;

    public IEnumerator<ActionBind> GetEnumerator() => _actions.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
