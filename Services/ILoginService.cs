namespace comprobantes_back.Services
{
    public interface ILoginService<T>
    {
        public Task<string> Login(T user);
        public Task<T> GetById(int id);
        public Task<List<T>> GetAll();
    }
}
