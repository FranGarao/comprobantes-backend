using comprobantes_back.Models;
using System.Text.Json;

namespace comprobantes_back.Services
{
    public class InvoiceService : ICommonService<Invoice>
    {
        private readonly string _filePath = "./db/invoices.json";
        public InvoiceService() { }
        public async Task<IEnumerable<Invoice>> GetAllASync()
        {
            using var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
            var json = await JsonSerializer.DeserializeAsync<IEnumerable<Invoice>>(stream);
            return json; 
        }

        public Task<Invoice> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Invoice> Add(Invoice entity)
        {
            throw new NotImplementedException();
        }
        public Task<Invoice> UpdateAsync(Invoice entity)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
