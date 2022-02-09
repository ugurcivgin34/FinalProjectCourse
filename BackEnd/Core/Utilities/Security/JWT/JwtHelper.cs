using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Core.Utilities.Security.JWT
{

    //
    public class JwtHelper : ITokenHelper
    {
        //using Microsoft.Extensions.Configuration;
        public IConfiguration Configuration { get; } //asp.net web Api deki appsetting.json u okumaya yarar
        private TokenOptions _tokenOptions; //IConfiguration da appsetting.json da okudğumuz verileri TokenOptions a aktarcaz.Aşağıda mapleyip aktardık.
        //Zaten bunu class şekilde tanımlayıp appsetting.json daki jwt özelliklerinin aynısını classın özellikleri olarak verdik.

        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>(); //appsetting.jsom dak, her madde bir section..Get<TokenOPiton> ile sınıfının değerleri ile map le

        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration); //bu token ne zaman bitecek, kaç dk eklesin.Onu da configuration dan alıyor.application json dosyasını okumaya yarıyor ICınfiguration Caonfiguratin kısmı.Enjekte ettik zaten.Tarihe çevirdik


            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey); //Anahtar değerini applicatoin json daki security keyden alacak, tokeni oluştaracak güvenlik anahtarını elde etmiş olduk
            //Bir algoritmayı kullanarak bir tane kendimize token oluşturacağız.Token oluştururken bir key e ihtiyacımız var.Kendi bildiğimiz özel bir anahtar
            //Bizde bunu kullanıyor olacağız.//var secutiyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey)) normalde bu şekilde yazabilirdik.
            //Fakat başka yerde de kullanma ihtimalimiz olduğundan sürekli böyle yazmak olmazdı


            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);//Hangi algoritmayı hangi anahtayı kllnacak 
            //Bizim securit key ve algoritmamızı belirlediğimiz bir nesnedir.

            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);//jwt de olması gerkenlern yani app setting de verdiğimiz şeyler _tokenoptins,kullanıcı bilgisi ve bu adamın claimleri neler, neyi kullanarak yapacak 
          

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler(); //tokeni handler ile yazıyor olmamız gerekiyor
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }


        //Token üretme
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now, //expression bilgisi şuandan önce ise geçerli değildir.
                claims: SetClaims(user, operationClaims), //claim şekild vermemiz gerekiyordu.o yüzden işlem yaptık.Claim yapısı şekilde yani vermemiz gerekiyor
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        //private List<Claim> şekilde de diyebilirdik IEnumerable List in base , o yüzden farketmezdi
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString()); 
            claims.AddEmail(user.Email); // claims.Add(new Claim("email",user.Email)) şekilde de yazabilirdik bunu ve diğerlerini fakat sürekli böyle yazmak zaman kaybı da olur.O yüzden extension yapısı yaptık
            claims.AddName($"{user.FirstName} {user.LastName}"); //user.FirstName + user.Lastname şekilde de yazabiliriz
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }
}
