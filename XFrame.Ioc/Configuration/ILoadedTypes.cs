namespace XFrame.Ioc.Configuration
{
    public interface ILoadedTypes
    {
        IReadOnlyCollection<Type> TypesLoaded { get; }
    }
}
