using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dog_Proj.Models.DAL;

namespace DOGS1.Models {


    public class User
    {
        string username;
        string phone;
        string sex;
        int age;
        DateTime ending_date;
        Dictionary<string, string[]> availablity;
        int familyId;

        public User(string username, string phone, string sex, int age, int familyId)
        {
            this.username = username;
            this.phone = phone;
            this.sex = sex;
            this.age = age;
            //    this.ending_date = ending_date;
            //    this.availablity = availablity;
            this.familyId = familyId;
        }


        //public User(string username, string phone, string sex, int age, DateTime ending_date, Dictionary<string, string[]> availablity, int familyId)
        //{
        //    this.username = username;
        //    this.phone = phone;
        //    this.sex = sex;
        //    this.age = age;
        //    this.ending_date = ending_date;
        //    this.availablity = availablity;
        //    this.familyId = familyId;
        //}

        public string Username { get => username; set => username = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Sex { get => sex; set => sex = value; }
        public int Age { get => age; set => age = value; }
        public DateTime Ending_date { get => ending_date; set => ending_date = value; }
        public Dictionary<string, string[]> Availablity { get => availablity; set => availablity = value; }
        public int FamilyId { get => familyId; set => familyId = value; }





        public int InsertUser()
        {
            DataServices ds = new DataServices();
            return ds.InsertUser(this);

        }
    } }