using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{

    //Kullanıcı istek de bulunurken eğer yetki gerektiren birşey ise ,elinde tokeni bulunursa onu da isteğin içine atıp paketin içine atıp yani hepsini 
    //istek şekild egönderirse clieanta gönderir.Buna accesstoken denir.Erişim anahtarı
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
