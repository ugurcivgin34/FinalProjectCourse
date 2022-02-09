using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{
    //İnjectionları kontrol ettiğimiz nokta
    public static class ServiceTool
    {

        //Api de ve ya autofac de oluşturduğumuz injaksectionları oluşturabilmeye yarıyor.İnterface in karşılığını bu tool sayesinde artık alabiliriz
        public static IServiceProvider ServiceProvider { get; private set; }


        //.net in servislerini al ve onları kendin build et
        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
