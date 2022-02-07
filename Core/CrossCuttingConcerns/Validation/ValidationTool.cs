using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        //ProductValidator şeklinde bir validator tanımladık.Aslında onu kastetiyor IValidator.yani aşağıda metodun içinde productvalidator ve product ı vermiş olduk
        //Doğrulamayı sağlayacak yapı,doğrulanacak class
        public static void Validate(IValidator validator,object entity) //entity,dto ekleyebiliriz.o yüzden object tanımladık,herşeyin base i çünkü
        {

            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context); //ProductValidator kullnarak doğrulama yapacak
            if (!result.IsValid) // geçerli değilse hata fırlat
            {
                throw new ValidationException(result.Errors);
            }

            //    var context = new ValidationContext<Product>(product);
            //    ProductValidator productValidator = new ProductValidator();
            //    var result = productValidator.Validate(context); //ProductValidator kullnarak doğrulama yapacak
            //    if (!result.IsValid) // geçerli değilse hata fırlat
            //    {
            //        throw new ValidationException(result.Errors);
            //    }
            //}
        }
    }
}
