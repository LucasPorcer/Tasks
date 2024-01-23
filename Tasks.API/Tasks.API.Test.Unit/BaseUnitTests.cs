using Microsoft.Extensions.DependencyInjection;
using System;

namespace Tasks.API.Test.Unit
{
    public class BaseUnitTests
    {
        protected Func<IServiceProvider> ServiceProviderFactory { get; }
        protected IServiceCollection ServiceCollection { get; }

        protected BaseUnitTests()
        {
            var services = new ServiceCollection();

            ServiceCollection = services;
            ServiceProviderFactory = () => services.BuildServiceProvider();
        }
    }
}
