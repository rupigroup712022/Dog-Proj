using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dog_Proj.Models.DAL;

namespace Dog_Proj.Models
{
    public class Account
    {
        int id;
        string familyname;
        bool moreAnimals;
        string street;
        int homeNum;
        int linkedUsers;
        int numOfPoints;
        string email;
        string yardSize;
        string houseType;
        float avgPoint;
        string passwords;
        string cityName;


        public Account(string familyname, bool moreAnimals, string street, int homeNum,string email, string yardSize, string houseType, string passwords, string cityName)
        {
            
            Familyname = familyname;
            MoreAnimals = moreAnimals;
            Street = street;
            HomeNum = homeNum;
            Email = email;
            YardSize = yardSize;
            HouseType = houseType;
            Passwords = passwords;
            CityName = cityName;
            NumOfPoints = 15;
            AvgPoint = 0;

        }

        public int Id { get => id; set => id = value; }
        public string Familyname { get => familyname; set => familyname = value; }
        public bool MoreAnimals { get => moreAnimals; set => moreAnimals = value; }
        public string Street { get => street; set => street = value; }
        public int HomeNum { get => homeNum; set => homeNum = value; }
        public int LinkedUsers { get => linkedUsers; set => linkedUsers = value; }
        public int NumOfPoints { get => numOfPoints; set => numOfPoints = value; }
        public string Email { get => email; set => email = value; }
        public string YardSize { get => yardSize; set => yardSize = value; }
        public string HouseType { get => houseType; set => houseType = value; }
        public float AvgPoint { get => avgPoint; set => avgPoint = value; }
        public string Passwords { get => passwords; set => passwords = value; }
        public string CityName { get => cityName; set => cityName = value; }



        public Account() { }

        public int Insert()
        {
            DataServices ds = new DataServices();
            return ds.Insert(this);
        }

        public Account ReadAccount(string Email, string Password)
        {
            DataServices dbs = new DataServices();
            return dbs.ReadAccount(Email, Password);
        }

        public List<string> GetUserSelction(int Id)
        {
            DataServices dbs = new DataServices();
            return dbs.GetUserSelction(Id);
        }



    }
}