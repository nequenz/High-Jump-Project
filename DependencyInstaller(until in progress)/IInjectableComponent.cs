

public interface IInjectableComponent
{
    public bool IsEnabled { get; }

    public void Awake();

    public void Away();
        
    public void SetRootComponent(RootComponent installComponent);
}