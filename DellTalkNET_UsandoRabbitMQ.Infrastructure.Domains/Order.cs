using DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains.Enums;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains
{
    public class Order : EntityBase
    {
        public virtual Customer? Customer { get; set; }
        public virtual string? Number { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual string? Status { get; set; }
        public virtual DateTime? AuthorizeDate { get; set; }
    }
}
