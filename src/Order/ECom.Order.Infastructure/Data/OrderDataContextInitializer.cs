using ECom.Order.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ECom.Order.Infastructure.Data
{
    public class OrderDataContextInitializer
    {
        public static async Task Initialize(OrderDataContext context, ILoggerFactory loggerFactory, int? retry = 3)
        {
            Stopwatch stopwatch = new Stopwatch();
            int retryCount = retry.Value;
            try
            {
                context.Database.Migrate();

                if (!context.Orders.Any())
                {
                    //stopwatch.Start();
                    context.Orders.AddRange(DummyOrders());
                    await context.SaveChangesAsync();
                    //stopwatch.Stop();
                    //Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                }
            }
            catch (System.Exception ex)
            {
                if (retryCount < 3)
                {
                    retryCount++;
                    var log = loggerFactory.CreateLogger<OrderDataContextInitializer>();
                    log.LogError(ex.Message);
                    await Initialize(context, loggerFactory, retryCount);
                }
            }
        }

        private static IEnumerable<OrderEntity> DummyOrders()
        {
            var dummyOrders = new List<OrderEntity>();
            dummyOrders.Add(new OrderEntity
            {
                FirstName = "Oğuzhan",
                LastName = "Nalbant",
                Country = "Turkey",
                EmailAddress = "oguz.nalbant@gmail.com"
            });

            return dummyOrders;
        }
    }
}
