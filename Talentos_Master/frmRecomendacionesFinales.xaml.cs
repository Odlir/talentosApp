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
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Browser;

namespace Talentos_Master
{
    public partial class frmRecomendacionesFinales : IPaginaContenida
    {
        Session SessionActual;

        private List<TalentoUC> talentos1 = new List<TalentoUC>();
		private List<TalentoUC> talentos2 = new List<TalentoUC>();
        private List<Recomendacion> recomendaciones= new List<Recomendacion>();

        int longitudDescripcion;

        public frmRecomendacionesFinales()
        {
            InitializeComponent();

            SessionActual = Session.getInstance();
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = false;
            SessionActual.paso4 = true;

            //txtfullname.Text = SessionActual.participante.NickName;

            if (SessionActual.participante.Sexo.Equals(1))
                txtfullname.Text = "Bienvenida, " + SessionActual.participante.NickName;//string.Concat("Hola ",SessionActual.participante.NickName);
            else
                txtfullname.Text = "Bienvenido, " + SessionActual.participante.NickName;//string.Concat("Hola ",SessionActual.participante.NickName);

            recomendaciones.Clear();
            talentos1.Clear();
            talentos2.Clear();
            PopulateDataItems();
            talentosListBox.ItemsSource = talentos1;
            talentosListBox2.ItemsSource = talentos2;


            txtVolver.Text = "<< Volver  ";
            txtVolverResultados.Text = "<< Volver a Mis resultados  ";
        }
        //cargar talentos más desarrollados
        private void PopulateDataItems()
        {
            if (SessionActual.Buzon1.lstTalento.Count>=10)
            {
                int i;
                for (i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
                {
                    if (SessionActual.Buzon1.lstTalento[i].seleccionado)
                    {
                        talento1.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image, SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].Descripcion);
                        break;
                    }
                }

                for (i = i+1; i < SessionActual.Buzon1.lstTalento.Count; i++)
                {
                    if (SessionActual.Buzon1.lstTalento[i].seleccionado)
                    {
                        talento2.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image, SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].Descripcion);
                        break;
                    }
                }

                for (i = i + 1; i < SessionActual.Buzon1.lstTalento.Count; i++)
                {
                    if (SessionActual.Buzon1.lstTalento[i].seleccionado)
                    {
                         talento3.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image, SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].Descripcion);
                        break;
                    }
                }

                for (i = i + 1; i < SessionActual.Buzon1.lstTalento.Count; i++)
                {
                    if (SessionActual.Buzon1.lstTalento[i].seleccionado)
                    {
                        talento4.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image, SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].Descripcion);
                        break;
                    }
                }

                for (i = i + 1; i < SessionActual.Buzon1.lstTalento.Count; i++)
                {
                    if (SessionActual.Buzon1.lstTalento[i].seleccionado)
                    {
                        talento5.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image, SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].Descripcion);
                        break;
                    }
                }

                for (i = i + 1; i < SessionActual.Buzon1.lstTalento.Count; i++)
                {
                    if (SessionActual.Buzon1.lstTalento[i].seleccionado)
                    {
                        talento6.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image, SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].Descripcion);
                        break;
                    }
                }

                for (i = i + 1; i < SessionActual.Buzon1.lstTalento.Count; i++)
                {
                    if (SessionActual.Buzon1.lstTalento[i].seleccionado)
                    {
                        talento7.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image, SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].Descripcion);
                        break;
                    }
                }

                for (i = i + 1; i < SessionActual.Buzon1.lstTalento.Count; i++)
                {
                    if (SessionActual.Buzon1.lstTalento[i].seleccionado)
                    {
                        talento8.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image, SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].Descripcion);
                        break;
                    }
                }

                for (i = i + 1; i < SessionActual.Buzon1.lstTalento.Count; i++)
                {
                    if (SessionActual.Buzon1.lstTalento[i].seleccionado)
                    {
                        talento9.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image, SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].Descripcion);
                        break;
                    }
                }

                for (i = i + 1; i < SessionActual.Buzon1.lstTalento.Count; i++)
                {
                    if (SessionActual.Buzon1.lstTalento[i].seleccionado)
                    {
                        talento10.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image, SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].Descripcion);
                        break;
                    }
                }

                //talento2.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[1].IdTalento, SessionActual.Buzon1.lstTalento[1].Image, SessionActual.Buzon1.lstTalento[1].Nombre, SessionActual.Buzon1.lstTalento[1].Descripcion);
                //talento3.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[2].IdTalento, SessionActual.Buzon1.lstTalento[2].Image, SessionActual.Buzon1.lstTalento[2].Nombre, SessionActual.Buzon1.lstTalento[2].Descripcion);
                //talento4.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[3].IdTalento, SessionActual.Buzon1.lstTalento[3].Image, SessionActual.Buzon1.lstTalento[3].Nombre, SessionActual.Buzon1.lstTalento[3].Descripcion);
                //talento5.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[4].IdTalento, SessionActual.Buzon1.lstTalento[4].Image, SessionActual.Buzon1.lstTalento[4].Nombre, SessionActual.Buzon1.lstTalento[4].Descripcion);
                //talento6.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[5].IdTalento, SessionActual.Buzon1.lstTalento[5].Image, SessionActual.Buzon1.lstTalento[5].Nombre, SessionActual.Buzon1.lstTalento[5].Descripcion);
                //talento7.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[6].IdTalento, SessionActual.Buzon1.lstTalento[6].Image, SessionActual.Buzon1.lstTalento[6].Nombre, SessionActual.Buzon1.lstTalento[6].Descripcion);
                //talento8.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[7].IdTalento, SessionActual.Buzon1.lstTalento[7].Image, SessionActual.Buzon1.lstTalento[7].Nombre, SessionActual.Buzon1.lstTalento[7].Descripcion);
                //talento9.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[8].IdTalento, SessionActual.Buzon1.lstTalento[8].Image, SessionActual.Buzon1.lstTalento[8].Nombre, SessionActual.Buzon1.lstTalento[8].Descripcion);
                //talento10.DataContext = new TalentoUC(SessionActual.Buzon1.lstTalento[9].IdTalento, SessionActual.Buzon1.lstTalento[9].Image, SessionActual.Buzon1.lstTalento[9].Nombre, SessionActual.Buzon1.lstTalento[9].Descripcion);
            }
            //TalentoUC tal = null;


            //for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count / 2; i++)
            //{
            //    tal = new TalentoUC(SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image, SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].Descripcion);
            //    talentos1.Add(tal);
            //}

            //for (int i = SessionActual.Buzon1.lstTalento.Count / 2; i < SessionActual.Buzon1.lstTalento.Count; i++)
            //{
            //    tal = new TalentoUC(SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image, SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].Descripcion);
            //    talentos2.Add(tal);
            //}



            //-----------------------------------------------------------------------------------------//
            //TalentoUC tal = null;

            ////Para probar rápidamente, descomentar este código y comentar el otro
            ////for (int i = 0; i < 5; i++)
            ////{
            ////    tal = new TalentoUC(1, "images/talentos/images/Image1.png", "Analítico", "Las personas abiertas a los demás están dispuestas a establecer relaciones con personas distintas a ellas mismas. Son inclusivos a la hora de organizar sus reuniones, se preocupan por las personas que están aisladas o excluidas. Encuentran en cada persona una característica individual que las distingue y aprecia las relaciones.");
            ////    talentos1.Add(tal);
            ////}

            //talento1.DataContext = new TalentoUC(1, "images/talentos/images/Image1.png", "Analítico", "Las personas abiertas a los demás están dispuestas a establecer relaciones con personas distintas a ellas mismas. Son inclusivos a la hora de organizar sus reuniones, se preocupan por las personas que están aisladas o excluidas. Encuentran en cada persona una característica individual que las distingue y aprecia las relaciones.");
            //talento2.DataContext = new TalentoUC(1, "images/talentos/images/Image1.png", "Analítico", "Las personas abiertas a los demás están dispuestas a establecer relaciones con personas distintas a ellas mismas. Son inclusivos a la hora de organizar sus reuniones, se preocupan por las personas que están aisladas o excluidas. Encuentran en cada persona una característica individual que las distingue y aprecia las relaciones.");
            //talento3.DataContext = new TalentoUC(1, "images/talentos/images/Image1.png", "Analítico", "Las personas abiertas a los demás están dispuestas a establecer relaciones con personas distintas a ellas mismas. Son inclusivos a la hora de organizar sus reuniones, se preocupan por las personas que están aisladas o excluidas. Encuentran en cada persona una característica individual que las distingue y aprecia las relaciones.");
            //talento4.DataContext = new TalentoUC(1, "images/talentos/images/Image1.png", "Analítico", "Las personas abiertas a los demás están dispuestas a establecer relaciones con personas distintas a ellas mismas. Son inclusivos a la hora de organizar sus reuniones, se preocupan por las personas que están aisladas o excluidas. Encuentran en cada persona una característica individual que las distingue y aprecia las relaciones.");
            //talento5.DataContext = new TalentoUC(1, "images/talentos/images/Image1.png", "Analítico", "Las personas abiertas a los demás están dispuestas a establecer relaciones con personas distintas a ellas mismas. Son inclusivos a la hora de organizar sus reuniones, se preocupan por las personas que están aisladas o excluidas. Encuentran en cada persona una característica individual que las distingue y aprecia las relaciones.");



            ////for (int i = 5; i < 10; i++)
            ////{
            ////    //tal = new TalentoUC(29, "images/talentos/images/Image29.png", "Orientado al logro", "A estas personas les gusta empezar proyectos nuevos y poner todo su empeño para lograrlos, identifican las oportunidades y organizan los recursos necesarios para ponerlas en marcha. Lo que los define son sus ganas de enfrentar lo desconocido (o riesgos) para lograr grandes beneficios (frutos de la acción realizada) y también su capacidad de trabajar frente a la incertidumbre. Una persona emprendedora no tiene miedo de cometer errores y si los comete, aprende de ellos.");
            ////    tal = new TalentoUC(29, "images/talentos/images/Image29.png", "Orientado al logro", "Para las personas orientadas al logro, el día comienza en cero y, al final del día, deben haber logrado algo tangible para así sentirse bien consigo mismas. Esto es así tanto para los días de trabajo, los fines de semana y las vacaciones. Necesitan recibir retroalimentación positiva a menudo, les gusta trabajar solos o con personas con alto desempeño. Prefieren el desafío de trabajar en un problema aunque esto signifique cargar con la responsabilidad personal del éxito o fracaso. Al superar obstáculos, desean sentir que el resultado, es decir el éxito o el fracaso, depende de sus propias acciones. No importa cuánto se necesite un día de descanso, si no hay algún tipo de logro, se sienten insatisfechas. Es como tener un “fuego interno” que empuja a conseguir siempre más. Luego de cada logro ese fuego disminuye un momento, pero rápidamente crece, empujando a la persona a asumir el siguiente reto. Una de las ventajas es que la persona puede trabajar largas jornadas sin llegar necesariamente al burn-out (síndrome del trabajador desgastado o del desgaste profesional).");
            ////    talentos2.Add(tal);
            ////}

            //talento6.DataContext = new TalentoUC(29, "images/talentos/images/Image29.png", "Orientado al logro", "Para las personas orientadas al logro, el día comienza en cero y, al final del día, deben haber logrado algo tangible para así sentirse bien consigo mismas. Esto es así tanto para los días de trabajo, los fines de semana y las vacaciones. Necesitan recibir retroalimentación positiva a menudo, les gusta trabajar solos o con personas con alto desempeño. Prefieren el desafío de trabajar en un problema aunque esto signifique cargar con la responsabilidad personal del éxito o fracaso. Al superar obstáculos, desean sentir que el resultado, es decir el éxito o el fracaso, depende de sus propias acciones. No importa cuánto se necesite un día de descanso, si no hay algún tipo de logro, se sienten insatisfechas. Es como tener un “fuego interno” que empuja a conseguir siempre más. Luego de cada logro ese fuego disminuye un momento, pero rápidamente crece, empujando a la persona a asumir el siguiente reto. Una de las ventajas es que la persona puede trabajar largas jornadas sin llegar necesariamente al burn-out (síndrome del trabajador desgastado o del desgaste profesional).");
            //talento7.DataContext = new TalentoUC(29, "images/talentos/images/Image29.png", "Orientado al logro", "Para las personas orientadas al logro, el día comienza en cero y, al final del día, deben haber logrado algo tangible para así sentirse bien consigo mismas. Esto es así tanto para los días de trabajo, los fines de semana y las vacaciones. Necesitan recibir retroalimentación positiva a menudo, les gusta trabajar solos o con personas con alto desempeño. Prefieren el desafío de trabajar en un problema aunque esto signifique cargar con la responsabilidad personal del éxito o fracaso. Al superar obstáculos, desean sentir que el resultado, es decir el éxito o el fracaso, depende de sus propias acciones. No importa cuánto se necesite un día de descanso, si no hay algún tipo de logro, se sienten insatisfechas. Es como tener un “fuego interno” que empuja a conseguir siempre más. Luego de cada logro ese fuego disminuye un momento, pero rápidamente crece, empujando a la persona a asumir el siguiente reto. Una de las ventajas es que la persona puede trabajar largas jornadas sin llegar necesariamente al burn-out (síndrome del trabajador desgastado o del desgaste profesional).");
            //talento8.DataContext = new TalentoUC(29, "images/talentos/images/Image29.png", "Orientado al logro", "Para las personas orientadas al logro, el día comienza en cero y, al final del día, deben haber logrado algo tangible para así sentirse bien consigo mismas. Esto es así tanto para los días de trabajo, los fines de semana y las vacaciones. Necesitan recibir retroalimentación positiva a menudo, les gusta trabajar solos o con personas con alto desempeño. Prefieren el desafío de trabajar en un problema aunque esto signifique cargar con la responsabilidad personal del éxito o fracaso. Al superar obstáculos, desean sentir que el resultado, es decir el éxito o el fracaso, depende de sus propias acciones. No importa cuánto se necesite un día de descanso, si no hay algún tipo de logro, se sienten insatisfechas. Es como tener un “fuego interno” que empuja a conseguir siempre más. Luego de cada logro ese fuego disminuye un momento, pero rápidamente crece, empujando a la persona a asumir el siguiente reto. Una de las ventajas es que la persona puede trabajar largas jornadas sin llegar necesariamente al burn-out (síndrome del trabajador desgastado o del desgaste profesional).");
            //talento9.DataContext = new TalentoUC(29, "images/talentos/images/Image29.png", "Orientado al logro", "Para las personas orientadas al logro, el día comienza en cero y, al final del día, deben haber logrado algo tangible para así sentirse bien consigo mismas. Esto es así tanto para los días de trabajo, los fines de semana y las vacaciones. Necesitan recibir retroalimentación positiva a menudo, les gusta trabajar solos o con personas con alto desempeño. Prefieren el desafío de trabajar en un problema aunque esto signifique cargar con la responsabilidad personal del éxito o fracaso. Al superar obstáculos, desean sentir que el resultado, es decir el éxito o el fracaso, depende de sus propias acciones. No importa cuánto se necesite un día de descanso, si no hay algún tipo de logro, se sienten insatisfechas. Es como tener un “fuego interno” que empuja a conseguir siempre más. Luego de cada logro ese fuego disminuye un momento, pero rápidamente crece, empujando a la persona a asumir el siguiente reto. Una de las ventajas es que la persona puede trabajar largas jornadas sin llegar necesariamente al burn-out (síndrome del trabajador desgastado o del desgaste profesional).");
            //talento10.DataContext = new TalentoUC(29, "images/talentos/images/Image29.png", "Orientado al logro", "Para las personas orientadas al logro, el día comienza en cero y, al final del día, deben haber logrado algo tangible para así sentirse bien consigo mismas. Esto es así tanto para los días de trabajo, los fines de semana y las vacaciones. Necesitan recibir retroalimentación positiva a menudo, les gusta trabajar solos o con personas con alto desempeño. Prefieren el desafío de trabajar en un problema aunque esto signifique cargar con la responsabilidad personal del éxito o fracaso. Al superar obstáculos, desean sentir que el resultado, es decir el éxito o el fracaso, depende de sus propias acciones. No importa cuánto se necesite un día de descanso, si no hay algún tipo de logro, se sienten insatisfechas. Es como tener un “fuego interno” que empuja a conseguir siempre más. Luego de cada logro ese fuego disminuye un momento, pero rápidamente crece, empujando a la persona a asumir el siguiente reto. Una de las ventajas es que la persona puede trabajar largas jornadas sin llegar necesariamente al burn-out (síndrome del trabajador desgastado o del desgaste profesional).");
           
                
        }

        private void ObtenerRecomendacion_Completed(object sender, TalentosReference.ObtenerRecomedacionCompletedEventArgs e)
        {
            this.IsEnabled = true;
            ObservableCollection<TalentosReference.RecomendacionBE> list = new ObservableCollection<Talentos_Master.TalentosReference.RecomendacionBE>();

            list = e.Result;
            StringBuilder str = new StringBuilder();

            str.Append(txtDescripcion.Text);
            str.Append("\n");
            str.Append("\n");
            str.Append("A continuación, algunas recomendaciones para desarrollar más este talento.\n");

            //Recomendacion recom = null;
            for (int i = 0; i < list.Count; i++)
            {
                str.Append((i + 1).ToString());
                str.Append(". ");
                str.Append(list[i].Descripcion.ToString());
                str.Append("\n");
                str.Append("\n");
                //str.Append("\n");

                //recom = new Recomendacion(list[i].Descripcion);
                //recomendaciones.Add(recom);

            }

            txtDescripcion.Text = str.ToString();


            int l = txtDescripcion.Text.Length;

           

              

                if (txtDescripcion.Text.Length > 2500)
                {
                    brInstruc.Height = 1020;
                    brpanel.Height = 1016;
                    lnSeparacion.Y2 = 906;

                    rowPanel.Height = new GridLength(1022);
                }
                else
                {
                    if (txtDescripcion.Text.Length > 2000)
                    {
                        brInstruc.Height = 990;
                        brpanel.Height = 986;
                        lnSeparacion.Y2 = 876;

                        rowPanel.Height = new GridLength(992);
                    }
                    else
                    {
                        brInstruc.Height = 829;
                        brpanel.Height = 825;
                        lnSeparacion.Y2 = 715;

                        rowPanel.Height = new GridLength(831);
                    }
                }
            

            //int incremento1, incremento2;
            //incremento1 = incremento2 = 0;

            //if (list.Count <= 10 && longitudDescripcion <= 400)
            //{
            //    if (list.Count <= 6)
            //    {
            //        incremento2 = -100;
            //    }
            //    else
            //        incremento1 = incremento2 = 0;
            //}
            //else
            //{
            //    if (longitudDescripcion > 400)
            //        incremento1 = 100;

            //    if (list.Count > 10)
            //        incremento2 = 200;
            //}

            //brInstruc.Height = 714 + incremento1 +incremento2;
            //brpanel.Height = 710 + incremento1 + incremento2;
            //lnSeparacion.Y2 = 586 + incremento1 + incremento2;
            

            //if (list.Count > 6)
            //{
            //    brInstruc.Height = 814;
            //    brpanel.Height = 810;
            //    lnSeparacion.Y2 = 846;
            //}
            //else
            //{
            //    brInstruc.Height = 714;
            //    brpanel.Height = 710;
            //    lnSeparacion.Y2 = 586;
            //}

            //txtRecomendaciones.Text = str.ToString();

          
            
           
           

            //canvDetalle.IsOpen = true;
            canvDetalle.Visibility = Visibility.Visible;
            canvTalentos.Visibility = Visibility.Collapsed;

            //_cambiarContenido.Invoke(Enumerador.Pagina.DetalleRecomendacion);
            //recomendListBox.ItemsSource = recomendaciones;
            
            //RecomendacionTalento.IsOpen = true;
        }

        private void talentosListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //RecomendacionTalento.IsOpen = false;
            TalentosReference.WSTalentosSoapClient ws = new Talentos_Master.TalentosReference.WSTalentosSoapClient();

            //TalentoUC ucTalento = ((TalentoUC)talentosListBox.SelectedItem);
            TalentoUC ucTalento = ((TalentoUC)((sender as ListBox).SelectedItem));
            ws.ObtenerRecomedacionCompleted += new EventHandler<TalentosReference.ObtenerRecomedacionCompletedEventArgs>(ObtenerRecomendacion_Completed);

            //if ((ucTalento.id != 5) && (ucTalento.id != 20))
            //    ws.ObtenerRecomedacionAsync(20);
            //else
            
            ws.ObtenerRecomedacionAsync(ucTalento.id);//(ucTalento.id);

            imgTalento.Source = new BitmapImage(new Uri(ucTalento.source, UriKind.Relative));
            txtDescripcion.Text = ucTalento.descripcion;
            txtNombreTalento.Text = ucTalento.nombre + " >>";

            longitudDescripcion = ucTalento.descripcion.Length;

            
        }


        private void btnCerrar2_Click(object sender, RoutedEventArgs e)
        {
            //RecomendacionTalento.IsOpen = false;
        }

        private void btnCerrarJuego_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _cambiarContenido.Invoke(Enumerador.Pagina.AgradecimientoJuego);
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _cambiarContenido.Invoke(Enumerador.Pagina.AgradecimientoJuego);
        }



        private void txtVolver_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //canvDetalle.IsOpen = false;

            brInstruc.Height = 814;
            brpanel.Height = 810;
            lnSeparacion.Y2 = 690;

            canvDetalle.Visibility = Visibility.Collapsed;
            canvTalentos.Visibility = Visibility.Visible;
        }

        private void txtCerrarSession_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SessionActual = Session.deleteInstance();
            _cambiarContenido.Invoke(Enumerador.Pagina.Login);
        }

        private void brInstrcResult_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            SessionActual.paso3 = true;
            SessionActual.paso2 = false;
            SessionActual.paso1 = false;
            SessionActual.paso4 = false;
            //_cambiarContenido.Invoke(Enumerador.Pagina.ResultadosClasificacion);
            //_cambiarContenido.Invoke(Enumerador.Pagina.EnvioReporte);
        }

        private void txtVolverResultados_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SessionActual.paso3 = true;
            //_cambiarContenido.Invoke(Enumerador.Pagina.ResultadosClasificacion);
            //_cambiarContenido.Invoke(Enumerador.Pagina.EnvioReporte);
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            HtmlPage.Window.Navigate(new Uri("http://www.davidfischman.com/index.php?lang=es"), "_blank");
        }

        private void talentosListBox2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //RecomendacionTalento.IsOpen = false;
            TalentosReference.WSTalentosSoapClient ws = new Talentos_Master.TalentosReference.WSTalentosSoapClient();

            //TalentoUC ucTalento = ((TalentoUC)talentosListBox.SelectedItem);
            TalentoUC ucTalento = ((TalentoUC)((sender as ListBox).SelectedItem));
            ws.ObtenerRecomedacionCompleted += new EventHandler<TalentosReference.ObtenerRecomedacionCompletedEventArgs>(ObtenerRecomendacion_Completed);

            //if ((ucTalento.id != 5) && (ucTalento.id != 20))
            //    ws.ObtenerRecomedacionAsync(20);
            //else
            ws.ObtenerRecomedacionAsync(ucTalento.id);//(ucTalento.id);

            imgTalento.Source = new BitmapImage(new Uri(ucTalento.source, UriKind.Relative));
            txtDescripcion.Text = ucTalento.descripcion;
            txtNombreTalento.Text = ucTalento.nombre + " >>";

            longitudDescripcion = ucTalento.descripcion.Length;
        }

      
        //ver recomendaciones de un talentos
        private void talento_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsEnabled = false;
            //RecomendacionTalento.IsOpen = false;
            TalentosReference.WSTalentosSoapClient ws = new Talentos_Master.TalentosReference.WSTalentosSoapClient();

            //TalentoUC ucTalento = ((TalentoUC)talentosListBox.SelectedItem);
            //TalentoUC ucTalento = ((TalentoUC)((sender as ListBox).SelectedItem));
            ucTalento ucTal = (sender as ucTalento);
            ws.ObtenerRecomedacionCompleted += new EventHandler<TalentosReference.ObtenerRecomedacionCompletedEventArgs>(ObtenerRecomendacion_Completed);

            //if ((ucTalento.id != 5) && (ucTalento.id != 20))
            //    ws.ObtenerRecomedacionAsync(20);
            //else
            //ws.ObtenerRecomedacionAsync(ucTalento.id);//(ucTalento.id);
            ws.ObtenerRecomedacionAsync(Convert.ToInt16(ucTal.img.Tag));//(ucTalento.id);

            imgTalento.Source = new BitmapImage(new Uri(ucTal.LayoutRoot.Tag.ToString(), UriKind.Relative));//new BitmapImage(new Uri(ucTal.img.Source.ToString(), UriKind.Relative));
            txtDescripcion.Text = ucTal.recomendacionBorder.Tag.ToString();//descripcion
            txtNombreTalento.Text = ucTal.canvTalento.Tag.ToString() + " >>"; //.nombre

            longitudDescripcion = ucTal.recomendacionBorder.Tag.ToString().Length;
        }

        private void brInstruc1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!SessionActual.paso1)
            {

                SessionActual.Buzon1.activo = true;
                SessionActual.Buzon2.activo = SessionActual.Buzon3.activo = false;
                _cambiarContenido.Invoke(Enumerador.Pagina.ClasificacionDetalle);
            }
        }

        private void brInstruc2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SessionActual.lstTalentos.Count.Equals(0) && SessionActual.Buzon1.lstTalento.Count >= 10 && SessionActual.Buzon3.lstTalento.Count >= 5)
                _cambiarContenido(Enumerador.Pagina.SeleccionarPrincipal);
        }

     


      



    }
}
