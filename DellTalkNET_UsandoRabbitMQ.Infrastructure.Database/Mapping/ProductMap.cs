using DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains;
using FluentNHibernate.Mapping;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Database.Mapping
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Table("tb_products");

            Id(x => x.Id).Column("id").GeneratedBy.Increment();

            Map(x => x.Name).Column("name").Not.Nullable();
            Map(x => x.Code).Column("code").Not.Nullable();

            Map(x => x.CreatedAt).Column("created_at").Nullable();
            Map(x => x.UpdatedAt).Column("updated_at").Nullable();
        }
    }
}
