using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineServices.Models
{
    public interface IAirLinesRepository
    {
        TblAirLine GetAirLineByNumber(string airLineNumber);
        IEnumerable<TblAirLine> GetAllTblAirLines();
        TblAirLine AddTblAirLine(TblAirLine tblAirLine);
        TblAirLine UpdateTblAirLine(TblAirLine tblAirLineChanges);
        TblAirLine DeleteTblAirLine(string airLineNumber);
    }
}
