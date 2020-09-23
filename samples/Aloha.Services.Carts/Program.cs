using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static DryIoc.Microsoft.DependencyInjection.DryIocAdapter;

namespace Aloha.Services.Carts
{
    public class Program
    {
        public static async Task Main(string[] args) =>
            await CreateHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new DryIocServiceProviderFactory(Startup.CreateMyPreConfiguredContainer()))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
