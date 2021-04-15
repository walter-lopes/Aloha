using Aloha.CQRS.Commands;
using Aloha.CQRS.Events;
using Aloha.MessageBrokers.CQRS;
using Aloha.Services.Carts.Application.Events.Externals;
using DryIoc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Aloha.MessageBrokers.RabbitMQ;
using Aloha.Persistence.MongoDB;
using Aloha.Services.Carts.Domain;
using System;

namespace Aloha.Services.Carts
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
            .AddEventHandlers()
            .AddInMemoryCommandDispatcher()
            .AddMongo()
            .AddRabbitMq()
            .AddServiceBusEventDispatcher()
            .AddMongoRepository<Cart, Guid>("carts")
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

            container
                .UseAloha()
                .UseRabbitMq()
                .SubscribeEvent<CustomerCreatedEvent>();

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
