using System;
using ChitFundInfo;
using ChitFundBL;
using System.Data;

namespace ChitFund
{
    public partial class LogIn : System.Web.UI.Page
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
        

        protected void btnNewRegistration_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("NewRegistration.aspx");
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

        protected void btnLogin_Click1(object sender, EventArgs e)
        {
            try
            {
                UserInfo userInfo = new UserInfo();
                userInfo.UserName = txtUserName.Text;
                userInfo.Password = Cryptographics.HashMD5AndEncodeBase64(txtPassword.Text);
                DataTable dtLogin = loginBL.UserLogin(userInfo);
                if (dtLogin == null || dtLogin.Rows.Count == 0)
                {
                    ShowMessage("User name is not exist.", "errorScript");
                }
                else
                {
                    Session["UserId"] = dtLogin.Rows[0]["id"];
                    Session["UserName"] = dtLogin.Rows[0]["Name"];
                    Response.Redirect("ChitFundDetails.aspx",false);
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

        //protected void Submit_Click(object sender, EventArgs e)
        //{
        //    UserInfo userInfo = new UserInfo();
        //    userInfo.UserName = Request.Form["userName"];
        //    userInfo.Password = Cryptographics.HashMD5AndEncodeBase64(Request.Form["password"]);
        //    DataTable dtLogin = loginBL.UserLogin(userInfo);
        //    if (dtLogin == null || dtLogin.Rows.Count == 0)
        //    {
        //        ShowMessage("User name is not exist.", "errorScript");
        //    }
        //    else
        //    {
        //        Session["UserId"] = dtLogin.Rows[0]["id"];
        //        Session["UserName"] = dtLogin.Rows[0]["Name"];
        //        Response.Redirect("ChitFundDetails.aspx");
        //    }
        //}

        // [WebMethod]
        //public static string SayHello(string name)
        //{
        //    return "Hello " + name;
        //}
        //{
        //UserInfo userInfo = new UserInfo();
        //userInfo.UserName = Request.Form["userName"];
        //userInfo.Password = Cryptographics.HashMD5AndEncodeBase64(Request.Form["password"]);
        //DataTable dtLogin = loginBL.UserLogin(userInfo);
        //if (dtLogin == null || dtLogin.Rows.Count == 0)
        //{
        //    ShowMessage("User name is not exist.", "errorScript");
        //}
        //else
        //{
        //    Session["UserId"] = dtLogin.Rows[0]["id"];
        //    Session["UserName"] = dtLogin.Rows[0]["Name"];
        //    Response.Redirect("ChitFundDetails.aspx");
        //}
        //    return true;
        //}

        //[WebMethod(enableSession: true)]
        //public bool ShowInserted();
        //{
        //return true;
        //}

    }
}