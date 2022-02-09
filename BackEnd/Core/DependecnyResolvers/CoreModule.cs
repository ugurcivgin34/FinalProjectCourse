using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependecnyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            //arka planda hazır bir ICacheManager instance oluşturuyor.IoC de insteasımız var
            serviceCollection.AddMemoryCache();//MemoryCacheManager daki  IMemoryCache _memoryCache yı enjekte etmiş olduk
                                               //.net core kendisi injec yapıyor.IMemoryCache sadece enjekte oluyor

            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//Aşağıdaki install 
                                                                                        //Her yapılan istekle ilgili oluşan contex,bizim clienatımız bir istek yaptığı zaman o isteğin başlagıncıdnan bitişine kadar yani
                                                                                        //requestin yapılmasında yanıt response un verilmesine kadarki süreçte o kullanıcının o isteğinin takip edilme işini
                                                                                        //HttpContextAccessor yapıyor.

            serviceCollection.AddSingleton<ICacheManager,MemoryCacheManager>();
            serviceCollection.AddSingleton<Stopwatch>();

        }
    }
}
