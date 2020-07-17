using Aloha.CQRS.Commands;
using Aloha.CQRS.Commands.Dispatchers;
using DryIoc;
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

        private IContainer _container;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services.AddControllers();

            mvcBuilder.AddControllersAsServices();

           

        }

        public void ConfigureContainer(IContainer container)
        {
          

            container.AddAloha().AddCommandHandlers().AddInMemoryCommandDispatcher().Build();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var container = app.ApplicationServices.GetRequiredService<IContainer>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static IContainer CreateMyPreConfiguredContainer() =>
           // This is an example configuration,
           // for possible options check the https://github.com/dadhi/DryIoc/blob/master/docs/DryIoc.Docs/RulesAndDefaultConventions.md
           new Container(rules =>

               // Configures property injection for Controllers, ensure that you've added `AddControllersAsServices` in `ConfigureServices`
               rules.With(propertiesAndFields: request =>
                   request.ServiceType.Name.EndsWith("Controller") ? PropertiesAndFields.Properties()(request) : null)
           );
    }
}
