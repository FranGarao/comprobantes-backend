using comprobantes_back.Models;
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

        public async Task<User> Login(User userData)
        {
            if (userData == null)
            {
                return null;
            }
            var users = await GetAll();
            var user = users.FirstOrDefault(i => i.UserName == userData.UserName);


            if (user != null && user.Password == userData.Password)
            {
                return user;
            }
            return null;
        }
    }
}
