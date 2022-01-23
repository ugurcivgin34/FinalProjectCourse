using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constans
{
    public  static class Messages
    {
        //private şeklinde yazsaydık değişken isimleri küçük harfle başlardı fakar public ile tanımladığımız için büyük harfle kullanılır
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        internal static string MaintenanceTime = "Sistem bakımda";
        internal static string ProductsListed = "Ürünler listelendi";
    }
}
