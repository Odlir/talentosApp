<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdministrarTalentos.aspx.cs" Inherits="AdministrarTalentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" Runat="Server">
    <center>
    <div>
        <h2>Administrar Talentos</h2>
        <asp:GridView ID="gvTalentos" runat="server" AutoGenerateColumns="false" 
            onrowcommand="gvTalentos_RowCommand">
            <Columns>
                <asp:BoundField HeaderText="Talento" DataField="Nombre" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px"/>
                <asp:BoundField HeaderText="Tendencia" DataField="Tendencia" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px"/>
                <asp:BoundField HeaderText="Tipo Talento" DataField="TipoTalento" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" />
                <asp:TemplateField HeaderText="Editar" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lnkEditar" Text="Editar" CommandArgument='<%#Eval("idTalento") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </center>
</asp:Content>

