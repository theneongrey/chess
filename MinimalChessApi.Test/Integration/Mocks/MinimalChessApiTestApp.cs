using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace MinimalChessApi.Test.e2e.Mocks
{
    public class MinimalChessApiTestApp : WebApplicationFactory<Program>
    {
        private Action<IServiceCollection> _servicesOverrides;

        public MinimalChessApiTestApp(Action<IServiceCollection> servicesOverrides)
        {
            _servicesOverrides = servicesOverrides;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            // Add mock/test services to the builder here
            builder.ConfigureServices(_servicesOverrides);

            return base.CreateHost(builder);
        }
    }
}
