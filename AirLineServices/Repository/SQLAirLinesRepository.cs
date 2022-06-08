using AirLineServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineServices.Repository
{
    public class SQLAirLinesRepository: IAirLinesRepository
    {
        private readonly AirLinesDBContext context;
        public SQLAirLinesRepository(AirLinesDBContext context)
        {
            this.context = context;
        }
        public TblAirLine AddTblAirLine(TblAirLine tblAirLine)
        {
            context.TblAirLines.Add(tblAirLine);
            context.SaveChanges();
            return tblAirLine;
        }
        public TblAirLine DeleteTblAirLine(string airLineNumber)
        {
            TblAirLine tblAirLine = context.TblAirLines.Where(a => a.AirLineNumber.Equals(airLineNumber)).FirstOrDefault();
            if (tblAirLine != null)
            {
                tblAirLine.ActiveInd = false;
                //context.TblAirLines.Remove(tblAirLine);
                context.SaveChanges();
            }
            return tblAirLine;
        }
        public IEnumerable<TblAirLine> GetAllTblAirLines()
        {
            List<TblAirLine> objAirLine = new List<TblAirLine>();
            objAirLine = context.TblAirLines.ToList();
            return objAirLine;
        }
        
        public TblAirLine GetAirLineByNumber(string airLineNumber)
        {
            TblAirLine objAirLine = new TblAirLine();
            objAirLine = context.TblAirLines.Where(a=>a.AirLineNumber.Equals(airLineNumber)).FirstOrDefault();
            //if (objAirLine != null)
           // {
            //    return objAirLine;
           // }
            return objAirLine;
        }

        public TblAirLine UpdateTblAirLine(TblAirLine tblAirLineChanges)
        {
            var tblAirLine = context.TblAirLines.Attach(tblAirLineChanges);
            tblAirLine.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return tblAirLineChanges;
        }
    }
}
