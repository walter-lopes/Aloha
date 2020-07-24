﻿using Aloha.Types;
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

        public static IContainer UseAloha(this IContainer container)
        {
            using (var scope = container.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<IStartupInitializer>();
                Task.Run(() => initializer.InitializeAsync()).GetAwaiter().GetResult();
            }

            return container;
        }

        public static TModel GetOptions<TModel>(this IAlohaBuilder builder, string settingsSectionName)
           where TModel : new()
        {

            var configuration = builder.Container.GetService<IConfiguration>();
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
