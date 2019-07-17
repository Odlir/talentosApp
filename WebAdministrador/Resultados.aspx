<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Resultados.aspx.cs" Inherits="Resultados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
    <div>
        <h2>Resultados de los Participantes</h2>
        <fieldset style="width:260px; text-align:left;">
            <legend>Busqueda</legend>
            <table>
                <tr>
                    <td><label>Fecha Inicio:</label></td>
                    <td><asp:TextBox ID="txtFechaInicio" runat="server" CssClass="tcal" /></td>
                </tr>
                <tr>
                    <td><label>Fecha Fin:</label></td>
                    <td><asp:TextBox ID="txtFechaFin" runat="server" CssClass="tcal" /></td>
                </tr>
                <tr>
                    <td><label>Empresa:</label></td>
                    <td><asp:TextBox ID="txtEmpresa" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                            onclick="btnBuscar_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
        
        <br /><br />
        <div>
            <asp:GridView ID="gvResultados" runat="server" AutoGenerateColumns="false" 
                onrowcommand="gvResultados_RowCommand">
                <Columns>
                    <asp:BoundField DataField="DNI" HeaderText="DNI" />
                    <asp:BoundField DataField="Nombres" HeaderText="Participante" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                    <asp:BoundField DataField="Empresa" HeaderText="Empresa" />
                    <asp:BoundField DataField="CorreoElectronico" HeaderText="Correo Electronico" />
                    <asp:BoundField DataField="CodigoEvaluacion" HeaderText="Código Evaluacion" />
                    <asp:TemplateField HeaderText="Reporte">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkReporte" runat="server" Text="Reporre" CommandArgument='<%#Eval("CodigoEvaluacion")%>' CommandName='<%#Eval("DNI") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <br />
        <%--<asp:Button ID="btnImportar" runat="server" Text="Importar Resultados" 
            onclick="btnImportar_Click"/>--%>
    </div>
    </asp:Content>

