using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dog_Proj.Models
{
    public class Pricing
    {
        string typeOfService;
        int price;

        public Pricing(string typeOfService, int price)
        {
            TypeOfService = typeOfService;
            Price = price;
        }

        public string TypeOfService { get => typeOfService; set => typeOfService = value; }
        public int Price { get => price; set => price = value; }
    }
}