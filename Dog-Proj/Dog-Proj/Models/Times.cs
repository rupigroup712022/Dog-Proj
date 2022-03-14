using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOGS1.Models
{
    public class Times
    {
        DateTime availableDays;
        DateTime availableHours;

        public Times(DateTime availableDays, DateTime availableHours)
        {
            AvailableDays = availableDays;
            AvailableHours = availableHours;
        }

        public DateTime AvailableDays { get => availableDays; set => availableDays = value; }
        public DateTime AvailableHours { get => availableHours; set => availableHours = value; }
    }
}