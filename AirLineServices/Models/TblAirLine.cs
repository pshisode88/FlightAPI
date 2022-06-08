using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineServices.Models
{
    public class TblAirLine
    {
        [Key]
        public int Id { get; set; }
        public string AirLineNumber { get; set; }
        public string AirLineName { get; set; }

        public string ContactNumber { get; set; }

        public bool ActiveInd { get; set; }

    }
    public class ApplicationUser : IdentityUser
    {

    }
}
