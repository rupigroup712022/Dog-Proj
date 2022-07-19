using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dog_Proj.Models.DAL;
using System.Net.Mail;

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

        public List<List<string>> GetAvUser(string day, string hour, int userid,short serviceId)
        {
            DataServices dbs = new DataServices();
            return dbs.GetAvUser(day, hour,userid, serviceId);
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

        public int CountRequestsApro(int userid)
        {
            DataServices dbs = new DataServices();
            return dbs.CountRequestsApro(userid);

        }
        public List<List<string>> getRequestHistory(int userid)
        {
            DataServices dbs = new DataServices();
            return dbs.getRequestHistory(userid);

        }

        public List<List<string>> getWaitResponse(int userid)
        {
            DataServices dbs = new DataServices();
            return dbs.getWaitResponse(userid);

        }

        public int CountWaitResponse(int userid)
        {
            DataServices dbs = new DataServices();
            return dbs.CountWaitResponse(userid);

        }
        

           public List<List<string>> getWaitApproval(int userid)
        {
            DataServices dbs = new DataServices();
            return dbs.getWaitApproval(userid);

        }


        public void setRequests(int userid,string serviceId,bool val,string type)
        {
            DataServices dbs = new DataServices();
           
            var smtpClient = new SmtpClient();

            //smtpClient.Send("email", "recipient", "subject", "body");
            List <string> str = dbs.setRequestsDb(userid, serviceId, val);
            try
            {
                if (val) 
                {

                    MailMessage mailMessage = new MailMessage()
                    {
                        Subject = "PETCOM: עדכון חדש",
                        Body = @"<html>
                                <style>
                                          p {
                                              font-family:Calibri;
                                              font-size: 40px;
                                              text-align: center;
                                            color:#000000;
                                           }
                                     body {
                                            background-color: #fad58c;
                                            float:right;
                                            text-align:center;
                                            color:#000000;
                                          }
                                     h1 {
                                             font-family: Calibri;
                                             font-size: 60px;
                                             text-align: center;
                                            color:#000000;
                                          }
                                   img{
                                             height: 10%;
                                             width: 50%;
                                      }

                             </style>
                              <body>
                                                
                           <div>  <a href='https://imgbb.com/'><img src='https://i.ibb.co/PZV5KXz/33.jpg' alt='33' border='0'></a><div>
                            <div> <h1 dir = 'rtl' > התקבלה בקשה חדשה!</h1></div>
                             <div><P dir = 'rtl' > הבקשה אושרה, היכנס לאתר לצפייה בפרטי הבקשה</P></div>
                             <div ><a class='rtl' href='https://proj.ruppin.ac.il/igroup71/prod/Pages/index.html'>כניסה לאתר</a></div>
                             <div><P dir = 'rtl' > אל תשכח לתת שירות בשביל לקבל!</P></div>
                              <div><P dir = 'rtl' > תמיד בשבילך ולעזרתך חברי PETCOM!</P></div>
                         
                  </body>
                  </html>",
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(str[0]);

                    smtpClient.Send(mailMessage);


                }
                else
                {
                    if (str[1].Length > 0 && str[2].Length > 0)
                    {
                        dbs.setPoints(Convert.ToInt32(str[2]), Convert.ToInt32(str[1]) + Convert.ToInt32(type));
                    }
                    MailMessage mailMessage = new MailMessage()
                    {
                        Subject = "PETCOM: עדכון חדש",
                        Body = @"<html>
                                <style>
                                          p {
                                              font-family:Calibri;
                                              font-size: 40px;
                                              text-align: center;
                                            color:#000000;
                                           }
                                     body {
                                            background-color: #fad58c;
                                            float:right;
                                            text-align:center;
                                            color:#000000;
                                          }
                                     h1 {
                                             font-family: Calibri;
                                             font-size: 60px;
                                             text-align: center;
                                            color:#000000;
                                          }
                                   img{
                                             height: 10%;
                                             width: 50%;
                                      }

                             </style>
                              <body>
                       
                           <div><a href='https://imgbb.com/'><img src='https://i.ibb.co/PZV5KXz/33.jpg' alt='33' border='0'></a></div>
                              <h1 dir = 'rtl' > התקבלה בקשה חדשה!</h1></div>
                             <div><P dir = 'rtl' > הבקשה נדחתה, היכנס לאתר לצפייה בפרטי הבקשה</P></div>
                             <div><a class='rtl' href='https://proj.ruppin.ac.il/igroup71/prod/Pages/index.html'>כניסה לאתר</a></div>
                             <div><P dir = 'rtl' > אל תשכח לתת שירות בשביל לקבל!</P></div>
                              <div><P dir = 'rtl' > תמיד בשבילך ולעזרתך חברי PETCOM!</P></div>
                         
                  </body>
                  </html>",
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(str[0]);

                   smtpClient.Send(mailMessage);// להחזיר נקודות
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            

        }



        public void setSelfCensel(int userid, string serviceId, string type)
        {
            DataServices dbs = new DataServices();
            List<string> str = dbs.setRequestsDb(userid, serviceId, false);
                    if (str[1].Length > 0 && str[2].Length > 0)
                    {
                        dbs.setPoints(Convert.ToInt32(str[2]), Convert.ToInt32(str[1]) + Convert.ToInt32(type));
                    }
                   
        }
         


        
        public List<List<string>> GetAvUserPension(int userid, short serviceId)
        {
            DataServices dbs = new DataServices();
            return dbs.GetAvUserPension(userid, serviceId);
        }
        


    }
}