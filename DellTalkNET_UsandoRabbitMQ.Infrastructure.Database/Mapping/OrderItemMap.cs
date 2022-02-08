using DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains;
using FluentNHibernate.Mapping;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Database.Mapping
{
    public class OrderItemMap : ClassMap<OrderItem>
    {
        public OrderItemMap()
        {
            Table("tb_order_items");

            Id(x => x.Id).Column("id").GeneratedBy.Identity();

            References(x => x.Product).Column("product_id").Not.Nullable();
            References(x => x.Order).Column("order_id").Not.Nullable();

            Map(x => x.Sequence).Column("sequence").Not.Nullable();
            Map(x => x.Amount).Column("amount").Not.Nullable();
            Map(x => x.Cost).Column("cost").Not.Nullable();

            Map(x => x.CreatedAt).Column("created_at").Nullable();
            Map(x => x.UpdatedAt).Column("updated_at").Nullable();
        }
    }
}
