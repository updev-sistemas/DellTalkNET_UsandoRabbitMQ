namespace DellTalkNET_UsandoRabbitMQ.Application.WebApi.Models.Orders.Create
{
    public class OrderCreateModel
    {
        public virtual DateTime? Date { get; set; }
        public virtual CustomerCreateModel? Customer { get; set; }
        public virtual List<OrderItemCreateModel>? Items { get; set; }
    }
}
