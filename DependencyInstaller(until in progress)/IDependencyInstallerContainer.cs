using System;

public interface IDependencyInstallerContainer
{
    public void SelectInstallerType<T>() where T : IDependencyInstaller;

    public IDependencyInstaller Select<ContractT>();

    public IDependencyInstaller FindInstaller<T>();

    public IDependencyInstaller FindInstaller(Type type);
}