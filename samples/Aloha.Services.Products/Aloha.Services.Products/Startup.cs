using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Aloha.MessageBrokers.RabbitMQ;
using Aloha.CQRS.Commands;
using Aloha.MessageBrokers.CQRS;
using Aloha.CQRS.Events;
using DryIoc;

namespace Aloha.Services.Products
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
        public void Configure(IApplicationBuilder app)
        {
            var container = app.ApplicationServices.GetRequiredService<IContainer>();

            container
                .UseAloha()
                .UseRabbitMq()
                .SubscribeEvent<ProductCreated>();

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
