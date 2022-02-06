using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>(); //using Microsoft.Extensions.DependencyInjection;
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            //Örnek olarak get all metodunun reflectedtype(namespace) al , yani namespace.manager yani  Busines.Concrete.IProductService(namespace + class) //IProductService 
            //olmasının sebebi interfacel den inpmelemnt aldığı için ana şey o .  yani özetlemek gerekirse metodun namaepscesinin classı + metodun ismi
            //Northwind.Business.IproductService.GetAll işlemi yaptık kısaca
            

            var arguments = invocation.Arguments.ToList();
            //Metodun parametleri varsa listeye çevirecek

            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";//parametrler arasına virgül koyup birleştirir . İKi soru işaretnin anlamı ise
            //varsa soldakini yoksa sağdakini ekle demek.                    ?parametre null değilse listeye çevrilebiliyorsa ekle değilse null ekle
            //metot da parametreler varsa Northwind.Business.IproductService.GetAll(parametre varsa tabi) şeklinde yapar.Key i oluşturmuş olduk

            if (_cacheManager.IsAdd(key))
                //key değeri var mı ,cache de var mı yani
            {
                invocation.ReturnValue = _cacheManager.Get(key); //normalde business de return değeri veritabanına gidip gelen değerdir.Ama burda veritabanına gitmesin de cacheden getirsin demek
                //eğer varsa metodu hiç çalıştırmadan geri döndürecek.Yani kısaca cache de key varsa  cachedeki değeri alıp döndürecek

                return;
            }
            invocation.Proceed();
            //metodu devam ettir , yani metodu çalıştıracak sonra

            _cacheManager.Add(key, invocation.ReturnValue, _duration);
            //cache keyi , dönüş değerini ve süresini ekleyecek
        }
    }
}
