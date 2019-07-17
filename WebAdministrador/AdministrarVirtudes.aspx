<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdministrarVirtudes.aspx.cs" Inherits="AdministrarVirtudes" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" Runat="Server">
    <center>
    <div>
        <h2>Administrar Virtudes</h2>
        <asp:GridView ID="gvVirtudes" runat="server" AutoGenerateColumns="false"
            onrowcommand="gvVirtudes_RowCommand">
            <Columns>
                <asp:BoundField HeaderText="Virtud" DataField="Nombre" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px"/>
                <asp:BoundField HeaderText="Tendencia" DataField="Tendencia" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px"/>
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

