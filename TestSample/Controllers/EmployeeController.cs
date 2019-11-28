using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestSample.Controllers
{
    public class EmployeeController : Controller
    {
        [Produces("application/json")]
        [Route("api/Employee")]

        public object Get()
        {
                var data1 = EmpoyeeDetails.GetAllRecords().ToList();

                return  new { Items = data1, Count = data1.Count() };
            
        }
    }
}