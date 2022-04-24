using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dog_Proj.Models.DAL;

namespace Dog_Proj.Models {


    public class User
    {
        string username;
        string phone;
        string sex;
        int age;
        DateTime ending_date;
        Dictionary<string, string[]> availablity;
        int familyId;
        public User() { }
        public User(string username, string phone, string sex, int age, int familyId)
        {
            this.username = username;
            this.phone = phone;
            this.sex = sex;
            this.age = age;
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

        public List<List<string>> GetAvUser(string day, string hour, int userid)
        {
            DataServices dbs = new DataServices();
            return dbs.GetAvUser(day, hour,userid);
        }

        public List<List<string>> getIncomeApprovedRequests(int userid)
        {
            DataServices dbs = new DataServices();
            return dbs.getIncomeApprovedRequests(userid);

        }

        public List<List<string>> getIncomePendingRequests(int userid)
        {
            DataServices dbs = new DataServices();
            return dbs.getIncomePendingRequests(userid);

        }

        public void setRequests(int userid,string serviceId,bool val)
        {
            DataServices dbs = new DataServices();
             dbs.setRequestsDb(userid,serviceId,val);

        }

    }
}