using System;
using System.Collections.Generic;
using UnityEngine;

public delegate bool ActionKeyPredicate();

public struct ActionBind
{
    private int _actionID;
    private Action _bindedAction;
    private ActionKeyPredicate _actionKeyboardPredicate;
    private bool _isBlocked;

    public int ActionID { get => _actionID; }
    public bool IsBlocked { get=> _isBlocked; }

    public ActionBind(int actionID, Action action , ActionKeyPredicate predicate)
    {
        _actionID = actionID;
        _bindedAction = action;
        _actionKeyboardPredicate = predicate;
        _isBlocked = false;
    }

    public void Invoke()
    {
        if( !_isBlocked && _bindedAction != null && _actionKeyboardPredicate() )
        {
            _bindedAction();
        }
    }

    public void SetBlocked(bool isBlocked) => _isBlocked = isBlocked;

}

public class InputHandleCoordinator : MonoBehaviour
{
    [SerializeField] private List<LocalInputHandler> _inputers = new List<LocalInputHandler>();

    public void SetEnabled(string inputerName, bool isEnabled = true)
    {
        LocalInputHandler localInputHandler = _inputers.Find(localInputer => localInputer.Name == inputerName);

        if( localInputHandler )
        {
            localInputHandler.enabled = isEnabled;
        }
    }
}
