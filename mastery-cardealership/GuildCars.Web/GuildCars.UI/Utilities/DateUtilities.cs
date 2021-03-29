using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Utilities
{
    public class DateUtilities
    {
        public static string FormatDate(DateTime date)
        {
            return date.ToString("MM / dd / yyyy");
        }
    }
}