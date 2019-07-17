using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Resources;
using System.IO;
using System.Windows.Markup;
using System.Windows.Controls.Theming;


namespace Talentos_Master
{
    //TODO: (ITERACION 1 - II ENTREGA) PREPARAR TEMAS CUSTOMIZABLES

    //TODO: (ITERACION 3) OPTIMIZAR TEMAS CUSTOMIZABLES : CONSULTA EN BD

    //TODO: (ITERACION 3) CORREGIR CONFIGURACIÓN DE RESOLUCIÓN DE PANTALLA

    //TODO: (ITERACION 5) OPTIMIZAR TEMAS CUSTOMIZABLES DE ACUERDO A EVENTOS PROGRAMADOS 

    public partial class App : Application
    {
        private Session SessionActual;

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;
            InitializeComponent();

            //TalentosReferenceWCF.ServiceContractClient client = new Talentos_Master.TalentosReferenceWCF.ServiceContractClient();
            //client.SkinActivoObtenerCompleted += new EventHandler<Talentos_Master.TalentosReferenceWCF.SkinActivoObtenerCompletedEventArgs>(obtenerTema_Completed);
            //client.SkinActivoObtenerAsync();

            //TalentosReference.WSTalentosSoapClient WS = new Talentos_Master.TalentosReference.WSTalentosSoapClient();
            //WS.obtenerTemaActualCompleted += new EventHandler<Talentos_Master.TalentosReference.obtenerTemaActualCompletedEventArgs>(obtenerTema_Completed);
            //WS.obtenerTemaActualAsync();

            TalentosReference.WSTalentosSoapClient WS = new Talentos_Master.TalentosReference.WSTalentosSoapClient();
            WS.SkinActivoObtenerCompleted += new EventHandler<Talentos_Master.TalentosReference.SkinActivoObtenerCompletedEventArgs>(obtenerTema_Completed);
            WS.SkinActivoObtenerAsync();
        }

        //private void obtenerTema_Completed(object sender, TalentosReference.obtenerTemaActualCompletedEventArgs e)
        //{
        //    TalentosReference.ParametroBE objParametroBE = new Talentos_Master.TalentosReference.ParametroBE();
        //    objParametroBE = e.Result;

        //    ResourceDictionary rd = null;

        //    if (objParametroBE.value.Equals("Selva"))
        //    {
        //        rd = new temas.temaSelva.Tema();
        //    }
        //    else if (objParametroBE.value.Equals("Burbujas"))
        //    {
        //        rd = new temas.temaBurbujas.Tema();
        //    }
        //    else
        //    {
        //        rd = new temas.temaTrabajo.Tema();
        //    }

        //    this.Resources = rd;

        //    this.RootVisual = new MainPage();

        //}

        private void obtenerTema_Completed(object sender, TalentosReference.SkinActivoObtenerCompletedEventArgs e)
        {
            TalentosReference.SkinBE skinbe = new Talentos_Master.TalentosReference.SkinBE();
            skinbe = e.Result;

            SessionActual = Session.getInstance();
            SessionActual.skin = skinbe;

            ResourceDictionary rd = null;

            if (skinbe.Descripcion.Equals("Selva"))
            {
                rd = new temas.temaSelva.Tema();
            }
            else if (skinbe.Descripcion.Equals("Burbujas"))
            {
                rd = new temas.temaSelva.Tema();
                // rd = new temas.temaBurbujas.Tema();
            }
            else
            {
                rd = new temas.temaSelva.Tema();
                //rd = new temas.temaTrabajo.Tema();
            }

            this.Resources = rd;

            this.RootVisual = new MainPage();
            

        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Session sesion = Session.getInstance();
        }

        private void Application_Exit(object sender, EventArgs e)
        {

        }
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }
        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}
