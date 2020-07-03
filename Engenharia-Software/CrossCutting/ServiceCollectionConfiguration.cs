using Engenharia_Software.Domain.CrossCutting;
using Engenharia_Software.Domain.Infra.IDP;
using Engenharia_Software.Domain.Infra.Integration;
using Engenharia_Software.Domain.Services;
using Engenharia_Software.Infra.IDP;
using Engenharia_Software.Infra.Integration;
using Engenharia_Software.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Engenharia_Software.CrossCutting
{
    public class ServiceCollectionConfiguration
    {
        public ServiceProvider Configure()
        {
            return 
                new ServiceCollection()
                    .AddTransient<IIdentityProvider, IdentityProvider>()
                    .AddTransient<ISearchService, SearchService>()
                    .AddSingleton<IJsonConverter,JsonConverter>()
                    .AddSingleton<ICallAPI, CallAPI>()
                    .AddSingleton(LoadConfiguration())
                    .BuildServiceProvider();
        }

        private IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
