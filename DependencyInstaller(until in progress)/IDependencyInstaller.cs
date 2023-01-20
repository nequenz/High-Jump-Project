public enum TypeParams
{
    Instance,
    Singleton
}

public interface IDependencyInstaller
{
    public IDependencyInstaller Bind<I, T>(TypeParams typeParam) where T : class, I;

    public T Resolve<T>();

    public void SetContainer(IDependencyInstallerContainer installContainer);
}