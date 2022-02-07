using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    //Autofac i kullanarak yaptığımız aspect sınıfı
    public class ValidationAspect : MethodInterception //Aspect =>metod hata verdiğinde başında sonunda yada ortasında çalışacak yapı
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType) //validator type ver.Productmanager add metodunun üstekündeki [ValidationASpect(typeof(ProductValidator))]
            //typeof kısmı yani
        {
            //attitirubute ler type ile çalışır
            //defensive coding
            //validatör den farklı type de yapabilir.Bunun önüne geçmek için yaptık.Typeof(product ) da yazıabilir kullanıcı.Bunun önüne geçmek için yaptık
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) //gönderilen validatorType bir IValidator değilse hata versin
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }

        //Doğrulama başta yapılır o yüzden onbefore şekilde yaptık
        protected override void OnBefore(IInvocation invocation)
        {
            //ProductValidator ü newledi.Referansını kullanabilcez.Çünkü manager da sadece typeof nu verip kullandırdık.Ama elimizde insteans olmadığı için
            //yapamazdık.Bu yüzden ProductValitar bir insteansını oluştur ve onu IValidator haline getir.referans değişltirme yok
            var validator = (IValidator)Activator.CreateInstance(_validatorType);  //reflection çalışma anında birşeyleri çalıştırmayı sağlıyor.Mesela new lemeeyi çalışma anında yap diyor
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //çalışma tipini bul ,validatorType nin base AbstractValidator<Product>
            //ProductValidator ün base  ProductValidator:AbstractValidator<Product> daki product ı alacak.Birden fazla da olabilir ama 1 olduğunu bliyoru zzaten.
            //Sağlama almak için [0] şekilde tanımladık.Yani product tipi product entity olduğunu ifade ettik.


            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); //onunda parametlerini bul,yani ilgili metodun parametlerini bulmaya sağlıyor
            //Metodun parametlerini gez, yani productManager daki Add metodunun parametrelerinin tiğini gezecek.Eğer gezilen tiplerden entityType ile uyaşan varsa
            //Onları valide edecek,Aşağıdaki foreach ile geziyor zaten

            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
