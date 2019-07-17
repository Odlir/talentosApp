<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRadar.aspx.cs" Inherits="Talentos_Master.Web.frmRadar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style=" height:100%; width:100%;" >
    <div>
    <center>   
    <%--<asp:Image  ID="img" runat="server"   ImageUrl="http://chart.apis.google.com/chart?cht=r&chd=t:300,400,200,100,200,300&chs=550x200&chco=8A2EB8,BDD350,2E5CB8,CC3333&chl=Water%20Polo%20(30%)|Rugby%20(40%)|Hockey%20(20%)|Soccer%20(10%)|Dive" />--%>
    <asp:Label ID="titulo" runat="server" Text="Resultados: Desarrollo de Talentos" Font-Bold="true" Font-Names="Arial" ></asp:Label></br></br>
    <asp:Image  ID="img" runat="server" />
    <br />
    <asp:Label ID="lblIndic" runat="server" Text="Haz clic derecho sobre la imagen si deseas guardarla." Font-Names="Arial" ForeColor="Orange" Font-Size="Small"  />
    </center>
    </div>
    </form>
</body>
</html>
