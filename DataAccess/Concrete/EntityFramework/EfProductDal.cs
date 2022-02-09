using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    //Bu şekilde yapmamızın amacı şu şekilde; EfEntityRepositoryBase<Product, NorthwindContext>, bu kısım entitframework,
    //business da IProductDal a bağımlıydı, yarın birgün entityframework kullanmayız dapper kullanabiliriz yada nhibirnate de kullanabiliriz
    // kimisi sql server kimisi oracle kimisi postresql de kullanabilir. EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    //IProductDal o yüzden kullandık extra olarak.
    //İkinci olarak IProductDal a da interface zaten ürüne ait özel operasyonları yazacağız. DTo gibi . O yüzden  EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    //bu şekilde yapı kurduk. Hiç bir şeye bağımlı olmadık
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName,
                                 UnitsInStock = p.UnitsInStock
                             };
                return result.ToList();
            }
        }








        //public void Add(Product entity)
        //{
        //    //IDisposable pattern implementation of c#
        //    //using in işi bittikten sonra  Garbage Collector bellekten temizler
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        //veri kaynağından gönderdiğimiz producta bir tane nesneyi eşleştir
        //        var addedEntity = context.Entry(entity); //refereansı bul
        //        addedEntity.State = EntityState.Added;
        //        context.SaveChanges();
        //    }
        //}

        //public void Delete(Product entity)
        //{
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        var deletedEntity = context.Entry(entity);
        //        deletedEntity.State = EntityState.Deleted;
        //        context.SaveChanges();
        //    }
        //}

        //public Product Get(Expression<Func<Product, bool>> filter)
        //{
        //    using(NorthwindContext context = new NorthwindContext())
        //    {
        //        return context.Set<Product>().SingleOrDefault(filter);
        //    }
        //}

        //public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        //{
        //    using(NorthwindContext context = new NorthwindContext())
        //    {
        //        return filter == null 
        //            ? context.Set<Product>().ToList() 
        //            : context.Set<Product>().Where(filter).ToList(); //Yukarda expression yaptığımız için lamda (=>) şeklinde kullanmaya gerek kalmadı.Arka planda algılıyor
        //        //Parametre olarak göndereceğimiz şey lamda
        //    }
        //}

        //public void Update(Product entity)
        //{
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        var updateEntity = context.Entry(entity);
        //        updateEntity.State = EntityState.Modified;
        //        context.SaveChanges();
        //    }
        //}

    }
}
