namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains
{
    public abstract class EntityBase
    {
        public virtual long? Id { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
    }
}