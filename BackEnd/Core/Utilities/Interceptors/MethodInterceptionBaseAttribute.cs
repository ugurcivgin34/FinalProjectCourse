using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{

    //classlara yada metodlara ekleyebilirsin,birden fazla yere ekleyebilirsin , inheretnce ypaılan yere de ekleyebilirsin
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor //autofac in aop kısmı da var
    {
        public int Priority { get; set; } //Hangi priority önce çalışsın , loglama ,yetkilendirme vs

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
