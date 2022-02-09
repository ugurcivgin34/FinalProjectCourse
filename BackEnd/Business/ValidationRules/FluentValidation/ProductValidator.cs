using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {

        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0); //0dan büyük olmalı
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);//category id si 1 olduğu zaman 10 dan büyük ve 0 olanı getir unit priceleri
            //Dto lar içinde validator kullanılabilir.İçececek kategorisindeki ürünlerin fiyatı mininum 10 lira olmalı
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı"); //kendi kuralımızı yazacaksan böyle yapmamız gerekiyor
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
