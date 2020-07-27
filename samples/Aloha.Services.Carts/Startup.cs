using Aloha.CQRS.Commands;
using Aloha.CQRS.Commands.Dispatchers;
using Aloha.CQRS.Notifications;
using Aloha.MessageBrokers.CQRS;
using Aloha.MessageBrokers.RabbitMQ;
using Aloha.Services.Carts.Events.External;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                .AddCommandHandlers()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryNotificationDispatcher()
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

            container
                .UseAloha()
                .UseRabbitMq()
                .SubscribeEvent<CustomerCreatedEvent>();

            app.UseHttpsRedirection();
            app.UseRouting();
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
