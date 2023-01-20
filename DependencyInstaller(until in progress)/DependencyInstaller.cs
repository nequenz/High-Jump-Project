using System;
using System.Collections.Generic;

public struct ImplementatedType
{
    private Type _type;
    private TypeParams _params;
    private object _instance;

    public object Instance => _instance;
    public Type ImplementationType => _type;
    public TypeParams Params => _params;

    public ImplementatedType(Type type, TypeParams typeParams)
    {
        _type = type;
        _params = typeParams;
        _instance = null;  
    }

    public void SetInstance(object instance) => _instance = instance;
}



public class DependencyInstaller : IDependencyInstaller
{
    private IDependencyInstallerContainer _container;
    private Dictionary<Type, ImplementatedType> _matches = new();

    public IDependencyInstaller Bind<I, T>(TypeParams typeParam) where T : class, I
    {
        Type matchedInterface = typeof(I);
        Type matchedType = typeof(T);
        ImplementatedType type;

        if (_matches.ContainsKey(matchedInterface))
            throw new TypeAccessException("[" + matchedInterface.Name + "] is matched to [" + matchedType.Name + "] yet.");

        type = new ImplementatedType(typeof(T), typeParam);

        _matches.Add(typeof(I), type);

        return this;
    }

    public T Resolve<T>()
    {
        ImplementatedType implementated;
        object instance = default;

        bool result = _matches.TryGetValue( typeof(T), out implementated);

        if (result == false)
            throw new ArgumentNullException(typeof(T) + " has no implementation.");

        if (implementated.Params == TypeParams.Instance)
        {
            instance = Activator.CreateInstance(implementated.ImplementationType);
        }
        else if(implementated.Params == TypeParams.Singleton)
        {
            if (implementated.Instance is null)
                instance = Activator.CreateInstance(implementated.ImplementationType);

            implementated.SetInstance(instance);
            _matches.Remove(typeof(T));
            _matches.Add(typeof(T), implementated);
        }

        return (T)instance;
    }

    public void SetContainer(IDependencyInstallerContainer installContainer) => _container = installContainer;
}