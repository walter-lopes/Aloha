using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Aloha.CQRS.Commands;
using Aloha.Persistence.MongoDB;
using Aloha.Services.Products.Domain;
using System;

namespace Aloha.Services.Products
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

            services
                 .AddAloha()
                .AddCommandHandlers()
                .AddInMemoryCommandDispatcher()
                .AddMongo()
                .AddMongoRepository<Product, Guid>("products")
                .Build();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
