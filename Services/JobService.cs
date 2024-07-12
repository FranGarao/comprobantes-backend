using comprobantes_back.Models;
using System.IO;
using System.Text.Json;

namespace comprobantes_back.Services
{
    public class JobService : ICommonService<Job>
    {
        private readonly string _filePath = "./db/jobs.json";

        public async Task<List<Job>> GetAllASync()
        {
            using var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
            var jobs = await JsonSerializer.DeserializeAsync<List<Job>>(stream);
            return jobs;
        }

        public async Task<Job> GetById(int id)
        {
            var jobs = await GetAllASync();
            var job = jobs.FirstOrDefault(j => j.Id == id);
            if (job == null)
            {
                return null;
            }
            return job;
        }

        public async Task<Job> Add(Job job)
        {
            var jobs = await GetAllASync();
            jobs.Add(job);
            var jsonJobs = JsonSerializer.Serialize(jobs);

            File.WriteAllText(_filePath, jsonJobs);

            return job;
        }

        public async Task<Job> UpdateAsync(int id, Job jobUpdate)
        {
            var job = await GetById(id);
            if (job != null)
            {
                var jobs = await GetAllASync();
                var index = jobs.FindIndex(i => i.Id == id);
                jobs[index] = jobUpdate;
                var jsonInvoices = JsonSerializer.Serialize(jobs);
                File.WriteAllText(_filePath, jsonInvoices);
                return job;
            }
            return null;
        }
        public async Task<Job> DeleteAsync(int id)
        {
            var job = await GetById(id);
            var jobs = await GetAllASync();
            jobs.RemoveAll(i => i.Id == id);
            var jsonInvoices = JsonSerializer.Serialize(jobs);
            File.WriteAllText(_filePath, jsonInvoices);
            return job;
        }
    }
}
