using CalculatorServer.Logic;
using CalculatorServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorServer.Controllers
{
    [ApiController]
    [Route("Convertor")]
    public class CalaculatorController : ControllerBase
    {
        private IConfiguration configuration;

        public CalaculatorController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        [HttpPost]
        [Route("ConvertEquation")]
        public IActionResult Calc([FromBody] CalcRequest req)
        {
            try
            {
                var filePath = configuration.GetSection("MySettings").GetSection("FilePath").Value;
                var response = CalculatorLogic.Calculate(req.CalcString, filePath);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetHistoryResult")]
        public IActionResult GetHistoryResult()
        {
            try
            {
                CalcResponse response = new CalcResponse();
                var filePath = configuration.GetSection("MySettings").GetSection("FilePath").Value;
                response.AllResults = CalculatorLogic.getAllResults(filePath);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
