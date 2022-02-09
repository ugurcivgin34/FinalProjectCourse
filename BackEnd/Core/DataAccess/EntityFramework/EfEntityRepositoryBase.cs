using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    //Hangi tabloyu verirsek onu kullancak.IEntityRepository<TEntity> diyerek TEntity yi verdik , bu tabloyu kullan anlamında

    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new() //DataAccess katmanında new NorthWindContext şekilde yapıyorduk.yani newlenebilir olmalı gerekdiğini
        //belirttik.
    {
        public void Add(TEntity entity)
        {
            //IDisposable pattern implementation of c#
            //using in işi bittikten sonra  Garbage Collector bellekten temizler
            using (TContext context = new TContext())
            {
                //veri kaynağından gönderdiğimiz producta bir tane nesneyi eşleştir
                var addedEntity = context.Entry(entity); //refereansı bul
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList(); //Yukarda expression yaptığımız için lamda (=>) şeklinde kullanmaya gerek kalmadı.Arka planda algılıyor
                //Parametre olarak göndereceğimiz şey lamda
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updateEntity = context.Entry(entity);
                updateEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
