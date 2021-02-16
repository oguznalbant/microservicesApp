using ECom.Cart.Api.Data.Abstract;
using ECom.Cart.Api.Data.Concrete;
using ECom.Cart.Api.Repository.Abstract;
using ECom.Cart.Api.Repository.Concrete;
using ECom.EventBusRabbitMq;
using ECom.EventBusRabbitMq.Abstraction;
using ECom.EventBusRabbitMq.Events;
using ECom.EventBusRabbitMq.Producer;
using ECom.EventBusRabbitMq.Producer.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using StackExchange.Redis;

namespace ECom.Cart.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Redis connection from appsetting.json
            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var configuration = ConfigurationOptions.Parse(Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddTransient<IShoppingCartContext, ShoppingCartContext>();
            services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddTransient(typeof(IEventBusProducer<ShoppingCartCheckoutEvent>), typeof(EventBusProducer));

            services.AddAutoMapper(typeof(Startup)); // automapper self DI

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Cart Api", Version = "v1" });
            });


            // rabbitmq dependecy injectioný burdan yapýlýp connectionda appsettingsden saglanan configurationlarla saðlanýyor.
            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = Configuration["EventBus:Hostname"] // appsettingsdeki hostname alýnýþ þekli
                };

                if (!string.IsNullOrEmpty(Configuration["EventBus:Username"]))
                {
                    factory.UserName = Configuration["EventBus:Username"];
                }

                if (!string.IsNullOrEmpty(Configuration["EventBus:Password"]))
                {
                    factory.UserName = Configuration["EventBus:Password"];
                }

                return new RabbitMQConnection(factory);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Shopping Cart Api V1"));
        }
    }
}
