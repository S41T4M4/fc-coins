using EAFCCoinsManager.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EAFCCoinsManager.Services
{
    public class TokenService
    {
        public static object GenerateToken(Usuarios usuarios)
        {
            var secretKey = Environment.GetEnvironmentVariable("SECRET_KEY") ?? "default_secret_key_with_minimum_length_32";

            if (secretKey.Length < 32)
            {
                throw new ArgumentException("A chave secreta deve ter pelo menos 32 caracteres.");
            }

            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim("usuariosId", usuarios.id.ToString()),
                new Claim(ClaimTypes.Name, usuarios.nome),
                new Claim(ClaimTypes.Email, usuarios.email),
                new Claim(ClaimTypes.Role, usuarios.role)
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return new { token = tokenString };
        }
    }


}
