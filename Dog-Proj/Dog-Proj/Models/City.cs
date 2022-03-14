using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOGS1.Models
{
    public class City
    {
        string cityName;
        string Street;
        public City(string cityName)
        {
            CityName = cityName;
        }

        public string CityName { get => cityName; set => cityName = value; }
    }
}