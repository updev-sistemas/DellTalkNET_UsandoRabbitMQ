using DellTalkNET_UsandoRabbitMQ.Infrastructure.Domains;
using FluentNHibernate.Mapping;

namespace DellTalkNET_UsandoRabbitMQ.Infrastructure.Database.Mapping
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Table("tb_customers");

            Id(x => x.Id).Column("id").GeneratedBy.Identity();

            Map(x => x.Document).Column("document").Not.Nullable();
            Map(x => x.Name).Column("name").Not.Nullable();
            Map(x => x.Email).Column("email").Not.Nullable();
            Map(x => x.Birthday).Column("birthday").Not.Nullable();

            Map(x => x.CreatedAt).Column("created_at").Nullable();
            Map(x => x.UpdatedAt).Column("updated_at").Nullable();
        }
    }
}
