using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key); //generic metod 
        object Get(string key);
        void Add(string key, object value, int duration); //cachde ne kadar duracak duration
        bool IsAdd(string key); //cachde varmı , yoksa veritabanından getir cashe ekle 
        void Remove(string key); //cach den uçurma
        void RemoveByPattern(string pattern); //  içinde get olan yada product olanları uçur gibi 
    }
}
