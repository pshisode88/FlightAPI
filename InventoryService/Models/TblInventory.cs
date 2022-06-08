using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Models
{
    public class TblInventory
    {
        public int Id { get; set; }
        public string FlightName { get; set; }
        public string FlightNumber { get; set; }
        public string AirLineNumber { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ScheduleDay { get; set; }
        public int BusinessSeats { get; set; }
        public int NonBusinessSeats { get; set; }
        public int TicketCost { get; set; }
        public int NumberOfRows { get; set; }
        public string Meal { set; get; }
        public bool ActiveIND { get; set; }

    }
    
}
