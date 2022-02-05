using DellTalkNET_UsandoRabbitMQ.Application.WebApi.Models.Customers;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DellTalkNET_UsandoRabbitMQ.Application.WebApi.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork db;

        public CustomerController(IUnitOfWork unitOfWork)
            => this.db = unitOfWork;


        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Infrastructure.Domains.Customer>? collection = db.Customer.GetAll();

            return Ok(collection);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{document}")]
        public IActionResult Get(string document)
        {
            Infrastructure.Domains.Customer? target = db.Customer.GetAll(x => x.Document == document).FirstOrDefault();

            if (target != null)
            {
                return Ok(new
                {
                    target?.Document,
                    target?.Name,
                    target?.Email,
                    CreatedAt = target?.CreatedAt?.ToString("dd/MM/yyyy HH:mm"),
                    UpdatedAt = target?.UpdatedAt?.ToString("dd/MM/yyyy HH:mm")
                });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
