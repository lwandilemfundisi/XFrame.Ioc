namespace XFrame.Ioc.Configuration
{
    public class LoadedTypes : ILoadedTypes
    {
        public LoadedTypes(
            IEnumerable<Type> loadedTypes)
        {
            TypesLoaded = loadedTypes.ToList();
        }

        public IReadOnlyCollection<Type> TypesLoaded { get; }
    }
}
