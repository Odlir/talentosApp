﻿#pragma checksum "C:\Users\Luis\Desktop\Reporte Temperamentos\VERSION_UPC\Talentos UPC\Talentos_Master\frmInicio.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F9D903891C2BEB37B67522DEEF1B8613"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18444
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
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


namespace Talentos_Master {
    
    
    public partial class frmInicio : Talentos_Master.IPaginaContenida {
        
        internal System.Windows.Controls.Border ppVerificarCantidad;
        
        internal System.Windows.Controls.Grid grdVerificar;
        
        internal System.Windows.Controls.TextBlock txtppVerificarCantidad;
        
        internal System.Windows.Controls.Border btnIniciarSession;
        
        internal System.Windows.Controls.TextBlock txtSinSesion;
        
        internal System.Windows.Controls.BusyIndicator BusyWindow;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Talentos_Master;component/frmInicio.xaml", System.UriKind.Relative));
            this.ppVerificarCantidad = ((System.Windows.Controls.Border)(this.FindName("ppVerificarCantidad")));
            this.grdVerificar = ((System.Windows.Controls.Grid)(this.FindName("grdVerificar")));
            this.txtppVerificarCantidad = ((System.Windows.Controls.TextBlock)(this.FindName("txtppVerificarCantidad")));
            this.btnIniciarSession = ((System.Windows.Controls.Border)(this.FindName("btnIniciarSession")));
            this.txtSinSesion = ((System.Windows.Controls.TextBlock)(this.FindName("txtSinSesion")));
            this.BusyWindow = ((System.Windows.Controls.BusyIndicator)(this.FindName("BusyWindow")));
        }
    }
}
