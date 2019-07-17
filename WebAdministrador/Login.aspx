<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="Recursos/css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
<div id="wrapper">
<div id="header"></div>

	<div id="page">
		<div id="bgtop">
			<div id="bgbottom">
	<table style="width:500px; height:404px; background-image:url('Recursos/images/login.png')" 
          border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td height="40">&nbsp;</td>
      </tr>
      <tr>
        <td height="249"><table width="77%" border="0" align="center">
        <tr>
        
        <td class="main" colspan="2" valign="bottom">
            &nbsp;</td>
        </tr>
          <tr>
            <td class="style2"><span class="main" 
                    style="font-family: 'Arial Unicode MS'; font-size: small; color: #666666">
                <br />
                <br />
                Usuario:</span></td>
            <td class="style3">
                <br />
                <br />             
                <asp:TextBox ID="txtUsuario" runat="server" Width="150px" ></asp:TextBox>
                <br />
            </td>
          </tr>
          <tr>
            <td class="style2" style="font-family: 'Arial Unicode MS'; color: #808080">
                <span class="main" 
                    style="font-family: 'Arial Unicode MS'; font-size: small; color: #666666; font-weight: normal">Contraseña:</span></td>
            <td class="style3"><asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" 
                    Width="150px"></asp:TextBox>
                <br />
                <br />
              </td>
          </tr>
          <tr>
            <td colspan="2" align="center">
                <label>
             <asp:Button ID="btnIngresar" runat="server" Text="Login" CssClass="main" 
                    OnClick="btnIngresar_Click" Height="22px" Width="74px"
                    />
            </label>
              </td>
          </tr>
          <tr>
            <td colspan="2" align="left" 
                  style="font-family: 'Arial Unicode MS'; font-size: x-small; font-weight: normal; color: #FF0000;">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsuario" ErrorMessage="Por favor ingrese su usuario" Display="Dynamic"></asp:RequiredFieldValidator>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txtContrasena" ErrorMessage="Por favor ingrese su contraseña." Display="Dynamic">
                </asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="style1" colspan="2" 
                    style="font-family: 'Arial Unicode MS'; font-size: x-small"><asp:Label ID="lblMensajeError" runat="server" CssClass="main"></asp:Label></td>
            </tr>
        </table>
      </tr>
      
    </table>
	</div>
</div>
</div>
	<div id="footer" style="width: 100%; position: absolute; top: 531px; left: 0px;">
		<p><a href="http://www.upc.edu.pe/" shape="default">Universidad Peruana de Ciencias 
            Aplicadas</a></p>
	</div>
	<!-- end #footer -->
</div>
<!-- end #wrapper -->


    </form>

</body>
</html>
