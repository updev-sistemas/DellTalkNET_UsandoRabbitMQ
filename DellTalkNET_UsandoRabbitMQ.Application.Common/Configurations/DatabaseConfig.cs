namespace DellTalkNET_UsandoRabbitMQ.Application.Common.Configurations
{
    public class DatabaseConfig
    {
        public virtual string? Host { get; set; }
        public virtual int? Port { get; set; }
        public virtual string? DbName { get; set; }
        public virtual string? Username { get; set; }
        public virtual string? Password { get; set; }
    }
}
