using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChitFundInfo;
using ChitFundBL;
using System.Data;
using AjaxControlToolkit;

namespace ChitFund
{
    public partial class NewChitFund : System.Web.UI.Page, System.IDisposable
    {
        ChitFundDetailsInfo chitFundDetailsInfo = new ChitFundDetailsInfo();
        ChitFundDetailsBL chitFundDetailsBL = new ChitFundDetailsBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    Session["ChitFundId"] = Convert.ToInt64(Request.QueryString["ChitFundId"]);
                    BindFormEdit(Convert.ToInt64(Request.QueryString["ChitFundId"]));

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
                DataTable dtChitFund = chitFundDetailsBL.GetChitFundById(chitFundId);
                if (dtChitFund != null && dtChitFund.Rows.Count > 0)
                {
                    Session["ChitFundId"] = dtChitFund.Rows[0]["id"];
                    txtChitFundName.Text = dtChitFund.Rows[0]["ChitName"].ToString();
                    txtChitAddress.Text = dtChitFund.Rows[0]["ChitAddress"].ToString();
                    txtChitAmount.Text = dtChitFund.Rows[0]["ChitAmount"].ToString();
                    txtTotalMember.Text = dtChitFund.Rows[0]["TotalMember"].ToString();
                    txtTotalAmount.Text = dtChitFund.Rows[0]["TotalAmount"].ToString();
                    cStartDate.SelectedDate =Convert.ToDateTime( dtChitFund.Rows[0]["StartDate"]);
                    txtInterval.Text = dtChitFund.Rows[0]["Interval"].ToString();
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
                chitFundDetailsInfo = new ChitFundDetailsInfo();
                chitFundDetailsInfo.Id = Convert.ToInt64(Session["ChitFundId"]) != 0 ? Convert.ToInt64(Session["ChitFundId"]) : Convert.ToInt64(0);
                chitFundDetailsInfo.ChitOwnerId = Convert.ToInt64(Session["UserId"]);
                chitFundDetailsInfo.ChitName = txtChitFundName.Text.ToString();
                chitFundDetailsInfo.ChitAddress = txtChitAddress.Text.ToString();
                chitFundDetailsInfo.ChitAmount = Convert.ToInt64(txtChitAmount.Text);
                chitFundDetailsInfo.TotalMember = Convert.ToInt64(txtTotalMember.Text);
                chitFundDetailsInfo.TotalAmount = Convert.ToInt64(txtTotalAmount.Text);
                chitFundDetailsInfo.StartDate = Convert.ToDateTime(txtDate1.Text);
                chitFundDetailsInfo.Interval = Convert.ToInt32(txtInterval.Text);
                chitFundDetailsInfo.CreatedBy = Convert.ToInt64(Session["UserId"]);
                chitFundDetailsInfo.CreatedDate = Convert.ToDateTime(DateTime.Now);
                chitFundDetailsInfo.ModifiedBy = Convert.ToInt64(Session["UserId"]);
                chitFundDetailsInfo.ModifiedDate = Convert.ToDateTime(DateTime.Now);
                if (chitFundDetailsBL.SaveChitFund(chitFundDetailsInfo) > 0)
                {
                    Response.Redirect("ChitFundDetails.aspx");
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
    }
}