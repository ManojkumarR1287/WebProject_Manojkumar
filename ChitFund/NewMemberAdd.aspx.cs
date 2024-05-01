using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChitFundInfo;
using ChitFundBL;
using System.Data;

namespace ChitFund
{
    public partial class NewMemberAdd : System.Web.UI.Page
    {
        ChitFundMembersInfo chitFundMembersInfo = new ChitFundMembersInfo();
        ChitFundMembersBL chitFundMembersBL = new ChitFundMembersBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    Session["ChitMemberId"] = Convert.ToInt64(Request.QueryString["ChitMemberId"]);
                    BindFormEdit(Convert.ToInt64(Session["ChitMemberId"]));
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
        private void BindFormEdit(long chitMemberId)
        {
            try
            {
                DataTable dtMember = chitFundMembersBL.GetChitFundMembetbyId(chitMemberId);
                if (dtMember != null && dtMember.Rows.Count > 0)
                {
                    Session["ChitMemberId"] = dtMember.Rows[0]["id"];
                    txtChitMemberName.Text = dtMember.Rows[0]["ChitMemberName"].ToString();
                    txtChitMemberAddress.Text = dtMember.Rows[0]["ChitMemberAddress"].ToString();
                    txtMobile1.Text = dtMember.Rows[0]["Mobile1"].ToString();
                    txtMobile2.Text = dtMember.Rows[0]["Mobile2"].ToString();
                    txtEmail.Text = dtMember.Rows[0]["Email"].ToString();
                }
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
                chitFundMembersInfo.Id = Convert.ToInt64(Session["ChitMemberId"]) != 0 ? Convert.ToInt64(Session["ChitMemberId"]) : Convert.ToInt64(0);
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
                    Response.Redirect("MemberDetails.aspx?ChitMemberId=" + Session["ChitMemberId"]);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
    }
}