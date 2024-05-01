using System;
using System.Configuration;
using ChitFundInfo;
using System.Data;
using System.Data.SqlClient;
using SQL;


namespace ChitFundDL
{
    public class ChitFundDetailsDL
    {
        string connection = string.Empty;
        public long SaveChitFund(ChitFundDetailsInfo chitFundDetailsInfo)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[14];
            sqlParameters[0] = new SqlParameter("@id", chitFundDetailsInfo.Id);
            sqlParameters[1] = new SqlParameter("@ChitName", chitFundDetailsInfo.ChitName);
            sqlParameters[2] = new SqlParameter("@ChitAddress", chitFundDetailsInfo.ChitAddress);
            sqlParameters[3] = new SqlParameter("@ChitAmount", chitFundDetailsInfo.ChitAmount);
            sqlParameters[4] = new SqlParameter("@TotalAmount", chitFundDetailsInfo.TotalAmount);
            sqlParameters[5] = new SqlParameter("@TotalMember", chitFundDetailsInfo.TotalMember);
            sqlParameters[6] = new SqlParameter("@Interval", chitFundDetailsInfo.Interval);
            sqlParameters[7] = new SqlParameter("@StartDate", chitFundDetailsInfo.StartDate);
            sqlParameters[8] = new SqlParameter("@CreatedDate", chitFundDetailsInfo.CreatedDate);
            sqlParameters[9] = new SqlParameter("@CreatedBy", chitFundDetailsInfo.CreatedBy);
            sqlParameters[10] = new SqlParameter("@ModifiedDate", chitFundDetailsInfo.ModifiedDate);
            sqlParameters[11] = new SqlParameter("@ModifiedBy", chitFundDetailsInfo.ModifiedBy);
            sqlParameters[12] = new SqlParameter("@ChitOwnerId", chitFundDetailsInfo.ChitOwnerId);
            sqlParameters[13] = new SqlParameter("@ChitFundId", SqlDbType.BigInt);
            sqlParameters[13].Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spInsertChitFund", sqlParameters);
            if (sqlParameters[13].Value != null && Convert.ToInt64(sqlParameters[13].Value) > 0)
            {
                return Convert.ToInt64(sqlParameters[13].Value);
            }
            return 0;
        }

        public DataTable GetChitFundById(long chitFundId)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", chitFundId);
            return SQLHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "spGetChitFundById", sqlParameters);
        }
        public DataSet GetChitFunds(long userId)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ChitOwnerId", userId);
            return SQLHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "spGetChitFunds", sqlParameters);
        }
        public long DeleteChitFunds(long chitFundId)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", chitFundId);
            return SQLHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spDeleteChitFundById", sqlParameters);
        }
    }
}
