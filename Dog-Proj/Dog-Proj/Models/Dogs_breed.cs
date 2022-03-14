using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOGS1.Models
{
    public class Dogs_breed
    {
        string breedName;

        public Dogs_breed(string breedName)
        {
            BreedName = breedName;
        }

        public string BreedName { get => breedName; set => breedName = value; }
    }
}