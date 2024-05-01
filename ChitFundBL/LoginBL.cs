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
    public class LoginBL
    {
        public long SaveUser(UserInfo userInfo)
        {
            try
            {
                var user = new ChitFundDL.LoginDL();
                return user.SaveUser(userInfo);
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
        public DataTable UserLogin(UserInfo userInfo)
        {
            try
            {
                var user = new ChitFundDL.LoginDL();
                return user.UserLogin(userInfo);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
