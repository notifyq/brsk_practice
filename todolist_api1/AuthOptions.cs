using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace todolist_api1
{
    public class AuthOptions
    {
        public const string ISSUER = "https://localhost:44357"; // издатель токена
        public const string AUDIENCE = "https://localhost:44357"; // потребитель токена
        public const string KEY = "iDkwhatIneedwritetOthisiinefor256!123";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
           new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
