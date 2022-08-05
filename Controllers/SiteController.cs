using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ORC.workshop.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ORC.workshop.Controllers
{
    [Route("api/[controller]")]
    public class SiteController : Controller
    {
        private readonly IConfiguration _configuration;
        public SiteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/values
        [Route("getall/{company_id}")]
        [HttpGet]
        public JsonResult GetAll(int company_id)
        {
            try
            {
                string sqlDataSource = _configuration.GetConnectionString("ORCdb");
                JsonResult result = new JsonResult(new SiteService(sqlDataSource).GetSites(company_id));
                return result;
            }
            catch (Exception ex)
            {
                JsonResult result = new JsonResult(ex.Message);
                result.StatusCode = 400;
                return result;
            }
        }

    }
}
