using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FishStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FishStore.Controllers
{
    public class OrdersController : SecureControllerBase
    {
        private string _connectionString;

        public OrdersController(IConfiguration config)
        {
            _connectionString = config.GetSection("ConnectionString").Value;
        }

        [HttpGet]
        public IEnumerable<Order> GetAll()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                var orders = db.Query<Order>("Select * from Orders where userid = @userid", new {UserId});

                var fishOrders = db.Query<FishOrder>(@"
                    Select fish.Id FishId,
	                   fish.Name FishName,
	                   Quantity,
	                   Image,
	                   Price,
                       OrderId
                    from orderfish
	                    join Fish 
		                    on fish.Id = OrderFish.FishId
                    where orderid in @orderIds",
                    new { orderIds = orders.Select(x => x.Id) });

                foreach (var order in orders)
                {
                    order.Fishes = fishOrders.Where(fo => fo.OrderId == order.Id);
                }

                return orders;
            }
        }

    }
}