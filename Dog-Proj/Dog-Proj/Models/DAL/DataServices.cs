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
                    foreach (var day in user.Availablity)
                    {
                        foreach (var hour in day.Value)
                        {
                            SqlCommand insertCommandHours = CreateInsertCommand(con, id, day.Key, hour);
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

                    str = Convert.ToInt32(dataReader["id"]);
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

            string commandStr = "INSERT INTO TimesU (userId,availableDays,availableHours) VALUES (@userId,@availableDays,@availableHours)";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@userId", SqlDbType.SmallInt);
            cmd.Parameters["@userId"].Value = userId;
            cmd.Parameters.Add("@availableDays", SqlDbType.NChar);
            cmd.Parameters["@availableDays"].Value = availableDays;
            cmd.Parameters.Add("@availableHours", SqlDbType.NChar);
            cmd.Parameters["@availableHours"].Value = availableHours;
        

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
            cmd.Parameters.Add("@avgPoint", SqlDbType.Int);
            cmd.Parameters["@avgPoint"].Value = (int)account.AvgPoint;
            return cmd;
        }

        public int InsertDog(Dog dog)
        {
            int existName = checkingDogname(dog.Dogname);
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


        public int checkingDogname(string dogname)
        {
            SqlConnection con = null;
            try
            {
                // Connect
                con = Connect("DogsProjDB");
                // Create the insert command
                SqlCommand selectCommand = NameDogCheck(con, dogname);
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
        private SqlCommand NameDogCheck(SqlConnection con, string dogname)
        {
            string str = "SELECT * FROM Dogs4 WHERE dogname LIKE @dogname";
            SqlCommand cmd = createCommand(con, str);
            cmd.Parameters.Add("@dogname", SqlDbType.Char);
            cmd.Parameters["@dogname"].Value = dogname;
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
            string str = "SELECT username,sex FROM UsersFamliy WHERE familyId LIKE @familyId";
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


        private SqlCommand emailandpasswordCheck(SqlConnection con, string Email, string Passwords)
        {
            string str = "SELECT * FROM Accounts WHERE email LIKE @email and [passwords] LIKE @passwords";
            SqlCommand cmd = createCommand(con, str);
            cmd.Parameters.Add("@email", SqlDbType.Char);
            cmd.Parameters["@email"].Value = Email;
            cmd.Parameters.Add("@passwords", SqlDbType.Char);
            cmd.Parameters["@passwords"].Value = Passwords;
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

    }
}