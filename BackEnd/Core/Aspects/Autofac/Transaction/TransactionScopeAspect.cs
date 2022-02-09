using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation) //intercept bu bloğu çalıştır demek
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed(); //=>Yani productmanager daki AddTranscitonalTest metodunun içindeki kısımları çalıştır demek.yani içindeki kodlar try içinde çalışmış oluyor bir nevi
                    //bunu aspect kullanmadan da yapardık fakat düzenli olsun diye yaptık

                    transactionScope.Complete();
                }
                catch (System.Exception e)
                {
                    transactionScope.Dispose();
                    throw;
                }
            }
        }
    }
}
