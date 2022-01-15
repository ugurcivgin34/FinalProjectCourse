using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductDal
    {
        //Metotlar default da public dir.
        List<Product> GetAll();
        void Add(Product product);
        void Update(Product product);
        void Delete(int productId);
        List<Product> GetAllByCategory(int categoryId);
    }
}
