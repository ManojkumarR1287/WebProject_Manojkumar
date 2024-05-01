using System;
using System.Configuration;
using ChitFundInfo;
using System.Data;
using System.Data.SqlClient;
using SQL;

namespace ChitFundDL
{
    public class LoginDL
    {
        string connection = string.Empty;

        public long SaveUser(UserInfo userInfo)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[11];
            sqlParameters[0] = new SqlParameter("@id",userInfo.Id);
            sqlParameters[1] = new SqlParameter("@Name",userInfo.Name);
            sqlParameters[2] = new SqlParameter("@UserName",userInfo.UserName);
            sqlParameters[3] = new SqlParameter("@Password",userInfo.Password);
            sqlParameters[4] = new SqlParameter("@ChitOwnerMail",userInfo.ChitOwnerMail);
            sqlParameters[5] = new SqlParameter("@Mobile",userInfo.Mobile);
            sqlParameters[6] = new SqlParameter("@CreatedDate",userInfo.CreatedDate);
            sqlParameters[7] = new SqlParameter("@CreatedBy",userInfo.CreatedBy);
            sqlParameters[8] = new SqlParameter("@ModifiedDate",userInfo.ModifiedDate);
            sqlParameters[9] = new SqlParameter("@ModifiedBy",userInfo.ModifiedBy);
            sqlParameters[10] = new SqlParameter("@Userid", SqlDbType.BigInt);
            sqlParameters[10].Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(connection,CommandType.StoredProcedure, "spInsertUser", sqlParameters);
            if (sqlParameters[10].Value != null && Convert.ToInt64(sqlParameters[10].Value) > 0)
            {
                return Convert.ToInt64(sqlParameters[10].Value);
            }
            return 0;
        }
        public DataTable Validation(UserInfo userInfo)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@UserName", userInfo.UserName);
            sqlParameters[1] = new SqlParameter("@Mobile", userInfo.Mobile);
           return SQLHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "spGetUser", sqlParameters);
        }
        public DataTable UserLogin(UserInfo userInfo)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@UserName", userInfo.UserName);
            sqlParameters[1] = new SqlParameter("@Password", userInfo.Password);
            return SQLHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "spLoginUser", sqlParameters);
        }
    }
}
