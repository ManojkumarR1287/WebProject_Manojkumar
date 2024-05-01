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
    public class ChitFundDetailsBL
    {
        public long SaveChitFund(ChitFundDetailsInfo cfInfo)
        {
            try
            {
                var cf = new ChitFundDL.ChitFundDetailsDL();
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
        public DataSet GetChitFundDetails( long userId)
        {
            try
            {
                var cf = new ChitFundDL.ChitFundDetailsDL();
                return cf.GetChitFunds(userId);
            }
            catch (Exception)
            {
                return default;
            }
        }
        public DataTable GetChitFundById(long chitFundId)
        {
            try
            {
                var cf = new ChitFundDL.ChitFundDetailsDL();
                return cf.GetChitFundById(chitFundId);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
