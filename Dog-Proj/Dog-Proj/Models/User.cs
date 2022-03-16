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
        DateTime availableDays;
        DateTime availableHours;
        DateTime ending_date;

        public User(int id, string username, int phone, string sex, int age, DateTime availableDays, DateTime availableHours, DateTime ending_date)
        {
            Id = id;
            Username = username;
            Phone = phone;
            Sex = sex;
            Age = age;
            AvailableDays = availableDays;
            AvailableHours = availableHours;
            Ending_date = ending_date;
        }

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public int Phone { get => phone; set => phone = value; }
        public string Sex { get => sex; set => sex = value; }
        public int Age { get => age; set => age = value; }
        public DateTime AvailableDays { get => availableDays; set => availableDays = value; }
        public DateTime AvailableHours { get => availableHours; set => availableHours = value; }
        public DateTime Ending_date { get => ending_date; set => ending_date = value; }

        public int Insert()
        {
            DataServices ds = new DataServices();
            return ds.Insert(this);
        }

    }
}