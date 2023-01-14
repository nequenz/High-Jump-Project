using System.Collections.Generic;
using UnityEngine;


public class InputHandleCoordinator : MonoBehaviour
{
    [SerializeField] private List<LocalInputHandler> _inputers = new List<LocalInputHandler>();

    public void SetEnableMode(int id, bool isEnabled = true)
    {
        LocalInputHandler localInputHandler = _inputers.Find(localInputer => localInputer.ID == id);

        if( localInputHandler )
        {
            localInputHandler.enabled = isEnabled;
        }
    }
}
