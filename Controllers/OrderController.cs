using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ORC.workshop.Models;
using ORC.workshop.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ORC.workshop.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {

        private readonly IConfiguration _configuration;

        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/values
        [Route("getall")]
        [HttpGet]
        public JsonResult GetAll()
        {
            try
            {
                string sqlDataSource = _configuration.GetConnectionString("ORCdb");
                JsonResult result = new JsonResult(new OrderService(sqlDataSource).GetOrder());
                return result;
            }
            catch (Exception ex)
            {
                JsonResult result = new JsonResult(ex.Message);
                result.StatusCode = 400;

                return result;
            }

        }

        [Route("addorder")]
        [HttpPost]
        public JsonResult AddOrder([FromBody] OrderModel order)
        {
            try
            {
                string sqlDataSource = _configuration.GetConnectionString("ORCdb");
                JsonResult result = new JsonResult(new OrderService(sqlDataSource).AddOrder(order));
                return result;
            }
            catch (Exception ex)
            {
                JsonResult result = new JsonResult(ex.Message);
                result.StatusCode = 400;
                return result;
            }
        }

        [Route("submit/{booking_id}")]
        [HttpPut]
        public JsonResult ChangeToSubmit(int booking_id)
        {
            try
            {
                string sqlDataSource = _configuration.GetConnectionString("ORCdb");
                JsonResult result = new JsonResult(new OrderService(sqlDataSource).ChangeToSubmit(booking_id));
                return result;
            }
            catch(Exception ex)
            {
                JsonResult result = new JsonResult(ex.Message);
                result.StatusCode = 404;
                return result;
            }
        }
        [Route("confirm/{booking_id}")]
        [HttpPut]
        public JsonResult ChangeToConfirm(int booking_id)
        {
            try
            {
                string sqlDataSource = _configuration.GetConnectionString("ORCdb");
                JsonResult result = new JsonResult(new OrderService(sqlDataSource).ChangeToConfirm(booking_id));
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
