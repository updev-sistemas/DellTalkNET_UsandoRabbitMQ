using DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains;
using FluentNHibernate.Mapping;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Database.Mapping
{
    public class InvoiceItemMap : ClassMap<InvoiceItem>
    {
        public InvoiceItemMap()
        {
            Table("tb_invoice_items");

            Id(x => x.Id).Column("id").GeneratedBy.Identity();

            References(x => x.Product).Column("product_id").Not.Nullable();
            References(x => x.Invoice).Column("invoice_id").Not.Nullable();

            Map(x => x.Sequence).Column("sequence").Not.Nullable();
            Map(x => x.Amount).Column("amount").Nullable();
            Map(x => x.Cost).Column("cost").Not.Nullable();

            Map(x => x.CreatedAt).Column("created_at").Nullable();
            Map(x => x.UpdatedAt).Column("updated_at").Nullable();
        }
    }
}
