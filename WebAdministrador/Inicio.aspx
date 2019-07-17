<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Inicio.aspx.cs" Inherits="Inicio" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" Runat="Server">
    <table style="height:100%; width: 900px;">
    <tr>
        <td valign="middle" align="center" >    
        <table cellpadding = "10" cellspacing = "20">
            <tr>
                <td><br /><br />
                <asp:Label ID="Label1" Text ="Bienvenido al módulo" runat="server" CssClass="label"></asp:Label><br />
                <asp:Label ID="Label2" Text=" de administración de la " runat ="server"  CssClass="label2"></asp:Label><br />
                <asp:Label ID="Label3" Text=" Herramienta de Talentos" runat = "server"  CssClass="label"></asp:Label><br /><br /><br /><br /><br /><br /><br />
                </td>
                <td style="width: 180px">
                <img src="Recursos/images/business_user.png" alt="Administrador" />
                </td>
            </tr>
        </table>
        </td>
    </tr>
</table>
</asp:Content>

