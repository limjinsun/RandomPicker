using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WheelOfFateAPI.RotationManagement;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WheelOfFateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotaController : Controller
    {
        [HttpGet]
        public JsonResult Get()
        {
            dynamic myDynamic = new ExpandoObject();
            myDynamic.NewPair = RotationManager.PickEngineers();
            myDynamic.RotaQue = RotationManager.engineerQue;
            return Json(myDynamic);
        }

        [HttpGet]
        [Route("{employeeId}")]
        public JsonResult Get(int employeeId)
        {
            return Json(RotationManager.GetEngineerById(employeeId));
        }
    }
}
