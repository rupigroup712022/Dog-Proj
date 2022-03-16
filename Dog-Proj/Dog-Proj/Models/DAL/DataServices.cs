using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dog_Proj.Models.DAL
{
    public class DataServices
    {

    //    public int Insert(User user)
    //    {
    //        int existEmail = checking(user.Email);
    //        if (existEmail == 0)
    //        {
    //            SqlConnection con = null;
    //            int numEffected = 0;

    //            try
    //            {
    //                //C - Connect to the Database
    //                con = Connect("articleProjDB");

    //                //C Create the Insert SqlCommand
    //                SqlCommand insertCommand = CreateInsertCommand(con, user);

    //                //E Execute
    //                numEffected = insertCommand.ExecuteNonQuery();
    //            }

    //            catch (Exception exep)
    //            {
    //                // this code needs to write the error to a log file
    //                throw new Exception("Exeption", exep);
    //            }

    //            finally
    //            {
    //                //C Close Connction
    //                if (con != null)
    //                    con.Close();
    //            }


    //            // num effected
    //            return numEffected;
    //        }
    //        return 0;
    //    }
    //}
}