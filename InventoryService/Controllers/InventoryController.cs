using InventoryService.Models;
using InventoryService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class InventoryController : ControllerBase
    {

        private readonly IInventoryRepository _inventoryRepository;
        public InventoryController(IInventoryRepository obj)
        {
            _inventoryRepository = obj;
        }

        //Start
        [HttpGet]
        [Route("getInventory")]
        public IActionResult GetAllInventory()
        {
            string response = string.Empty;
            try
            {
                var inventory = _inventoryRepository.GetAllInventory();
                if (inventory != null)
                {
                    return new OkObjectResult(inventory);
                }
                else
                {
                    return new OkObjectResult("Data Not Present");
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return new NotFoundObjectResult(response);
        }
        [HttpPost]
        [Route("getInventoryById")]
        public IActionResult GetInventoryByNumber(string inventoryNumber)
        {
            string response = string.Empty;
            try
            {
                var inventory = _inventoryRepository.GetInventoryByNumber(inventoryNumber);
                if (inventory != null)
                {
                    return new OkObjectResult(inventory);
                }

            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return new NotFoundObjectResult(response);
        }
        [HttpPost]
        [Route("addInventory")]
        public IActionResult Registration([FromBody] TblInventory tblinventory)
        {
            string response = string.Empty;
            try
            {

                var inventory = _inventoryRepository.AddInventory(tblinventory);
                if (inventory != null)
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

        [Route("updateInventory")]
        [HttpPost]
        public IActionResult UpdateTblAirLine([FromBody] TblInventory tblInventory)
        {
            string response = string.Empty;
            try
            {
                var inventory = _inventoryRepository.UpdateInventory(tblInventory);
                if (inventory != null)
                {
                    return new OkObjectResult(inventory);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return new NotFoundObjectResult(response);
        }

        [Route("blockInventory")]
        [HttpGet]
        public IActionResult BlockInventory(string FlightNumber, bool ActiveIND)
        {
            string response = string.Empty;
            try
            {
                var inventory = _inventoryRepository.BlockInventory(FlightNumber, ActiveIND);
                if (inventory != null)
                {
                    return new OkObjectResult(inventory);
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return new NotFoundObjectResult(response);
        }

        [HttpPost]
        [Route("getFlightById")]
        public IActionResult GetSearchFlight(DateTime dt, string fromPlace, string toPlace, string trip)
        {
            string response = string.Empty;
            try
            {
                DataTable dTInventory = _inventoryRepository.GetSearchFlight(dt, fromPlace, toPlace, trip);
                if (dTInventory != null)
                {
                    return new OkObjectResult(dTInventory);
                }

            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return new NotFoundObjectResult(response);
        }
        //End

    }
}
