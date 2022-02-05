
using DellTalkNET_UsandoRabbitMQ.Application.Common.Configurations;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Database.Contracts;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Database.Impl;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.MessageBroker;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.MessageBroker.Contracts;
using DellTalkNET_UsandoRabbitMQ.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;

namespace DellTalkNET_UsandoRabbitMQ.Application.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddOptions();

            services.AddResponseCompression();

            services.Configure<DatabaseConfig>(Configuration.GetSection("Database"));
            services.Configure<RabbitMQConfig>(Configuration.GetSection("RabbitMQ"));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDatabaseFactory, DatabaseFactory>();
            services.AddScoped<IMessageBrokerConsumer, MessageBrokerConsumer>();
            services.AddScoped<IMessageBrokerPublish, MessageBrokerPublish>();

        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseAuthorization(); 

            app.MapControllers();

            app.UseHttpsRedirection();

            app.UseResponseCompression();

        }
    }
}
