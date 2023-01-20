using System;
using UnityEngine;

public class Pauser : MonoBehaviour
{
    private bool _isPaused = false;

    public event Action Paused;
    public event Action Unpaused;

    public bool IsPaused => _isPaused;


    private void RealizePauseMode()
    {
        if( _isPaused )
        {
            Time.timeScale = 0;
            Paused?.Invoke();
        }
        else
        {
            Time.timeScale = 1.0f;
            Unpaused?.Invoke();
        }
    }

    public void EnablePause()
    {
        _isPaused = true;
        RealizePauseMode();
    }

    public void DisablePause()
    {
        _isPaused = false;
        RealizePauseMode();
    }

    public void Pause()
    {
        _isPaused = !_isPaused;
        RealizePauseMode();
    }
}

