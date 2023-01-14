using System;
using UnityEngine;

public class SlowMotionEffector : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _slowMotionTarget = 0.40f;

    public float SlowMotionTarget => _slowMotionTarget;

    public event Action SlowMotionEffectOn;
    public event Action SlowMotionEffectOff;

    public void EnableSlowMotionEffect()
    {
        Time.timeScale = _slowMotionTarget;

        SlowMotionEffectOn?.Invoke();
    }

    public void DisableSlowMotionEffect()
    {
        Time.timeScale = 1.0f;

        SlowMotionEffectOff?.Invoke();
    }

    public void Switch()
    {
        if (Time.timeScale == 1.0f)
            EnableSlowMotionEffect();
        else
            DisableSlowMotionEffect();

        Time.fixedDeltaTime = PhysicsExtended.DefaultFixedDeltaTime * Time.timeScale;
    }
}
