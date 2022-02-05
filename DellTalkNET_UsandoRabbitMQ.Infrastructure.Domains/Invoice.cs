namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains
{
    public class Invoice : EntityBase
    {
        public virtual Order? Order { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual string? Number { get; set; }
        public virtual DateTime? Date { get; set; }
    }
}
