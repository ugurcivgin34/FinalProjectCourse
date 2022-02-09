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
        //invocation : business method...Virtiual metodlar ezilmeyi bekleyen metodlardır.Bir aspect yazdığımız zaman,metodun neresinde çalışmasını istiyorsak
        //aşağıdaki ilgili Metod ları kullanırız.Aspect demek bu classı temel alan ve hangisi çalışssın istiyorsak onu içeren
        //operasyondur.
        protected virtual void OnBefore(IInvocation invocation) { }
        //IInvocation invocation burda metod demektir .Yani Add,Delete,Update,GetAll,GetById gibi metodlardır

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