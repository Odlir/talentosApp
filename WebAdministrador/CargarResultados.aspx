<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CargarResultados.aspx.cs" Inherits="CargarResultados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
    <a href="javascript:hideshow(document.getElementById('adiv'))">Carga masiva de participantes</a>
    <div id="adiv" style="font:24px bold; display: none;">
        <table>
            <tr>
                <td align="left">Institución:</td>
                <td align="left"><asp:TextBox ID="txtInstitucion" runat="server" /></td>
            </tr>
            <tr>
                <td align="left">Seleccionar archivo:</td>
                <td align="left"><asp:FileUpload ID="fuExcel" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Button ID="btnCargar" runat="server" Text="Cargar" 
                        onclick="btnCargar_Click" OnClientClick="return grabar();" /></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Visible="false" />
    <asp:Label ID="lblStatusOk" runat="server" ForeColor="Blue" Visible="false" />
    <br />
    
<script type="text/javascript">
    function hideshow(which) {
        if (!document.getElementById)
            return
        if (which.style.display == "block")
            which.style.display = "none"
        else
            which.style.display = "block"
    }

    function validate() {
        var institucion, excel;

        institucion = document.getElementById("ctl00_cphContenido_txtInstitucion").value;
        excel = document.getElementById("ctl00_cphContenido_fuExcel").value;

        if (institucion == "" || excel == "") {
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
</script>
</asp:Content>

