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

            if (Convert.ToInt32(dic["numOfPoints"]) >= Convert.ToInt32(Servicetype))
            {
                return dbs.InsertServices(userId, this);
            }

            else
            {
                throw new Exception("אין מספיק נקודות זכות");
            }
        }

       public bool InsertReqServices(int idService, int idUser,int reqUser, int servicetype)
        {
            ///idUser נותן השירות
          ///  reqUser מבקש השירות
            DataServices dbs = new DataServices();
            Dictionary<string, string> dic = dbs.getPoints(reqUser);
            if (Convert.ToInt32(dic["numOfPoints"]) >= Convert.ToInt32(servicetype))
            {
                dbs.setPointsWithCheck(Convert.ToInt32(dic["id"]), Convert.ToInt32(dic["numOfPoints"]) - Convert.ToInt32(servicetype), idService);
                string str = dbs.InsertReqServices(idService, idUser);


                if (str.Length > 0)
                {
                  
                        var smtpClient = new SmtpClient();
                    //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                    MailMessage mailMessage = new MailMessage() { 
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
                                             height: 150px;
                                             width: 500px;
                                      }

                             </style>
                              <body>
                        <div>
                             <div><img src='https://www.pexels.com/search/cute%20dogs/' /></div>
                             <div><h1 dir = 'rtl' > התקבלה בקשה חדשה!</h1></div>
                             <div><p dir = 'rtl' > כנס לאתר לצפות בפרטי הבקשה</p></div>
                             <div ><a class='rtl' href='index.html'>כניסה לאתר</a></div>
                             <div><p dir = 'rtl' > אל תשכח לתת שירות בשביל לקבל!</p></div>
                              <div><p dir = 'rtl' > תמיד בשבילך ולעזרתך חברי PETCOM!</p></div>
                         </div>
                  </body>
                  </html>",
                     IsBodyHtml = true
                };
                    mailMessage.To.Add(new MailAddress(str));
                    //mailMessage.Subject = "PETCOM: עדכון חדש";
                    //mailMessage.Body =mailMessage.Body
                    
                    smtpClient.Send(mailMessage);
                        return true;
                    
                  

                }
            }

            else
            {
                throw new Exception("אין מספיק נקודות זכות");
            }


            return false;
        }


        public void setRating(short service_id, short rating,short handlerId,int type)
        {
            DataServices dbs = new DataServices();
            Dictionary<string, string> dic= dbs.setRating(service_id, rating,handlerId);
            calculate(Convert.ToDouble(dic["avg"]), Convert.ToInt32(dic["rating_number"]), rating, Convert.ToInt16(dic["id"]), type,
                Convert.ToInt32(dic["numOfPoints"])) ;
        }


        public void calculate (double average, int rating_number, short rating, short familyId,int new_points,int prev_points)
        {
            double new_avg = (average + rating) / (rating_number + 1);
            DataServices dbs = new DataServices();
            int points = prev_points + new_points;
            dbs.setAvgRating(new_avg, rating_number+1, familyId, points);
            
        }

    }
}