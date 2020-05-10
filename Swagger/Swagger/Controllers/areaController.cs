using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swagger.Models;

namespace Swagger.Controllers
{
    public class areaController : ApiController
    {
        public IEnumerable<area> get()
        {
            using (VeggieDatabase db = new VeggieDatabase())
            {
                return db.areas.ToList();
            }
        }
    }
}
