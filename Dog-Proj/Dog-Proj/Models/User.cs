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
        Dictionary <string,string[]> avalability;
        public User(int id, string username, int phone, string sex, int age, DateTime ending_date, Dictionary<string,string[]> avalability)
        {
            Id = id;
            Username = username;
            Phone = phone;
            Sex = sex;
            Age = age;
            Availablity = avalability;
            Ending_date = ending_date;
        }

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public int Phone { get => phone; set => phone = value; }
        public string Sex { get => sex; set => sex = value; }
        public int Age { get => age; set => age = value; }
        public Dictionary<string, string[]> Availablity { get => avalability; set => avalability = value; }
        public DateTime Ending_date { get => ending_date; set => ending_date = value; }



        public int Insert()
        {
            DataServices ds = new DataServices();
            return ds.Insert(this);
        }

    }
}