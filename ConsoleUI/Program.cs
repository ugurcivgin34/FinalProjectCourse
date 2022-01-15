using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new InMemoryProductDal());
            productManager.Delete(2);
            foreach (var product in productManager.GetAll())
            {
                Console.WriteLine(product.ProductName);
            }

        }
    }
}
