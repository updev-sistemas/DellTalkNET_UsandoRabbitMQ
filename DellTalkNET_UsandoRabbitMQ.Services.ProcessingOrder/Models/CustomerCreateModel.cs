namespace DellTalkNET_UsandoRabbitMQ.Services.ProcessingOrder.Models
{
    public class CustomerCreateModel
    {
        public virtual string? Name { get; set; }
        public virtual string? Document { get; set; }
        public virtual string? Email { get; set; }
        public virtual DateTime? Birthday { get; set; }
    }
}
