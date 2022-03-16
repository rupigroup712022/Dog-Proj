using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOGS1.Models
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

        public Account(int id, string familyname, bool moreAnimals, string street, int homeNum, int linkedUsers, int numOfPoints, string email, string yardSize, string houseType, float avgPoint)
        {
            Id = id;
            Familyname = familyname;
            MoreAnimals = moreAnimals;
            Street = street;
            HomeNum = homeNum;
            LinkedUsers = linkedUsers;
            NumOfPoints = numOfPoints;
            Email = email;
            YardSize = yardSize;
            HouseType = houseType;
            AvgPoint = avgPoint;
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



        public Account() { }
    }
}