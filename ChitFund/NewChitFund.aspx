<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewChitFund.aspx.cs" Inherits="ChitFund.NewChitFund"  Theme="ChitFundTheme1"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--     <link rel="stylesheet"
          href="Style.css"/>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <asp:Label ID="ChitFundName" runat="server" Text="ChitFund Name:" class="sr-only"></asp:Label>
        </div>
        <div>
             <asp:TextBox ID="txtChitFundName" name="ChitFundName" placeholder="Enter your ChitFund Name" runat="server" required="required" class="form-control"></asp:TextBox>
        </div>

         <div>
              <asp:Label ID="ChitAddress" runat="server" Text="Chit Address:" class="sr-only"></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="txtChitAddress" name="ChitAddress" placeholder="Enter your Chit Address" runat="server" required="required" class="form-control"></asp:TextBox>
        </div>

         <div>
              <asp:Label ID="ChitAmount" runat="server" Text="Chit Amount:" class="sr-only"></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="txtChitAmount" name="ChitAmount" placeholder="Enter Chit Amount" runat="server" required="required" class="form-control" TextMode="Number" tooltip="Amount must contain number only"></asp:TextBox>
        </div>

        <div>
          <asp:Label ID="TotalMember" runat="server" Text="Total Member:" class="sr-only"></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="txtTotalMember" name="TotalAmount" placeholder="Enter your Total Member" runat="server" required="required" TextMode="Number" class="form-control"></asp:TextBox>
        </div>

         <div>
             <asp:Label ID="TotalAmount" runat="server" Text="Total Amount:" class="sr-only"></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="txtTotalAmount" name="TotalAmount" placeholder="Enter your Total Amount" runat="server" required="required" TextMode="Number" class="form-control"></asp:TextBox>
        </div>

        <div>
            <asp:Label ID="Interval" runat="server" Text="Interval:" class="sr-only"></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="txtInterval" name="Interval" placeholder="Enter the chit interval" runat="server" required="required" TextMode="Number" class="form-control"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="StartDate" runat="server" Text="Start Date:" class="sr-only"></asp:Label>
        </div>
        <div>
        <%--<asp:Calendar ID="cStartDate"  name="StartDate"  runat="server"></asp:Calendar>--%>
            <asp:TextBox runat="server" ID="txtDate1"  />
            <ajaxtoolkit:Calendarextender runat="server" ID="cStartDate"  Format="dd/MM/yyyy" 
                                        TargetControlID="txtDate1"/>
        </div>
         <div>
              <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" class="btn btn-lg btn-primary "/>
        </div>
         <div>
           
        </div>
       

</asp:Content>
