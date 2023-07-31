using Microsoft.Extensions.DependencyInjection;
using XFrame.Resilience;

namespace XFrame.Ioc
{
    public interface IContainer
    {
        IServiceCollection ServiceCollection { get; }

        IContainer SetupResilientStrategyForOptimisticConcurrency(int numberOfRetries, TimeSpan waitBeforeTrying);

        IContainer SetupContainer(Action<ISetup> setup);
    }
}
