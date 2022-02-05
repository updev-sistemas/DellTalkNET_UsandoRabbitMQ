using DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains;
using FluentNHibernate.Mapping;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Database.Mapping
{
    public class SkuMap : ClassMap<Sku>
    {
        public SkuMap()
        {
            Table("tb_skus");

            Id(x => x.Id).Column("id").GeneratedBy.Increment();

            References(x => x.Product).Column("product_id").Not.Nullable();

            Map(x => x.Unit).Column("unit").Not.Nullable();
            Map(x => x.Brand).Column("brand").Not.Nullable();
            Map(x => x.Barcode).Column("barcode").Not.Nullable();
            Map(x => x.Price).Column("price").Not.Nullable();

            Map(x => x.CreatedAt).Column("created_at").Nullable();
            Map(x => x.UpdatedAt).Column("updated_at").Nullable();
        }
    }
}
