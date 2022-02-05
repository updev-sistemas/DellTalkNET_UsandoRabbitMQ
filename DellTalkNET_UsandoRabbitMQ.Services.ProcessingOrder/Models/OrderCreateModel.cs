namespace DellTalkNET_UsandoRabbitMQ.Services.ProcessingOrder.Models
{
    public class OrderCreateModel
    {
        public virtual DateTime? Date { get; set; }
        public virtual CustomerCreateModel? Customer { get; set; }
        public virtual List<OrderItemCreateModel>? Items { get; set; }
    }
}
