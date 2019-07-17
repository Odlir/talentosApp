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
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Talentos_Master.TalentosReference;
using System.Collections.ObjectModel;
using System.Windows.Browser;


namespace Talentos_Master
{
    /// <summary>
    ///ESTA PÁGINA PERMITE MOSTRAR LOS PRIMEROS RESULTADOS DE UN JUGADOR
    ///PODRA VER LAS TENDENCIAS  Y LOS TALENTOS QUE POSEE ASOCIADOS A CADA TENDENCIA
    ///<summary>
    public partial class frmResultadosClasificacion  : IPaginaContenida
    {

        //TODO: (ITERACION 1 - II ENTREGA) Solo se deben mostrar las tendencias asociadas al Buzón 1(colocar un gif en el centro)
        //TODO: (ITERACION 1 - II ENTREGA) Los talentos que se muestran por tendencia deben estar alineados y no dentro de un carrousel pequeño.
        //TODO: (ITERACION 1 - II ENTREGA) Se deben usar colores que contrasten para que resalten los nombres de cada una de las tendencias.
        //TODO: (ITERACION 1 - II ENTREGA) Ordenar la presentación de los resultados.


        //TODO: (ITERACION 2 - I ENTREGA) Incluir en la Secuencia del Juego

        //TODO: (ITERACION 2 - II ENTREGA)Corregir Bugs

        //TODO: (ITERACION 3)  Imágenes de resumen de tendencias deben ser más grandes

        //TODO: (ITERACION 5)  Se deben mostrar las tendencias asociadas a los 3 buzones
       
        

      

       

        //private TerminoGlobal termino;

        private Session SessionActual;

        int cont1, cont2, cont3, cont4, cont5, cont6;

        private List<TalentitoMapaUC> ejecucion = new List<TalentitoMapaUC>();

        private List<TalentitoMapaUC> logro = new List<TalentitoMapaUC>();

        private List<TalentitoMapaUC> innovacion = new List<TalentitoMapaUC>();

        private List<TalentitoMapaUC> liderazgo = new List<TalentitoMapaUC>();

        private List<TalentitoMapaUC> personas = new List<TalentitoMapaUC>();

        private List<TalentitoMapaUC> estructura = new List<TalentitoMapaUC>();

        TranslateTransform img2transform = new TranslateTransform();

        public frmResultadosClasificacion()
        {
            InitializeComponent();

            // Graba la posición del centro
            
            ////buzonColores = BuzonGlobal.getInstance();
            SessionActual = Session.getInstance();
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = true;
            SessionActual.paso4 = false;

            ReasignarUrlsTalentos();

            SessionActual.Buzon1.activo = false;
            SessionActual.Buzon3.activo = false;
            ///termino = TerminoGlobal.getInstance();
            ///
            ///hacer esto al final del juego
            //MoveTalentosFromBuzon1ToBuzon2();
            //MoveTalentosFromBuzon3ToBuzon2();
            LoadTendenciasBuzon1();
            LoadTendenciasBuzon3();
           
          

            if (SessionActual.Buzon1.activo)
            {
                txtTitulo.Text = "Talentos";// que";
                txtTitulo2.Text = "más desarrollados";//me identifican";
                //buzonColores.b1 = buzonColores.b2 = buzonColores.b3 = false;
                SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = SessionActual.Buzon3.activo = false;
                //buzonColores.b1 = true;
                SessionActual.Buzon1.activo = true;
               
                //ppColores3.IsOpen = false;
                //ppColores1.IsOpen = true;
                ppColores3.Visibility = Visibility.Collapsed;
                ppColores1.Visibility = Visibility.Visible;
                canvasParte2.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (SessionActual.Buzon3.activo)
                {
                    txtTitulo3.Text = "Talentos" ;//que";
                    txtTitulo3_2.Text = "menos desarrollados";//me identifican";
                    //buzonColores.b1 = buzonColores.b2 = buzonColores.b3 = false;
                    SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = SessionActual.Buzon3.activo = false;
                    //buzonColores.b1 = true;
                    SessionActual.Buzon3.activo = true;
                   
                    //ppColores1.IsOpen = false;
                    ppColores1.Visibility = Visibility.Collapsed;
                    canvasParte2.Visibility = Visibility.Collapsed;
                    //ppColores3.IsOpen = true;
                    ppColores3.Visibility = Visibility.Visible;
                }
            }

            //ppColores1.IsOpen = true;
            //HtmlPage.Window.Invoke("ShowAdvertencia");

            //var window = new frmAdvertencia();
            //window.ShowDialog();
        }


        private void ReasignarUrlsTalentos()
        {
            for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
            {
                if (SessionActual.Buzon1.lstTalento[i].Image.Contains("a.png"))
                {
                    string urlEspalda = SessionActual.Buzon1.lstTalento[i].Image;
                    string urlFrente = SessionActual.Buzon1.lstTalento[i].Example.Replace("a.png", ".png");
                    urlFrente = urlFrente.Replace("talentos/example", "talentos/images");

                    SessionActual.Buzon1.lstTalento[i].Example = urlEspalda;
                    SessionActual.Buzon1.lstTalento[i].Image = urlFrente;
                }


            }

            for (int i = 0; i < SessionActual.Buzon2.lstTalento.Count; i++)
            {
                if (SessionActual.Buzon2.lstTalento[i].Image.Contains("a.png"))
                {
                    string urlEspalda = SessionActual.Buzon2.lstTalento[i].Image;
                    string urlFrente = SessionActual.Buzon2.lstTalento[i].Example.Replace("a.png", ".png");
                    urlFrente = urlFrente.Replace("talentos/example", "talentos/images");

                    SessionActual.Buzon2.lstTalento[i].Example = urlEspalda;
                    SessionActual.Buzon2.lstTalento[i].Image = urlFrente;
                }
            }

            for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
            {
                if (SessionActual.Buzon3.lstTalento[i].Image.Contains("a.png"))
                {
                    string urlEspalda = SessionActual.Buzon3.lstTalento[i].Image;
                    string urlFrente = SessionActual.Buzon3.lstTalento[i].Example.Replace("a.png", ".png");
                    urlFrente = urlFrente.Replace("talentos/example", "talentos/images");

                    SessionActual.Buzon3.lstTalento[i].Example = urlEspalda;
                    SessionActual.Buzon3.lstTalento[i].Image = urlFrente;
                }
            }

        }
        
        private void LoadTendenciasBuzon1()
        {
            //buzonColores.lstImagebuzon1.Clear();
            SessionActual.Buzon1.LstImagebuzon.Clear();
            //buzonColores.lstImagebuzon2.Clear();
            SessionActual.Buzon2.LstImagebuzon.Clear();
            //buzonColores.lstImagebuzon3.Clear();
            SessionActual.Buzon3.LstImagebuzon.Clear();

            Color1ListBox.Items.Clear();
            Color2ListBox.Items.Clear();
            Color3ListBox.Items.Clear();
            Color4ListBox.Items.Clear();
            Color5ListBox.Items.Clear();

            ejecucion.Clear();
            logro.Clear();
            innovacion.Clear();
            liderazgo.Clear();
            personas.Clear();
            estructura.Clear();

        


            TalentitoMapaUC talentito = null;


            //deleteImages();

            cont1 = cont2 = cont3 = cont4 = cont5 = cont6 = 3;
            //for (int i = 0; i < buzonColores.buzon1.lstTalento.Count && buzonColores.b1; i++)
           for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count ; i++)
           //for (int i = 0; i < 3; i++)
            {
                talentito = null;
                if (SessionActual.Buzon1.lstTalento[i].seleccionado)
                {
                    switch (SessionActual.Buzon1.lstTalento[i].IdColor)
                    {
                        case 1:
                            //talentito = new TalentitoMapaUC("OrangeRed", SessionActual.Buzon1.lstTalento[i].Nombre);
                            talentito = new TalentitoMapaUC("#FFFF6600", SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image);

                            
                            //talentito = new TalentitoMapaUC("OrangeRed", "tttt");
                            ejecucion.Add(talentito); break;
                        case 2:
                            //talentito = new TalentitoMapaUC("Blue", SessionActual.Buzon1.lstTalento[i].Nombre);//"tttt");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            talentito = new TalentitoMapaUC("#FF0066FF", SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image);//"tttt");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            logro.Add(talentito); break;
                        case 3:
                            //talentito = new TalentitoMapaUC("Yellow", SessionActual.Buzon1.lstTalento[i].Nombre); //"Con Sentido del Humor");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            talentito = new TalentitoMapaUC("#FFFFCC00", SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image); //"Con Sentido del Humor");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            innovacion.Add(talentito); break;
                        case 4:
                            talentito = new TalentitoMapaUC("#FF7D0036", SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image);//"Con Sentido del Humor");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            liderazgo.Add(talentito); break;
                        case 5:
                            talentito = new TalentitoMapaUC("#FFCC0000", SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image); //"tttt");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            personas.Add(talentito); break;
                        case 6:
                            talentito = new TalentitoMapaUC("#FF065F1B", SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image); //"tttt");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            estructura.Add(talentito); break;
                    }
                }
             
            }

            Color1ListBox.ItemsSource = ejecucion;
            Color2ListBox.ItemsSource = logro;
            Color3ListBox.ItemsSource = innovacion;
            Color4ListBox.ItemsSource = liderazgo;
            Color5ListBox.ItemsSource = personas;
            Color6ListBox.ItemsSource = estructura;
        }

        private void LoadTendenciasBuzon3()
        {
            

            ejecucion.Clear();
            logro.Clear();
            innovacion.Clear();
            liderazgo.Clear();
            personas.Clear();
            estructura.Clear();

            

            TalentitoMapaUC talentito = null;



            cont1 = cont2 = cont3 = cont4 = cont5 = cont6 = 3;

            for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count ; i++)

            {
                talentito = null;
                if (SessionActual.Buzon3.lstTalento[i].seleccionado)
                {
                    switch (SessionActual.Buzon3.lstTalento[i].IdColor)
                    {
                        case 1:
                            talentito = new TalentitoMapaUC("#FFFF6600", SessionActual.Buzon3.lstTalento[i].Nombre, SessionActual.Buzon3.lstTalento[i].IdTalento, SessionActual.Buzon3.lstTalento[i].Image);
                            ejecucion.Add(talentito); break;
                        case 2:
                            talentito = new TalentitoMapaUC("#FF0066FF", SessionActual.Buzon3.lstTalento[i].Nombre, SessionActual.Buzon3.lstTalento[i].IdTalento, SessionActual.Buzon3.lstTalento[i].Image);//"tttt");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            logro.Add(talentito); break;
                        case 3:
                            talentito = new TalentitoMapaUC("#FFFFCC00", SessionActual.Buzon3.lstTalento[i].Nombre, SessionActual.Buzon3.lstTalento[i].IdTalento, SessionActual.Buzon3.lstTalento[i].Image); //"Con Sentido del Humor");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            innovacion.Add(talentito); break;
                        case 4:
                            talentito = new TalentitoMapaUC("#FF7D0036", SessionActual.Buzon3.lstTalento[i].Nombre, SessionActual.Buzon3.lstTalento[i].IdTalento, SessionActual.Buzon3.lstTalento[i].Image);//"Con Sentido del Humor");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            liderazgo.Add(talentito); break;
                        case 5:
                            talentito = new TalentitoMapaUC("#FFCC0000", SessionActual.Buzon3.lstTalento[i].Nombre, SessionActual.Buzon3.lstTalento[i].IdTalento, SessionActual.Buzon3.lstTalento[i].Image); //"tttt");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            personas.Add(talentito); break;
                        case 6:
                            talentito = new TalentitoMapaUC("#FF065F1B", SessionActual.Buzon3.lstTalento[i].Nombre, SessionActual.Buzon3.lstTalento[i].IdTalento, SessionActual.Buzon3.lstTalento[i].Image); //"tttt");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            estructura.Add(talentito); break;
                    }
                }

            }

            Color1ListBox3.ItemsSource = ejecucion;
            Color2ListBox3.ItemsSource = logro;
            Color3ListBox3.ItemsSource = innovacion;
            Color4ListBox3.ItemsSource = liderazgo;
            Color5ListBox3.ItemsSource = personas;
            Color6ListBox3.ItemsSource = estructura;
        }



        private void imgBuzon1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!SessionActual.Buzon1.activo)
            {

                ResourceDictionary rd = new temas.temaSelva.Tema();
                this.Resources = rd;

                imgBuzon1.Style = (Style)this.Resources["GlassBorderStyle"];

                LinearGradientBrush blanco = new LinearGradientBrush();
                GradientStop o1 = new GradientStop();
                o1.Color = Colors.White;
                blanco.GradientStops.Add(o1);

                txt11.Foreground = blanco;
                txt21.Foreground = blanco;
                txt31.Foreground = blanco;
            }
        }

        private void imgBuzon1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!SessionActual.Buzon1.activo)
            {

                ResourceDictionary rd = new temas.temaSelva.Tema();
                this.Resources = rd;

                imgBuzon1.Style = (Style)this.Resources["GlassBorderStyleBrillo"];

                LinearGradientBrush blanco = new LinearGradientBrush();
                GradientStop o1 = new GradientStop();
                o1.Color = Colors.White;
                blanco.GradientStops.Add(o1);

                txt11.Foreground = blanco;
                txt21.Foreground = blanco;
                txt31.Foreground = blanco;
            }
        }





        private void imgBuzon3_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!SessionActual.Buzon3.activo)
            {

                ResourceDictionary rd = new temas.temaSelva.Tema();
                this.Resources = rd;

                buzon3.Style = (Style)this.Resources["GlassBorderStyle"];

                LinearGradientBrush blanco = new LinearGradientBrush();
                GradientStop o1 = new GradientStop();
                o1.Color = Colors.White;
                blanco.GradientStops.Add(o1);

                txt13.Foreground = blanco;
                txt23.Foreground = blanco;
                txt33.Foreground = blanco;

            }


        }

        private void imgBuzon3_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!SessionActual.Buzon3.activo)
            {

                ResourceDictionary rd = new temas.temaSelva.Tema();
                this.Resources = rd;

                buzon3.Style = (Style)this.Resources["GlassBorderStyleBrillo"];

                LinearGradientBrush blanco = new LinearGradientBrush();
                GradientStop o1 = new GradientStop();
                o1.Color = Colors.White;
                blanco.GradientStops.Add(o1);

                txt13.Foreground = blanco;
                txt23.Foreground = blanco;
                txt33.Foreground = blanco;
            }

        }

        //cerrar popup con resultados
        private void btnCerrar2_Click_1(object sender, RoutedEventArgs e)
        {
            //ppColores1.IsOpen = false;
            ppColores1.Visibility = Visibility.Collapsed;
            canvasParte2.Visibility = Visibility.Visible;

            //ppColores3.IsOpen = false;
            ppColores3.Visibility = Visibility.Collapsed;

            Zoom.Visibility = Visibility.Collapsed;
           
        }

       
        //ver resultados del buzón 1
        private void imgBuzon1Nivel2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            txtTitulo.Text = "Talentos ";//que ";
            txtTitulo2.Text = "más desarrollados";//me identifican";
            //buzonColores.b1 = buzonColores.b2 = buzonColores.b3 = false;
            SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = SessionActual.Buzon3.activo = false;
            //buzonColores.b1 = true;
            SessionActual.Buzon1.activo = true;
            
            //ppColores3.IsOpen = false;
            ppColores3.Visibility = Visibility.Collapsed;
            //ppColores1.IsOpen = true;
            ppColores1.Visibility = Visibility.Visible;
            canvasParte2.Visibility = Visibility.Collapsed;
        }

        private void imgBuzon1Nivel2_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            popup1.Visibility = Visibility.Visible;
        }

        private void imgBuzon1Nivel2_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //MouseEnter.Stop();
            //MouseLeave.Begin();
            popup1.Visibility = Visibility.Collapsed;
        }


        //Ir a la sgte pantalla
        private void sgte_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = false;
            SessionActual.paso4 = true;

            _cambiarContenido.Invoke(Enumerador.Pagina.ResultadoEstadistico);
        }

        //Mueve los talentos no seleccionados del buzon me identifica al buzon no estoy seguro
        private void MoveTalentosFromBuzon1ToBuzon2()
        {
            ObservableCollection<TalentoBE> lstTalento = new ObservableCollection<TalentoBE>();

            for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
            //for (int i = 0; i < buzonColores.buzon1.lstTalento.Count; i++)
            {
                //if (!buzonColores.buzon1.lstTalento[i].seleccionado)
                if (!SessionActual.Buzon1.lstTalento[i].seleccionado)
                {
                    //buzonColores.buzon2.lstTalento.Add(buzonColores.buzon1.lstTalento[i]);
                    //lstTalento.Add(buzonColores.buzon1.lstTalento[i]);
                    SessionActual.Buzon2.lstTalento.Add(SessionActual.Buzon1.lstTalento[i]);
                    lstTalento.Add(SessionActual.Buzon1.lstTalento[i]);

                }
            }

           
            for (int i = 0; i < lstTalento.Count; i++)
            {
                // buzonColores.buzon1.lstTalento.Remove(lstTalento[i]);
                SessionActual.Buzon1.lstTalento.Remove(lstTalento[i]);
            }
        }

        private void MoveTalentosFromBuzon3ToBuzon2()
        {
            ObservableCollection<TalentoBE> lstTalento = new ObservableCollection<TalentoBE>();

            for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
            {

                if (!SessionActual.Buzon3.lstTalento[i].seleccionado)
                {
                    SessionActual.Buzon2.lstTalento.Add(SessionActual.Buzon3.lstTalento[i]);
                    lstTalento.Add(SessionActual.Buzon3.lstTalento[i]);

                }
            }


            for (int i = 0; i < lstTalento.Count; i++)
            {
                SessionActual.Buzon3.lstTalento.Remove(lstTalento[i]);
            }
        }

        private void buzon3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            txtTitulo3.Text = "Talentos ";//que ";
            txtTitulo3_2.Text = "menos desarrollados"; //me identifican";
            //buzonColores.b1 = buzonColores.b2 = buzonColores.b3 = false;
            SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = SessionActual.Buzon3.activo = false;
            //buzonColores.b1 = true;
            SessionActual.Buzon3.activo = true;
            
            //ppColores1.IsOpen = false;
            ppColores1.Visibility = Visibility.Collapsed;
            canvasParte2.Visibility = Visibility.Collapsed;
            //ppColores3.IsOpen = true;
            ppColores3.Visibility = Visibility.Visible;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
         
            _cambiarContenido.Invoke(Enumerador.Pagina.ResultadoEstadistico);
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
           
            _cambiarContenido.Invoke(Enumerador.Pagina.ResultadoEstadistico);
        }

        

        private void ucTalentitoMapa_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ucTalentitoMapa uc = (ucTalentitoMapa)(sender as ucTalentitoMapa);

            

            string url = uc.txtNombre.Tag.ToString();

       

            Point aqui = e.GetPosition(this);
            TalentoZoom.imgTalento.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            TalentoZoom.imgTalento.Tag = url;

          

            img2transform = new TranslateTransform();


            img2transform.X = aqui.X-132;

            img2transform.Y = aqui.Y;

            if(aqui.X>380)
                img2transform.X = aqui.X - 265;

            if (aqui.Y > 167 && aqui.Y<=360)//291)
                img2transform.Y = aqui.Y - 175;

            if ( aqui.Y > 360)//291)
                img2transform.Y = aqui.Y - 350;

            Zoom.RenderTransform = img2transform;

            Zoom.Visibility = Visibility.Visible;
        }

        private void btnCerrarZoom_Click(object sender, RoutedEventArgs e)
        {
            Zoom.Visibility = Visibility.Collapsed;
        }
    }
}
