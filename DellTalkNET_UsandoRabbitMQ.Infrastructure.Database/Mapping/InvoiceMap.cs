using DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains;
using FluentNHibernate.Mapping;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Database.Mapping
{
    public class InvoiceMap : ClassMap<Invoice>
    {
        public InvoiceMap()
        {
            Table("tb_invoices");

            Id(x => x.Id).Column("id").GeneratedBy.Increment();

            References(x => x.Order).Column("order_id").Not.Nullable();
            References(x => x.Customer).Column("customer_id").Not.Nullable();

            Map(x => x.Number).Column("number").Not.Nullable();
            Map(x => x.Date).Column("date_exp").Not.Nullable();

            Map(x => x.CreatedAt).Column("created_at").Nullable();
            Map(x => x.UpdatedAt).Column("updated_at").Nullable();
        }
    }
}
