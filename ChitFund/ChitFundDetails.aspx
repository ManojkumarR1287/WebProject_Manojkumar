<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChitFundDetails.aspx.cs" Inherits="ChitFund.ChitFundDetails" Trace="true"  Theme="ChitFundTheme1"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<!DOCTYPE html>
<html>
<head>
    <title>jQuery Grid Inline Editing</title>
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
    <%--<script type="text/javascript">
        $(document).ready(function () {
            var grid, countries;
            grid = $('#grid').grid({
                dataSource: '/Players/Get',
                uiLibrary: 'bootstrap4',
                primaryKey: 'ID',
                inlineEditing: { mode: 'command' },
                columns: [
                    { field: 'ID', width: 44 },
                    { field: 'Name', editor: true },
                    { field: 'CountryName', title: 'Nationality', type: 'dropdown', editField: 'CountryID', editor: { dataSource: '/Locations/GetCountries', valueField: 'id' } },
                    { field: 'DateOfBirth', type: 'date', editor: true },
                    { field: 'IsActive', title: 'Active', type: 'checkbox', editor: true, width: 90, align: 'center' }
                ],
                pager: { limit: 5 }
            });
            grid.on('rowDataChanged', function (e, id, record) {
                // Clone the record in new object where you can format the data to format that is supported by the backend.
                var data = $.extend(true, {}, record);
                // Format the date to format that is supported by the backend.
                data.DateOfBirth = gj.core.parseDate(record.DateOfBirth, 'mm/dd/yyyy').toISOString();
                // Post the data to the server
                $.ajax({ url: '/Players/Save', data: { record: data }, method: 'POST' })
                    .fail(function () {
                        alert('Failed to save.');
                    });
            });
            grid.on('rowRemoving', function (e, $row, id, record) {            
                if (confirm('Are you sure?')) {
                    $.ajax({ url: '/Players/Delete', data: { id: id }, method: 'POST' })
                        .done(function () {
                            grid.reload();
                        })
                        .fail(function () {
                            alert('Failed to delete.');
                        });
                }
            });
        });
    </script>--%>
    <br />  
        <div id="mainContainer" class="container">  
            <div class="shadowBox">  
                <div class="page-container">  
                    <div class="container">  
                        <div >  
                            <p class="text-sm-center" style="height:auto; font-size: x-large; color: #008000; font-style: normal; font-weight: bold; font-variant: normal;" >Chit Fund Details</p>  
                            <%--<asp:Label ID="lChitFundDetails" Text="Chit Fund Details" runat="server" />--%>
                            <span class="text-info"> </span>  
                        </div>  
                        <div class="row">  
                            <div class="col-lg-12 ">  
                                <div class="table-responsive" style= "overflow-x:scroll; Overflow:scroll; max-height: 400px; width: 900px">  
                                    <asp:GridView ID="grdChitFundDetails" runat="server" Width="100%"  AutoGenerateSelectButton="True" AutoGenerateEditButton="true" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="id" EmptyDataText="There are no data records to display."   OnSelectedIndexChanged="grdChitFundDetails_SelectedIndexChanged" OnRowEditing="grdChitFundDetails_RowEditing1" OnPreRender="grdChitFundDetails_PreRender">  
                                        <Columns>  
                                            <asp:BoundField DataField="id" HeaderText="id" ReadOnly="false" SortExpression="id" Visible ="true" HeaderStyle-Width="0px"/>  
                                            <asp:BoundField DataField="ChitName" HeaderText="Chit Name" SortExpression="ChitName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />  
                                            <asp:BoundField DataField="ChitAddress" HeaderText="Chit Address" SortExpression="ChitAddress" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />  
                                            <asp:BoundField DataField="ChitAmount" HeaderText="Chit Amount" SortExpression="ChitAmount" ItemStyle-CssClass="visible-xs" HeaderStyle-CssClass="visible-xs" />  
                                            <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" SortExpression="TotalAmount" HeaderStyle-CssClass="visible-md" ItemStyle-CssClass="visible-md" />  
                                            <asp:BoundField DataField="TotalMember" HeaderText="Total Member" SortExpression="TotalMember   " ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />  
                                            <asp:BoundField DataField="Interval" HeaderText="Interval" SortExpression="Interval" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs" />  
                                            <asp:BoundField DataField="StartDate" HeaderText="Start Date" SortExpression="StartDate" HeaderStyle-CssClass="visible-md" ItemStyle-CssClass="visible-md" />  
                                            <asp:BoundField DataField="ChitOwnerName" HeaderText="Chit Owner Name" SortExpression="ChitOwnerName" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />  
                                           
                                        </Columns>  
                                    </asp:GridView>  
                                </div>  
                            </div>  
                        </div>  
                    </div>  
                </div>  
            </div>  
        </div>  
    <br/>
    <div id="ButtonContainer" class="container">  
         <table id="grid1">
             <tr>
                 <td style="width:100px"><asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click"/> </td>
                 <td><asp:Button ID="btnTrace" runat="server" Text="Trace" OnClick="btnTrace_Click"/></td>
             </tr>
         </table>
       
        </div>
</body>
</html>
        </asp:Content>