using comprobantes_back.Models;
using comprobantes_back.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace comprobantes_back.Controllers
{
    //[Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class InvoicesController : ControllerBase
    {
        private readonly ICommonService<Invoice> _invoiceService;
        public InvoicesController(ICommonService<Invoice> service,
            ICommonService<Job> jobService)
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno al obtener las facturas: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Invoice>> Add(Invoice invoice)
        {
            if (invoice != null)
            {
                await _invoiceService.Add(invoice);

                //SendWhatsAppMessage(invoice);

                return CreatedAtAction(nameof(GetAll), new { id = invoice.Id }, invoice);

            }
            return BadRequest("No se pudo crear el comprobante.");
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Invoice>> GetById(int id)
        {
            var invoice = await _invoiceService.GetById(id);

            if (invoice == null)
            {
                return NotFound($"No se encontró el comprobante con el id: {id}");
            }

            return Ok(invoice);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Invoice>> Delete(int id)
        {
            var invoice = await _invoiceService.DeleteAsync(id);

            if (invoice == null)
            {
                return NotFound($"No se encontró el comprobante con el id: {id}");
            }

            return Ok(invoice);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Invoice>> Update(int id, Invoice invoice)
        {
            var updatedInvoice = await _invoiceService.UpdateAsync(id, invoice);

            if (updatedInvoice == null)
            {
                return BadRequest($"No se encontró el comprobante con el id: {invoice.Id}");
            }

            return Ok(updatedInvoice);
        }
    }
}
