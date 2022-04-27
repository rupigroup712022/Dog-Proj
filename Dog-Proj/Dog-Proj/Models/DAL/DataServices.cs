using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Dog_Proj.Models;

namespace Dog_Proj.Models.DAL
{
    public class DataServices
    {

        public int InsertUser(User user)
        {
            int existName = checking(user.Username, user.FamilyId);
            if (existName == 0)
            {
                SqlConnection con = null;
                int id = 0;

                try
                {
                    //C - Connect to the Database
                    con = Connect("DogsProjDB");

                    //C Create the Insert SqlCommand
                    SqlCommand insertCommand = CreateInsertCommandUser(con, user);

                    //E Execute
                      var numeffected = insertCommand.ExecuteNonQuery();
                     id = checking(user.Username, user.FamilyId);
                    foreach (var day in user.Availablity)//הכנסה למילון
                    {
                        foreach (var hour in day.Value)
                        {
                            SqlCommand insertCommandHours = CreateInsertCommand(con, id, day.Key, hour);//הכנסה טבלת זמינות
                            numeffected += insertCommandHours.ExecuteNonQuery();
                        }

                    }
                }

                catch (Exception exep)
                {
                    // this code needs to write the error to a log file
                    throw new Exception("Exeption", exep);
                }

                finally
                {
                    //C Close Connction
                    if (con != null)
                        con.Close();
                }


                // num effected
                return id;
            }

            return 0;
        }



        public int checking(string username,int familyId)
        {
            SqlConnection con = null;
            try
            {
                // Connect
                con = Connect("DogsProjDB");
                // Create the insert command
                SqlCommand selectCommand = NameCheck(con, username, familyId);
                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                int str = 0;
                while (dataReader.Read())
                {

                    str += Convert.ToInt32(dataReader["id"]);
                    
                }
                dataReader.Close();

                if (str == 0)
                {
                    return 0;
                }
                return str;
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in username", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }
        }




        private SqlCommand NameCheck(SqlConnection con, string username, int familyId)
        {
            string str = "SELECT id FROM UsersFamliy WHERE username LIKE @username and familyId = @familyId";
            SqlCommand cmd = createCommand(con, str);
            cmd.Parameters.Add("@username", SqlDbType.NChar);
            cmd.Parameters["@username"].Value = username;
            cmd.Parameters.Add("@familyId", SqlDbType.SmallInt);
            cmd.Parameters["@familyId"].Value = (short)familyId;
            return cmd;
        }

        private SqlCommand CreateInsertCommandUser(SqlConnection con, User user)
        {
         
            string commandStr = "INSERT INTO UsersFamliy (username,phone,sex,age,familyId) VALUES (@username,@phone,@sex,@age,@familyId)";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@username", SqlDbType.NChar);
            cmd.Parameters["@username"].Value = user.Username;
            cmd.Parameters.Add("@phone", SqlDbType.Char);
            cmd.Parameters["@phone"].Value = user.Phone;
            cmd.Parameters.Add("@sex", SqlDbType.NChar);
            cmd.Parameters["@sex"].Value = user.Sex;
            cmd.Parameters.Add("@age", SqlDbType.Int);
            cmd.Parameters["@age"].Value = user.Age;
            cmd.Parameters.Add("@familyId", SqlDbType.Int);
            cmd.Parameters["@familyId"].Value = user.FamilyId;
           
            return cmd;
        }

        private SqlCommand CreateInsertCommand(SqlConnection con,int userId, string availableDays, string availableHours)
        {

            string commandStr = "INSERT INTO TimesAvailablity (userId,availableDays,availableHours) VALUES (@userId,@availableDays,@availableHours)";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@userId", SqlDbType.SmallInt);
            cmd.Parameters["@userId"].Value = userId;
            cmd.Parameters.Add("@availableDays", SqlDbType.NChar);
            cmd.Parameters["@availableDays"].Value = availableDays;
            cmd.Parameters.Add("@availableHours", SqlDbType.NChar);
            cmd.Parameters["@availableHours"].Value = availableHours;
        

            return cmd;
        }

        public string InsertReqServices(int idService, int idUser)
        {
                SqlConnection con = null;
                 int numeffected;

                try
                {
                    //C - Connect to the Database
                    con = Connect("DogsProjDB");

                    //C Create the Insert SqlCommand
                    SqlCommand insertCommand = CreateInsertServiceReq(con, idService, idUser);

                    //E Execute
                     numeffected = insertCommand.ExecuteNonQuery();
                SqlCommand insertCommandEmail = getEmail(idService.ToString(), con);
                SqlDataReader dataReader = insertCommandEmail.ExecuteReader(CommandBehavior.CloseConnection);
                
                string str = "";
                while (dataReader.Read())
                {

                    str = dataReader["email"].ToString();

                }
                dataReader.Close();

                if (str == "")
                {
                    throw new Exception("No email was found");
                }

                return str;

            }

            catch (Exception exep)
                {
                    // this code needs to write the error to a log file
                    throw new Exception("Exeption", exep);
                }

                finally
                {
                    //C Close Connction
                    if (con != null)
                        con.Close();
                }


            
        }
        private SqlCommand CreateInsertServiceReq(SqlConnection con, int idService, int idUser)
        {

            string commandStr = "INSERT INTO PendingReq (idService,idUser) VALUES (@idService,@idUser)";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@idService", SqlDbType.SmallInt);
            cmd.Parameters["@idService"].Value = (short)idService;
            cmd.Parameters.Add("@idUser", SqlDbType.SmallInt);
            cmd.Parameters["@idUser"].Value =(short)idUser;
            return cmd;
        }


        public int Insert(Account account)
        {
            int existEmail = checkingEmail(account.Email);
            if (existEmail == 0)
            {
                SqlConnection con = null;
                int id = 0;
                

                try
                {
                    //C - Connect to the Database
                    con = Connect("DogsProjDB");

                    //C Create the Insert SqlCommand
                    SqlCommand insertCommand = CreateInsertCommand(con, account);

                    //E Execute
                    //insertCommand.Parameters.Add("@id", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                    var num = insertCommand.ExecuteNonQuery();
                    id= checkingEmail(account.Email);

                }


                catch (Exception exep)
                {
                    // this code needs to write the error to a log file
                    throw new Exception("Exeption", exep);
                }

                finally
                {
                    //C Close Connction
                    if (con != null)
                        con.Close();
                }


                // num effected
                return id;
            }
            return 0;
        }

        public int checkingEmail(string email)
        {
            SqlConnection con = null;
            try
            {
                // Connect
                con = Connect("DogsProjDB");
                // Create the insert command
                SqlCommand selectCommand = emailCheck(con, email);
                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                int str = 0;
                while (dataReader.Read())
                {
                    //str = dataReader["id"].ToString();
                    str = Convert.ToInt32(dataReader["id"]);
                }
                dataReader.Close();
                if (str==0)
                {
                    return 0;
                }
                return str;
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in reading Email", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand emailCheck(SqlConnection con, string email)
        {
            string str = "SELECT id FROM Accounts WHERE email LIKE @email";
            SqlCommand cmd = createCommand(con, str);
            cmd.Parameters.Add("@email", SqlDbType.Char);
            cmd.Parameters["@email"].Value = email;
            return cmd;
        }

        public List<string> GetAddress(int familyId)
        {
            SqlConnection con = null;
            try
            {
                // Connect
                con = Connect("DogsProjDB");
                // Create the insert command
                SqlCommand selectCommand = AddressCheck(con, familyId);
                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                List<string> str = new List<string>();
                while (dataReader.Read())
                {
                    str.Add((string)dataReader["city"]);
                    str.Add((string)dataReader["street"]);
                    str.Add(dataReader["homeNum"].ToString());
                }
                dataReader.Close();
                if (str.Count == 0)
                {
                    return str;
                }
                return str;
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed in reading Address", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand AddressCheck(SqlConnection con, int id)
        {
            string str = "SELECT city,street,homeNum FROM Accounts WHERE id = @id";
            SqlCommand cmd = createCommand(con, str);
            cmd.Parameters.Add("@id", SqlDbType.SmallInt);
            cmd.Parameters["@id"].Value = (short)id;
            return cmd;
        }

        private SqlCommand CreateInsertCommand(SqlConnection con, Account account)
        {
           
            string commandStr = "INSERT INTO Accounts (familyname,moreAnimals,city,street,homeNum,linkedUsers,numOfPoints,email,yardSize,houseType,avgPoint,passwords) VALUES (@familyname,@moreAnimals,@city,@street,@homeNum,@linkedUsers,@numOfPoints,@email,@yardSize,@houseType,@avgPoint,@passwords)";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@familyname", SqlDbType.NChar);
            cmd.Parameters["@familyname"].Value = account.Familyname;
            cmd.Parameters.Add("@moreAnimals", SqlDbType.Bit);
            cmd.Parameters["@moreAnimals"].Value = account.MoreAnimals;
            cmd.Parameters.Add("@street", SqlDbType.NChar);
            cmd.Parameters["@street"].Value = account.Street;
            cmd.Parameters.Add("@homeNum", SqlDbType.Int);
            cmd.Parameters["@homeNum"].Value = account.HomeNum;
            cmd.Parameters.Add("@email", SqlDbType.NChar);
            cmd.Parameters["@email"].Value = account.Email;
            cmd.Parameters.Add("@yardSize", SqlDbType.NChar);
            cmd.Parameters["@yardSize"].Value = account.YardSize;
            cmd.Parameters.Add("@city", SqlDbType.NChar);
            cmd.Parameters["@city"].Value = account.CityName;
            cmd.Parameters.Add("@houseType", SqlDbType.NChar);
            cmd.Parameters["@houseType"].Value = account.HouseType;
            cmd.Parameters.Add("@passwords", SqlDbType.NChar);
            cmd.Parameters["@passwords"].Value = account.Passwords;
            cmd.Parameters.Add("@numOfPoints", SqlDbType.Int);
            cmd.Parameters["@numOfPoints"].Value = account.NumOfPoints;
            cmd.Parameters.Add("@linkedUsers", SqlDbType.TinyInt);
            cmd.Parameters["@linkedUsers"].Value = Convert.ToByte(account.LinkedUsers);
            cmd.Parameters.Add("@avgPoint", SqlDbType.Float);
            cmd.Parameters["@avgPoint"].Value = account.AvgPoint;
            return cmd;
        }

        public int InsertDog(Dog dog)
        {
            int existName = checkingDogname(dog.Dogname, dog.FamilyNum);
            if (existName == 0)
            {
                SqlConnection con = null;
                int numEffected = 0;

                try
                {
                    //C - Connect to the Database
                    con = Connect("DogsProjDB");

                    //C Create the Insert SqlCommand
                    SqlCommand insertCommand = CreateInsertCommandDog(con, dog);

                    //E Execute
                    numEffected = insertCommand.ExecuteNonQuery();
                }

                catch (Exception exep)
                {
                    // this code needs to write the error to a log file
                    throw new Exception("Exeption", exep);
                }

                finally
                {
                    //C Close Connction
                    if (con != null)
                        con.Close();
                }


                // num effected
                return numEffected;
            }
            return 0;
        }


        public int checkingDogname(string dogname, int familyNum)
        {
            SqlConnection con = null;
            try
            {
                // Connect
                con = Connect("DogsProjDB");
                // Create the insert command
                SqlCommand selectCommand = NameDogCheck(con, dogname, familyNum);
                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                string str = "";
                while (dataReader.Read())
                {
                    str = (string)dataReader["username"];
                }
                dataReader.Close();

                if (str == "")
                {
                    return 0;
                }
                return 1;
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed ", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand NameDogCheck(SqlConnection con, string dogname, int familyNum)
        {
            string str = "SELECT * FROM Dogs4 WHERE dogname LIKE @dogname and familyNum LIKE @familyNum";
            SqlCommand cmd = createCommand(con, str);
            cmd.Parameters.Add("@dogname", SqlDbType.Char);
            cmd.Parameters["@dogname"].Value = dogname;
            cmd.Parameters.Add("@familyNum", SqlDbType.SmallInt);
            cmd.Parameters["@familyNum"].Value = (short)familyNum;
            return cmd;
        }

        public List<string> GetUserSelction(int Id)
        {
            SqlConnection con = null;
            try
            {
                // Connect
                con = Connect("DogsProjDB");
                // Create the insert command
                SqlCommand selectCommand = UserSelction(con,Id);
                // Execute the command
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
               List <string> str=new List<string>() ;
                while (dataReader.Read())
                {
                    str.Add((string)dataReader["username"]);
                    str.Add((string)dataReader["sex"]);
                    str.Add(dataReader["id"].ToString());
                }
                dataReader.Close();

                return str;
            }
            catch (Exception ex)
            {
                // write the error to log
                throw new Exception("failed ", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }
        }

        private SqlCommand UserSelction(SqlConnection con, int familyId)
        {
            string str = "SELECT id,username,sex FROM UsersFamliy WHERE familyId LIKE @familyId";
            SqlCommand cmd = createCommand(con, str);
            cmd.Parameters.Add("@familyId", SqlDbType.SmallInt);
            cmd.Parameters["@familyId"].Value =(short)(familyId);
            return cmd;
        }
        private SqlCommand CreateInsertCommandDog(SqlConnection con, Dog dog)
        {
            string commandStr = "INSERT INTO Dogs4 (dogname,familyNum,dogBreed,age,size,sex,neutering,dog_character,picture) VALUES (@dogname,@familyNum,@dogBreed,@age,@size,@sex,@neutering,@dog_character,@picture)";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@picture", SqlDbType.NChar);
            cmd.Parameters["@picture"].Value = dog.Picture;
            cmd.Parameters.Add("@dogname", SqlDbType.NChar);
            cmd.Parameters["@dogname"].Value = dog.Dogname;
            cmd.Parameters.Add("@familyNum", SqlDbType.SmallInt); 
            cmd.Parameters["@familyNum"].Value = dog.FamilyNum;
            cmd.Parameters.Add("@dogBreed", SqlDbType.NChar);
            cmd.Parameters["@dogBreed"].Value = dog.DogBreed;
            cmd.Parameters.Add("@age", SqlDbType.Int);
            cmd.Parameters["@age"].Value = dog.Age;
            cmd.Parameters.Add("@size", SqlDbType.NChar);
            cmd.Parameters["@size"].Value = dog.Size;
            cmd.Parameters.Add("@sex", SqlDbType.NChar);
            cmd.Parameters["@sex"].Value = dog.Sex;
            cmd.Parameters.Add("@neutering", SqlDbType.Bit);
            cmd.Parameters["@neutering"].Value = dog.Neutering;
            cmd.Parameters.Add("@dog_character", SqlDbType.NChar);
            cmd.Parameters["@dog_character"].Value = dog.Dog_character;
            return cmd;
        }


        public Account ReadAccount(string email, string passwords)
        {
            SqlConnection con = null;
            try
            {
                con = Connect("DogsProjDB");
                SqlCommand selectCommand = emailandpasswordCheck(con, email, passwords);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                Account account = new Account();
                while (dataReader.Read())
                {
                    account.Id = Convert.ToInt32(dataReader["id"]);
                    account.Familyname = (string)dataReader["familyname"];
                    account.MoreAnimals = Convert.ToBoolean(dataReader["moreAnimals"]);
                    account.Street = (string)dataReader["street"];
                    account.HomeNum = Convert.ToInt32(dataReader["homeNum"]);
                    account.Email = (string)dataReader["email"];
                    account.YardSize = (string)dataReader["yardSize"];
                    account.HouseType = (string)dataReader["HouseType"];
                    account.Passwords = (string)dataReader["passwords"];
                }

                dataReader.Close();
                if (account.Email == null && account.Passwords == null)
                {
                    int existing = checkingEmail(email);
                    if (existing == 1)
                    {
                        account.Passwords = "The password is wrong!";
                        return account;
                    }
                    return null;
                }

                return account;
            }
            catch (Exception ex)
            {

                throw new Exception("failed", ex);
            }
            finally
            {

                if (con != null)
                    con.Close();
            }
        }


        private SqlCommand emailandpasswordCheck(SqlConnection con, string email, string passwords)
        {
            string str = "SELECT * FROM Accounts WHERE email LIKE @email and [passwords] LIKE @passwords";
            SqlCommand cmd = createCommand(con, str);
            cmd.Parameters.Add("@email", SqlDbType.Char);
            cmd.Parameters["@email"].Value = email;
            cmd.Parameters.Add("@passwords", SqlDbType.Char);
            cmd.Parameters["@passwords"].Value = passwords;
            return cmd;
        }


        public int InsertServices(int UserId, Service service)
        {
                SqlConnection con = null;
                 int numEffected = 0;
                try
                {
                    //C - Connect to the Database
                    con = Connect("DogsProjDB");

                //C Create the Insert SqlCommand
                if (service.ServiceName == "feeding")
                {
                    SqlCommand insertCommand = CreateInsertCommandservice(UserId, service, con);
                    numEffected = insertCommand.ExecuteNonQuery();

                }
                if (service.ServiceName == "pension")
                {
                    SqlCommand insertCommand = CreateInsertCommandservicepension(UserId, service, con);
                    numEffected = insertCommand.ExecuteNonQuery();

                }
                if (service.ServiceName == "walk")
                {
                    SqlCommand insertCommand = CreateInsertCommandservicewalk(UserId, service, con);
                    numEffected = insertCommand.ExecuteNonQuery();
                }
                SqlCommand getSerIdCommsnd = getSerIdCom(con, service.ServiceName, service.ServiceDate, UserId, service.ServiceHour);
                SqlDataReader dataReader = getSerIdCommsnd.ExecuteReader(CommandBehavior.CloseConnection);
                int i = 0;
                while (dataReader.Read())
                {
                    i = Convert.ToInt32(dataReader["id"].ToString());// ID של השירות 
                }

                dataReader.Close();

                return i;
          
                //E Execute
                }

                catch (Exception exep)
                {
                    // this code needs to write the error to a log file
                    throw new Exception("Error", exep);
                }

                finally
                {
                    //C Close Connction
                    con.Close();
                }


            }

        private SqlCommand getSerIdCom(SqlConnection con, string serviceName, string serviceDate, int UserId, string serviceHour)
        {
            string str = "SELECT id FROM ServicesDog WHERE serviceName LIKE @serviceName and serviceDate" +
                " LIKE @serviceDate and UserId LIKE @UserId and serviceHour LIKE @serviceHour ";
            SqlCommand cmd = createCommand(con, str);
            cmd.Parameters.Add("@serviceName", SqlDbType.NChar);
            cmd.Parameters["@serviceName"].Value = serviceName;
            cmd.Parameters.Add("@serviceDate", SqlDbType.NChar);
            cmd.Parameters["@serviceDate"].Value = serviceDate;
            cmd.Parameters.Add("@UserId", SqlDbType.Int);
            cmd.Parameters["@UserId"].Value = UserId;
            cmd.Parameters.Add("@serviceHour", SqlDbType.NChar);
            cmd.Parameters["@serviceHour"].Value = serviceHour;
            return cmd;
            }

            private SqlCommand CreateInsertCommandservicewalk(int UserId, Service service,SqlConnection con)
        {

            string commandStr = "INSERT INTO ServicesDog (serviceName,serviceDate,serviceDay,serviceHour,note,servicetype,UserId,familyId)" +
                " VALUES (@serviceName,@serviceDate,@serviceDay,@serviceHour,@note,@servicetype,@UserId,@familyId)";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@serviceName", SqlDbType.NChar);
            cmd.Parameters["@serviceName"].Value = service.ServiceName;
            cmd.Parameters.Add("@serviceDate", SqlDbType.NChar);
            cmd.Parameters["@serviceDate"].Value = service.ServiceDate;
            cmd.Parameters.Add("@serviceDay", SqlDbType.NChar);
            cmd.Parameters["@serviceDay"].Value = service.ServiceDay;
            cmd.Parameters.Add("@serviceHour", SqlDbType.NChar);
            cmd.Parameters["@serviceHour"].Value = service.ServiceHour;
            cmd.Parameters.Add("@note", SqlDbType.NChar);
            cmd.Parameters["@note"].Value = service.Note;
            cmd.Parameters.Add("@servicetype", SqlDbType.NChar);
            cmd.Parameters["@servicetype"].Value = service.Servicetype;
             cmd.Parameters.Add("@UserId", SqlDbType.SmallInt);
            cmd.Parameters["@UserId"].Value = UserId;
            cmd.Parameters.Add("@familyId", SqlDbType.SmallInt);
            cmd.Parameters["@familyId"].Value = service.FamilyId;
            return cmd;
        }
        private SqlCommand CreateInsertCommandservice(int UserId, Service service, SqlConnection con)
        {

            string commandStr = "INSERT INTO ServicesDog (serviceName,serviceDate,serviceDay,serviceHour,note,servicetype,quantity,UserId,familyId) VALUES" +
                " (@serviceName,@serviceDate,@serviceDay,@serviceHour,@note,@servicetype,@quantity,@UserId,@familyId)";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@serviceName", SqlDbType.NChar);
            cmd.Parameters["@serviceName"].Value = service.ServiceName;
            cmd.Parameters.Add("@serviceDate", SqlDbType.NChar);
            cmd.Parameters["@serviceDate"].Value = service.ServiceDate;
            cmd.Parameters.Add("@serviceDay", SqlDbType.NChar);
            cmd.Parameters["@serviceDay"].Value = service.ServiceDay;
            cmd.Parameters.Add("@serviceHour", SqlDbType.NChar);
            cmd.Parameters["@serviceHour"].Value = service.ServiceHour;
            cmd.Parameters.Add("@note", SqlDbType.NChar);
            cmd.Parameters["@note"].Value = service.Note;
            cmd.Parameters.Add("@servicetype", SqlDbType.NChar);
            cmd.Parameters["@servicetype"].Value = service.Servicetype;
            cmd.Parameters.Add("@quantity", SqlDbType.NChar);
            cmd.Parameters["@quantity"].Value = service.Quantity;
            cmd.Parameters.Add("@UserId", SqlDbType.SmallInt);
            cmd.Parameters["@UserId"].Value = UserId;
            cmd.Parameters.Add("@familyId", SqlDbType.SmallInt);
            cmd.Parameters["@familyId"].Value = service.FamilyId;
            return cmd;
        }
        private SqlCommand CreateInsertCommandservicepension(int UserId, Service service, SqlConnection con)
        {
            string commandStr = "INSERT INTO ServicesDog (serviceName,serviceDate,serviceHour,note,servicetype,UserId,familyId) " +
                "VALUES (@serviceName,@serviceDate,@serviceHour,@note,@servicetype,@UserId,@familyId)";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@serviceName", SqlDbType.NChar);
            cmd.Parameters["@serviceName"].Value = service.ServiceName;
            cmd.Parameters.Add("@serviceDate", SqlDbType.NChar);
            cmd.Parameters["@serviceDate"].Value = service.ServiceDate;
            cmd.Parameters.Add("@serviceHour", SqlDbType.NChar);
            cmd.Parameters["@serviceHour"].Value = service.ServiceHour;
            cmd.Parameters.Add("@note", SqlDbType.NChar);
            cmd.Parameters["@note"].Value = service.Note;
            cmd.Parameters.Add("@servicetype", SqlDbType.NChar);
            cmd.Parameters["@servicetype"].Value = service.Servicetype;
            cmd.Parameters.Add("@UserId", SqlDbType.SmallInt);
            cmd.Parameters["@UserId"].Value = UserId;
            cmd.Parameters.Add("@familyId", SqlDbType.SmallInt);
            cmd.Parameters["@familyId"].Value = service.FamilyId;
            return cmd;
        }

        public List<List<string>> GetAvUser(string day, string hour,int userid)
        {
            SqlConnection con = null;
            try
            {
                con = Connect("DogsProjDB");
                SqlCommand selectCommand = AvDayHourCheck(con, day, hour,userid);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                int i=0;
                List<List<string>>user = new List<List<string>>();
                while (dataReader.Read())
                {
                    user.Add(new List<string>());// המרה לסטרינגים הכנסה לרשימת של סטרינגים
                    user[i].Add((string)dataReader["username"]);
                    user[i].Add(Convert.ToInt32(dataReader["age"]).ToString());
                    user[i].Add((string)dataReader["phone"]);
                    user[i].Add((dataReader["avgPoint"]).ToString());
                    user[i].Add(Convert.ToInt32(dataReader["UserId"]).ToString());
                    user[i].Add((dataReader["city"]).ToString());
                    user[i].Add((dataReader["street"]).ToString());
                    user[i].Add((dataReader["homeNum"]).ToString());

                    i++;
                }

                dataReader.Close();
                
                return user;
            }
            catch (Exception ex)
            {

                throw new Exception("failed", ex);
            }
            finally
            {

                if (con != null)
                    con.Close();
            }
        }




        private SqlCommand AvDayHourCheck(SqlConnection con, string day, string hour, int familyId)// שאילתה שבודקת מי זמין ביום ובשעה הספציפית שהוכנסו
        {
            string str = "SELECT username,age,phone,avgPoint,UserId,city,street,homeNum" +
                " FROM TimesAvailablity T JOIN  UsersFamliy U ON T.UserId=U.id JOIN Accounts A on" +
                " U.familyId=A.id WHERE T.availableDays LIKE @availableDays and T.availableHours" +
                " LIKE @availableHours and U.familyId!=@familyId";
            SqlCommand cmd = createCommand(con, str);
            cmd.Parameters.Add("@availableDays", SqlDbType.Char);
            cmd.Parameters["@availableDays"].Value = day;
            cmd.Parameters.Add("@availableHours", SqlDbType.Char);
            cmd.Parameters["@availableHours"].Value = hour;
            cmd.Parameters.Add("@familyId", SqlDbType.SmallInt);
            cmd.Parameters["@familyId"].Value =(short)familyId;
            return cmd;
        }

        SqlConnection Connect(string connectionStringName)
        {

            string connectionString = WebConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;

        }
        SqlCommand createCommand(SqlConnection con, string commandStr)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = commandStr;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandTimeout = 5;
            return cmd;
        }

        public List<List<string>> getIncomePendingRequests(int userid)
        {
            SqlConnection con = null;
            try
            {
                con = Connect("DogsProjDB");
                SqlCommand selectCommand = incomePendingRequest(con,userid);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                int i = 0;
                List<List<string>> pendingRequestList = new List<List<string>>();
                while (dataReader.Read())
                {
                    pendingRequestList.Add(new List<string>());// המרה לסטרינגים הכנסה לרשימת של סטרינגים
                    pendingRequestList[i].Add((dataReader["serviceName"]).ToString());
                    pendingRequestList[i].Add((dataReader["serviceDate"]).ToString());
                    pendingRequestList[i].Add((dataReader["serviceDay"]).ToString());
                    pendingRequestList[i].Add((dataReader["serviceHour"]).ToString());
                    pendingRequestList[i].Add((dataReader["servicetype"]).ToString());
                    pendingRequestList[i].Add((dataReader["dogname"]).ToString());
                    pendingRequestList[i].Add((dataReader["dogBreed"]).ToString());
                    pendingRequestList[i].Add((dataReader["age"]).ToString());
                    pendingRequestList[i].Add((dataReader["size"]).ToString());
                    pendingRequestList[i].Add((dataReader["sex"]).ToString());
                    pendingRequestList[i].Add((dataReader["neutering"]).ToString());
                    pendingRequestList[i].Add((dataReader["username"]).ToString());
                    pendingRequestList[i].Add((dataReader["phone"]).ToString());
                    pendingRequestList[i].Add((dataReader["city"]).ToString());
                    pendingRequestList[i].Add((dataReader["street"]).ToString());
                    pendingRequestList[i].Add((dataReader["homeNum"]).ToString());
                    pendingRequestList[i].Add((dataReader["id"]).ToString());
                    i++;
                }


                dataReader.Close();

                return pendingRequestList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed to insert pending list", ex);
            }
            finally
            {

                if (con != null)
                    con.Close();
            }
        }




        private SqlCommand incomePendingRequest(SqlConnection con, int userid)
        {
            
            string str = " SELECT S.serviceName, S.serviceDate, S.serviceDay,S.serviceHour,S.servicetype,D.dogname,D.dogBreed,D.age,D.size,D.sex,D.neutering," +
            " U.username,U.phone,A.city,A.street,A.homeNum,S.id " +
            " FROM PendingReq P JOIN ServicesDog S ON " +
            " P.idService = S.id JOIN Dogs4 D on D.familyNum = S.familyId JOIN UsersFamliy U ON U.id = S.UserId JOIN Accounts A ON A.id = S.familyId " +
            " JOIN ( SELECT DISTINCT PD.idService AS TmpID FROM ServicesDog  SD JOIN PendingReq PD ON PD.idService = SD.id " +
            " WHERE SD.serviceName != 'pension' or PD.approvedreq != 1) as t on t.TmpID = S.id " +
            " WHERE P.idUser LIKE @userid AND P.approvedreq is NULL " +
            " and DATEDIFF(MINUTE, CONVERT(varchar(10), (S.serviceDate), 126)+' ' + substring(S.serviceHour, 1, 5),GETDATE()) <= 0 ";
            SqlCommand cmd = createCommand(con, str);
            cmd.Parameters.Add("@userid", SqlDbType.SmallInt);
            cmd.Parameters["@userid"].Value = (short)userid;
            return cmd;
        }

        public List<List<string>> getIncomeApprovedRequests(int userid)
        {
            SqlConnection con = null;
            try
            {
                con = Connect("DogsProjDB");
                SqlCommand selectCommand = incomeApprovedRequests(con, userid);
                SqlDataReader dataReader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
                int i = 0;
                List<List<string>> approvedRequestList = new List<List<string>>();
                while (dataReader.Read())
                {
                    approvedRequestList.Add(new List<string>());// המרה לסטרינגים הכנסה לרשימת של סטרינגים
                    approvedRequestList[i].Add((dataReader["serviceName"]).ToString());//0
                    approvedRequestList[i].Add((dataReader["serviceDate"]).ToString());//1
                    approvedRequestList[i].Add((dataReader["serviceDay"]).ToString());//2
                    approvedRequestList[i].Add((dataReader["serviceHour"]).ToString());//3
                    approvedRequestList[i].Add((dataReader["servicetype"]).ToString());//4
                    approvedRequestList[i].Add((dataReader["dogname"]).ToString());//5
                    approvedRequestList[i].Add((dataReader["dogBreed"]).ToString());//6
                    approvedRequestList[i].Add((dataReader["age"]).ToString());//7
                    approvedRequestList[i].Add((dataReader["size"]).ToString());//8
                    approvedRequestList[i].Add((dataReader["sex"]).ToString());//9
                    approvedRequestList[i].Add((dataReader["neutering"]).ToString());//10
                    approvedRequestList[i].Add((dataReader["username"]).ToString());//11
                    approvedRequestList[i].Add((dataReader["phone"]).ToString());//12
                    approvedRequestList[i].Add((dataReader["city"]).ToString());//13
                    approvedRequestList[i].Add((dataReader["street"]).ToString());//14
                    approvedRequestList[i].Add((dataReader["homeNum"]).ToString());//15
                    approvedRequestList[i].Add((dataReader["id"]).ToString());//16
                    i++;
                }


                dataReader.Close();

                return approvedRequestList;
            }
            catch (Exception ex)
            {

                throw new Exception("failed to insert approved list", ex);
            }
            finally
            {

                if (con != null)
                    con.Close();
            }
        }




        private SqlCommand incomeApprovedRequests(SqlConnection con, int userid)
        {
            string str = "SELECT S.serviceName, S.serviceDate, S.serviceDay,S.serviceHour,S.servicetype,D.dogname,D.dogBreed,D.age,D.size,D.sex,D.neutering," +
                " U.username,U.phone,A.city,A.street,A.homeNum,S.id FROM PendingReq P JOIN ServicesDog S ON" +
                " P.idService=S.id JOIN Dogs4 D on D.familyNum=S.familyId JOIN UsersFamliy U" +
                " ON U.id=S.UserId JOIN Accounts A ON A.id=S.familyId" +
                " WHERE P.idUser LIKE @userid AND S.serviceName!='pension' AND P.approvedreq=1";
            SqlCommand cmd = createCommand(con, str);
            cmd.Parameters.Add("@userid", SqlDbType.SmallInt);
            cmd.Parameters["@userid"].Value = (short)userid;
            return cmd;
        }


        public string setRequestsDb(int userid, string serviceId, bool val)
        {

            SqlConnection con = null;
            int numEffected = 0;
            try
            {
                //C - Connect to the Database
                con = Connect("DogsProjDB");

                //C Create the Insert SqlCommand
                SqlCommand insertCommand = createSetAnswerCommand(userid, serviceId, val, con);
                numEffected = insertCommand.ExecuteNonQuery();
                SqlCommand insertCommandEmail = getEmail(serviceId, con);
                SqlDataReader dataReader = insertCommandEmail.ExecuteReader(CommandBehavior.CloseConnection);
                string str="";
                while (dataReader.Read())
                {

                    str = dataReader["email"].ToString();

                }
                dataReader.Close();

                if (str == "")
                {
                     throw new Exception("No email was found"); 
                }
              
                return str;

            }

            catch (Exception exep)
            {
                // this code needs to write the error to a log file
                throw new Exception("Error", exep);
            }

            finally
            {
                //C Close Connction
                con.Close();
            }


        }

        private SqlCommand createSetAnswerCommand( int userid, string serviceId, bool val, SqlConnection con)
        {
            string str = "UPDATE PendingReq set approvedreq=@val WHERE idService=@serviceId AND idUser=@userid";
            SqlCommand cmd = createCommand(con, str);
            cmd.Parameters.Add("@serviceId", SqlDbType.SmallInt);
            cmd.Parameters["@serviceId"].Value =  Convert.ToInt16(serviceId);
            cmd.Parameters.Add("@userid", SqlDbType.SmallInt);
            cmd.Parameters["@userid"].Value = (short)userid;
            cmd.Parameters.Add("@val", SqlDbType.Bit);
            if (val)
            {
                cmd.Parameters["@val"].Value = 1;
            }
            else
            {
                cmd.Parameters["@val"].Value = 0;
            }
            return cmd;
        }

        //public short validateRequest(userId)
        //{
        //    string str = "SELECT S.idService FROM ServicesDog S" +
        //        " JOIN PendingReq P ON P.idService=S.id" +

        //    SqlCommand cmd = createCommand(con, str);
        //    cmd.Parameters.Add("@serviceId", SqlDbType.SmallInt);
        //}

        private SqlCommand getEmail(string serviceId, SqlConnection con)
        {
            string str = "SELECT email FROM ServicesDog S JOIN Accounts A ON S.familyId=A.id WHERE S.id LIKE @serviceId";
            SqlCommand cmd = createCommand(con, str);
            cmd.Parameters.Add("@serviceId", SqlDbType.SmallInt);
            cmd.Parameters["@serviceId"].Value = Convert.ToInt16(serviceId);
            return cmd;
        }


    }
}