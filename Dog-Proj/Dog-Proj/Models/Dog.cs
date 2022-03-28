using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dog_Proj.Models.DAL;

namespace DOGS1.Models
{
    public class Dog
    {
        int id;
        string picture;
        string dogname;
        int familyNum;
        string dogBreed;
        int age;
        string size;
        string sex;
        bool neutering;
        string dog_character;

    
    

        public Dog() { }

        public Dog(int id, string picture, string dogname, int familyNum, string dogBreed, int age, string size, string sex, bool neutering, string dog_character)
        {
            Id = id;
            Picture = picture;
            Dogname = dogname;
            FamilyNum = familyNum;
            DogBreed = dogBreed;
            Age = age;
            Size = size;
            Sex = sex;
            Neutering = neutering;
            Dog_character = dog_character;
        }

        public int Id { get => id; set => id = value; }
        public string Picture { get => picture; set => picture = value; }
        public string Dogname { get => dogname; set => dogname = value; }
        public int FamilyNum { get => familyNum; set => familyNum = value; }
        public string DogBreed { get => dogBreed; set => dogBreed = value; }
        public int Age { get => age; set => age = value; }
        public string Size { get => size; set => size = value; }
        public string Sex { get => sex; set => sex = value; }
        public bool Neutering { get => neutering; set => neutering = value; }

        public string Dog_character  { get => dog_character; set => dog_character = value; }

        public int Insert()
        {
            DataServices ds = new DataServices();
            return ds.Insert(this);
        }


    }
}