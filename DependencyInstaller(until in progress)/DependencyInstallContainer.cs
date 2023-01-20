using System;
using System.Collections.Generic;

public class DependencyInstallContainer : IDependencyInstallerContainer
{
    private Dictionary<Type, IDependencyInstaller> _matches = new();
    private Type _chosenInstallerType;

    public DependencyInstallContainer(Type type)
    {
        _chosenInstallerType = type;
    }

    public void SelectInstallerType<T>() where T : IDependencyInstaller => _chosenInstallerType = typeof(T);

    public IDependencyInstaller Select<ContractT>() 
    {
        Type type = typeof(ContractT);
        IDependencyInstaller installer;

        if (_matches.ContainsKey(type))
            throw new TypeAccessException("[" + type.Name + "] has a installer.");

        installer = (IDependencyInstaller)Activator.CreateInstance(_chosenInstallerType);
        installer.SetContainer(this);
        _matches.Add(type, installer);

        return installer;
    }

    public IDependencyInstaller FindInstaller<T>() => _matches.GetValueOrDefault(typeof(T));

    public IDependencyInstaller FindInstaller(Type type) => _matches.GetValueOrDefault(type);
}
