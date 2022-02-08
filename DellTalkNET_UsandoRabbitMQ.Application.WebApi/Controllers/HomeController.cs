using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DellTalkNET_UsandoRabbitMQ.Application.WebApi.Controllers
{
    [ApiController]
    [Route("/api")]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [HttpGet]
        [Route("init")]
        public IActionResult Index()
        {
            object result = new
            {
                Name = "[Dell Talks .NET] => Usando RabbitMQ",
                Version = "1.0.0"
            };
            return Ok(result);
        }
    }
}
