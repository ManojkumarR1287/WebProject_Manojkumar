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
    public partial class ChitFundDetails : System.Web.UI.Page
    {
        ChitFundDetailsBL chitFundDetailsBL = new ChitFundDetailsBL();
        long chitFundId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    BindFormView();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
        private void BindFormView()
        {
            try
            {
                DataSet ds = chitFundDetailsBL.GetChitFundDetails(Convert.ToInt64(Session["UserId"]));
                grdChitFundDetails.DataSource = ds.Tables[0];
                grdChitFundDetails.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

        protected void grdChitFundDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = grdChitFundDetails.SelectedRow;
                if (row != null)
                {
                    chitFundId = Convert.ToInt64(row.Cells[1].Text);
                    Response.Redirect("ChitFundAuctionDetailsAndMemberDetails.aspx?ChitFundId=" + chitFundId);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
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

        protected void grdChitFundDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = grdChitFundDetails.SelectedRow;
                if (row != null)
                {
                    chitFundId = Convert.ToInt64(row.Cells[1].Text);
                    Response.Redirect("NewChitFund.aspx?ChitFundId=" + chitFundId);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

        protected void grdChitFundDetails_RowEditing1(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridViewRow row = grdChitFundDetails.Rows[e.NewEditIndex];
                long chitFundId = Convert.ToInt64(row.Cells[1].Text);
                if (chitFundId > 0)
                {
                    Response.Redirect("NewChitFund.aspx?ChitFundId=" + chitFundId);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

        protected void btnTrace_Click(object sender, EventArgs e)
        {
            Trace.Warn("Trace warn demo");
            Trace.Write("Trace write demo");
        }

        protected void grdChitFundDetails_PreRender(object sender, EventArgs e)
        {
            grdChitFundDetails.Columns[0].Visible = false;
        }
    }
}