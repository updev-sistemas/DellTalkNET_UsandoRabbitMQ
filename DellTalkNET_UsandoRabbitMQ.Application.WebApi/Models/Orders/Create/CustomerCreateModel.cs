namespace DellTalkNET_UsandoRabbitMQ.Application.WebApi.Models.Orders.Create
{
    public class CustomerCreateModel
    {
        public virtual string? Name { get; set; }
        public virtual string? Document { get; set; }
        public virtual string? Email { get; set; }
        public virtual DateTime? Birthday { get; set; }
    }
}
