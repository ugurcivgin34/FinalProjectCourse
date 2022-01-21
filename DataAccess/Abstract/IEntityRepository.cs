using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{

    //generic constraint
    //class : referans tip
    //IEntity : IEntity olabilir veya IEntity implemente eden bir nesne olabilir
    //new(): new'lenebilir olmalı
    public interface IEntityRepository<T> where T: class,IEntity,new()  //T yi burda kısıtladık. Çünkü t yerine herşey yazılabilir.
        //Bunun da önüne geçmek için generic constraint(generic kısıtlama) uyguladık.
    {
        //delege

        //ürünleri category e göre listele,ürünleri fiyata göre listele,şu iki fiyat arasında olanları getir gibi filtreler yapmak için böyle bir yapı kullanılıyor
        //Bunlara  expression yapısı dır.Delege yani


        //İş katmanında linq ile yaptığımız sorguları expression yaptığımız için burda arka planda anlıyor
        List<T> GetAll(Expression<Func<T,bool>> filter=null); //linq kullanmamızı sağlayacak . filter=null dememizin sebebi filtre vermeyedebilirsin demek.Filtre verip filtreleyip öyle de data getirebilirsin
        T Get(Expression<Func<T, bool>> filter); //filter sadece yazdığımız veriye göre getirir.Filter zorunlu
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
      
    }
}
