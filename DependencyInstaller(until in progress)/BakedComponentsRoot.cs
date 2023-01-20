using System.Collections.Generic;

public class BakedComponentsRoot
{
    private RootComponent _rootComponent;
    private List<IInjectableComponent> _bakedComponents = new();

    public RootComponent InstallComponent => _rootComponent;

    public BakedComponentsRoot(RootComponent installComponent)
    {
        _rootComponent = installComponent;
    }

    public T GetInstance<T>()
    {
        object component = _bakedComponents.Find((instance) => instance is T);

        return component is not null ? (T)component : default;
    }

    public void BakeComponent(IInjectableComponent newComponent)
    {
        IInjectableComponent sameComponent = _bakedComponents.Find((component) => component.GetType() == newComponent.GetType());

        if (sameComponent is not null)
        {
            sameComponent.Away();
            _bakedComponents.Remove(sameComponent);
        }
            

        _bakedComponents.Add(newComponent);
    }
}
