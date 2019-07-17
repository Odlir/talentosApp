<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdministrarTendencias.aspx.cs" Inherits="AdministrarTendencias" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" Runat="Server">
    <center>
    <div>
        <h2>Administrar Tendencias</h2>
        <asp:GridView ID="gvTendencias" runat="server" AutoGenerateColumns="false" 
            onrowcommand="gvTendencias_RowCommand" >
            <Columns>
                <asp:BoundField HeaderText="Tendencia" DataField="Nombre" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px"/>
                <asp:BoundField HeaderText="Desctipcion" DataField="Descripcion" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="230px" />
                <asp:BoundField HeaderText="Color" DataField="Color" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                <asp:TemplateField HeaderText="Editar" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lnkEditar" Text="Editar" CommandArgument='<%#Eval("TendenciaId") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </center>
</asp:Content>

