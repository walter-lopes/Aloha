using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aloha.CQRS.Commands;
using Aloha.CQRS.Events;
using Aloha.MessageBrokers.CQRS;
using Aloha.MessageBrokers.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Aloha.Services.Customers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services
                .AddAloha()
                .AddCommandHandlers()
                .AddInMemoryCommandDispatcher()
                .AddEventHandlers()
                .AddServiceBusEventDispatcher()
                .AddRabbitMq()
                .Build();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
