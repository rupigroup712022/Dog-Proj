using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dog_Proj.Models.DAL;
using System.Net.Mail;


namespace Dog_Proj.Models
{
    public class Service
    {
        string serviceName;
        string serviceDate;
        string serviceDay;
        string serviceHour;
        string note;
        string servicetype;
        string quantity;
        int userId;
        int familyId;

        public Service() { }
        public Service(string serviceName, string serviceDate, string note, string servicetype, string quantity, int userId, int familyId)
        {
            this.serviceName = serviceName;
            this.serviceDate = serviceDate;
            this.note = note;
            this.servicetype = servicetype;
            this.quantity = quantity;
            UserId = userId;
            this.familyId = familyId;
        }

        public Service(string serviceName, string serviceDate, string serviceDay, string serviceHour, string note, string servicetype, string quantity, int userId, int familyId)
        {
            this.serviceName = serviceName;
            this.serviceDate = serviceDate;
            this.serviceDate = serviceDay;
            this.serviceDate = serviceHour;
            this.note = note;
            this.servicetype = servicetype;
            this.quantity = quantity;
            UserId = userId;
            this.familyId = familyId;
        }

    
        public string ServiceName { get => serviceName; set => serviceName = value; }
        public string ServiceDate { get => serviceDate; set => serviceDate = value; }
        public string ServiceDay { get => serviceDay; set => serviceDay = value; }
        public string ServiceHour { get => serviceHour; set => serviceHour = value; }

        public string Note { get => note; set => note = value; }
        public string Servicetype { get => servicetype; set => servicetype = value; }
        public string Quantity { get => quantity; set => quantity = value; }
        public int UserId { get => userId; set => userId = value; }
        public int FamilyId { get => familyId; set => familyId = value; }

        public int InsertServices(int userId)
        {

            DataServices dbs = new DataServices();
            Dictionary<string, string> dic = dbs.getPoints(UserId);

            var smtpClient = new SmtpClient();

            //smtpClient.Send("email", "recipient", "subject", "body");

            if (Convert.ToInt32(dic["numOfPoints"]) >= Convert.ToInt32(Servicetype))
            {
                dbs.setPoints(Convert.ToInt32(dic["id"]), Convert.ToInt32(dic["numOfPoints"]) - Convert.ToInt32(Servicetype));
                return dbs.InsertServices(userId, this);
            }

            else
            {
                throw new Exception("אין מספיק נקודות זכות");
                ///לטפל במקרה קצה של 3 בקשות
            }
        }

       public void InsertReqServices(int idService, int idUser)
        {
            ///idUser נותן השירות
            DataServices ds = new DataServices();
     
                string str = ds.InsertReqServices(idService, idUser);
            Console.WriteLine(str);
            try
            {
                var smtpClient = new SmtpClient();

                var mailMessage = new MailMessage
                {
                    Subject = "התקבלה בקשה חדשה:",
                    Body = "<h1>Hello</h1> </br><p>היכנס לאתר לצפייה בפרטי הבקשה</p>",
                    IsBodyHtml = true
                };
                mailMessage.To.Add(str);

                smtpClient.Send(mailMessage);
            }
            catch (Exception)
            {
                Console.WriteLine(
                    "auth error");
            }

            
        }


        public void setRating(short service_id, short rating,short handlerId,int type)
        {
            DataServices dbs = new DataServices();
            Dictionary<string, string> dic= dbs.setRating(service_id, rating,handlerId);
            calculate(Convert.ToDouble(dic["avg"]), Convert.ToInt32(dic["rating_number"]), rating, Convert.ToInt16(dic["id"]), type,
                Convert.ToInt32(dic["numOfPoints"])) ;
        }


        public void calculate (double average, int rating_number, short rating, short familyId,int new_points,int prev_points)
            //צריך לטפל בחריגה של החזת נקודות בעת ביטול בקשה 
        {
            double new_avg = (average + rating) / (rating_number + 1);
            DataServices dbs = new DataServices();
            int points = prev_points + new_points;
            dbs.setAvgRating(new_avg, rating_number+1, familyId, points);
            
        }

    }
}