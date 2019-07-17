<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModificarTendencia.aspx.cs" Inherits="ModificarTendencia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Recursos/css/style.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server" class="formulario">
    <div style="margin-left:20px; margin-top:20px;">
        <h2>Edici&oacute;n de Tendencias</h2>
        <table cellspacing="0px" cellpadding="4px">
            <tr>
                <td><label>Tendencia</label></td>
                <td><asp:TextBox ID="txtTendencia" runat="server" Width="300px" MaxLength="150" CssClass="textBox" /></td>
            </tr>
            <tr>
                <td><label>Descripci&oacute;n</label></td>
                <td><asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" CssClass="textBox" 
                        Height="150px" Width="300px" MaxLength="150" /></td>
            </tr>
            <tr>
                <td><label>Color</label></td>
                <td><asp:TextBox ID="txtColor" runat="server" CssClass="textBox" Width="300px" MaxLength="150" /></td>
            </tr>
        </table>
        <br />
        
        <asp:Button ID="btnGrabar" runat="server" Text="Grabar" 
            CssClass="belize-hole-flat-button" OnClientClick="return grabar();" 
            onclick="btnGrabar_Click" />
        &nbsp;&nbsp;
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" 
            CssClass="belize-hole-flat-button" onclick="btnRegresar_Click" />
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    function validate() {
        var talento, peso, descripcion;

        talento = document.getElementById("txtTalento").value;
        descripcion = document.getElementById("txtDescripcion").value;
        
        if (talento == "" || descripcion == "") {
            alert("Debe llenar todos los campos");
            return false;
        }

        return true;
    }

    function grabar() {
        if (confirm("¿Esta seguro de grabar la Tendencia?") == true) {
            if (validate()) {
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }
</script>
