using System.Collections.Generic;
using UnityEngine;


public class InputCoordinator : MonoBehaviour
{
    [SerializeField] private List<LocalInput> _inputers = new List<LocalInput>();

    public void SetEnableMode(int id, bool isEnabled = true)
    {
        LocalInput localInputHandler = _inputers.Find(localInputer => localInputer.ID == id);

        if( localInputHandler )
        {
            localInputHandler.enabled = isEnabled;
        }
    }
}
