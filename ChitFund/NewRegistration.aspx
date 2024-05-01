<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRegistration.aspx.cs" Inherits="ChitFund.NewRegistration" %>
<!DOCTYPE html>
<script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script>
<script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script>
<link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css'
    media="screen"/>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ChitFund New Registration</title>
     <link rel="stylesheet"
          href="Style.css"/>
    <style type="text/css">
        .auto-style1 {
            width: 85px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Chit Fund</h1>
        <h3>Enter your login details</h3>
        <div>
             <label for="name"> Name: </label>
        </div>
        <div>
             <input type="text" id="txtname" name="txtname" placeholder="Enter your Username" required="required"/> 
        </div>

         <div>
             <label for="userName"> User name: </label>
        </div>
        <div>
             <input type="text" id="userName" name="userName" placeholder="Enter your Username" required="required"/> 
        </div>

         <div>
             <label for="password">  Password: </label>
        </div>
        <div>
             <input id="password"  type="password" name="password" placeholder="Enter your Password" required="required" tooltip="Password must contain: Minimum 8 characters at-least 1 Alphabet and 1 Number"  CssClass="form-control" title="Minimum 8 characters at-least 1 Alphabet and 1 Number" pattern="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{3,}$"/> 
        </div>

         <div>
             <label for="chitOwnerMail">  G-Mail: </label>
        </div>
        <div>
             <input type="text" id="chitOwnerMail" TextMode="Email" name="chitOwnerMail" placeholder="Enter your G-Mail" required="required" title="Should be G-Mail format" pattern="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"/> 
        </div>

        <div>
             <label for="mobile">  Mobile number: </label>
        </div>
        <div>
             <input type="text" id="mobile" name="mobile" placeholder="Enter your mobile number" required="required" pattern="^[6789]\d{9,9}$" title="Phone number with 6-9 and remaing 9 digit with 0-9"/> 

        </div>
         <div>
             <br/>
              <asp:Button ID="Submit"  Text="Submit" runat="server" OnClick="Submit_Click" class="auto-style1" />
        </div>
         <div>
             <label id="error">  </label>
        </div>
       
    </form>
</body>
</html>
