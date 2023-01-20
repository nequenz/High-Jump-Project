using UnityEngine;

[System.Serializable]
public abstract class InjectedComponent : IInjectableComponent
{
    [SerializeReference] private RootComponent _root;
    private bool _isEnabled = true;

    protected RootComponent Root => _root;

    public bool IsEnabled => _isEnabled;

    protected void Enable() => _isEnabled = true;

    protected void Disable() => _isEnabled = false;

    public abstract void Awake();

    public abstract void Away();

    public void SetRootComponent(RootComponent installComponent) => _root = installComponent;
}