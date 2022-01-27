using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Reflection;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            // ProductTest();
            //CategoryTest();

            //DortIslem dortIslem = new DortIslem(4,2);
            //Console.WriteLine(dortIslem.Topla(8,2));

            var tip = typeof(DortIslem);//tipini aldık
            //var dortIslem=(DortIslem)Activator.CreateInstance(type,5,5);// new ledik bir nevi .Çalışma anında yapıyor bunu ama
            var instance = Activator.CreateInstance(tip, 5, 5);
            MethodInfo methodInfo = instance.GetType().GetMethod("Topla2"); // yukarıdaki instance ile bağını kaybediyor
            Console.WriteLine(methodInfo.Invoke(instance, null));  //invoke onu çalıştırıyor yani metodu
            //hangi örneğin topla2 sini ını çalıştırıyoruz  anlamına geliyor

            Console.WriteLine("---------------------------");

            var metodlar = tip.GetMethods();
            foreach (var info in metodlar)
            {
                Console.WriteLine("Metod adı : {0}" , info.Name);
                foreach (var parameterInfo in info.GetParameters())
                {
                    Console.WriteLine("Parametre : {0}",parameterInfo.Name);

                }
                foreach (var attibute in info.GetCustomAttributes())
                {
                    Console.WriteLine("Attribute Name:{0}",attibute.GetType().Name);
                }
            }



        }

        public class DortIslem
        {
            private int _sayi1;
            private int _sayi2;

            public DortIslem(int sayi1,int sayi2)
            {
                _sayi1 = sayi1;
                _sayi2 = sayi2;
            }
            public DortIslem()
            {

            }
            public int Topla(int sayi1,int sayi2)
            {
                return sayi1 + sayi2;
            }

            [MetodName("Çarpma")]
            public int Topla2()
            {
                return _sayi2 + _sayi1;
            }



            
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var item in categoryManager.GetAll())
            {
                Console.WriteLine(item.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());
            var result = productManager.GetProductDetails();
            if (result.Success == true)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + "  /  " + product.CategoryName);
                }
            }

        }
        public class MetodNameAttribute : Attribute
        {
            private string v;

            public MetodNameAttribute(string v)
            {
                this.v = v;
            }
        }
    }
}
