using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChitFundInfo;
using ChitFundBL;

namespace ChitFund
{
    public partial class NewMemberDetails : System.Web.UI.Page
    {
        ChitFundMembersInfo chitFundMembersInfo = new ChitFundMembersInfo();
        ChitFundMembersBL chitFundMembersBL = new ChitFundMembersBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session["ChitFundId"] = Convert.ToInt64(Request.QueryString["ChitFundId"]);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                chitFundMembersInfo = new ChitFundMembersInfo();
                chitFundMembersInfo.ChitMemberName = txtChitMemberName.Text.ToString();
                chitFundMembersInfo.ChitMemberAddress = txtChitMemberAddress.Text.ToString();
                chitFundMembersInfo.Mobile1 = Convert.ToInt64(txtMobile1.Text);
                chitFundMembersInfo.Mobile2 = Convert.ToInt64(txtMobile2.Text);
                chitFundMembersInfo.Email = txtEmail.Text.ToString();
                chitFundMembersInfo.CreatedBy = Convert.ToInt64(Session["UserId"]);
                chitFundMembersInfo.CreatedDate = Convert.ToDateTime(DateTime.Now);
                chitFundMembersInfo.ModifiedBy = Convert.ToInt64(Session["UserId"]);
                chitFundMembersInfo.ModifiedDate = Convert.ToDateTime(DateTime.Now);
                if (chitFundMembersBL.SaveChitFundMember(chitFundMembersInfo) > 0)
                {
                    Response.Redirect("NewMemberAssignToChitFund.aspx?ChitFundId=" + Session["ChitFundId"]);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
    }
}