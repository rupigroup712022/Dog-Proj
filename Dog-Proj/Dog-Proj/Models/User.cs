using Dog_Proj.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOGS1.Models
{
    public class User
    {
        int id;
        string username;
        int phone;
        string sex;
        int age;
        DateTime ending_date;
        Dictionary <string,string[]> availablity;
        int familyId; 
        public User(int id, string username, int phone, string sex, int age, Dictionary<string,string[]> availablity)
        {
            Id = id;
            Username = username;
            Phone = phone;
            Sex = sex;
            Age = age;
            Availablity = availablity;
            FamilyId = familyId;
        }

        public User(int id, string username, int phone, string sex, int age,int familyId)
        {
            Id = id;
            Username = username;
            Phone = phone;
            Sex = sex;
            Age = age;
            FamilyId = familyId;
        }

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public int Phone { get => phone; set => phone = value; }
        public string Sex { get => sex; set => sex = value; }
        public int Age { get => age; set => age = value; }
        public Dictionary<string, string[]> Availablity { get => availablity; set => availablity = value; }
        public DateTime Ending_date { get => ending_date; set => ending_date = value; }
        public int FamilyId { get => familyId; set => familyId = value; }



        public int Insert()
        {
            DataServices ds = new DataServices();
            return ds.Insert(this);
        }

    }
}