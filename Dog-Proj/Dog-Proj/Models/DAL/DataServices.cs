using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DOGS1.Models;

namespace Dog_Proj.Models.DAL
{
    public class DataServices
    {

        public int Insert(User user)
        {
            int existName = checking(user.Username);
            if (existName == 0)
            {
                SqlConnection con = null;
                int numEffected = 0;

                try
                {
                    //C - Connect to the Database
                    con = Connect("DogsProjDB");

                    //C Create the Insert SqlCommand
                    SqlCommand insertCommand = CreateInsertCommand(con, user);

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



        public int checking(string username)
        {
            SqlConnection con = null;
            try
            {
                // Connect
                con = Connect("DogsProjDB");
                // Create the insert command
                SqlCommand selectCommand = NameCheck(con, username);
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
                throw new Exception("failed in reading Email", ex);
            }
            finally
            {
                // Close the connection
                if (con != null)
                    con.Close();
            }
        }
        private SqlCommand NameCheck(SqlConnection con, string username)
        {
            string str = "SELECT * FROM Users WHERE username LIKE @username";
            SqlCommand cmd = createCommand(con, str);
            cmd.Parameters.Add("@username", SqlDbType.Char);
            cmd.Parameters["@username"].Value = username;
            return cmd;
        }

        private SqlCommand CreateInsertCommand(SqlConnection con, User user)
        {
         
            string commandStr = "INSERT INTO Users (username,phone,sex,age,availableDays,availableHours) VALUES (@username,@phone,@sex,@age,@availableDays,@availableHours)";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@username", SqlDbType.Char);
            cmd.Parameters["@username"].Value = user.Username;
            cmd.Parameters.Add("@phone", SqlDbType.Char);
            cmd.Parameters["@phone"].Value = user.Phone;
            cmd.Parameters.Add("@sex", SqlDbType.Char);
            cmd.Parameters["@sex"].Value = user.Sex;
            cmd.Parameters.Add("@age", SqlDbType.Char);
            cmd.Parameters["@age"].Value = user.Age;
            cmd.Parameters.Add("@availableDays", SqlDbType.Char);
            cmd.Parameters["@availableDays"].Value = user.AvailableDays;
            cmd.Parameters.Add("@availableHours", SqlDbType.Char);
            cmd.Parameters["@availableHours"].Value = user.AvailableHours;
             return cmd;
        }

        public int Insert(Account account)
        {
            int existEmail = checkingEmail(account.Email);
            if (existEmail == 0)
            {
                SqlConnection con = null;
                int numEffected = 0;

                try
                {
                    //C - Connect to the Database
                    con = Connect("DogsProjDB");

                    //C Create the Insert SqlCommand
                    SqlCommand insertCommand = CreateInsertCommand(con, account);

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
                string str = "";
                while (dataReader.Read())
                {
                    str = (string)dataReader["email"];
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
            string str = "SELECT * FROM Accounts WHERE email LIKE @email";
            SqlCommand cmd = createCommand(con, str);
            cmd.Parameters.Add("@email", SqlDbType.Char);
            cmd.Parameters["@email"].Value = email;
            return cmd;
        }


        private SqlCommand CreateInsertCommand(SqlConnection con, Account account)
        {
           
            string commandStr = "INSERT INTO Account (familyname,moreAnimals,street,homeNum,email,yardSize,yardSize,passwords) VALUES (@familyname,@moreAnimals,@street,@homeNum,@email,@yardSize,@houseType,@passwords)";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@familyname", SqlDbType.NChar);
            cmd.Parameters["@familyname"].Value = account.Familyname;
            cmd.Parameters.Add("@moreAnimals", SqlDbType.Char);
            cmd.Parameters["@moreAnimals"].Value = account.MoreAnimals;
            cmd.Parameters.Add("@street", SqlDbType.Char);
            cmd.Parameters["@street"].Value = account.Street;
            cmd.Parameters.Add("@homeNum", SqlDbType.Char);
            cmd.Parameters["@homeNum"].Value = account.HomeNum;
            cmd.Parameters.Add("@email", SqlDbType.Char);
            cmd.Parameters["@email"].Value = account.Email;
            cmd.Parameters.Add("@yardSize", SqlDbType.Char);
            cmd.Parameters["@yardSize"].Value = account.YardSize;
            cmd.Parameters.Add("@yardSize", SqlDbType.Char);
            cmd.Parameters["@yardSize"].Value = account.YardSize;
            cmd.Parameters.Add("@passwords", SqlDbType.Char);
            cmd.Parameters["@passwords"].Value = account.Passwords;
            return cmd;
        }

        public int Insert(Dog dog)
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
                    SqlCommand insertCommand = CreateInsertCommand(con, dog);

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
                throw new Exception("failed in reading Email", ex);
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
            string str = "SELECT * FROM Dogs WHERE dogname LIKE @dogname";
            SqlCommand cmd = createCommand(con, str);
            cmd.Parameters.Add("@dogname", SqlDbType.Char);
            cmd.Parameters["@dogname"].Value = dogname;
            return cmd;
        }

        private SqlCommand CreateInsertCommand(SqlConnection con, Dog dog)
        {
            string commandStr = "INSERT INTO Dogs (picture,dogname,dogBreed,age,size,sex,neutering,dog_character) VALUES (@picture,@dogname,@dogBreed,@age,@size,@sex,@neutering,@dog_character)";
            SqlCommand cmd = createCommand(con, commandStr);
            cmd.Parameters.Add("@picture", SqlDbType.Char);
            cmd.Parameters["@picture"].Value = dog.Picture;//איך מתייחסים לתמונה 
            cmd.Parameters.Add("@dogname", SqlDbType.Char);
            cmd.Parameters["@dogname"].Value = dog.Dogname;
            cmd.Parameters.Add("@dogBreed", SqlDbType.Char);
            cmd.Parameters["@dogBreed"].Value = dog.DogBreed;
            cmd.Parameters.Add("@age", SqlDbType.Char);
            cmd.Parameters["@age"].Value = dog.Age;
            cmd.Parameters.Add("@size", SqlDbType.Char);
            cmd.Parameters["@size"].Value = dog.Size;
            cmd.Parameters.Add("@sex", SqlDbType.Char);
            cmd.Parameters["@sex"].Value = dog.Sex;
            cmd.Parameters.Add("@neutering", SqlDbType.Char);
            cmd.Parameters["@neutering"].Value = dog.Neutering;
            cmd.Parameters.Add("@dog_character", SqlDbType.Char);
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