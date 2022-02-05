using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        //password vericeğiz , dışarı out olanları yani 2 yapıyı dışarı çıkaracağız
        //Girilen passwordun hash sini oluşturmaya yarıyor.
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) // değişen nesne aynı zamanda byte[] e aktarılacak.out kullandık o yüzden
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())  //kety oluşturuyor
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }


        //password kullanıcının girdiği parola.Sisteme tekrar girdiği parola.
        //sonradan sisteme girmek isteyen kişinin verdiği passwordun bizim veri kaynağımızdaki hashle ilgili salt a göre eşlenip eşlenmeyeceğni
        //verdiğimiz yerdir
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))  
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i]!=passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
          
        }
    }
}
