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
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType) //validator type ver
        {
            //attitirubute ler type ile çalışır
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) //gönderilen validatorType bir IValidator değilse hata versin
            {
                throw new System.Exception("Bur bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
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
