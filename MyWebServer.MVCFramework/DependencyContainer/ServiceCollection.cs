
namespace MyWebServer.MVCFramework.DependencyContainer
{
    public class ServiceCollection : IServiceCollection
    {
        private readonly Dictionary<Type, Type> container = [];
        public void Add<TSource, TDesctination>()
        {
            this.container[typeof(TSource)] = typeof(TDesctination);
        }

        public object CreateInstance(Type type)
        {
            if (this.container.ContainsKey(type))
            {
                type = this.container[type];
            }

            var consctructor = type.GetConstructors()
                .OrderBy(x => x.GetParameters().Count())
                .FirstOrDefault();

            var parameters = consctructor.GetParameters();

            var parametersValues = new List<object>();

            foreach (var parameter in parameters)
            {
                var parameterValue = CreateInstance(parameter.ParameterType);
                parametersValues.Add(parameterValue);
            }
            return consctructor.Invoke([.. parametersValues]);
        }
    }
}
