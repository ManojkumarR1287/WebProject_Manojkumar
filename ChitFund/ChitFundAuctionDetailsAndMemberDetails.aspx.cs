using System;
using ChitFundBL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ChitFundInfo;
namespace ChitFund
{
    public partial class ChitFundAuctionDetailsAndMemberDetails : System.Web.UI.Page
    {
        ChitFundAuctionBL chitFundAuctionBL = new ChitFundAuctionBL();
        ChitFundMembersBL chitFundMembersBL = new ChitFundMembersBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    BindFormView(Convert.ToInt64(Request.QueryString["ChitFundId"]));
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
        private void BindFormView(long chitFundId)
        {
            try
            {
                DataSet dsAuctionDetails = chitFundAuctionBL.GetChitAuctionByChitFundId(chitFundId);
                if (dsAuctionDetails.Tables[1] != null && dsAuctionDetails.Tables[1].Rows.Count > 0)
                {
                    Session["TotalMember"] = dsAuctionDetails.Tables[1].Rows[0]["TotalMember"].ToString();
                }
                grdChitFundAuctionDetails.DataSource = dsAuctionDetails.Tables[0];
                grdChitFundAuctionDetails.DataBind();
                DataSet dsMemberDetails = chitFundMembersBL.GetChitFundMemberDetails(chitFundId);
                grdMemberDetails.DataSource = dsMemberDetails.Tables[0];
                grdMemberDetails.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

        protected void btnNewChitFund_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("NewChitFund.aspx", true);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

        protected void btnEditChitFund_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("NewChitFund.aspx", true);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

        protected void btnAuction_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("AuctionDetails.aspx", true);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

        protected void btnAddMember_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("MemberDetails.aspx", true);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

        protected void grdChitFundAuctionDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = grdChitFundAuctionDetails.Rows[e.NewEditIndex];
                long chitFundAuctionId = Convert.ToInt64(row.Cells[1].Text);
                if (chitFundAuctionId > 0)
                {
                    Response.Redirect("NewAuction.aspx?ChitFundAuctionId=" + chitFundAuctionId + "&ChitFundId=" + Request.QueryString["ChitFundId"] + "&Add=No");
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
                DataTable dtAuction = (DataTable)grdChitFundAuctionDetails.DataSource;
                if (dtAuction != null && dtAuction.Rows.Count > 0)
                {
                    if (Convert.ToInt64(Session["TotalMember"])-1 != dtAuction.Rows.Count)
                    {
                        Response.Redirect("NewAuction.aspx?ChitFundId=" + Request.QueryString["ChitFundId"] + "&Add=Yes");
                    }
                    else
                    {
                        ShowMessage("Chit Auction limit reached.", "Information");
                    }
                }
                else
                {
                    Response.Redirect("NewAuction.aspx?ChitFundId=" + Request.QueryString["ChitFundId"] + "&Add=Yes");
                }
             
               
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

        protected void btnAssignMember_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateMemberCount(Convert.ToInt64(Request.QueryString["ChitFundId"]), 0) == true)
                {
                    Response.Redirect("NewMemberAssignToChitFund.aspx?ChitFundId=" + Request.QueryString["ChitFundId"]);
                }
                else
                {
                    ShowMessage("Chit Member limit reached.", "Information");
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

        protected void grdMemberDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = grdMemberDetails.Rows[e.NewEditIndex];
                long chitFundId = Convert.ToInt64(row.Cells[1].Text);
                if (chitFundId > 0)
                {
                    Response.Redirect("NewChitFund.aspx?id=" + chitFundId);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
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

        protected void grdChitFundAuctionDetails_PreRender(object sender, EventArgs e)
        {
            try
            {
                grdChitFundAuctionDetails.Columns[0].Visible = false;
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