<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewAuction.aspx.cs" Inherits="ChitFund.NewAuction"  Theme="ChitFundTheme1"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--     <link rel="stylesheet"
          href="Style.css"/>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <div>
            <asp:Label ID="ChitNo" runat="server" Text="Auction No:" class="sr-only"></asp:Label>
        </div>
        <div>
             <asp:TextBox ID="txtAuctionNo" name="AuctionNo" placeholder="Enter Auction number" runat="server" required="required" TextMode="Number" class="form-control"></asp:TextBox>
        </div>
     <div>
            <asp:Label ID="ChitTakenBy" runat="server" Text="Chit TakenBy:" class="sr-only"></asp:Label>
        </div>
        <div>
            <%--<asp:TextBox ID="cbChitTakenBy" name="ChitTakenBy" placeholder="Enter Final Settlement Amount" runat="server" required="required" class="form-control"></asp:TextBox>--%>
            <asp:DropDownList ID="ddlChitTakenBy" AppendDataBound = true AppendDataBoundItems="true"  runat="server"></asp:DropDownList>
        </div>
         <div>
              <asp:Label ID="Dedution" runat="server" Text="Dedution :" class="sr-only"></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="txtDedution" name="Dedution" placeholder="Enter Dedution" runat="server" AutoPostBack="true"  required="required" TextMode="Number" class="form-control"  OnTextChanged="txtDedution_TextChanged"></asp:TextBox>
        </div>

         <div>
              <asp:Label ID="MemberBalanceCount" runat="server" Text="Member Balance Count:" class="sr-only"></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="txtMemberBalanceCount" name="MemberBalanceCount" placeholder="Enter Member Balance Count" AutoPostBack="true"   TextMode="Number" runat="server" required="required" class="form-control"  tooltip="Amount must contain number only"></asp:TextBox>
        </div>

        <div>
          <asp:Label ID="Interest" runat="server" Text="Interest:" class="sr-only"></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="txtInterest" name="Interest" placeholder="Enter Interest" runat="server" required="required" TextMode="Number" AutoPostBack="true"   class="form-control" OnTextChanged="txtInterest_TextChanged"></asp:TextBox>
        </div>

         <div>
             <asp:Label ID="Settlement" runat="server" Text="Settlement:" class="sr-only"></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="txtSettlement" name="Settlement" placeholder="Settlement" runat="server" required="required" TextMode="Number" AutoPostBack="true"   class="form-control"></asp:TextBox>
        </div>

        <div>
            <asp:Label ID="RoundUpAmount" runat="server" Text="RoundUp Amount:" class="sr-only"></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="txtRoundUpAmount" name="RoundUpAmount" placeholder="RoundUp Amount" runat="server" TextMode="Number" required="required" AutoPostBack="true"   class="form-control" OnTextChanged="txtRoundUpAmount_TextChanged"></asp:TextBox>
        </div>
         <div>
            <asp:Label ID="FinalSettlementAmount" runat="server" Text="Final Settlement Amount:" class="sr-only"></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="txtFinalSettlementAmount" name="FinalSettlementAmount" placeholder="Enter Final Settlement Amount" runat="server" TextMode="Number" required="required" class="form-control"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="ChitDate" runat="server" Text="Chit Date :" class="sr-only"></asp:Label>
        </div>
        <div>
        <%--<asp:Calendar ID="cChitDate"  name="ChitDate" runat="server"></asp:Calendar>--%>

            <asp:TextBox runat="server" ID="txtDate1"/>
            <ajaxtoolkit:Calendarextender runat="server" ID="cChitDate"  Format="dd/MM/yyyy" 
                                        TargetControlID="txtDate1"/>
        </div>
         <div>
             <br/>
              <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" class="btn btn-lg btn-primary "/>
        </div>
         <div>
           
        </div>
       

</asp:Content>
