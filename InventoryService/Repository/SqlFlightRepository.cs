using InventoryService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Repository
{
    public class SqlFlightRepository : IInventoryRepository
    {
        private readonly InventoryDbContext context;
        public SqlFlightRepository(InventoryDbContext context)
        {
            this.context = context;
        }

        //public IEnumerable<TblInventory> GetSearchFlight(DateTime dt, string fromPlace, string toPleace, string trip)
        //{
        //    List<TblAirLine> objAirLine = new List<TblAirLine>();
        //    objAirLine = context.TblAirLines.ToList();
        //    return objAirLine;
        //}

        public DataTable GetSearchFlight(DateTime dt, string fromPlace, string toPleace, string trip)
        {
            DataTable dataTable = new DataTable();
            try
            {
               
                var result = from dtt in context.TblInventorys
                             where (dtt.FromPlace.Contains(fromPlace) && dtt.ToPlace.Contains(toPleace))
                             select new { dtt.FlightName, dtt.FromPlace, dtt.ToPlace };
                dataTable.Rows.Add(result);
            }
            catch (Exception)
            {

            }
            return dataTable;
        }

        public TblInventory GetInventoryByNumber(string inventoryNumber)
        {
            TblInventory objInventory = new TblInventory();
            objInventory = context.TblInventorys.Where(a => a.AirLineNumber.Equals(inventoryNumber)).FirstOrDefault();
            //if (objAirLine != null)
            // {
            //    return objAirLine;
            // }
            return objInventory;
        }
        public IEnumerable<TblInventory> GetAllInventory()
        {
            List<TblInventory> objInventory = new List<TblInventory>();
            objInventory = context.TblInventorys.ToList();
            return objInventory;
        }
        public TblInventory AddInventory(TblInventory tblInventory)
        {
            context.TblInventorys.Add(tblInventory);
            context.SaveChanges();
            return tblInventory;
        }
        public TblInventory UpdateInventory(TblInventory tblInventoryChanges)
        {
            var tblInventory = context.TblInventorys.Attach(tblInventoryChanges);
            tblInventory.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return tblInventoryChanges;
        }
        public TblInventory BlockInventory(string FlightNumber, bool ActiveIND)
        {
            TblInventory tblInventory = context.TblInventorys.Where(a => a.Id.Equals(FlightNumber)).FirstOrDefault();
            if (tblInventory != null)
            {
                tblInventory.ActiveIND = false;
                context.SaveChanges();
            }
            return tblInventory;
        }

        


        
    }
}
