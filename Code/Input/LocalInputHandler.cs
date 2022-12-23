using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LocalInputHandler : MonoBehaviour, IEnumerable<ActionBind>
{
    private List<ActionBind> _actions = new List<ActionBind>();
    private bool _isEnabled = true;

    public bool IsEnabled => _isEnabled;
    public abstract string Name { get; }

    protected void HandleInput()
    {
        if (Input.anyKey && IsEnabled == true)
        {
            foreach (ActionBind action in _actions)
            {
                action.Invoke();
            }
        }
    }

    protected void AttachAction(ActionBind actionBind) => _actions.Add( actionBind );

    protected void AttachAction(int actionID, Action action, ActionKeyPredicate predicate)
    {
        _actions.Add(new(actionID, action, predicate));
    }

    protected bool TryRemoveAction(int actionID)
    {
        return _actions.Remove( _actions.Find(action => action.ActionID == actionID) );
    }

    public void SetActionEnableMode( int actionID, bool isEnabled = false)
    {
        for(int i=0;i<_actions.Count;i++)
        {
            if( _actions[i].ActionID == actionID)
            {
                _actions[i].SetBlocked( isEnabled );
            }
        }
    }

    public void SetEnableMode(bool isEnabled) => _isEnabled = isEnabled;

    public IEnumerator<ActionBind> GetEnumerator() => _actions.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
