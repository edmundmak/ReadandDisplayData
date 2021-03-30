using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ReadData;

namespace ReadandDisplayData
{
   public class Startup
    {
        private readonly IServiceProvider provider;
        // access the built service pipeline
        public IServiceProvider Provider => provider;
        public Startup()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            // instantiate
            var services = new ServiceCollection();
            services.AddSingleton<IReadData, ReadData.ReadData>();
            // build the pipeline
            provider = services.BuildServiceProvider();
        }
              
    }
}
