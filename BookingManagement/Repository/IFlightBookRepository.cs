using BookingManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingManagement.Repository
{
    public interface IFlightBookRepository
    {
        TblFlightBook BookTicket(TblFlightBook tblFlightBook);
        TblFlightBook CancelTicket(string pNRNumber);

        IEnumerable<TblFlightBook> HistoryTickets(int userId);
    }
}
