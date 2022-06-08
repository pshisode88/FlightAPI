using InventoryService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Repository
{
    public interface IInventoryRepository
    {
        DataTable GetSearchFlight(DateTime dt, string fromPlace, string toPleace, string trip);
        TblInventory GetInventoryByNumber(string airLineNumber);
        IEnumerable<TblInventory> GetAllInventory();
        TblInventory AddInventory(TblInventory tblAirLine);
        TblInventory UpdateInventory(TblInventory tblAirLineChanges);
        TblInventory BlockInventory(string FlightNumber, bool ActiveIND);

    }
}
