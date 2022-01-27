using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        public override void Intercept(IInvocation invocation) //invocation çalıştırmak istediğimiz metod oluyor
            //burası çatı, yani metodlar çalışmadan önce burdan geçer ,kurallara bakar
        {
            var isSuccess = true;
            OnBefore(invocation); //metodun başında çalışır
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e); //hata alma esnasında
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation); // metod başarılı olursa bu çalışsın
                }
            }
            OnAfter(invocation); //metod sonunda çalışır
        }
    }
}