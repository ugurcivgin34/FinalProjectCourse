using Core.Entities.Concrete;

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper //Başka teknik de yapabiliriz.İnterface burda o yüzden kullandık
    {

        //APİ<------------------------Kullanıcı giriş parola girdi 
        //APİ kısmında CreateToken çalışacak eğer doğru ise    ,doğru ise ilgili kullanıcı için veritabanına gidecek
        //bu kullanıcnın claimlerini bulacak,orda bir tane jwt üretecek içerisinde bu bilgileri barındıran yani sonra bunları
        //------------------------------>buraya verecek
        //

        AccessToken CreateToken(User user, List<OperationClaim> operationClaims); //kullanıcı bilgleri ve rollerini verdik
    }
}
