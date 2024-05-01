using System;
using ChitFundInfo;
using ChitFundBL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ChitFund
{
    public partial class NewRegistration : System.Web.UI.Page
    {
        LoginBL loginBL = new LoginBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            UserInfo userInfo = new UserInfo();
            userInfo.Name = Request.Form["txtname"];
            userInfo.UserName = Request.Form["userName"];
            userInfo.Password = Cryptographics.HashMD5AndEncodeBase64(Request.Form["password"]);
            userInfo.ChitOwnerMail = Request.Form["chitOwnerMail"];
            userInfo.Mobile = Convert.ToInt64(Request.Form["mobile"]);
            userInfo.CreatedDate = DateTime.Now;
            DataTable dt = loginBL.Validation(userInfo);
            if (dt == null || dt.Rows.Count == 0)
            {
                if (loginBL.SaveUser(userInfo) > 0)
                {
                    Response.Redirect("LogIn.aspx");
                }
                else
                {
                    ShowMessage("New user registration failed.", "errorScript");
                }
            }
            else
            {
                string strQuery = string.Format("Mobile = {0}", Request.Form["mobile"]);
                DataRow drValidate = dt.Select(strQuery).FirstOrDefault();
                if (drValidate != null)
                    ShowMessage("Mobile number " + drValidate["Mobile"] + " already given " + drValidate["UserName"] + ".", "errorScript");
                strQuery = string.Format("UserName = '{0}'", Request.Form["userName"]);
                drValidate = dt.Select(strQuery).FirstOrDefault();
                if (drValidate != null)
                    ShowMessage("User name " + drValidate["UserName"] + " already exist.", "errorScript");
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex);
        }
    }
        private void ShowMessage(string message, string scriptType = "errorScript")
        {
            try
            {
                string script = "alert('" + message + "');";
                ClientScript.RegisterStartupScript(typeof(NewRegistration), scriptType, script, true);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
    }
}