<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewMemberAssignToChitFund.aspx.cs" Inherits="ChitFund.NewMemberAssignToChitFund"  Theme="ChitFundTheme1"%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
<html>
<head>
    <title>Member Assign to ChitFund</title>
    <meta charset="utf-8" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="ChitFund.ChitFundDetails" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
    <script src="https://unpkg.com/gijgo@1.9.14/js/gijgo.js" type="text/javascript"></script>
    <link href="https://unpkg.com/gijgo@1.9.14/css/gijgo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12">
                <table id="grid"></table>
            </div>
        </div>
    </div>
    
    <br />  
        <div id="mainContainer" class="container">  
            <div class="shadowBox">  
                <div class="page-container">  
                    <div class="container">  
                        <div >  
                            <p class="text-sm-center" style="height:auto; font-size: x-large; color: #008000; font-style: normal; font-weight: bold; font-variant: normal;" >Select Member to Assign Chit Fund</p>  
                            <%--<asp:Label ID="lChitFundDetails" Text="Chit Fund Details" runat="server" />--%>
                            <span class="text-info"> </span>  
                        </div>  
                        <div class="row">  
                            <div class="col-lg-12 ">  
                                <div class="table-responsive">  
                                    <asp:GridView ID="grdMemberDetails" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" AutoGenerateSelectButton="true" DataKeyNames="id" EmptyDataText="There are no data records to display." OnSelectedIndexChanged="grdMemberDetails_SelectedIndexChanged" OnPreRender="grdMemberDetails_PreRender" >  
                                        <Columns>  
                                            <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" Visible ="true" />  
                                            <asp:BoundField DataField="ChitFundId" HeaderText="ChitFundId" ReadOnly="True" SortExpression="ChitFundId" Visible ="false" />  
                                            <asp:BoundField DataField="ChitMemberName" HeaderText="Chit Member Name" SortExpression="ChitMemberName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />  
                                            <asp:BoundField DataField="ChitMemberAddress" HeaderText="Chit Member Address" SortExpression="ChitMemberAddress" ItemStyle-CssClass="visible-xs" HeaderStyle-CssClass="visible-xs" />  
                                            <asp:BoundField DataField="Mobile1" HeaderText="Mobile" SortExpression="Mobile1" HeaderStyle-CssClass="visible-md" ItemStyle-CssClass="visible-md" />  
                                            <asp:BoundField DataField="Mobile2" HeaderText="Alternative Mobile" SortExpression="Mobile2" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />  
                                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />  
                                        </Columns>  
                                    </asp:GridView>  
                                </div>  
                            </div>  
                        </div>  
                    </div>  
                </div>  
            </div>  
            <asp:Button ID="AddNewAuction" runat="server" Text="New Member" OnClick="AddNewAuction_Click"/>
        </div>  
</body>
</html>
</asp:Content>
