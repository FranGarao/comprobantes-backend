using comprobantes_back.Models;
using comprobantes_back.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace comprobantes_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly ICommonService<Invoice> _invoiceService;
        public InvoicesController(ICommonService<Invoice> service)
        {
            _invoiceService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetAll()
        {
            try
            {
                var invoices = await _invoiceService.GetAllASync(); // Espera la tarea aquí

                Console.WriteLine(invoices); // Esto puede no ser necesario en producción

                return Ok(invoices); // Devuelve los resultados dentro de Ok()
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las facturas: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al obtener las facturas");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Invoice>> Add(Invoice invoice)
        {
            Console.WriteLine(invoice);
            return Ok(invoice);
        }
    }
}
