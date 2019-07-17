<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModificarVirtud.aspx.cs" Inherits="ModificarVirtud" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Recursos/css/style.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <br />
    <form id="form1" runat="server" class="formulario">
    <div style="margin-left:20px; margin-top:20px;">
        <h2>Edici&oacute;n de Virtudes</h2>
        <table cellspacing="0px" cellpadding="4px">
            <tr>
                <td><label>Virtud</label></td>
                <td><asp:TextBox ID="txtVirtud" runat="server" Width="300px" MaxLength="60" CssClass="textBox" /></td>
            </tr>
            <tr>
                <td><label>Descripci&oacute;n</label></td>
                <td><asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" CssClass="textBox" 
                        Height="150px" Width="300px" MaxLength="5000" /></td>
            </tr>
            <tr>
                <td><label>Tendencia</label></td>
                <td><asp:DropDownList ID="ddlTendencia" runat="server" Width="300px" /></td>
            </tr>
            <tr>
                <td><label>Imagen</label></td>
                <td><asp:Image ID="imgImagen" runat="server" Width="236" Height="339" />
                    <br />
                    <asp:FileUpload ID="fuImagen" runat="server" />
                    &nbsp;
                    <asp:Button ID="btnVisualizar" runat="server" Text="Visualizar" 
                        onclick="btnVisualizar_Click" />
                </td>
            </tr>
        </table>
        <br />
        
        <asp:Button ID="btnGrabar" runat="server" Text="Grabar" CssClass="belize-hole-flat-button" 
            OnClientClick="return grabar();" onclick="btnGrabar_Click" />
        &nbsp;&nbsp;
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" 
            CssClass="belize-hole-flat-button" onclick="btnRegresar_Click" 
            />
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    function validate() {
        var talento, peso, descripcion;

        talento = document.getElementById("txtVirtud").value;
        descripcion = document.getElementById("txtDescripcion").value;
        
        if (talento == "" || descripcion == "") {
            alert("Debe llenar todos los campos");
            return false;
        }

        return true;
    }

    function grabar() {
        if (confirm("¿Esta seguro de grabar la Virtud?") == true) {
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
