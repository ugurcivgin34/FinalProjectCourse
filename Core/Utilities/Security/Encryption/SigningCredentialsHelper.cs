using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        // jwt servicelerinin web apide,web apinin kullanabileceği jwt larının oluşturabilmesi için
        //bu şifrelemenin web apinin kendisininde ihtiyacı var,gelen jwt nin doğrulanması gerekiyor.
        //Bu yüzden aşağıdaki gibi hangi keyi kullancak hangi algoritmayı kullancak onları belirttik
        //Zaten hashing helper de de belirtmiştik.O yüzden burda da belirtmemiz lzım 

        //Kullanıcı bilgilerin aslında bizim cretentialslarımız oluyor

        //Bir sisteme girmek için keye ihtiyaç var.O bizim credetialsımız yani
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
