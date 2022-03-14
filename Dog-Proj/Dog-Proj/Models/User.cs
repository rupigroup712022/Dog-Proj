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
        int available;
        DateTime ending_date;

        public User(int id, string username, int phone, string sex, int age, int available, DateTime ending_date)
        {
            Id = id;
            Username = username;
            Phone = phone;
            Sex = sex;
            Age = age;
            Available = available;
            Ending_date = ending_date;
        }

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public int Phone { get => phone; set => phone = value; }
        public string Sex { get => sex; set => sex = value; }
        public int Age { get => age; set => age = value; }
        public int Available { get => available; set => available = value; }
        public DateTime Ending_date { get => ending_date; set => ending_date = value; }
    }
}