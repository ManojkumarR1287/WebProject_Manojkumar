using System;
using System.Configuration;
using ChitFundInfo;
using System.Data;
using System.Data.SqlClient;
using SQL;


namespace ChitFundDL
{
   public class ChitFundAuctionDL
    {
        string connection = string.Empty;
        public long SaveChitFund(ChitFundAuctionInfo chitFundAuctionInfo)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[16];
            sqlParameters[0] = new SqlParameter("@id", chitFundAuctionInfo.Id);
            sqlParameters[1] = new SqlParameter("@ChitFundId", chitFundAuctionInfo.ChitFundId);
            sqlParameters[2] = new SqlParameter("@ChitNo", chitFundAuctionInfo.ChitNo);
            sqlParameters[3] = new SqlParameter("@ChitDate", chitFundAuctionInfo.ChitDate);
            sqlParameters[4] = new SqlParameter("@Dedution", chitFundAuctionInfo.Dedution);
            sqlParameters[5] = new SqlParameter("@MemberBalanceCount", chitFundAuctionInfo.MemberBalanceCount);
            sqlParameters[6] = new SqlParameter("@Interest", chitFundAuctionInfo.Interest);
            sqlParameters[7] = new SqlParameter("@Settlement", chitFundAuctionInfo.Settlement);
            sqlParameters[8] = new SqlParameter("@RoundUpAmount", chitFundAuctionInfo.RoundUpAmount);
            sqlParameters[9] = new SqlParameter("@FinalSettlementAmount", chitFundAuctionInfo.FinalSettlementAmount);
            sqlParameters[10] = new SqlParameter("@ChitTakenBy", chitFundAuctionInfo.ChitTakenBy);
            sqlParameters[11] = new SqlParameter("@CreatedDate", chitFundAuctionInfo.CreatedDate);
            sqlParameters[12] = new SqlParameter("@CreatedBy", chitFundAuctionInfo.CreatedBy);
            sqlParameters[13] = new SqlParameter("@ModifiedDate", chitFundAuctionInfo.ModifiedDate);
            sqlParameters[14] = new SqlParameter("@ModifiedBy", chitFundAuctionInfo.ModifiedBy);
            sqlParameters[15] = new SqlParameter("@ChitFundAuctionId", SqlDbType.BigInt);
            sqlParameters[15].Direction = ParameterDirection.Output;
            SQLHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spInserttblChitFundAuction", sqlParameters);
            if (sqlParameters[15].Value != null && Convert.ToInt64(sqlParameters[15].Value) > 0)
            {
                return Convert.ToInt64(sqlParameters[15].Value);
            }
            return 0;
        }

        public DataTable GetChitAuctionById(long chitAuctionId)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", chitAuctionId);
            return SQLHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "spGetChitAuctionById", sqlParameters);
        }
        public DataTable GetChitAuctions()
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[0];
            //sqlParameters[0] = new SqlParameter("@Id", chitFundId);
            return SQLHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "spGetChitAuctions", sqlParameters);
        }
        public long DeleteChitAuctions(long chitFundId)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", chitFundId);
            return SQLHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "spDeleteChitAuctionById", sqlParameters);
        }
        public DataSet GetChitAuctionByChitFundId(long chitFundId)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ChitFundId", chitFundId);
            return SQLHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "spGetChitAuctionByChitFundId", sqlParameters);
        }
        public DataSet GetChitAuctionByChitFundAuctionId(long chitFundAuctionId)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ChitFundAuctionId", chitFundAuctionId);
            return SQLHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "spGetChitAuctionByChitFundAuctionId", sqlParameters);
        }
        public DataTable GetChitMemberByChitFundId(long chitFundId)
        {
            connection = ConfigurationManager.ConnectionStrings["ChitFundConnectionString"].ConnectionString;
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ChitFundId", chitFundId);
            return SQLHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "spGetChitMemberByChitFundId", sqlParameters);
        }
    }
}
