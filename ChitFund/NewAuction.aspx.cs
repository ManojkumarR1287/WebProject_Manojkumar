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
    public partial class NewAuction : System.Web.UI.Page, System.IDisposable
    {
        ChitFundAuctionInfo chitFundAuctionInfo = new ChitFundAuctionInfo();
        ChitFundAuctionBL chitFundAuctionBL = new ChitFundAuctionBL();
        DataSet dtChitFundAuction = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    txtAuctionNo.Enabled = false;
                    txtMemberBalanceCount.Enabled = false;
                    txtInterest.Enabled = false;
                    Session["ChitFundAuctionId"] = Convert.ToInt64(Request.QueryString["ChitFundAuctionId"]);
                    Session["ChitFundId"] = Convert.ToInt64(Request.QueryString["ChitFundId"]);
                    if (Request.QueryString["Add"].ToString() == "Yes")
                    {
                        BindFormAdd(Convert.ToInt64(Session["ChitFundId"]));
                    }
                    else
                    {
                        BindFormEdit(Convert.ToInt64(Request.QueryString["ChitFundAuctionId"]));
                    }
                    LoadChitTakenBy(Convert.ToInt64(Request.QueryString["ChitFundId"]));
                    cChitDate.EndDate = DateTime.Now;
                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex);
                }
            }
        }
        private void LoadChitTakenBy(long chitFundId)
        {
            DataTable dtChitFundMember = chitFundAuctionBL.GetChitMemberByChitFundId(chitFundId);
            if (dtChitFundMember != null && dtChitFundMember.Rows.Count > 0)
            {
                try
                {
                    ddlChitTakenBy.DataSource = dtChitFundMember;
                    ddlChitTakenBy.DataValueField = "id";
                    ddlChitTakenBy.DataTextField = "ChitMemberName";
                    ddlChitTakenBy.SelectedValue = dtChitFundMember.Rows[0]["ChitMemberName"].ToString();
                    ddlChitTakenBy.DataBind();
                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex);
                }
            }
        }
        private void BindFormEdit(long chitFundAuctionId)
        {
            try
            {
                dtChitFundAuction = chitFundAuctionBL.GetChitAuctionByChitFundAuctionId(chitFundAuctionId);
                if (dtChitFundAuction != null && dtChitFundAuction.Tables[0].Rows.Count > 0)
                {
                    Session["ChitFundAuctionId"] = dtChitFundAuction.Tables[0].Rows[0]["id"];
                    txtAuctionNo.Text = dtChitFundAuction.Tables[0].Rows[0]["ChitNo"].ToString();
                    cChitDate.SelectedDate = Convert.ToDateTime(dtChitFundAuction.Tables[0].Rows[0]["ChitDate"]);
                    txtDedution.Text = dtChitFundAuction.Tables[0].Rows[0]["Dedution"].ToString();
                    txtMemberBalanceCount.Text = dtChitFundAuction.Tables[0].Rows[0]["MemberBalanceCount"].ToString();
                    txtInterest.Text = dtChitFundAuction.Tables[0].Rows[0]["Interest"].ToString();
                    txtSettlement.Text = dtChitFundAuction.Tables[0].Rows[0]["Settlement"].ToString();
                    txtRoundUpAmount.Text = dtChitFundAuction.Tables[0].Rows[0]["RoundUpAmount"].ToString();
                    txtFinalSettlementAmount.Text = dtChitFundAuction.Tables[0].Rows[0]["FinalSettlementAmount"].ToString();
                    ddlChitTakenBy.SelectedValue = dtChitFundAuction.Tables[0].Rows[0]["ChitTakenBy"].ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
        private void BindFormAdd(long chitFundId)
        {
            try
            {
                dtChitFundAuction = chitFundAuctionBL.GetChitAuctionByChitFundId(chitFundId);
                if (dtChitFundAuction != null && dtChitFundAuction.Tables[0].Rows.Count > 0)
                {
                    txtAuctionNo.Text = Convert.ToInt64(Convert.ToInt64(dtChitFundAuction.Tables[0].Rows[0]["ChitNo"]) +1 ).ToString();
                    cChitDate.SelectedDate = Convert.ToDateTime(dtChitFundAuction.Tables[0].Rows[0]["ChitDate"]);
                    txtMemberBalanceCount.Text = (Convert.ToInt64(dtChitFundAuction.Tables[0].Rows[0]["MemberBalanceCount"]) - 1).ToString();
                    txtDedution.Text = dtChitFundAuction.Tables[0].Rows[0]["Dedution"].ToString();
                    if (Convert.ToInt64(txtInterest.Text) > 0)
                    {
                        txtSettlement.Text = (Convert.ToInt64(dtChitFundAuction.Tables[0].Rows[0]["ChitAmount"]) - Convert.ToInt64(txtInterest.Text)).ToString();
                    }
                }
                else
                {
                    if (dtChitFundAuction != null && dtChitFundAuction.Tables[1].Rows.Count > 0)
                    {
                        txtAuctionNo.Text = 1.ToString();
                        txtMemberBalanceCount.Text = (Convert.ToInt64(dtChitFundAuction.Tables[1].Rows[0]["TotalMember"]) - 1).ToString();
                    }
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
                chitFundAuctionInfo = new ChitFundAuctionInfo();
                chitFundAuctionInfo.Id = Convert.ToInt64(Session["ChitFundAuctionId"]) != 0 ? Convert.ToInt64(Session["ChitFundAuctionId"]) : Convert.ToInt64(0);
                chitFundAuctionInfo.ChitNo = Convert.ToInt64(txtAuctionNo.Text);
                chitFundAuctionInfo.ChitDate = Convert.ToDateTime(txtDate1.Text);
                chitFundAuctionInfo.Dedution = Convert.ToInt64(txtDedution.Text);
                chitFundAuctionInfo.MemberBalanceCount = Convert.ToInt64(txtMemberBalanceCount.Text);
                chitFundAuctionInfo.Interest = Convert.ToInt64(txtInterest.Text);
                chitFundAuctionInfo.Settlement = Convert.ToInt64(txtSettlement.Text);
                chitFundAuctionInfo.RoundUpAmount = Convert.ToInt64(txtRoundUpAmount.Text);
                chitFundAuctionInfo.FinalSettlementAmount = Convert.ToInt64(txtFinalSettlementAmount.Text);
                chitFundAuctionInfo.ChitTakenBy = Convert.ToInt64(ddlChitTakenBy.SelectedValue);
                chitFundAuctionInfo.ChitFundId = Convert.ToInt64(Session["ChitFundId"]);
                chitFundAuctionInfo.CreatedBy = Convert.ToInt64(Session["UserId"]);
                chitFundAuctionInfo.CreatedDate = Convert.ToDateTime(DateTime.Now);
                chitFundAuctionInfo.ModifiedBy = Convert.ToInt64(Session["UserId"]);
                chitFundAuctionInfo.ModifiedDate = Convert.ToDateTime(DateTime.Now);
                if (chitFundAuctionBL.SaveChitFund(chitFundAuctionInfo) > 0)
                {
                    Response.Redirect("ChitFundAuctionDetailsAndMemberDetails.aspx?ChitFundId=" + Session["ChitFundId"]);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }

        }

        protected void txtDedution_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt64(txtDedution.Text) > 0 && Convert.ToInt64(txtMemberBalanceCount.Text) > 0)
                {
                    long memBal = (Convert.ToInt64(txtMemberBalanceCount.Text) - 1);
                    if (memBal == 0)
                    {
                        txtInterest.Text = "0".ToString();
                    }
                    else
                    {
                        txtInterest.Text = (Convert.ToInt64(txtDedution.Text) / (Convert.ToInt64(txtMemberBalanceCount.Text) - 1)).ToString();
                    }
                }
                dtChitFundAuction = chitFundAuctionBL.GetChitAuctionByChitFundId(Convert.ToInt64(Request.QueryString["ChitFundId"]));
                if (Convert.ToInt64(txtInterest.Text) > 0 && Convert.ToInt64(txtMemberBalanceCount.Text) > 0)
                {
                    if (dtChitFundAuction != null && dtChitFundAuction.Tables[1].Rows.Count > 0)
                    {
                        txtSettlement.Text = (Convert.ToInt64(dtChitFundAuction.Tables[1].Rows[0]["ChitAmount"]) - (Convert.ToInt64(txtInterest.Text))).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

        protected void txtInterest_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dtChitFundAuction = chitFundAuctionBL.GetChitAuctionByChitFundId(Convert.ToInt64(Request.QueryString["ChitFundId"]));
                if (Convert.ToInt64(txtInterest.Text) > 0 && Convert.ToInt64(txtMemberBalanceCount.Text) > 0)
                {
                    if (dtChitFundAuction != null && dtChitFundAuction.Tables[1].Rows.Count > 0)
                    {
                        txtSettlement.Text =(Convert.ToInt64(dtChitFundAuction.Tables[1].Rows[0]["ChitAmount"]) / (Convert.ToInt64(txtInterest.Text))).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }

        protected void txtRoundUpAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt64(txtRoundUpAmount.Text) > 0 )
                {
                    dtChitFundAuction = chitFundAuctionBL.GetChitAuctionByChitFundId(Convert.ToInt64(Request.QueryString["ChitFundId"]));
                    if (dtChitFundAuction != null && dtChitFundAuction.Tables[1].Rows.Count > 0)
                    {
                        long amount1 = Convert.ToInt64(Convert.ToInt64(txtAuctionNo.Text) * Convert.ToInt64(dtChitFundAuction.Tables[1].Rows[0]["ChitAmount"]));
                        long amount2 = Convert.ToInt64(Convert.ToInt64(txtMemberBalanceCount.Text) * Convert.ToInt64(txtRoundUpAmount.Text));
                        txtFinalSettlementAmount.Text = (Convert.ToInt64((amount1 +amount2)- Convert.ToInt64(txtRoundUpAmount.Text))).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex);
            }
        }
    }
}