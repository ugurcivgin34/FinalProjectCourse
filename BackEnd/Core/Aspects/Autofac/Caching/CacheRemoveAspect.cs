using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    //Datamız bozulduğu zaman yani yeni data eklenirse,data güncellenirse ,data silinirse CacheRemove u kallanırız
    //Yani manager da cache yönetimi yaparken o manager da veriyi manipüle eden  yani bozulan metotlara bunu uygularız 

    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation) //onSuccess de kullanmızın sebebi örneğin veri tabanına veri eklerken belki hata alınacak ve ekleyemeyecek
            //Böyle durumda cache den silmek saçma olur.O yüzden herşey tamamlandıktan sonra cache den silmek daha mantıklı
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
