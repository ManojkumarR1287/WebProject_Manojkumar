using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChitFundDL;
using ChitFundInfo;
namespace ChitFundBL
{
    public class ChitFundAuctionBL
    {
        public long SaveChitFund(ChitFundAuctionInfo cfInfo)
        {
            try
            {
                var cf = new ChitFundDL.ChitFundAuctionDL();
                return cf.SaveChitFund(cfInfo);
            }
            catch (Exception)
            {
                return default;
            }
        }
        public DataTable Validation(UserInfo userInfo)
        {
            try
            {
                var user = new ChitFundDL.LoginDL();
                return user.Validation(userInfo);
            }
            catch (Exception)
            {
                return default;
            }
        }
        public DataSet GetChitAuctionByChitFundId(long chitFundId)
        {
            try
            {
                var cf = new ChitFundDL.ChitFundAuctionDL();
                return cf.GetChitAuctionByChitFundId(chitFundId);
            }
            catch (Exception)
            {
                return default;
            }
        }
        public DataSet GetChitAuctionByChitFundAuctionId(long chitFundId)
        {
            try
            {
                var cf = new ChitFundDL.ChitFundAuctionDL();
                return cf.GetChitAuctionByChitFundAuctionId(chitFundId);
            }
            catch (Exception)
            {
                return default;
            }
        }
        public DataTable GetChitMemberByChitFundId(long chitFundId)
        {
            try
            {
                var cf = new ChitFundDL.ChitFundAuctionDL();
                return cf.GetChitMemberByChitFundId(chitFundId);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
