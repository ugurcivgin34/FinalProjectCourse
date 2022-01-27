using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => //yay�n 
            Host.CreateDefaultBuilder(args)
                    //.netin kendi IoC yap�s� var fakat biz autofac yaparak IoC kullanmak istedik.O y�zden b�yle bir yap� yapt�k
                .UseServiceProviderFactory(new AutofacServiceProviderFactory()) //servise sa�lay�c�� fabrikas� olarak kulllan
                .ConfigureContainer<ContainerBuilder>(builder=> 
                {
                    builder.RegisterModule(new AutofacBusinessModule());
                })

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
