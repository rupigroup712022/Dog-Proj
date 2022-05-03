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
        int UserId;
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
        public int UserId1 { get => UserId; set => UserId = value; }
        public int FamilyId { get => familyId; set => familyId = value; }

        public int InsertServices(int UserId)
        {
            DataServices ds = new DataServices();
            return ds.InsertServices( UserId, this);

        }
        

       public void InsertReqServices(int idService, int idUser)
        {
            DataServices ds = new DataServices();
            string str= ds.InsertReqServices(idService, idUser);

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


        public int setRating(short service_id, short rating,short handlerId)
        {
            DataServices dbs = new DataServices();
            return dbs.setRating(service_id, rating,handlerId);
            
        }


    }
}