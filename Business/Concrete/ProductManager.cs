using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.CCS;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;

        }

        //Claim => product.add,admin bunlar birer claim dir.Anahtar demek
        //jwt=> json web token. client istek atar api ye , kullanıcı kayıt olunca jwt ile client e gönderir
        //karşı tarafta da local storage , cooki ,mobil tarafda saklanan bir veri kaynağı da olbilir. Jwt buralarda saklanır
        //salting = tuzlama yani kullanıcnın girdiği parolayı daha da güçlendirmek için yapılan bir yapı
        //encrpytion => şifreleme . Verinin tamamını encript etmiş oluyoruz.Şifrelemek
        //decprtion => o şifreyi çözmek
        //hash = > veriyi hashlemiyoruz . Datayı belli bir algoritmaya göre hashliyoruz . data oluşuyor yani.Ama bu data nın karşılığı o baştaki data değil
        //encrtption ile key oluşturuyoruz aslında . 
        //ev örneği gibi. Evden çıkarken kapıyı kitliyoruz , girerken de kapıyı açıp giriyoruz.Çıkarken enctpriton girerken decprtion yapıyoruz


        //[SecuredOperation("product.add,admin")]
        //[ValidationAspect(typeof(ProductValidator))]
        //[CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //var context = new ValidationContext<Product>(product);
            //ProductValidator validation = new ProductValidator();
            //var result = validation.Validate(context);
            //if (!result.IsValid)
            //{
            //    throw new ValidationException(result.Errors);
            //}
            //ValidationTool.Validate(new ProductValidator(),product);

            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                  CheckIfProductNameExists(product.ProductName),
                  CheckIfCategoryLimitExceded());
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        [CacheAspect] //key,calue
        public IDataResult<List<Product>> GetAll()
        {
            //if (DateTime.Now.Hour == 20)
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime); //ErrorResult da olur
            //}
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }
        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }


        [CacheAspect]
        [PerformanceAspect(5)] //bu metodun çalışması 5 sn yeyi geçerse beni uyar.Yani sistemde yavaşlık var anlamında
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")] //Yani update işlemi yapıldığında başarılı şekilde yapıldığında yani IProductService.Get ile başlayan metotları cache den sil demek
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }


        //Bir kategoride en fazla 10 ürün olabilir
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            //select count(*) from products where categoryId=1
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 80)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        //ProductName aynı isminde olan product ekleme
        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any(); //Any varmı demek,bu şarta sağlayan elaman var mı
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }

            return new SuccessResult();
        }

        //Eğer mevcut kategori sayısı 15'i geçti ise sisteme yeni ürün eklenemez
        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult AddTransctionalTest(Product product)
        {
            Add(product);
            if (product.UnitPrice<10)
            {
                throw new Exception("");
            }
            Add(product);
            return null;
        }
    }
}
