using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Contact
    {
        public int ContactID { get; set; }

        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
