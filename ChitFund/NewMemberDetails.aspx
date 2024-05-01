<%@ Page Title="Member" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewMemberDetails.aspx.cs" Inherits="ChitFund.NewMemberDetails"  Theme="ChitFundTheme1"%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <asp:Label ID="ChitMemberName" runat="server" Text="Member Name:" class="sr-only"></asp:Label>
        </div>
        <div>
             <asp:TextBox ID="txtChitMemberName" name="ChitMemberName" placeholder="Enter Member Name" runat="server" required="required" class="form-control"></asp:TextBox>
        </div>

         <div>
              <asp:Label ID="ChitMemberAddress" runat="server" Text="Member Address:" class="sr-only"></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="txtChitMemberAddress" name="ChitMemberAddress" placeholder="Enter Member Address" runat="server" required="required" class="form-control"></asp:TextBox>
        </div>

         <div>
              <asp:Label ID="Mobile1" runat="server" Text="Mobile:" class="sr-only"></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="txtMobile1" name="Mobile1" placeholder="Enter Mobile Number" runat="server" required="required" class="form-control" TextMode="Number" pattern="^[6789]\d{9,9}$" tooltip="Mobile number must contain number only"></asp:TextBox>
        </div>

        <div>
          <asp:Label ID="Mobile2" runat="server" Text="Alternative Mobile:" class="sr-only"></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="txtMobile2" name="Mobile2" placeholder="Enter Alternative Mobile" runat="server" required="required" class="form-control" TextMode="Number" pattern="^[6789]\d{9,9}$" tooltip="Mobile number must contain number only"></asp:TextBox>
        </div>

         <div>
             <asp:Label ID="Email" runat="server" Text="Email:" class="sr-only"></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="txtEmail" name="Email" placeholder="Enter Email" runat="server" required="required" TextMode="Email" class="form-control" title="Should be G-Mail format" pattern="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"></asp:TextBox>
        </div>

        
         <div>
             <br/>
              <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" class="btn btn-lg btn-primary "/>
        </div>
         <div>
           
        </div>
    </asp:Content>

