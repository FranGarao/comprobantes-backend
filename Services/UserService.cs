using comprobantes_back.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace comprobantes_back.Services
{
    public class UserService : ILoginService<User>
    {
        private readonly string _filePath = "./db/users.json";


        public async Task<List<User>> GetAll()
        {
            using var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
            var users = await JsonSerializer.DeserializeAsync<List<User>>(stream);

            return users;
        }
        public async Task<User> GetById(int id)
        {
            var users = await GetAll();
            var user = users.FirstOrDefault(i => i.Id == id);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public Task<User> GetById()
        {
            throw new NotImplementedException();
        }

        public async Task<string> Login(User userData)
        {
            if (userData == null)
            {
                return null;
            }

            var users = await GetAll();
            var user = users.FirstOrDefault(i => i.UserName == userData.UserName && i.Password == userData.Password);

            if (user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("askop[owike90234812opk@@#%$^$idouj23--32193jdaijhd"); //TODO: cambiar xd
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, userData.UserName)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }

            return null;
        }
    }
}
