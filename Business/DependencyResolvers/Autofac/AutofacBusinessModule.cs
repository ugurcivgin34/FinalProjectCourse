using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    //Artık autofac module sin. Burdaki amaç şu,biz WebAppı katmanındaki startup dosyasında IoC yaptık.Yani a interface i 
    //geldiğinde ona karşılık b somut classını ver anlamında bir yapı kurduk.O yapıyı buraya taşıyacağız.Yarın bir gün api
    //değişebilir.Api katmanına bağlı olmasını istemedik

    //Autofac aynı zaman da AOP yapısını da kurmamızı sağlar
    public class AutofacBusinessModule : Module
    {
        //Uygulama ayağı kalktığında Load her zaman çalışır
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance(); //biri senden IProductService isterse sen ona ProductManager insteansını ver
            //newleme yapıyor bir nevi aslında.SingleInstans diyerek 1 kere instead ı üretmesini sağladık.
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
