namespace DellTalkNET_UsandoRabbitMQ.Application.WebApi.Models.Orders.Create
{
    public class OrderItemCreateModel
    {
        public virtual ProductCreateModel? Product { get; set; }
        public virtual decimal? Cost { get; set; }
        public virtual decimal? Amount { get; set; }
    }
}
