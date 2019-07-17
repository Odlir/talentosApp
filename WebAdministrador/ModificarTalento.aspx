<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModificarTalento.aspx.cs" Inherits="ModificarTalento" %>

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
        <h2>Edici&oacute;n de Talentos</h2>
        <table cellspacing="0px" cellpadding="4px">
            <tr>
                <td><label>Talento</label></td>
                <td><asp:TextBox ID="txtTalento" runat="server" Width="300px" MaxLength="60" CssClass="textBox" /></td>
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
            onclick="btnGrabar_Click" OnClientClick="return grabar();" />
        &nbsp;&nbsp;
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="belize-hole-flat-button" 
            onclick="btnRegresar_Click" />
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
        if (confirm("¿Esta seguro de grabar el Talento?") == true) {
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

    function isNumberKey(event) {
        if (!(!event.shiftKey //Disallow: any Shift+digit combination
            && !(event.keyCode < 48 || event.keyCode > 57) //Disallow: everything but digits
            || !(event.keyCode < 96 || event.keyCode > 105) //Allow: numeric pad digits
            || event.keyCode == 46 // Allow: delete
            || event.keyCode == 8  // Allow: backspace
            || event.keyCode == 9  // Allow: tab
            || event.keyCode == 27 // Allow: escape
            || (event.keyCode == 65 && (event.ctrlKey === true || event.metaKey === true)) // Allow: Ctrl+A
            || (event.keyCode == 67 && (event.ctrlKey === true || event.metaKey === true)) // Allow: Ctrl+C
        //Uncommenting the next line allows Ctrl+V usage, but requires additional code from you to disallow pasting non-numeric symbols
        //|| (event.keyCode == 86 && (event.ctrlKey === true || event.metaKey === true)) // Allow: Ctrl+Vpasting 
            || (event.keyCode >= 35 && event.keyCode <= 39) // Allow: Home, End
            )) {
            event.preventDefault();
        }
    }
</script>
