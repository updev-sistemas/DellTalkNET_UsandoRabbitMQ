namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains
{
    public class Customer : EntityBase
    {
        public virtual string? Document { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Email { get; set; }
        public virtual DateTime? Birthday { get; set; }
    }
}
