using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    //Context : Db tabloları ile proje classlarını birbirine bağlamak
    public class NorthwindContext : DbContext
    {
        //Proje hangi veritabanı ile ilişkili olduğunu belirttiğimiz kısım
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-7H18640;Database=Northwind;Trusted_Connection=true"); 
            //@ koyarız ki ters slash ı anlasın yoksa c#da başka anlamı var
            //Trusted_Connection=true yapmamızın sebebi ise kulanıcı adı ve şifre kullanmaya gerek olmadığını belirttik
        }

        //Sağdakiler veritabanındaki tablo adına karşılık geliyor
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
