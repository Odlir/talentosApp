#pragma checksum "C:\Users\MALENA\UPC\TALENTOS\2010-00\MODIFICACIONES-DISEÑO\TalentosXVI\ModificarParaResolucion\Talentos\Talentos_Master\frmResultadoEstadistico.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3D06B46DAFB59DD1115FC6C436FFD5C9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;
using Talentos_Master;
using Visifire.Charts;


namespace Talentos_Master {
    
    
    public partial class frmResultadoEstadistico : Talentos_Master.IPaginaContenida {
        
        internal System.Windows.Controls.TextBlock txtCerrarSession;
        
        internal System.Windows.Documents.Run txtfullname;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel Instruccion;
        
        internal System.Windows.Controls.Border brHeader;
        
        internal System.Windows.Controls.Border brInstruc1;
        
        internal System.Windows.Controls.TextBlock txtInstruccion1;
        
        internal System.Windows.Shapes.Line line1;
        
        internal System.Windows.Controls.Border brInstruc2;
        
        internal System.Windows.Controls.TextBlock txtInstruccion2;
        
        internal System.Windows.Shapes.Line Line2;
        
        internal System.Windows.Controls.Border brInstrcResult;
        
        internal System.Windows.Controls.TextBlock txtInstruccionResult;
        
        internal System.Windows.Shapes.Line Line3;
        
        internal System.Windows.Controls.Border brInstruccion3;
        
        internal System.Windows.Controls.TextBlock txtInstruccion3;
        
        internal System.Windows.Documents.Run textInfo;
        
        internal System.Windows.Documents.Run textInfo2;
        
        internal System.Windows.Documents.Run textInfo3;
        
        internal System.Windows.Documents.Run txtTitle1;
        
        internal System.Windows.Documents.Run txtTitle2;
        
        internal System.Windows.Controls.Canvas canv;
        
        internal Visifire.Charts.Chart MyChart;
        
        internal System.Windows.Documents.Run txtTitulo;
        
        internal System.Windows.Documents.Run txtOrientacion;
        
        internal System.Windows.Controls.ToolTip toolTip;
        
        internal System.Windows.Media.ImageBrush imgTalento;
        
        internal System.Windows.Controls.ListBox talentosListBox;
        
        internal System.Windows.Controls.Canvas Zoom;
        
        internal System.Windows.Controls.Button btnCerrarZoom;
        
        internal Talentos_Master.ucTalentoLupita TalentoZoom;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Talentos_Master;component/frmResultadoEstadistico.xaml", System.UriKind.Relative));
            this.txtCerrarSession = ((System.Windows.Controls.TextBlock)(this.FindName("txtCerrarSession")));
            this.txtfullname = ((System.Windows.Documents.Run)(this.FindName("txtfullname")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.Instruccion = ((System.Windows.Controls.StackPanel)(this.FindName("Instruccion")));
            this.brHeader = ((System.Windows.Controls.Border)(this.FindName("brHeader")));
            this.brInstruc1 = ((System.Windows.Controls.Border)(this.FindName("brInstruc1")));
            this.txtInstruccion1 = ((System.Windows.Controls.TextBlock)(this.FindName("txtInstruccion1")));
            this.line1 = ((System.Windows.Shapes.Line)(this.FindName("line1")));
            this.brInstruc2 = ((System.Windows.Controls.Border)(this.FindName("brInstruc2")));
            this.txtInstruccion2 = ((System.Windows.Controls.TextBlock)(this.FindName("txtInstruccion2")));
            this.Line2 = ((System.Windows.Shapes.Line)(this.FindName("Line2")));
            this.brInstrcResult = ((System.Windows.Controls.Border)(this.FindName("brInstrcResult")));
            this.txtInstruccionResult = ((System.Windows.Controls.TextBlock)(this.FindName("txtInstruccionResult")));
            this.Line3 = ((System.Windows.Shapes.Line)(this.FindName("Line3")));
            this.brInstruccion3 = ((System.Windows.Controls.Border)(this.FindName("brInstruccion3")));
            this.txtInstruccion3 = ((System.Windows.Controls.TextBlock)(this.FindName("txtInstruccion3")));
            this.textInfo = ((System.Windows.Documents.Run)(this.FindName("textInfo")));
            this.textInfo2 = ((System.Windows.Documents.Run)(this.FindName("textInfo2")));
            this.textInfo3 = ((System.Windows.Documents.Run)(this.FindName("textInfo3")));
            this.txtTitle1 = ((System.Windows.Documents.Run)(this.FindName("txtTitle1")));
            this.txtTitle2 = ((System.Windows.Documents.Run)(this.FindName("txtTitle2")));
            this.canv = ((System.Windows.Controls.Canvas)(this.FindName("canv")));
            this.MyChart = ((Visifire.Charts.Chart)(this.FindName("MyChart")));
            this.txtTitulo = ((System.Windows.Documents.Run)(this.FindName("txtTitulo")));
            this.txtOrientacion = ((System.Windows.Documents.Run)(this.FindName("txtOrientacion")));
            this.toolTip = ((System.Windows.Controls.ToolTip)(this.FindName("toolTip")));
            this.imgTalento = ((System.Windows.Media.ImageBrush)(this.FindName("imgTalento")));
            this.talentosListBox = ((System.Windows.Controls.ListBox)(this.FindName("talentosListBox")));
            this.Zoom = ((System.Windows.Controls.Canvas)(this.FindName("Zoom")));
            this.btnCerrarZoom = ((System.Windows.Controls.Button)(this.FindName("btnCerrarZoom")));
            this.TalentoZoom = ((Talentos_Master.ucTalentoLupita)(this.FindName("TalentoZoom")));
        }
    }
}
