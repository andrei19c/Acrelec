using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System.Net.Http;
using Microsoft.Extensions.Http;
using Acrelec.SCO.Core.Services;
using Acrelec.SCO.Core.Managers;
using Acrelec.SCO.Core.Interfaces;
using Acrelec.SCO.Core.Providers;
using Acrelec.SCO.Core.HttpClients;

namespace Acrelec.SCO.Core
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder();
            builder.AddConfiguration(configuration);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<ILogService, LogService>();
            services.AddSingleton<IOrderManager, OrderManager>();
            services.AddSingleton<IItemsProvider, ItemsProvider>();
            services.AddTransient<IScoHttpClient, ScoHttpClient>();
        }

        public void Configure(IApplicationBuilder app)
        {
            
        }
    }
}
