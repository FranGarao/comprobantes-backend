using comprobantes_back.Models;
using System.Text.Json;

namespace comprobantes_back.Services
{
    public class InvoiceService : ICommonService<Invoice>
    {
        private readonly string _filePath = "./db/invoices.json";
        private ICommonService<Job> _jobService;
        public InvoiceService(ICommonService<Job> jobService)
        {
            _jobService = jobService;
        }
        public async Task<List<Invoice>> GetAllASync()
        {
                using var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
                var invoices = await JsonSerializer.DeserializeAsync<List<Invoice>>(stream);

                return invoices;
        }

        public async Task<Invoice> GetById(int id)
        {
            var invoices = await GetAllASync();
            var invoice = invoices.FirstOrDefault(i => i.Id == id);
            if (invoice == null)
            {
                return null;
            }
            return invoice;
        }
        public async Task<Invoice> Add(Invoice invoice)
        {
            var invoices = await GetAllASync();
            var job = await _jobService.GetById(invoice.JobId);

            var newInvoice = new Invoice
            {
                Id = invoice.Id,
                Name = invoice.Name,
                DeliveryDate = invoice.DeliveryDate,
                Phone = invoice.Phone,
                Total = invoice.Total,
                Deposit = invoice.Deposit,
                Balance = invoice.Balance,
                JobId = invoice.JobId,
                Job = job.Name,
            };

            invoices.Add(newInvoice);
            var jsonInvoices = JsonSerializer.Serialize(invoices);

            File.WriteAllText(_filePath, jsonInvoices);

            return invoice;
        }
        public async Task<Invoice> UpdateAsync(int id, Invoice entity)
        {
            var invoice = await GetById(id);
            if (invoice != null)
            {
                var invoices = await GetAllASync();
                var index = invoices.FindIndex(i => i.Id == id);
                invoices[index] = entity;
                var jsonInvoices = JsonSerializer.Serialize(invoices);
                File.WriteAllText(_filePath, jsonInvoices);
                return invoice;
            }
            return null;
        }


        public async Task<Invoice> DeleteAsync(int id)
        {
            var invoice = await GetById(id);
            var invoices = await GetAllASync();
            invoices.RemoveAll(i => i.Id == id);
            var jsonInvoices = JsonSerializer.Serialize(invoices);
            File.WriteAllText(_filePath, jsonInvoices);
            return invoice;
        }

    }
}
