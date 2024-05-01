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
    public partial class MemberDetails : System.Web.UI.Page
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
                DataSet dsMemberDetails = chitFundMembersBL.GetChitFundMembers();
                grdMemberDetails.DataSource = dsMemberDetails.Tables[0];
                grdMemberDetails.DataBind();
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
                Response.Redirect("NewMemberAdd.aspx?ChitMemberId= 0");
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
                long chitMemberId = Convert.ToInt64(row.Cells[1].Text);
                if (chitMemberId > 0)
                {
                    Response.Redirect("NewMemberAdd.aspx?ChitMemberId=" + chitMemberId);
                }
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