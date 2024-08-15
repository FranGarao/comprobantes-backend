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
    public class CustomerController : ControllerBase
    {
        private readonly ICommonService<Customer> _customerService;
        public CustomerController(ICommonService<Customer> service)
        {
            _customerService = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            try
            {
                var customers = await _customerService.GetAllASync(); // Espera la tarea aquí

                Console.WriteLine(customers); // Esto puede no ser necesario en producción

                return Ok(customers); // Devuelve los resultados dentro de Ok()
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los trabajos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno al obtener los trabajos: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Add(Customer customer)
        {
            if (customer != null)
            {
                await _customerService.Add(customer);
                return CreatedAtAction(nameof(GetAll), new { id = customer.Id }, customer);
            }
            return null;
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> Update(int id, Customer customer)
        {
            var updatedCustomer = await _customerService.UpdateAsync(id, customer);

            if (updatedCustomer == null)
            {
                return NotFound($"No se encontró el comprobante con el id: {customer.Id}");
            }

            return Ok(updatedCustomer);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Customer>> Delete(int id)
        {
            var customer = await _customerService.DeleteAsync(id);

            if (customer == null)
            {
                return NotFound($"No se encontró el comprobante con el id: {id}");
            }

            return Ok(customer);
        }
    }
}
