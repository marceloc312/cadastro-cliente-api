using Api.Core.Contracts.Services;
using Api.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Core.TestsUnidade.Config
{
    public class IntegrationTestFixture
    {
        public IntegrationTestFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IClienteService, ClienteService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
        public ServiceProvider ServiceProvider { get; private set; }
    }
}
