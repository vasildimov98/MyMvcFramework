namespace MyWebServer.MVCFramework.DependencyContainer
{
    public interface IServiceCollection
    {
        public void Add<TSource, TDesctination>();

        public object CreateInstance(Type type);
    }
}
