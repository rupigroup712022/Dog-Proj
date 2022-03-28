using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOGS1.Models
{
    public class Availablity
    {
        int id;
        DateTime availableDays;
        DateTime availableHours;

        public Availablity(int id,DateTime availableDays, DateTime availableHours)
        {

            AvailableDays = availableDays;
            AvailableHours = availableHours;
        }

        public DateTime AvailableDays { get => availableDays; set => availableDays = value; }
        public DateTime AvailableHours { get => availableHours; set => availableHours = value; }
    }
}