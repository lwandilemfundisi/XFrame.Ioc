using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using XFrame.Common.JsonSerializer;
using XFrame.Ioc.Configuration;
using XFrame.Resilience;

namespace XFrame.Ioc
{
    public abstract class Container : IContainer
    {
        protected readonly List<Type> _types = new List<Type>();
        private readonly Setup _setup = new Setup();

        protected Container(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
            RegisterDefaults(ServiceCollection);
        }

        #region IContainer

        public IServiceCollection ServiceCollection { get; }

        public IContainer SetupResilientStrategyForOptimisticConcurrency(int numberOfRetries, TimeSpan waitBeforeTrying)
        {
            _setup.RetryCountOnOptimisticConcurrencyExceptions = numberOfRetries;
            _setup.WaitThenTryAfterOnOptimisticConcurrencyExceptions = waitBeforeTrying;
            return this;
        }

        public IContainer SetupContainer(Action<ISetup> runSetup)
        {
            runSetup(_setup);
            return this;
        }

        #endregion

        #region Methods

        protected virtual void RegisterDefaults(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();
            serviceCollection.TryAddSingleton<IJsonSerializer, JsonSerializer>();
            serviceCollection.TryAddTransient<IJsonOptions, JsonOptions>();
            serviceCollection.TryAddTransient<IOptimisticConcurrencyResilientStrategy, OptimisticConcurrencyResilientStrategy>();
            serviceCollection.TryAddTransient<ISetup>(_ => _setup);
            serviceCollection.TryAddTransient<ICancellationConfiguration>(_ => _setup);
            serviceCollection.TryAddTransient(typeof(ITransientFaultHandler<>), typeof(TransientFaultHandler<>));
            serviceCollection.AddSingleton<ILoadedTypes>(r => new LoadedTypes(_types));
        }

        #endregion
    }
}
