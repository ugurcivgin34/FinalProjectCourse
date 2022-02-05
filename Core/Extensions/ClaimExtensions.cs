using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{

    //extension yapıcaksak statik olması önemli
    public static class ClaimExtensions
    {
        //.net içinde bir yapıya extra metod eklemek istiyorsak extension yaparız. this ile genişletip medot oluşturmuş oluruz o yapı için
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            //using System.IdentityModel.Tokens.Jwt;
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email)); //kaydedilmiş isimlerden yararlandık
        }

        public static void AddName(this ICollection<Claim> claims, string name)//claims.AddName şekilde yazabileceğiz
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }

        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }

        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        }
    }
}
