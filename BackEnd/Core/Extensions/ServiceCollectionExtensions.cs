using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public  static class ServiceCollectionExtensions
    {
        //IServiceCollection bizim apimizin servis bağımlılıklarını eklediğimiz yada araya girmesini istediğimiz servisleri eklediğimiz koleksiyonun kendisidir
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection,ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }
            return ServiceTool.Create(serviceCollection);
        }
    }
}
//Bu yaptığımız hareket core katmanı da dahil olmak üzere ekleyeceğimiz bütün injeksectionları toplayacağımız bir yapı oldu
