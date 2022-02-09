using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;

        public InMemoryProductDal()
        {
            _products = new List<Product> {
                new Product{ProductId=1,CategoryId=1,ProductName="Bardak",UnitPrice=15,UnitsInStock=15},
                new Product{ProductId=2,CategoryId=1,ProductName="Kamera",UnitPrice=500,UnitsInStock=3},
                new Product{ProductId=3,CategoryId=2,ProductName="Telefon",UnitPrice=1500,UnitsInStock=2},
                new Product{ProductId=4,CategoryId=2,ProductName="Klavye",UnitPrice=150,UnitsInStock=65},
                new Product{ProductId=5,CategoryId=2,ProductName="Fare",UnitPrice=85,UnitsInStock=1}
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(int productId)
        {

            Product productToDelete = null; //new Product(); şeklinde yazmaya gerek yok, zaten aşağıda linq sorgusu sonucu bulduğu değeri adresini atayacak,boşuna bellekte yer açmış olacaktı
            //Ling kullanmasaydık
            //foreach (var p in _products)
            //{
            //    if (product.ProductId == p.ProductId)
            //    {
            //        productToDelete = p;
            //    }
            //}

            //LINQ - Language Integrated Query
            //Id bazlı ifadelerde SingleOrDefault yazmak mantıklı olur
            //productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId); //Bir eleman bulmaya yarar,yani tek tek dolaşır(SingleOrDefault)
            productToDelete = _products.SingleOrDefault(p => p.ProductId == productId);


            _products.Remove(productToDelete);
        }

        public List<Product> GetAll()
        {
            return _products;
        }

    
        public void Update(Product product)
        {
           Product productToUpdate  = _products.SingleOrDefault(p => p.ProductId == product.ProductId);

            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            //Where koşulu içindeki şarta uyan bütün elamanları yeni bir liste haline getirir ve onu döndürür.Sql mantığı gibi
           return  _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }
    }
}
