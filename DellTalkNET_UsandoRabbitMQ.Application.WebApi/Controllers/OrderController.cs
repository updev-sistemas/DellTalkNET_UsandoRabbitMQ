using DellTalkNET_UsandoRabbitMQ.Application.Common.Const;
using DellTalkNET_UsandoRabbitMQ.Application.WebApi.Models.Orders.Create;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.MessageBroker.Contracts;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace DellTalkNET_UsandoRabbitMQ.Application.WebApi.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    { 
        private readonly IUnitOfWork db;
        private readonly IMessageBrokerPublish queue;

        public OrderController(IUnitOfWork unitOfWork, IMessageBrokerPublish queue)
        {
            this.db = unitOfWork;
            this.queue = queue;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] OrderCreateModel model)
        {
            // Aplicação de Validação diversas

            this.queue.ToQueue(QueueConst.QUEUE_NEW_ORDER, JsonSerializer.Serialize(model));

            return Ok(model);
        }
    }
}
