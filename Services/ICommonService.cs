namespace comprobantes_back.Services
{
    public interface ICommonService<T>
    {
        public Task<IEnumerable<T>> GetAllASync();
        public Task<T> GetById(int id);
        public Task<T> Add(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task DeleteAsync(int id);
    }
}
