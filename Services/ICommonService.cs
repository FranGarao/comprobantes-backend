namespace comprobantes_back.Services
{
    public interface ICommonService<T>
    {
        public Task<List<T>> GetAllASync();
        public Task<T> GetById(int id);
        public Task<T> Add(T entity);
        public Task<T> UpdateAsync(int id, T entity);
        public Task<T> DeleteAsync(int id);
    }
}
