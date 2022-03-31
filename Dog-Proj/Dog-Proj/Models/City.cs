using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dog_Proj.Models
{
    public class City
    {
        string cityName;
   
        public City(string cityName)
        {
            CityName = cityName;
        }

        public string CityName { get => cityName; set => cityName = value; }
        public City() { }

    }
}