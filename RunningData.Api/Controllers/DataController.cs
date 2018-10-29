using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RunningData.Api.Parameters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RunningData.Api.Controllers
{
    [Route("data")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class DataController : Controller
    {
        // POST: data/fuel
        [HttpPost]
        [Route("fuel")]
        [ProducesResponseType(200, Type=typeof(bool))]
        [MapToApiVersion("1.0")]
        public bool InsertFuelData([FromBody]FuelDataRequest request)
        {
            return true;
        }
    }
}
