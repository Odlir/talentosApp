<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Async="true" CodeFile="CargaMasivaParticipantes.aspx.cs" Inherits="CargaMasivaParticipantes" %>

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
    <div>
        <asp:GridView ID="gvParticipantes" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="DNI" DataField="DNI" />
                <asp:BoundField HeaderText="Nombres" DataField="Nombres" />
                <asp:BoundField HeaderText="Apellido Paterno" DataField="ApellidoPaterno" />
                <asp:BoundField HeaderText="Apellido Materno" DataField="ApellidoMaterno" />
                <asp:BoundField HeaderText="Sexo" DataField="Sexo" />
                <asp:BoundField HeaderText="Fecha Nacimiento" DataField="FechaNacimiento" />
                <asp:BoundField HeaderText="Nivel Instrucción" DataField="NivelInstruccion" />
                <asp:BoundField HeaderText="Cargo en la Empresa" DataField="Cargo" />
                <asp:BoundField HeaderText="Correo Electronico" DataField="CorreoElectronico" />
                <asp:BoundField HeaderText="Empresa" DataField="Institucion" />
            </Columns>
        </asp:GridView>
    </div>
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

