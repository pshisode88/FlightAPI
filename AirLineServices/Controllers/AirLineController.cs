using AirLineServices.Models;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineServices.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class AirLineController : ControllerBase
    {

        private readonly IAirLinesRepository _airLinesRepository;
        public AirLineController(IAirLinesRepository a)
        {
            _airLinesRepository = a;
        }

        //Start
        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        [Route("getAirLine")]
        public IActionResult GetAllTblAirLines()
        {
            string response = string.Empty;
            try
            {
                var airLines = _airLinesRepository.GetAllTblAirLines();
                if (airLines != null)
                {
                    return new OkObjectResult(airLines);
                    //return Json(new { data = "AirLines details not exists", isSuccess = false });
                }
                else
                {
                    //return Json(new { data = airLines, isSuccess = true });
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
                //return Json(new { data = ex.Message, isSuccess = true });
            }
            return new NotFoundObjectResult(response);
        }
        [HttpPost]
        [Route("getAirLineById")]
        public IActionResult GetAirLineByNumber(string airLineNumber)
        {
            string response = string.Empty;
            try
            {
                var airLines = _airLinesRepository.GetAirLineByNumber(airLineNumber);
                if (airLines != null)
                {
                    return new OkObjectResult(airLines);
                }

            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return new NotFoundObjectResult(response);
        }
        [HttpPost]
        [Route("addAirLine")]
        public IActionResult Registration([FromBody] TblAirLine tblAirLine)
        {
            string response = string.Empty;
            try
            {

                var airLines = _airLinesRepository.AddTblAirLine(tblAirLine);
                if (airLines != null)
                {
                    response = "Data successfully inserted.";
                    return new OkObjectResult(response);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return new NotFoundObjectResult(response);
        }

        [Route("updateAirLine")]
        [HttpPost]
        public IActionResult UpdateTblAirLine([FromBody] TblAirLine tblAirLine)
        {
            string response = string.Empty;
            try
            {
                var airLines = _airLinesRepository.UpdateTblAirLine(tblAirLine);
                if (airLines != null)
                {
                    return new OkObjectResult(airLines);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return new NotFoundObjectResult(response);
        }

        [Route("deleteAirLine")]
        [HttpGet]
        public IActionResult DeleteTblAirLine(string airLineNumber)
        {
            string response = string.Empty;
            try
            {
                var airLines = _airLinesRepository.DeleteTblAirLine(airLineNumber);
                if (airLines != null)
                {
                    return new OkObjectResult(airLines);
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return new NotFoundObjectResult(response);
        }

    }
}
