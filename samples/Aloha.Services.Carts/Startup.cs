using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aloha.CQRS.Commands;
using Aloha.CQRS.Events;
using Aloha.MessageBrokers.CQRS;
using Aloha.MessageBrokers.RabbitMQ;
using DryIoc;
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

        public void ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services.AddControllers();
            mvcBuilder.AddControllersAsServices();
        }

        public void ConfigureContainer(IContainer container)
        {
            container
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

            var container = app.ApplicationServices.GetRequiredService<IContainer>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static IContainer CreateMyPreConfiguredContainer() =>
          new Container(rules =>
              rules.With(propertiesAndFields: request =>
                  request.ServiceType.Name.EndsWith("Controller") ? PropertiesAndFields.Properties()(request) : null)
          );
    }
}
