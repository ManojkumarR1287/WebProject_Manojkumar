using System;
using System.Configuration;
using ChitFundInfo;
using System.Data;
using System.Data.SqlClient;
using SQL;


namespace ChitFundDL
{
   public class ChitFundMembersDL
    {
        string connection = string.Empty;
        public long SaveChitFundMember(ChitFundMembersInfo chitFundMembersInfo)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[11];
            sqlParameters[0] = new SqlParameter("@id", chitFundMembersInfo.Id);
            sqlParameters[1] = new SqlParameter("@ChitMemberName", chitFundMembersInfo.ChitMemberName);
            sqlParameters[2] = new SqlParameter("@ChitMemberAddress", chitFundMembersInfo.ChitMemberAddress);
            sqlParameters[3] = new SqlParameter("@Mobile1", chitFundMembersInfo.Mobile1);
            sqlParameters[4] = new SqlParameter("@Mobile2", chitFundMembersInfo.Mobile2);
            sqlParameters[5] = new SqlParameter("@Email", chitFundMembersInfo.Email);
            sqlParameters[6] = new SqlParameter("@CreatedDate", chitFundMembersInfo.CreatedDate);
            sqlParameters[7] = new SqlParameter("@CreatedBy", chitFundMembersInfo.CreatedBy);
            sqlParameters[8] = new SqlParameter("@ModifiedDate", chitFundMembersInfo.ModifiedDate);
            sqlParameters[9] = new SqlParameter("@ModifiedBy", chitFundMembersInfo.ModifiedBy);
            sqlParameters[10] = new SqlParameter("@ChitFundMemberId", SqlDbType.BigInt);
            sqlParameters[10].Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spInsertChitFundMember", sqlParameters);
            if (sqlParameters[10].Value != null && Convert.ToInt64(sqlParameters[10].Value) > 0)
            {
                return Convert.ToInt64(sqlParameters[10].Value);
            }
            return 0;
        }
        public long SaveChitFundMemberByChitFundId(long chitFundId, long memberId)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@ChitFundId", chitFundId);
            sqlParameters[1] = new SqlParameter("@MemberId", memberId);
            sqlParameters[2] = new SqlParameter("@id", SqlDbType.BigInt);
            sqlParameters[2].Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spInsertAssignedChitMembers", sqlParameters);
            if (sqlParameters[2].Value != null && Convert.ToInt64(sqlParameters[2].Value) > 0)
            {
                return Convert.ToInt64(sqlParameters[2].Value);
            }
            return 0;
        }

        public DataTable GetChitFundMemberById(long chitFundMemberId)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", chitFundMemberId);
            return SQLHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "spGetChitFundMemberById", sqlParameters);
        }
        public DataSet GetChitFundMembers()
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[0];
            //sqlParameters[0] = new SqlParameter("@Id", chitFundId);
            return SQLHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "spGetChitFundMembers", sqlParameters);
        }
        public long DeleteChitFundMembers(long chitFundMemberId)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", chitFundMemberId);
            return SQLHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spDeleteChitFundMemberById", sqlParameters);
        }
        public DataSet GetChitFundMemberByChitFundId(long chitFundId)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ChitFundId", chitFundId);
            return SQLHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "spGetChitFundMemberByChitFundId", sqlParameters);
        }
        public DataSet GetChitFundMemberNotAssignedDetails(long chitFundId)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ChitFundId", chitFundId);
            return SQLHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "spGetChitMemberNotAssignedToChitFundByChitFundId", sqlParameters);
        }
        public bool ValidationMember(long chitFundId, long memberId)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@ChitFundId", chitFundId);
            sqlParameters[1] = new SqlParameter("@MemberId", memberId);
            DataTable dt = SQLHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "spGetChitFundMemberForValidationById", sqlParameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }
        public bool ValidationCount(long chitFundId, long memberId)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ChitFundId", chitFundId);
            DataTable dt = SQLHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "spGetChitFundMemberCountForValidationById", sqlParameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }
    }
}
