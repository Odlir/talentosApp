#pragma checksum "C:\Users\MALENA\UPC\TALENTOS\2010-00\MODIFICACIONES-DISEÑO\TalentosXV\Talentos\Talentos_Master\ucItemGaleria.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "62300E8F5CDDEB55687BB712378821C2"
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


namespace Talentos_Master {
    
    
    public partial class ucItemGaleria : Talentos_Master.IPaginaContenida {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Image imgTalento;
        
        internal System.Windows.Controls.StackPanel buttseven;
        
        internal Talentos_Master.ButtonSeven btnVoltear;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Talentos_Master;component/ucItemGaleria.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.imgTalento = ((System.Windows.Controls.Image)(this.FindName("imgTalento")));
            this.buttseven = ((System.Windows.Controls.StackPanel)(this.FindName("buttseven")));
            this.btnVoltear = ((Talentos_Master.ButtonSeven)(this.FindName("btnVoltear")));
        }
    }
}
