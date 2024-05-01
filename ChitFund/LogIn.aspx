<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="ChitFund.LogIn" Theme="ChitFundTheme1"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>

    <form id="form1" runat="server" name="LogIn">
            <div class="container">
            <div class="row">
                <div class="col-lg-3">
                    &nbsp;
                </div>
                <div class="col-lg-6">
                    <form  class="form-signin">
                        &nbsp;
                        &nbsp;                                            
                        <h1 class="form-signin-heading" style="text-align: center">ChitFund Login</h1>
                        <h2 class="form-signin-heading" style="text-align: center">Enter your login credentials</h2>
                        &nbsp;
                        <asp:Label ID="lInputEmail" Text="Username" runat="server" class="sr-only" />
                        <asp:TextBox ID="txtUserName" name="txtUserName"  runat="server" Text="3am" class="form-control" required="required" autofocus="autofocus"></asp:TextBox>

                        &nbsp;
                         <asp:Label ID="lInputPassword" Text="Password" runat="server" class="sr-only" />
                         <asp:TextBox ID="txtPassword" name="txtPassword"  TextMode="Password" runat="server" Text="ma3"  class="form-control" required="required" ></asp:TextBox>
                        <div class="checkbox"/>
                        <label>
                            <input type="checkbox" value="remember-me" />
                            Remember me
                        </label>
                        <%--<input class="btn btn-lg btn-primary btn-block" runat="server" type="submit" value="Sign In" onclick="LoginSubmit" />--%>
                       <br/>
                        <br/>
                            <asp:Button runat="server" id="btnLogin" Text="Log In" OnClick="btnLogin_Click1" />
                         &nbsp;&nbsp;
                         <asp:Button runat="server" id="btnNewRegistration" Text="New Registration" OnClick="btnNewRegistration_Click" />
                    </form>
                     
                </div>
                <div class="col-lg-3">
                </div>
            </div>
        </div>
</form>

</body>
</html>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
<script src="js/bootstrap.min.js"></script>


