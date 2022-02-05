using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DellTalkNET_UsandoRabbitMQ.Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IUnitOfWork db;

        public InvoiceController(IUnitOfWork unitOfWork)
            => this.db = unitOfWork;



    }
}
