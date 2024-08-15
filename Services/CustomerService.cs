using comprobantes_back.Models;
using System.Text.Json;

namespace comprobantes_back.Services
{
    public class CustomerService : ICommonService<Customer>
    {
        private readonly string _filePath = "./db/customers.json";

        public async Task<List<Customer>> GetAllASync()
        {
            using var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
            var customers = await JsonSerializer.DeserializeAsync<List<Customer>>(stream);
            return customers;
        }

        public async Task<Customer> GetById(int id)
        {
            var customers = await GetAllASync();
            var customer = customers.FirstOrDefault(j => j.Id == id);
            if (customer == null)
            {
                return null;
            }
            return customer;
        }
        public async Task<Customer> Add(Customer customer)
        {
            var customers = await GetAllASync();
            customers.Add(customer);
            var jsonCustomers = JsonSerializer.Serialize(customers);

            File.WriteAllText(_filePath, jsonCustomers);

            return customer;
        }

        public async Task<Customer> DeleteAsync(int id)
        {
            var customer = await GetById(id);
            var customers = await GetAllASync();
            customers.RemoveAll(i => i.Id == id);
            var jsonCustomers = JsonSerializer.Serialize(customers);
            File.WriteAllText(_filePath, jsonCustomers);
            return customer;
        }

        public async Task<Customer> UpdateAsync(int id, Customer customerUpdate)
        {
            var customer = await GetById(id);
            if (customer != null)
            {
                var customers = await GetAllASync();
                var index = customers.FindIndex(i => i.Id == id);
                customers[index] = customerUpdate;
                var jsonInvoices = JsonSerializer.Serialize(customers);
                File.WriteAllText(_filePath, jsonInvoices);
                return customer;
            }
            return null;
        }
    }
}
