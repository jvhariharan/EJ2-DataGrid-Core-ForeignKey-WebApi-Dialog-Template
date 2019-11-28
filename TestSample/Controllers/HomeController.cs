using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestSample.Models;

namespace TestSample.Controllers
{

    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EditPartial([FromBody] CRUDModel<OrdersDetails> value)
        {
            ViewBag.datasource = OrdersDetails.GetAllRecords();
            return PartialView("_DialogEditPartial", value.value);
        }

        public IActionResult AddPartial([FromBody] CRUDModel<OrdersDetails> value)
        {
            ViewBag.datasource = OrdersDetails.GetAllRecords();
            return PartialView("_DialogAddPartial");
        }

        public IActionResult UrlDatasource([FromBody]Data dm)
        {
            var order = OrdersDetails.GetAllRecords();
            var employee = EmpoyeeDetails.GetAllRecords();
            var Data = order.ToList();
            int count = order.Count();
            //return dm.requiresCounts ? Json(new { result = Data.Skip(dm.skip).Take(dm.take), count = count }) : Json(Data);
            if (dm.skip != 0)
                Data = Data.Skip(dm.skip).ToList();
            if (dm.take != 0)
                Data = Data.Take(dm.take).ToList();
            return dm.requiresCounts ? Json(new { result = Data, count = count }) : Json(Data);
        }

        public ActionResult Update([FromBody]CRUDModel<OrdersDetails> value)
        {
            var ord = value.value;
            OrdersDetails val = OrdersDetails.GetAllRecords().Where(or => or.OrderID == ord.OrderID).FirstOrDefault();
            val.OrderID = ord.OrderID;
            val.EmployeeID = ord.EmployeeID;
            val.CustomerID = ord.CustomerID;
            val.Freight = ord.Freight;
            val.OrderDate = ord.OrderDate;
            val.ShipCity = ord.ShipCity;

            return Json(value.value);
        }
        //insert the record
        public ActionResult Insert([FromBody]CRUDModel<OrdersDetails> value)
        {

            OrdersDetails.GetAllRecords().Insert(0, value.value);
            return Json(value.value);
        }
        //Delete the record
        public ActionResult CellEditDelete([FromBody]CRUDModel<OrdersDetails> value)
        {
            OrdersDetails.GetAllRecords().Remove(OrdersDetails.GetAllRecords().Where(or => or.OrderID == int.Parse(value.key.ToString())).FirstOrDefault());
            return Json(value);
        }


        public class Data
        {

            public bool requiresCounts { get; set; }
            public int skip { get; set; }
            public int take { get; set; }
        }

        public class DataManager
        {

            public int skip { get; set; }

            public int take { get; set; }

            public string tab { get; set; }

            public string[] draggedRow { get; set; }
            public int position { get; set; }

            public bool requiresCounts { get; set; }

            public List<Wheres> where { get; set; }

            public List<Search> search { get; set; }

        }

        public class Wheres
        {
            public string field { get; set; }
            public bool ignoreCase { get; set; }

            public bool isComplex { get; set; }
            public string value { get; set; }
            [JsonProperty("operator")]
            public string Operator { get; set; }

        }
        public class Search
        {
            public string[] fields { get; set; }
            public bool ignoreCase { get; set; }
            public string key { get; set; }
            [JsonProperty("operator")]
            public string Operator { get; set; }
        }

        public class CRUDModel<T> where T : class
        {
            public string action { get; set; }

            public string table { get; set; }

            public string keyColumn { get; set; }

            public object key { get; set; }

            public T value { get; set; }

            public List<T> added { get; set; }

            public List<T> changed { get; set; }

            public List<T> deleted { get; set; }

            public IDictionary<string, object> @params { get; set; }
        }
    }
     public class OrdersDetails
    {
        public static List<OrdersDetails> order = new List<OrdersDetails>();
        public OrdersDetails()
            {

            }
            public OrdersDetails(int OrderID, string CustomerId, int EmployeeId, double Freight, bool Verified, DateTime OrderDate, string ShipCity, string ShipName, string ShipCountry, DateTime ShippedDate, string ShipAddress)
            {
                this.OrderID = OrderID;
                this.CustomerID = CustomerId;
                this.EmployeeID = EmployeeId;
                this.Freight = Freight;
                this.ShipCity = ShipCity;
                this.Verified = Verified;
                this.OrderDate = OrderDate;
                this.ShipName = ShipName;
                this.ShipCountry = ShipCountry;
                this.ShippedDate = ShippedDate;
                this.ShipAddress = ShipAddress;
            }
        public static List<OrdersDetails> GetAllRecords()
        {
            if (order.Count() == 0)
            {
                int code = 10000;
                for (int i = 1; i < 10; i++)
                {
                    order.Add(new OrdersDetails(code + 1, "ALFKI", i + 0, 2.3 * i, false, new DateTime(1991, 05, 15), "Berlin", "Simons bistro", "Denmark", new DateTime(1996, 7, 16), "Kirchgasse 6"));
                    order.Add(new OrdersDetails(code + 2, "ANATR", i + 2, 3.3 * i, true, new DateTime(1990, 04, 04), "Madrid", "Queen Cozinha", "Brazil", new DateTime(1996, 9, 11), "Avda. Azteca 123"));
                    order.Add(new OrdersDetails(code + 3, "ANTON", i + 1, 4.3 * i, true, new DateTime(1957, 11, 30), "Cholchester", "Frankenversand", "Germany", new DateTime(1996, 10, 7), "Carrera 52 con Ave. Bolívar #65-98 Llano Largo"));
                    order.Add(new OrdersDetails(code + 4, "BLONP", i + 3, 5.3 * i, false, new DateTime(1930, 10, 22), "Marseille", "Ernst Handel", "Austria", new DateTime(1996, 12, 30), "Magazinweg 7"));
                    order.Add(new OrdersDetails(code + 5, "BOLID", i + 4, 6.3 * i, true, new DateTime(1953, 02, 18), "Tsawassen", "Hanari Carnes", "Switzerland", new DateTime(1997, 12, 3), "1029 - 12th Ave. S."));
                    code += 5;
                }
            }
            return order;
        }

            public int? OrderID { get; set; }
            public string CustomerID { get; set; }
            public int? EmployeeID { get; set; }
            public double? Freight { get; set; }
            public string ShipCity { get; set; }
            public bool Verified { get; set; }
            public DateTime OrderDate { get; set; }

            public string ShipName { get; set; }

            public string ShipCountry { get; set; }

            public DateTime ShippedDate { get; set; }
            public string ShipAddress { get; set; }
        }

    public class EmpoyeeDetails
    {
        public static List<EmpoyeeDetails> order = new List<EmpoyeeDetails>();
        public EmpoyeeDetails()
        {

        }
        public EmpoyeeDetails(int OrderID, string CustomerId, int EmployeeId)
        {
            this.OrderID = OrderID;
            this.CustomerID = CustomerId;
            this.EmployeeID = EmployeeId;
        }
        public static List<EmpoyeeDetails> GetAllRecords()
        {
            if (order.Count() == 0)
            {
                int code = 10000;
                    order.Add(new EmpoyeeDetails(code + 1, "JEEVA",  0));
                    order.Add(new EmpoyeeDetails(code + 2, "ANATR",  2));
                    order.Add(new EmpoyeeDetails(code + 3, "ANTON",  1));
                    order.Add(new EmpoyeeDetails(code + 4, "BLONP",  3));
                    order.Add(new EmpoyeeDetails(code + 5, "BOLID",  4));
            }
            return order;
        }

        public int? OrderID { get; set; }
        public string CustomerID { get; set; }
        public int? EmployeeID { get; set; }
    }
}
