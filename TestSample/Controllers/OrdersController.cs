using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularwithASPCore.Models;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static TestSample.Controllers.HomeController;

namespace AngularwithASPCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Orders")]
    
    public class OrdersController : Controller
    {
        // GET: api/Orders
        [HttpGet]

        public object Get()
        {
            var data = OrdersDetails.GetAllRecords().ToList();

            return  new { Items = data, Count = data.Count() };
        }

        // GET: api/Orders/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Orders
        [HttpPost]
        public object Post([FromBody]OrdersDetails value)
        {
            OrdersDetails.GetAllRecords().Insert(0, value);
            return value;
        }


        // PUT: api/Orders/5
        [HttpPut]
        public object Put(int id, [FromBody]OrdersDetails value)
        {
            var data = OrdersDetails.GetAllRecords().Where(or => or.OrderID == value.OrderID).FirstOrDefault();
            if (data != null)
            {
                var ord = value;
                OrdersDetails val = OrdersDetails.GetAllRecords().Where(or => or.OrderID == ord.OrderID).FirstOrDefault();
                val.OrderID = ord.OrderID;
                val.EmployeeID = ord.EmployeeID;
                val.CustomerID = ord.CustomerID;
                val.Freight = ord.Freight;
                val.OrderDate = ord.OrderDate;
                val.ShipCity = ord.ShipCity;
            }
            return value;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id:int}")]
        [Route("Orders/{id:int}")]
        public void Delete(int id)
        {
            var data = OrdersDetails.GetAllRecords().Where(or => or.OrderID == id).FirstOrDefault();
            OrdersDetails.GetAllRecords().Remove(data);
        }

        
    }
    public class Data
    {
        public bool requiresCounts { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
    }
}
