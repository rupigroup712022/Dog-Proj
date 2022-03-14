using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOGS1.Models
{
    public class Time
    {
        DateTime availableDays;
        DateTime availableHours;

        public Time(DateTime availableDays, DateTime availableHours)
        {
            AvailableDays = availableDays;
            AvailableHours = availableHours;
        }

        public DateTime AvailableDays { get => availableDays; set => availableDays = value; }
        public DateTime AvailableHours { get => availableHours; set => availableHours = value; }
    }
}