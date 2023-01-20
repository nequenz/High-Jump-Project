using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class RootComponent : MonoBehaviour
{
    [SerializeField] DependenciesService _dependenciesService;

    public event Action Enabled;
    public event Action Disabled;
    public event Action Updated;
    public event Action FixedUpdated;

    private void OnEnable()
    {
        OnSelfEnable();
        Enabled?.Invoke();
    }

    private void OnDisable()
    {
        OnSelfDisable();
        Disabled?.Invoke();
    }

    private void Update()
    {
        OnUpdate();
        Updated?.Invoke();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate();
        FixedUpdated?.Invoke();
    }

    protected virtual void OnUpdate()
    {
    }

    protected virtual void OnFixedUpdate()
    {
    }

    protected virtual void OnSelfEnable()
    {
    }

    protected virtual void OnSelfDisable()
    {
    }

    public abstract void ResolveDependencies();

    public I ResolveComponent<I>()
    {
        return _dependenciesService.ResolveComponent<I>(GetType(),this);
    }


}