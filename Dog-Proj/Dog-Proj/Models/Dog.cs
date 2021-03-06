using Dog_Proj.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dog_Proj.Models
{
    public class Dog
    {
       
        string dogname;
        int familyNum;
        string dogBreed;
        int age;
        string size;
        string sex;
        bool neutering;
        string dog_character;
        string picture;

        public Dog(string dogname, int familyNum, string dogBreed, int age, string size, string sex, bool neutering, string dog_character ,string picture)
        {
            this.dogname = dogname;
            this.familyNum = familyNum;
            this.dogBreed = dogBreed;
            this.age = age;
            this.size = size;
            this.sex = sex;
            this.neutering = neutering;
            this.dog_character = dog_character;
            this.picture = picture;
        }

        public string Dogname { get => dogname; set => dogname = value; }
        public int FamilyNum { get => familyNum; set => familyNum = value; }
        public string DogBreed { get => dogBreed; set => dogBreed = value; }
        public int Age { get => age; set => age = value; }
        public string Size { get => size; set => size = value; }
        public string Sex { get => sex; set => sex = value; }
        public bool Neutering { get => neutering; set => neutering = value; }
        public string Dog_character { get => dog_character; set => dog_character = value; }
        public string Picture { get => picture; set => picture = value; }

        public int InsertDog()
        {
            DataServices ds = new DataServices();
            return ds.InsertDog(this);
        }




        }
    }