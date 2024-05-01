using System;
using System.Web.UI.WebControls;
using ChitFundBL;
using System.Data;

namespace ChitFund
{
    public partial class NewMemberAssignToChitFund : System.Web.UI.Page, System.IDisposable
    {
        ChitFundMembersBL chitFundMembersBL = new ChitFundMembersBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    Session["ChitFundId"] = Convert.ToInt64(Request.QueryString["ChitFundId"]);
                    BindFormEdit(Convert.ToInt64(Session["ChitFundId"]));
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
  
        private void BindFormEdit(long chitFundId)
        {
            try
            {
                DataSet dsMemberDetails = chitFundMembersBL.GetChitFundMemberNotAssignedDetails(chitFundId);
                if (dsMemberDetails != null && dsMemberDetails.Tables[0].Rows.Count > 0)
                {
                    grdMemberDetails.DataSource = dsMemberDetails.Tables[0];
                    grdMemberDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
        protected void grdMemberDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = grdMemberDetails.SelectedRow;
                if (row != null)
                {
                    long memberId = Convert.ToInt64(row.Cells[1].Text);
                    long chitFundId = Convert.ToInt64(Session["ChitFundId"]);
                    if (ValidateMember(chitFundId, memberId) == true)
                    {
                        if (ValidateMemberCount(chitFundId, memberId) == true)
                        {
                            if (chitFundMembersBL.SaveChitFundMemberByChitFundId(chitFundId, memberId) > 0)
                            {
                                Response.Redirect("ChitFundAuctionDetailsAndMemberDetails.aspx?ChitFundId=" + Session["ChitFundId"]);
                            }
                        }
                        else
                        {
                            ShowMessage("Chit Member limit reached.", "Information");
                            Response.Redirect("ChitFundAuctionDetailsAndMemberDetails.aspx?ChitFundId=" + Session["ChitFundId"]);
                        }
                    }
                    else
                    {
                        ShowMessage("Member already added.", "Faild");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

        protected void AddNewAuction_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("NewMemberDetails.aspx?ChitFundId=" + Session["ChitFundId"]);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
        private bool ValidateMember(long chitFundId, long memberId)
        {
            try
            {
              return chitFundMembersBL.Validation(chitFundId, memberId);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
                return false;
            }
        }
        private bool ValidateMemberCount(long chitFundId, long memberId)
        {
            try
            {
                return chitFundMembersBL.ValidationCount(chitFundId, memberId);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
                return false;
            }
        }
        private void ShowMessage(string message, string scriptType = "errorScript")
        {
            try
            {
                string script = "alert('" + message + "')";
                ClientScript.RegisterStartupScript(typeof(NewRegistration), scriptType, script, true);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

        protected void grdMemberDetails_PreRender(object sender, EventArgs e)
        {
            try
            {
                grdMemberDetails.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
    }
}