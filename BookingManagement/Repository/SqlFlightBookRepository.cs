using BookingManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingManagement.Repository
{
    public class SqlFlightBookRepository : IFlightBookRepository
    {
        private readonly BookingDbContext context;
        public SqlFlightBookRepository(BookingDbContext context)
        {
            this.context = context;
        }
        public TblFlightBook BookTicket(TblFlightBook tblFlightBook)
        {
            Random rnd = new Random();
            int pnrNumber = rnd.Next(100000, 999999);
            tblFlightBook.PnrNumber = pnrNumber;
            tblFlightBook.ActiveIND = true;
            tblFlightBook.FlightDate = DateTime.Now;

            context.TblFlightBooks.Add(tblFlightBook);
            context.SaveChanges();
            TblFlightBook objRow = context.TblFlightBooks.Where(a => a.Id.Equals(tblFlightBook.Id)).FirstOrDefault();
            return objRow;
        }
        public TblFlightBook CancelTicket(string pNRNumber)
        {
            //TblFlightBook objInventory = new TblFlightBook();
            var tblFlightBook = context.TblFlightBooks.Where(a => a.PnrNumber.Equals(Convert.ToInt32(pNRNumber))).FirstOrDefault();
            //var hours = (tblFlightBook.FlightDate - DateTime.Now).Hours;
           // if(hours => 24)
            //{
           // }
            tblFlightBook.ActiveIND = false;
            context.SaveChanges();
            return tblFlightBook;
        }
        public IEnumerable<TblFlightBook> HistoryTickets(int UserId)
        {
            try
            {
                List<TblFlightBook> tblFlightBook = new List<TblFlightBook>();
                tblFlightBook = context.TblFlightBooks
                    .Where(x=>x.UserId.Equals(UserId) 
                    && x.ActiveIND.Equals(true)).ToList();
                return tblFlightBook;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
