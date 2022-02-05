namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains
{
    public class Product : EntityBase
    {
        public virtual string? Name { get; set; }
        public virtual string? Code { get; set; }
    }
}
