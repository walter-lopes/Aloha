using Aloha.Types;
using DryIoc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Aloha
{
    public static class Extensions
    {
        private const string SectionName = "app";

        public static IAlohaBuilder AddAloha(this IContainer services, string sectionName = SectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            var builder = AlohaBuilder.Create(services);
            //var options = builder.GetOptions<AppOptions>(sectionName);
            //builder.Services.AddMemoryCache();
            //services.AddSingleton(options);
            //services.AddSingleton<IServiceId, ServiceId>();
            //if (!options.DisplayBanner || string.IsNullOrWhiteSpace(options.Name))
            //{
            //    return builder;
            //}

           

            return builder;
        }

        public static IApplicationBuilder UseAloha(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<IStartupInitializer>();
                Task.Run(() => initializer.InitializeAsync()).GetAwaiter().GetResult();
            }

            return app;
        }

        public static TModel GetOptions<TModel>(this IAlohaBuilder builder, string settingsSectionName)
           where TModel : new()
        {

            var configuration = builder.Services.GetService<IConfiguration>();
            return configuration.GetOptions<TModel>(settingsSectionName);
        }

        public static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName)
           where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(sectionName).Bind(model);
            return model;
        }
    }
}
