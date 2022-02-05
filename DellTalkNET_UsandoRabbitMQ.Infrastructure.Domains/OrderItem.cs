namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains
{
    public class OrderItem : EntityBase
    {
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
        public virtual short? Sequence { get; set; }
        public virtual decimal? Amount { get; set; }
        public virtual decimal? Cost { get; set; }
        public virtual decimal? Total => ((Amount ?? 0) * (Cost ?? 0));
    }
}
