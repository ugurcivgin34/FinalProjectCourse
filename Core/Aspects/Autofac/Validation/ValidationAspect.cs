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
        public ValidationAspect(Type validatorType) //validator type ver
        {
            //attitirubute ler type ile çalışır
            //defensive coding
            //validatör den farklı type de yapabilir.Bunun önüne geçmek için yaptık
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) //gönderilen validatorType bir IValidator değilse hata versin
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }

        //Doğrulama başta yapılır o yüzden onbefore şekilde yaptık
        protected override void OnBefore(IInvocation invocation)
        {
            //ProductValidator ü newledi.Referansını kullanabilcez
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //reflection çalışma anında birşeyleri çalıştırmayı sağlıyor.Mesela new lemeeyi çalışma anında yap diyor
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //çalışma tipini bul ,validatorType nin  AbstractValidator<Product>
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); //onunda parametlerini bul,yani ilgili metodun parametlerini bulmaya sağlıyor
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
