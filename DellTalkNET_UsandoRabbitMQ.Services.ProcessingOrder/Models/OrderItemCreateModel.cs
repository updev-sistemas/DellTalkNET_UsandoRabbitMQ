namespace DellTalkNET_UsandoRabbitMQ.Services.ProcessingOrder.Models
{
    public class OrderItemCreateModel
    {
        public virtual ProductCreateModel? Product { get; set; }
        public virtual decimal? Cost { get; set; }
        public virtual decimal? Amount { get; set; }
    }
}
