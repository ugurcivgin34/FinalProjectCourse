using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
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
            //Autofac bize aop imkan� sa�l�yor
            //Autofac,Ninject,CastleWindsor,StructureMap,LightInJect -->IoC Container
            services.AddControllers();
            services.AddSingleton<IProductService,ProductManager>();
            //Biri constructor da IProductService verirse onun kar��l���nda ProductManager i newleyip referans�n� veriyor
            //Arka planda referans olu�turur.IoC ler bizim yerimize new liyor
            //Yani IProductService �eklinde bir�ey g�r�rsen onun kar��l��� ProductManager �eklinde tan�mlat�yoruz.Yani arka planda newliyor.
            //Singleton t�m bellekte bir tane productmanager olu�turuyor.bin tane client gelsin bir kere newledi�i i�in bir kere ayn� insteans � hep veriyor.
            //��inde data tutmuyorsak bunu kullanmak mant�kl�
            services.AddSingleton<IProductDal, EfProductDal>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
