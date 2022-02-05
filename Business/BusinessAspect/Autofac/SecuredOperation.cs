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

    //jwt
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
