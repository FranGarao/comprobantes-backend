using comprobantes_back.Models;
using comprobantes_back.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace comprobantes_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                Console.WriteLine($"Error al obtener las facturas: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno al obtener las facturas: {ex.Message}");
            }
        }

    }
}
