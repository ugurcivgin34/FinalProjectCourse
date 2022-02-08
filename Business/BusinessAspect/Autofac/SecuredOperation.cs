using Business.Constans;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;


namespace Business.BusinessAspect.Autofac
{

    //Security aspectleri business de yazılır herzaman.Çünkü her business ın kendine göre securitysi değişebilir
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor; //jwt ı da gönderip istek gönderebliriz, her istek için httpcontext oluşturur , herkes için bir thread oluşur

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            //Böyle yapmamızın sebebi aspectler de injection yapılmıyor.Bu yüzden servicetool şekilde bir yapı kurduk.
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>(); //using Microsoft.Extensions.DependencyInjection;
            //  productService = ServiceTool.ServiceProvider.GetService<IProductService>();
            //windowsform da çalıştığımızı düşünürsek yukarıdaki gibi yapabiliriz
            //belki api ile çalışmayacaz.O yüzden servicetool yaptık

            //Api den business business den dalı .ağırıyor.Yani bir zincir var.Fakat burda aspect bu zincirin içinde yok.Yani burda injection yaparsak başarılı olamayız
            //asp.web.api bunu göremez..Aspect zincirin içinde değil.Bu yüzden burdaki depencyleri yakalayabilmemiz için servicetool yapısını kurduk
            //Özetlemek gerekirse bu kod autofac ile kendi servis mimarimi oluşturdumuğumuz yapıya ulaşıp kullancak

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return; //metodu çalıştırmaya devam et
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
