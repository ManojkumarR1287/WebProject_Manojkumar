﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using context = System.Web.HttpContext;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ChitFund
{
 
    /// <summary>  
    /// Summary description for ExceptionLogging  
    /// article by Vithal Wadje  

    /// </summary>  
    public static class ExceptionLogging
    {
        private static String exepurl;
        static SqlConnection con;
        private static void connecttion()
        {
            string constr =  ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            con = new SqlConnection(constr);
            con.Open();
        }
        public static void SendExcepToDB(Exception exdb)
        {
            connecttion();
            exepurl = context.Current.Request.Url.ToString();
            SqlCommand com = new SqlCommand("spInsertLog", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ExceptionMsg", exdb.Message.ToString());
            com.Parameters.AddWithValue("@ExceptionType", exdb.GetType().Name.ToString());
            com.Parameters.AddWithValue("@ExceptionURL", exepurl);
            com.Parameters.AddWithValue("@ExceptionSource", exdb.StackTrace.ToString());
            com.ExecuteNonQuery();
        }
    }
}