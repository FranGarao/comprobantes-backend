using comprobantes_back.Models;
using comprobantes_back.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace comprobantes_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobsController : ControllerBase
    {
        private readonly ICommonService<Job> _jobService;
        public JobsController(ICommonService<Job> service)
        {
            _jobService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetAll()
        {
            try
            {
                var jobs = await _jobService.GetAllASync(); // Espera la tarea aquí

                Console.WriteLine(jobs); // Esto puede no ser necesario en producción

                return Ok(jobs); // Devuelve los resultados dentro de Ok()
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los trabajos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno al obtener los trabajos: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Job>> Create(Job job)
        {
            if (job != null)
            {
                await _jobService.Add(job);
                return CreatedAtAction(nameof(GetAll), new { id = job.Id }, job);
            }
            return null;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Job>> Update(int id, Job job)
        {
            var updatedCustomer = await _jobService.UpdateAsync(id, job);

            if (updatedCustomer == null)
            {
                return NotFound($"No se encontró el trabajo con el id: {job.Id}");
            }

            return Ok(updatedCustomer);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Job>> Delete(int id)
        {
            var job = await _jobService.DeleteAsync(id);

            if (job == null)
            {
                return NotFound($"No se encontró el comprobante con el id: {id}");
            }

            return Ok(job);
        }
    }
}
