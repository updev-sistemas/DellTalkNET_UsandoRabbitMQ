namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains
{
    public class Sku : EntityBase
    {
        public virtual Product? Product { get; set; }
        public virtual string? Unit { get; set; }
        public virtual string? Brand { get; set; }
        public virtual string? Barcode { get; set; }
        public virtual decimal? Price { get; set; }
    }
}
