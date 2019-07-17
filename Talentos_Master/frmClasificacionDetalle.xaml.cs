using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

namespace Talentos_Master
{
    /// <summary>
    ///ESTA PÁGINA CONTIENE EL CARROUSEL DE LOS TALENTOS QUE YA HAN SIDO CLASIFICADOS A LOS BUZONES.
    ///DESDE AQUI, SE PODRÁN MOVER TALENTOS DE UN BUZÓN A OTRO (REGLAS DEL JUEGO)
    /// <summary>

    //TODO: (ITERACION 1 - II ENTREGA) Modificar diseño de talentos y del Detalle Informativo de cada talento
    //TODO: (ITERACION 1 - II ENTREGA) Instrucciones de pie de página deben ser más grandes
    //TODO: (ITERACION 1 - II ENTREGA) Modificar diseño de buzones. Agregar efecto que permita distinguir en qué buzón se encuentra el jugador actualmente.


    //TODO: (ITERACION 2 - I ENTREGA) Incluir en la Secuencia del Juego

    //TODO: (ITERACION 2 - II ENTREGA)Corregir Bugs

    public partial class frmClasificacionDetalle : IPaginaContenida
    {
        private static double IMAGE_WIDTH = 170;        // Ancho de la Imagen
        private static double IMAGE_HEIGHT = 200;       // Altua de la Imagen
        private static double SPRINESS = 0.4;		    // Controla la velocidad de salto
        private static double DECAY = 0.5;			    // Controla  la velocidad de caida
        private static double SCALE_DOWN_FACTOR = 0.5;  // Scala entre imágenes
        private static double OFFSET_FACTOR = 100;      // Distancia entre imagenes
        private static double OPACITY_DOWN_FACTOR = 0.4;    // Alpha entre imagenes
        private static double MAX_SCALE = 1.5;            // Escala Maxima
        private static double CRITICAL_POINT = 0.001;

        //private BuzonGlobal buzones;

        private double _xCenter;
        private double _yCenter;

        // Mostrar algo en el centro primero
        private double _targetB1;
        private double _targetB2;
        private double _targetB3;
        private double _currentB1 = 0;	// Posición Actual
        private double _currentB2 = 0;
        private double _currentB3 = 0;
        private double _springB1 = 0;		// Temp usado para almacenar el último movimiento
        private double _springB2 = 0;
        private double _springB3 = 0;	

        private static int FPS = 24;                // fps of the on enter frame event
        private DispatcherTimer _timer = new DispatcherTimer(); // on enter frame simulator

        private bool capturaB1;
        private bool capturaB2;
        private bool capturaB3;

        //private Point pos;
       // private Point posOrgin;

        private bool sobreB1;
        private bool sobreB2;
        private bool sobreB3;

        private int WIDTH = 650;
        private int HEIGHT = 340;

        bool b1, b2, b3;

        TranslateTransform img2transform;

       

        //private TalentoGlobal talento;
        private Session SessionActual;

        public frmClasificacionDetalle()
        {
            InitializeComponent();

            // Almacenar la posición del centro
            _xCenter = WIDTH / 2;
            _yCenter = HEIGHT / 2 + 20;// Height / 2;
            _targetB1 = 0;
            _targetB2 = 0;
            _targetB3 = 0;
          
            SessionActual = Session.getInstance();
            

            _currentB1 = 0;
            _currentB2 = 0;
            _currentB3 = 0;

            _springB1 = 0;
            _springB2 = 0;
            _springB3 = 0;

            int activo=0;
            if (SessionActual.Buzon1.activo)
                activo = 1;
            else if (SessionActual.Buzon2.activo)
                activo = 2;
            else if (SessionActual.Buzon3.activo)
                activo = 3;

            SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = SessionActual.Buzon3.activo = true;

            addImages();

            switch (activo)
            {
                case 1: SessionActual.Buzon1.activo = true; SessionActual.Buzon2.activo = false; SessionActual.Buzon3.activo = false; break;
                case 2: SessionActual.Buzon2.activo = true; SessionActual.Buzon1.activo = false; SessionActual.Buzon3.activo = false; break;
                case 3: SessionActual.Buzon3.activo = true; SessionActual.Buzon2.activo = false; SessionActual.Buzon1.activo = false; break;
            }
            capturaB1=capturaB2=capturaB3= false;
            sobreB1=sobreB2=sobreB3 = false;

            b1 = b2 = b3 = false;

            contCantidadTalentos();

            EfectoBuzonActual();

            if (SessionActual.lstTalentos.Count.Equals(0))
            {

                if ((SessionActual.Buzon1.lstTalento.Count >= Session.MAX_TALENTOS_MAS_DESARROLLADOS) && (SessionActual.Buzon3.lstTalento.Count >= Session.MAX_TALENTOS_MENOS_DESARROLLADOS))
                {
                    txtVolver1.Text = txtVolver2.Text = txtVolver3.Text = "Continuar >>  ";
                    txtVolver1.Visibility = txtVolver2.Visibility = txtVolver3.Visibility = Visibility.Visible;
                }
                else
                {
                    txtVolver1.Text = txtVolver2.Text = txtVolver3.Text = "";
                    txtVolver1.Visibility = txtVolver2.Visibility = txtVolver3.Visibility = Visibility.Visible;
                }
            }
            else
            {
                txtVolver1.Text = txtVolver2.Text = txtVolver3.Text = "<< Volver  ";
                txtVolver1.Visibility = txtVolver2.Visibility = txtVolver3.Visibility = Visibility.Visible;
            }

        }

        void EfectoBuzon1()
        {
            ResourceDictionary rd= new temas.temaSelva.Tema();
            this.Resources = rd;

            if(SessionActual.Buzon1.activo)
                imgBuzon1.Style = (Style)this.Resources["GlassBorderStyleMarron"];
            else if (SessionActual.Buzon2.activo)
                imgBuzon1_2.Style = (Style)this.Resources["GlassBorderStyleMarron"];
            else if (SessionActual.Buzon3.activo)
                imgBuzon1_3.Style = (Style)this.Resources["GlassBorderStyleMarron"];
        }

        void QuitarEfectoBuzon1()
        {

            ResourceDictionary rd = new temas.temaSelva.Tema();
            this.Resources = rd;

            if (SessionActual.Buzon1.activo)
            {
                imgBuzon1.Style = (Style)this.Resources["GlassBorderStyle"];

                LinearGradientBrush blanco = new LinearGradientBrush();
                GradientStop b1 = new GradientStop();
                b1.Color = Colors.White;
                blanco.GradientStops.Add(b1);

                txt11.Foreground = blanco;
                txt21.Foreground = blanco;
                txt31.Foreground = blanco;
            }
            else if (SessionActual.Buzon2.activo)
            {
                imgBuzon1_2.Style = (Style)this.Resources["GlassBorderStyle"];

                LinearGradientBrush blanco = new LinearGradientBrush();
                GradientStop b1 = new GradientStop();
                b1.Color = Colors.White;
                blanco.GradientStops.Add(b1);

                txt11_2.Foreground = blanco;
                txt21_2.Foreground = blanco;
                txt31_2.Foreground = blanco;
            }
            else if (SessionActual.Buzon3.activo)
            {
                imgBuzon1_3.Style = (Style)this.Resources["GlassBorderStyle"];

                LinearGradientBrush blanco = new LinearGradientBrush();
                GradientStop b1 = new GradientStop();
                b1.Color = Colors.White;
                blanco.GradientStops.Add(b1);

                txt11_3.Foreground = blanco;
                txt21_3.Foreground = blanco;
                txt31_3.Foreground = blanco;
            }
        }

        void EfectoBuzon2()
        {
            ResourceDictionary rd = new temas.temaSelva.Tema();
            this.Resources = rd;


            if(SessionActual.Buzon1.activo) 
                imgBuzon2.Style = (Style)this.Resources["GlassBorderStyleMarron"];
            else if(SessionActual.Buzon2.activo)
                imgBuzon2_2.Style = (Style)this.Resources["GlassBorderStyleMarron"];
            else if (SessionActual.Buzon3.activo)
                imgBuzon2_3.Style = (Style)this.Resources["GlassBorderStyleMarron"]; 
        }

        void QuitarEfectoBuzon2()
        {
            ResourceDictionary rd = new temas.temaSelva.Tema();
            this.Resources = rd;

            LinearGradientBrush blanco = new LinearGradientBrush();
            GradientStop b1 = new GradientStop();
            b1.Color = Colors.White;
            blanco.GradientStops.Add(b1);

            if (SessionActual.Buzon1.activo)
            {

                imgBuzon2.Style = (Style)this.Resources["GlassBorderStyle"];
                txt12.Foreground = blanco;
                txt22.Foreground = blanco;
                txt32.Foreground = blanco;
            }
            else if (SessionActual.Buzon2.activo)
            {
                imgBuzon2_2.Style = (Style)this.Resources["GlassBorderStyle"];
                txt12_2.Foreground = blanco;
                txt22_2.Foreground = blanco;
                txt32_2.Foreground = blanco;
            }
            else if (SessionActual.Buzon3.activo)
            {
                imgBuzon2_3.Style = (Style)this.Resources["GlassBorderStyle"];
                txt12_3.Foreground = blanco;
                txt22_3.Foreground = blanco;
                txt32_3.Foreground = blanco;
            }
        }

        void EfectoBuzon3()
        {
            ResourceDictionary rd = new temas.temaSelva.Tema();
            this.Resources = rd;

            if(SessionActual.Buzon1.activo)
                imgBuzon3.Style = (Style)this.Resources["GlassBorderStyleMarron"];
            else if(SessionActual.Buzon2.activo)
                imgBuzon3_2.Style = (Style)this.Resources["GlassBorderStyleMarron"];
            else if(SessionActual.Buzon3.activo)
                imgBuzon3_3.Style = (Style)this.Resources["GlassBorderStyleMarron"];
        }

        void QuitarEfectoBuzon3()
        {
            ResourceDictionary rd = new temas.temaSelva.Tema();
            this.Resources = rd;

            LinearGradientBrush blanco = new LinearGradientBrush();
            GradientStop b1 = new GradientStop();
            b1.Color = Colors.White;
            blanco.GradientStops.Add(b1);

            if (SessionActual.Buzon1.activo)
            {
                imgBuzon3.Style = (Style)this.Resources["GlassBorderStyle"];
                txt13.Foreground = blanco;
                txt23.Foreground = blanco;
                txt33.Foreground = blanco;
            }
            else if (SessionActual.Buzon2.activo)
            {
                imgBuzon3_2.Style = (Style)this.Resources["GlassBorderStyle"];
                txt13_2.Foreground = blanco;
                txt23_2.Foreground = blanco;
                txt33_2.Foreground = blanco;
            }
            else if (SessionActual.Buzon3.activo)
            {
                imgBuzon3_3.Style = (Style)this.Resources["GlassBorderStyle"];
                txt13_3.Foreground = blanco;
                txt23_3.Foreground = blanco;
                txt33_3.Foreground = blanco;
            }
        }

        void EfectoBuzonActual()
        {
            if (SessionActual.Buzon1.activo)
            {
                LayoutRoot.Visibility = Visibility.Visible;
                LayoutRoot2.Visibility = Visibility.Collapsed;
                LayoutRoot3.Visibility = Visibility.Collapsed;

                EfectoBuzon1();
                QuitarEfectoBuzon2();
                QuitarEfectoBuzon3();

            }
            else
            {
                if (SessionActual.Buzon2.activo)
                {
                    LayoutRoot2.Visibility = Visibility.Visible;
                    LayoutRoot.Visibility = Visibility.Collapsed;
                    LayoutRoot3.Visibility = Visibility.Collapsed;

                    EfectoBuzon2();
                    QuitarEfectoBuzon3();
                    QuitarEfectoBuzon1();
                }
                else
                {
                    if (SessionActual.Buzon3.activo)
                    {
                        LayoutRoot3.Visibility = Visibility.Visible;
                        LayoutRoot2.Visibility = Visibility.Collapsed;
                        LayoutRoot.Visibility = Visibility.Collapsed;

                        EfectoBuzon3();

                        QuitarEfectoBuzon1();
                        QuitarEfectoBuzon2();
                    }
                }
            }
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

        private void imgBuzon2_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!SessionActual.Buzon2.activo)
            {
                ResourceDictionary rd = new temas.temaSelva.Tema();
                this.Resources = rd;

                imgBuzon2.Style = (Style)this.Resources["GlassBorderStyle"];

                LinearGradientBrush blanco = new LinearGradientBrush();
                GradientStop o1 = new GradientStop();
                o1.Color = Colors.White;
                blanco.GradientStops.Add(o1);

                txt12.Foreground = blanco;
                txt22.Foreground = blanco;
                txt32.Foreground = blanco;
            }
        }

        private void imgBuzon2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!SessionActual.Buzon2.activo)
            {
                ResourceDictionary rd = new temas.temaSelva.Tema();
                this.Resources = rd;

                imgBuzon2.Style = (Style)this.Resources["GlassBorderStyleBrillo"];

                LinearGradientBrush blanco = new LinearGradientBrush();
                GradientStop o1 = new GradientStop();
                o1.Color = Colors.White;
                blanco.GradientStops.Add(o1);

                txt12.Foreground = blanco;
                txt22.Foreground = blanco;
                txt32.Foreground = blanco;
            }
        }

        private void imgBuzon3_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!SessionActual.Buzon3.activo)
            {
                ResourceDictionary rd = new temas.temaSelva.Tema();
                this.Resources = rd;

                imgBuzon3.Style = (Style)this.Resources["GlassBorderStyle"];

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

                imgBuzon3.Style = (Style)this.Resources["GlassBorderStyleBrillo"];

                LinearGradientBrush blanco = new LinearGradientBrush();
                GradientStop o1 = new GradientStop();
                o1.Color = Colors.White;
                blanco.GradientStops.Add(o1);

                txt13.Foreground = blanco;
                txt23.Foreground = blanco;
                txt33.Foreground = blanco;
            }
        }

        //Calcula la posición actual de los talentos 
        void _timer_Tick(object sender, EventArgs e)
        {
            //if (buzones.b1 && Math.Abs(_targetB1 - _current) < CRITICAL_POINT) return;
            if (SessionActual.Buzon1.activo && Math.Abs(_targetB1 - _currentB1) < CRITICAL_POINT) return;
            else
                //if (buzones.b2 && Math.Abs(_targetB2 - _current) < CRITICAL_POINT) return;
                if (SessionActual.Buzon2.activo && Math.Abs(_targetB2 - _currentB2) < CRITICAL_POINT) return;
                else
                    //if (buzones.b3 && Math.Abs(_targetB3 - _current) < CRITICAL_POINT) return;
                    if (SessionActual.Buzon3.activo && Math.Abs(_targetB3 - _currentB3) < CRITICAL_POINT) return;

            Image image;
            //for (int i = 0; i < buzones.lstImagebuzon1.Count && buzones.b1; i++)
            for (int i = 0; i < SessionActual.Buzon1.LstImagebuzon.Count && SessionActual.Buzon1.activo; i++)
            {
                //image = buzones.lstImagebuzon1[i];
                image = SessionActual.Buzon1.LstImagebuzon[i];
                posImage(image, i);
            }
            //for (int i = 0; i < buzones.lstImagebuzon2.Count && buzones.b2; i++)
            for (int i = 0; i < SessionActual.Buzon2.LstImagebuzon.Count && SessionActual.Buzon2.activo; i++)
            {
                //image = buzones.lstImagebuzon2[i];
                image = SessionActual.Buzon2.LstImagebuzon[i];
                posImage(image, i);
            }
            //for (int i = 0; i < buzones.lstImagebuzon3.Count && buzones.b3; i++)
            for (int i = 0; i < SessionActual.Buzon3.LstImagebuzon.Count && SessionActual.Buzon3.activo; i++)
            {
                //image = buzones.lstImagebuzon3[i];
                image = SessionActual.Buzon3.LstImagebuzon[i];
                posImage(image, i);
            }

            // Calcula la posición actual
            // agrega el efecto spring
            //if (buzones.b1)
            if (SessionActual.Buzon1.activo)
            {
                _springB1 = (_targetB1 - _currentB1) * SPRINESS + _springB1 * DECAY;
                _currentB1 += _springB1;
            }
            else
                //if (buzones.b2)
                if (SessionActual.Buzon2.activo)
                {
                    _springB2 = (_targetB2 - _currentB2) * SPRINESS + _springB2 * DECAY;
                    _currentB2 += _springB2;
                }
                else
                    //if (buzones.b3)
                    if (SessionActual.Buzon3.activo)
                    {
                        _springB3 = (_targetB3 - _currentB3) * SPRINESS + _springB3 * DECAY;
                        _currentB3 += _springB3;
                    }
        }

        //agrega talentos a los buzones seleccionados
        private void addImages()
        {
            if(SessionActual.Buzon1.activo)
                SessionActual.Buzon1.LstImagebuzon.Clear();
            if (SessionActual.Buzon2.activo)
                SessionActual.Buzon2.LstImagebuzon.Clear();
            if (SessionActual.Buzon3.activo)
                SessionActual.Buzon3.LstImagebuzon.Clear();

            //agrega talentos al buzón 1  "Talentos más desarrollados"
            for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count && SessionActual.Buzon1.activo; i++)
            {
                String url = SessionActual.Buzon1.lstTalento[i].Image;
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(url, UriKind.Absolute));
                image.Width = 170;
                image.Height = 200;
                image.Cursor = System.Windows.Input.Cursors.Hand;
                LayoutRoot.Children.Add(image);
                posImage(image, i);
                image.MouseLeftButtonDown += new MouseButtonEventHandler(img_MouseButtonDown);
                image.MouseLeftButtonUp += new MouseButtonEventHandler(img_MouseButtonUp);
                image.MouseMove += new MouseEventHandler(img_MouseMove);
                SessionActual.Buzon1.LstImagebuzon.Add(image);
            }

            //agrega talentos al buzón 2 "Talentos Intermedios"
            for (int i = 0; i < SessionActual.Buzon2.lstTalento.Count && SessionActual.Buzon2.activo; i++)
            {
                String url = SessionActual.Buzon2.lstTalento[i].Image;
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(url, UriKind.Absolute));
                image.Width = 170;
                image.Height = 200;
                image.Cursor = System.Windows.Input.Cursors.Hand;
                LayoutRoot2.Children.Add(image);
                posImage(image, i);
                image.MouseLeftButtonDown += new MouseButtonEventHandler(img_MouseButtonDown);
                image.MouseLeftButtonUp += new MouseButtonEventHandler(img_MouseButtonUp);
                image.MouseMove += new MouseEventHandler(img_MouseMove);
                SessionActual.Buzon2.LstImagebuzon.Add(image);
            }

            //agrega talentos al buzón 3 "Talentos Menos Desarrollados"
            for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count && SessionActual.Buzon3.activo; i++)
            {
                String url = SessionActual.Buzon3.lstTalento[i].Image;
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(url, UriKind.Absolute));
                image.Width = 170;
                image.Height = 200;
                image.Cursor = System.Windows.Input.Cursors.Hand;
                LayoutRoot3.Children.Add(image);
                posImage(image, i);
                image.MouseLeftButtonDown += new MouseButtonEventHandler(img_MouseButtonDown);
                image.MouseLeftButtonUp += new MouseButtonEventHandler(img_MouseButtonUp);
                image.MouseMove += new MouseEventHandler(img_MouseMove);
                SessionActual.Buzon3.LstImagebuzon.Add(image);
            }
        }

        #region Eventos para cada imagen de los talentos
        private void img_MouseEnter(object sender, MouseEventArgs e)
        {
            this.popup1.Visibility = Visibility.Visible;

        }

        private void img_MouseLeave(object sender, MouseEventArgs e)
        {
            this.popup1.Visibility = Visibility.Collapsed;

        }

        #endregion

        //calcula la posicion para un talento
        private void posImage(Image image, int index)
        {
            double diffFactor=0;
            if(SessionActual.Buzon1.activo)
                diffFactor= index - _currentB1;
            else if (SessionActual.Buzon2.activo)
                diffFactor = index - _currentB2;
            else if(SessionActual.Buzon3.activo)
                diffFactor = index - _currentB3;

            //escala y posiciona el talento de acuerdo a su indice y posición actual. 
            
            if (image != null)
            {
                ScaleTransform scaleTransform = new ScaleTransform();
                scaleTransform.ScaleX = MAX_SCALE - Math.Abs(diffFactor) * SCALE_DOWN_FACTOR;
                scaleTransform.ScaleY = MAX_SCALE - Math.Abs(diffFactor) * SCALE_DOWN_FACTOR;
                image.RenderTransform = scaleTransform;

                // reposiciona la imagen
                double left = _xCenter - (IMAGE_WIDTH * scaleTransform.ScaleX) / 2 + diffFactor * OFFSET_FACTOR;
                double top = _yCenter - (IMAGE_HEIGHT * scaleTransform.ScaleY) / 2 - 60;
                image.Opacity = 1 - Math.Abs(diffFactor) * OPACITY_DOWN_FACTOR;

                image.SetValue(Canvas.LeftProperty, left);
                image.SetValue(Canvas.TopProperty, top);

                // ordena el elemento por la escalaX
                image.SetValue(Canvas.ZIndexProperty, (int)Math.Abs(scaleTransform.ScaleX * 100));
            }
        }

        #region Eventos de un Talento

        private void img_MouseButtonDown(object sender, MouseEventArgs e)
        { 

                if ((SessionActual.Buzon1.activo) && SessionActual.Buzon1.LstImagebuzon.Count > Convert.ToInt32(_targetB1))
                {  
                    SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].CaptureMouse();
                    capturaB1 = true;
                    capturaB2 = false;
                    capturaB3 = false;
                }
                else
                    //if (buzones.b2)
                    if ((SessionActual.Buzon2.activo) && SessionActual.Buzon2.LstImagebuzon.Count > Convert.ToInt32(_targetB2))
                    { 
                        SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].CaptureMouse();
                        capturaB2 = true;
                        capturaB1 = false;
                        capturaB3 = false;
                    }

                    else
                      
                        if (SessionActual.Buzon3.LstImagebuzon.Count > Convert.ToInt32(_targetB3))
                        {
                            SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].CaptureMouse();
                            capturaB3 = true;
                            capturaB2 = false;
                            capturaB1 = false;
                        }
            

            QuitarEfectoBuzon1();
            QuitarEfectoBuzon2();
            QuitarEfectoBuzon3();
            EfectoBuzonActual();
        }

        private void img_MouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (capturaB1 || capturaB2 || capturaB3)
            {
                    if ((SessionActual.Buzon1.activo) && SessionActual.Buzon1.LstImagebuzon.Count > Convert.ToInt32(_targetB1))
                    {
                        SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].ReleaseMouseCapture();

                        #region 1

                        if ((sobreB2 || sobreB3) && !b1)
                        {
                            
                            if (SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].Image.Contains("a.png"))
                            {
                                string urlFrente = SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].Example;
                                string urlEspalda = SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].Image;

                                SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].Image = urlFrente;
                                SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].Example = urlEspalda;
                            }

                            if (b2)
                            {

                                if (!SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].puntaje.Equals(0))
                                {

                                    SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].puntaje = 0;
                                    if (SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].seleccionado)
                                    {
                                        SessionActual.cantCalificadosBuzon1--;
                                        //
                                        SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].seleccionado = false;
                                        SessionActual.cantSeleccionadosBuzon1--;
                                        //
                                    }
                                    else
                                        SessionActual.cantCalificadosBuzon2--;
                                }
                                //
                                else
                                {
                                    if (SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].seleccionado)
                                    {
                                        SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].seleccionado = false;
                                        SessionActual.cantSeleccionadosBuzon1--;
                                    }
                                }
                                //

                                SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].seleccionado = false;
                                SessionActual.Buzon2.lstTalento.Add(SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)]);
                            }
                            else
                            {
                                if (!SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].puntaje.Equals(0))
                                {

                                    SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].puntaje = 0;
                                    if (SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].seleccionado)
                                    {
                                        SessionActual.cantCalificadosBuzon1--;
                                        //
                                        SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].seleccionado = false;
                                        SessionActual.cantSeleccionadosBuzon1--;
                                        //
                                    }
                                    else
                                        SessionActual.cantCalificadosBuzon2--;
                                }
                                //
                                else
                                {
                                    if (SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].seleccionado)
                                    {
                                        SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].seleccionado = false;
                                        SessionActual.cantSeleccionadosBuzon1--;
                                    }
                                }
                                //

                                SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].seleccionado = false;

                                SessionActual.Buzon3.lstTalento.Add(SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)]);
                            }


                            for (int i = 0; i < SessionActual.Buzon1.LstImagebuzon.Count; i++)
                            {

                                LayoutRoot.Children.Remove(SessionActual.Buzon1.LstImagebuzon[i]);
                            }

                            SessionActual.Buzon1.lstTalento.RemoveAt(Convert.ToInt32(_targetB1));

                            SessionActual.Buzon1.LstImagebuzon.RemoveAt(Convert.ToInt32(_targetB1));
                            addImages();

                            capturaB1 = false;
                                if ((SessionActual.Buzon1.LstImagebuzon.Count > 0) && !Equals(SessionActual.Buzon1.LstImagebuzon.Count, Convert.ToInt32(_targetB1)))
                                {

                                    posImage(SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)], Convert.ToInt32(_targetB1));

                                    SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].Width = 170;

                                    SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].Height = 200;
                                }
                                else
                                    MoveLeft();

                        }
                        else
                        {
                            img2transform = new TranslateTransform();

                            SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].Width = 170;//270;//356;

                            SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].Height = 200;//300;// 356;


                            SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].Opacity = 1.0;

                            posImage(SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)], Convert.ToInt32(_targetB1));
                        }
                        #endregion
                    }
                    else

                        if ((SessionActual.Buzon2.activo) && SessionActual.Buzon2.LstImagebuzon.Count > Convert.ToInt32(_targetB2))
                        {
                            SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].ReleaseMouseCapture();
                            #region 2

                            if ((sobreB1 || sobreB3) && !b2)
                            {
                                if (SessionActual.Buzon2.lstTalento[Convert.ToInt32(_targetB2)].Image.Contains("a.png"))
                                {
                                    string urlFrente = SessionActual.Buzon2.lstTalento[Convert.ToInt32(_targetB2)].Example;
                                    string urlEspalda = SessionActual.Buzon2.lstTalento[Convert.ToInt32(_targetB2)].Image;

                                    SessionActual.Buzon2.lstTalento[Convert.ToInt32(_targetB2)].Image = urlFrente;
                                    SessionActual.Buzon2.lstTalento[Convert.ToInt32(_targetB2)].Example = urlEspalda;
                                }


                                if (b1)
                                {
                                    if (!SessionActual.Buzon2.lstTalento[Convert.ToInt32(_targetB2)].puntaje.Equals(0))
                                    {
                                        SessionActual.Buzon2.lstTalento[Convert.ToInt32(_targetB2)].puntaje = 0;
                                        SessionActual.cantCalificadosBuzon2--;

                                        
                                    }

                                    //
                                    SessionActual.Buzon2.lstTalento[Convert.ToInt32(_targetB2)].seleccionado = false;
                                    //

                                    SessionActual.Buzon1.lstTalento.Add(SessionActual.Buzon2.lstTalento[Convert.ToInt32(_targetB2)]);
                                }
                                else
                                {
                                    if (!SessionActual.Buzon2.lstTalento[Convert.ToInt32(_targetB2)].puntaje.Equals(0))
                                    {
                                        SessionActual.Buzon2.lstTalento[Convert.ToInt32(_targetB2)].puntaje = 0;
                                        SessionActual.cantCalificadosBuzon2--;
                                    }

                                    //
                                    SessionActual.Buzon2.lstTalento[Convert.ToInt32(_targetB2)].seleccionado = false;
                                    //

                                    SessionActual.Buzon3.lstTalento.Add(SessionActual.Buzon2.lstTalento[Convert.ToInt32(_targetB2)]);
                                }

                                for (int i = 0; i < SessionActual.Buzon2.LstImagebuzon.Count; i++)
                                {

                                    LayoutRoot2.Children.Remove(SessionActual.Buzon2.LstImagebuzon[i]);
                                }

                                SessionActual.Buzon2.lstTalento.RemoveAt(Convert.ToInt32(_targetB2));

                                SessionActual.Buzon2.LstImagebuzon.RemoveAt(Convert.ToInt32(_targetB2));
                                addImages();

                                capturaB2 = false;
                                if ((SessionActual.Buzon2.LstImagebuzon.Count > 0) && !Equals(SessionActual.Buzon2.LstImagebuzon.Count, Convert.ToInt32(_targetB2))) //(buzones.lstImagebuzon2.Count > 0)
                                {
                                    posImage(SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)], Convert.ToInt32(_targetB2));
                                    SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].Width = 170;
                                    SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].Height = 200;
                                }
                                else
                                    MoveLeft();

                            }
                            else
                            {
                                img2transform = new TranslateTransform();

                                SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].Width = 170;//270;//356;

                                SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].Height = 200;//300;// 356;


                                SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].Opacity = 1.0;

                                posImage(SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)], Convert.ToInt32(_targetB2));
                            }

                            #endregion
                        }
                        else
                        {
                            if (SessionActual.Buzon3.LstImagebuzon.Count > Convert.ToInt32(_targetB3))
                            {
                                SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].ReleaseMouseCapture();

                                #region 3
                                if ((sobreB1 || sobreB2) && !b3)
                                {
                                    if (SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].Image.Contains("a.png"))
                                    {
                                        string urlFrente = SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].Example;
                                        string urlEspalda = SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].Image;

                                        SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].Image = urlFrente;
                                        SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].Example = urlEspalda;
                                    }


                                    if (b1)
                                    {



                                        if (!SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].puntaje.Equals(0))
                                        {

                                            SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].puntaje = 0;

                                            //
                                            if (SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].seleccionado)
                                            {
                                                SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].seleccionado = false;
                                                SessionActual.cantSeleccionadosBuzon3--;
                                            }
                                            else
                                                SessionActual.cantCalificadosBuzon2--;
                                            
                                                


                                            //if (!SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].seleccionado)
                                                //SessionActual.cantCalificadosBuzon2--;
                                            //
                                        }
                                        SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].seleccionado = false;
                                        SessionActual.Buzon1.lstTalento.Add(SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)]);
                                    }
                                    else
                                    {



                                        if (!SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].puntaje.Equals(0))
                                        {
                                            SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].puntaje = 0;

                                            //
                                            if (SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].seleccionado)
                                            {
                                                SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].seleccionado = false;
                                                SessionActual.cantSeleccionadosBuzon3--;
                                            }
                                            else
                                                SessionActual.cantCalificadosBuzon2--;

                                            //if (!SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].seleccionado)
                                              //  SessionActual.cantCalificadosBuzon2--;

                                            //

                                        }
                                        SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].seleccionado = false;
                                        SessionActual.Buzon2.lstTalento.Add(SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)]);
                                    }

                                    for (int i = 0; i < SessionActual.Buzon3.LstImagebuzon.Count; i++)
                                    {

                                        LayoutRoot3.Children.Remove(SessionActual.Buzon3.LstImagebuzon[i]);
                                    }

                                    SessionActual.Buzon3.lstTalento.RemoveAt(Convert.ToInt32(_targetB3));
                                    SessionActual.Buzon3.LstImagebuzon.RemoveAt(Convert.ToInt32(_targetB3));
                                    addImages();

                                    capturaB3 = false;

                                    if ((SessionActual.Buzon3.LstImagebuzon.Count > 0) && !Equals(SessionActual.Buzon3.LstImagebuzon.Count, Convert.ToInt32(_targetB3)))
                                    {

                                        posImage(SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)], Convert.ToInt32(_targetB3));
                                        SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].Width = 170;
                                        SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].Height = 200;
                                    }
                                    else
                                        MoveLeft();

                                }
                                else
                                {
                                    img2transform = new TranslateTransform();

                                    SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].Width = 170;//270;//356;

                                    SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].Height = 200;//300;// 356;


                                    SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].Opacity = 1.0;

                                    posImage(SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)], Convert.ToInt32(_targetB3));
                                }

                                #endregion

                            }
                        }
            
            if (SessionActual.lstTalentos.Count.Equals(0) && ((SessionActual.Buzon1.lstTalento.Count.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && SessionActual.Buzon3.lstTalento.Count >= Session.MAX_TALENTOS_MENOS_DESARROLLADOS) ||
                (SessionActual.Buzon1.lstTalento.Count >= Session.MAX_TALENTOS_MAS_DESARROLLADOS && SessionActual.Buzon3.lstTalento.Count.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS)) ||
                (SessionActual.Buzon1.lstTalento.Count.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && SessionActual.Buzon3.lstTalento.Count.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS))))
            {
                if (!SessionActual.revisaClasif)
                {
                    _cambiarInstruccion.Invoke(Enumerador.Instruccion.SinNavegacion);

                    txtVolver1.Text = txtVolver2.Text = txtVolver3.Text = "";

                    _cambiarInstruccion.Invoke(Enumerador.Instruccion.EnBlanco);

                    txtCantidad.Visibility = Visibility.Collapsed;
                    MoveLeftButton.Visibility = Visibility.Collapsed;
                    MoveRightButton.Visibility = Visibility.Collapsed;


                    SessionActual.paso1 = false;
                    SessionActual.paso2 = false;
                    SessionActual.paso3 = false;
                    SessionActual.paso4 = false;
                    SessionActual.pasoCorrec = true;
                    //ppSeAcabo.IsOpen = true;

                    LayoutRoot.Visibility = Visibility.Collapsed;

                    LayoutRoot2.Visibility = Visibility.Collapsed;
                    LayoutRoot3.Visibility = Visibility.Collapsed;

                    imgBuzon1.Visibility = Visibility.Collapsed;
                    imgBuzon2.Visibility = Visibility.Collapsed;
                    imgBuzon3.Visibility = Visibility.Collapsed;

                    ppSeAcabo.Visibility = Visibility.Visible;
                }
                else
                {
                    SessionActual.revisaClasif = false;
                    _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);
                    txtVolver1.Text = txtVolver2.Text = txtVolver3.Text = "Continuar >>  ";
                }

            }
            else
            {
                txtCantidad.Visibility = Visibility.Visible;
                MoveLeftButton.Visibility = Visibility.Visible;
                MoveRightButton.Visibility = Visibility.Visible;

                _cambiarInstruccion.Invoke(Enumerador.Instruccion.instruccionCorreccion);

                    if (!SessionActual.lstTalentos.Count.Equals(0))
                    {
                        _cambiarInstruccion.Invoke(Enumerador.Instruccion.Anterior);
                        txtVolver1.Text = txtVolver2.Text = txtVolver3.Text = "<< Volver  ";
                    }
                    else
                    {
                        if (SessionActual.Buzon1.lstTalento.Count < Session.MAX_TALENTOS_MAS_DESARROLLADOS || SessionActual.Buzon3.lstTalento.Count < Session.MAX_TALENTOS_MENOS_DESARROLLADOS)
                        {
                            _cambiarInstruccion.Invoke(Enumerador.Instruccion.SinNavegacion);
                            txtVolver1.Text = txtVolver2.Text = txtVolver3.Text = "";
                        }
                        else
                        {
                            if (SessionActual.Buzon1.lstTalento.Count >= Session.MAX_TALENTOS_MAS_DESARROLLADOS && SessionActual.Buzon3.lstTalento.Count >= Session.MAX_TALENTOS_MENOS_DESARROLLADOS)
                            {
                                _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);
                                txtVolver1.Text = txtVolver2.Text = txtVolver3.Text = "Continuar >> ";
                            }
                        }
                    }

                SessionActual.paso1 = false;
                SessionActual.paso2 = false;
                SessionActual.paso3 = false;
                SessionActual.paso4 = false;
                SessionActual.pasoCorrec = true;

                ppSeAcabo.Visibility = Visibility.Collapsed;

                EfectoBuzonActual();

                imgBuzon1.Visibility = Visibility.Visible;
                imgBuzon2.Visibility = Visibility.Visible;
                imgBuzon3.Visibility = Visibility.Visible;

            }

            capturaB1 = false;
            capturaB2 = false;
            capturaB3 = false;
            sobreB1 = false;
            sobreB2 = false;
            sobreB3 = false;

            b1 = b2 = b3 = false;
            contCantidadTalentos();

            QuitarEfectoBuzon1();
            QuitarEfectoBuzon2();
            QuitarEfectoBuzon3();
            EfectoBuzonActual();
            }
        }

        private void img_MouseMove(object sender, MouseEventArgs e)
        {

            if (capturaB1 || capturaB2 || capturaB3)
            {
                Point pFinal = e.GetPosition(this);


                img2transform = new TranslateTransform();

                img2transform.X = pFinal.X - 300;//250;

                img2transform.Y = pFinal.Y - 100;

                if ((capturaB1) && (SessionActual.Buzon1.activo) && SessionActual.Buzon1.LstImagebuzon.Count > Convert.ToInt32(_targetB1))//(buzones.b1)
                {

                    SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].Width = 156;//356;
                    SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].Height = 246;//356;
                    SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].RenderTransform = img2transform;
                    SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].Opacity = 1.0;

                    LayoutRoot.Children.Remove(SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)]);
                    LayoutRoot.Children.Add(SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)]);

                    #region 1
                    if (pFinal.X > 0 && pFinal.X <= 140 && pFinal.Y > 257 && pFinal.Y <= 480)
                    {
                        sobreB1 = true;

                        SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].Opacity = 0.5;
                        SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].Width = 156;//356;
                        SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].Height = 246;//356;

                        b1 = true;
                    }
                    else // para el buzon 2

                        if (pFinal.X > 300 && pFinal.X <= 425 && pFinal.Y > 227 && pFinal.Y <= 480)
                        {
                            sobreB2 = true;

                            SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].Opacity = 0.5;
                            SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].Width = 156;//356;
                            SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].Height = 246;//356;

                            b2 = true;

                            EfectoBuzon2();
                            QuitarEfectoBuzon3();
                        }
                        else
                        {
                            if (pFinal.X > 500 && pFinal.X <= 690 && pFinal.Y > 237 && pFinal.Y <= 480)
                            {
                                sobreB3 = true;

                                SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].Opacity = 0.5;
                                SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].Width = 156;//356;
                                SessionActual.Buzon1.LstImagebuzon[Convert.ToInt32(_targetB1)].Height = 246;//356;

                                b3 = true;

                                EfectoBuzon3();
                                QuitarEfectoBuzon2();
                            }
                            //else
                            //{
                            //    sobreB1 = false;
                            //    sobreB2 = false;
                            //    sobreB3 = false;
                            //}
                        }
                    #endregion
                }
                else
                    if ((capturaB2) && (SessionActual.Buzon2.activo) && SessionActual.Buzon2.LstImagebuzon.Count > Convert.ToInt32(_targetB2))//(buzones.b2)
                    {
                        SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].Width = 156;//356;
                        SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].Height = 246;//356;
                        SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].RenderTransform = img2transform;
                        SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].Opacity = 1.0;

                        LayoutRoot2.Children.Remove(SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)]);
                        LayoutRoot2.Children.Add(SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)]);

                        #region 2

                        if (pFinal.X > 0 && pFinal.X <= 240 && pFinal.Y > 237 && pFinal.Y <= 480)
                        {
                            sobreB1 = true;

                            SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].Opacity = 0.5;

                            SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].Width = 156;//356;
                            SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].Height = 246;
                            b1 = true;

                            EfectoBuzon1();
                            QuitarEfectoBuzon3();
                        }
                        else // para el buzon 2
                        if (pFinal.X > 160 && pFinal.X <= 295 && pFinal.Y > 257 && pFinal.Y <= 480)
                        {
                            sobreB2 = true;

                            SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].Opacity = 0.5;
                            SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].Width = 156;//356;
                            SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].Height = 246;

                            b2 = true;


                        }
                        else
                            if (pFinal.X > 450 && pFinal.X <= 650 && pFinal.Y > 237 && pFinal.Y <= 480)
                            {
                                sobreB3 = true;
                                SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].Opacity = 0.5;

                                SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].Width = 156;//356;
                                SessionActual.Buzon2.LstImagebuzon[Convert.ToInt32(_targetB2)].Height = 246;
                                b3 = true;

                                EfectoBuzon3();
                                QuitarEfectoBuzon1();
                            }
                            else
                            {
                                sobreB1 = false;
                                sobreB2 = false;
                                sobreB3 = false;
                            }

                        #endregion
                    }
                    else
                    {
                        if (capturaB3 && (SessionActual.Buzon3.LstImagebuzon.Count > Convert.ToInt32(_targetB3)))
                        {
                            SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].Width = 156;
                            SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].Height = 246;
                            SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].RenderTransform = img2transform;
                            SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].Opacity = 1.0;

                            LayoutRoot3.Children.Remove(SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)]);
                            LayoutRoot3.Children.Add(SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)]);

                            #region 3
                            if (pFinal.X > 0 && pFinal.X <= 140 && pFinal.Y > 257 && pFinal.Y <= 480)//407 && e.GetPosition(this).Y <= 480)
                            {
                                sobreB1 = true;

                                SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].Opacity = 0.5;

                                SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].Width = 156;
                                SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].Height = 246;
                                b1 = true;

                                EfectoBuzon1();
                                QuitarEfectoBuzon2();
                            }
                            else // para el buzon 2
                            if (pFinal.X > 260 && pFinal.X <= 350 && pFinal.Y > 257 && pFinal.Y <= 480)
                            {
                                sobreB2 = true;

                                SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].Opacity = 0.5;
                                SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].Width = 156;
                                SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].Height = 246;

                                b2 = true;

                                EfectoBuzon2();
                                QuitarEfectoBuzon1();
                            }
                            else
                                if (pFinal.X > 310 && pFinal.X <= 470 && pFinal.Y > 257 && pFinal.Y <= 480)
                                {
                                    sobreB3 = true;

                                    SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].Opacity = 0.5;

                                    SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].Width = 156;
                                    SessionActual.Buzon3.LstImagebuzon[Convert.ToInt32(_targetB3)].Height = 246;
                                    b3 = true;
                                }
                                else
                                {
                                    sobreB1 = false;
                                    sobreB2 = false;
                                    sobreB3 = false;
                                }


                            #endregion

                        }
                    }
            }
            
        }

        #endregion

        public void moveIndex(int value)
        {
            if ((!capturaB1) && (!capturaB2) && (!capturaB3))
            {
                if (SessionActual.Buzon1.activo)//(buzones.b1)
                {
                    _targetB1 += value;
                    _targetB1 = Math.Max(0, _targetB1);
                    //_targetB1 = Math.Min(buzones.lstImagebuzon1.Count - 1, _targetB1);
                    //_targetB1 = Math.Min(SessionActual.Buzon1.LstImagebuzon.Count - 1, _targetB1);
                    _targetB1 = Math.Min(SessionActual.Buzon1.lstTalento.Count - 1, _targetB1);
                }
                else
                    if (SessionActual.Buzon2.activo)//(buzones.b2)
                    {
                        _targetB2 += value;
                        _targetB2 = Math.Max(0, _targetB2);
                        //_targetB2 = Math.Min(buzones.lstImagebuzon2.Count - 1, _targetB2);
                        //_targetB2 = Math.Min(SessionActual.Buzon2.LstImagebuzon.Count - 1, _targetB2);
                        _targetB2 = Math.Min(SessionActual.Buzon2.lstTalento.Count - 1, _targetB2);
                    }
                    else
                        if (SessionActual.Buzon3.activo)//(buzones.b3)
                        {
                            _targetB3 += value;
                            _targetB3 = Math.Max(0, _targetB3);
                            //_targetB3 = Math.Min(buzones.lstImagebuzon3.Count - 1, _targetB3);
                            //_targetB3 = Math.Min(SessionActual.Buzon3.LstImagebuzon.Count - 1, _targetB3);
                            _targetB3 = Math.Min(SessionActual.Buzon3.lstTalento.Count - 1, _targetB3);
                        }
            }

        }

        public void Start()
        {
            // start the enter frame event
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / FPS);
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Start();
        }

        // Mueve carrousel a la izquierda
        public void MoveLeft()
        {
            moveIndex(-1);
            Start();
        }

        // Mueve carrousel a la derecha
        public void MoveRight()
        {
            moveIndex(1);
            Start();
        }

        #region Cancelar y Aceptar del popup que aparece cuando se terminó de clasificar todos los talentos
        private void btnCancelarSeAcabo_Click(object sender, RoutedEventArgs e)
        {
            SessionActual.terminoClasificacion = true;
            _cambiarContenido.Invoke(Enumerador.Pagina.SeleccionarPrincipal);
        }

        private void btnAceptarSeAcabo_Click(object sender, RoutedEventArgs e)
        {
            _cambiarContenido.Invoke(Enumerador.Pagina.ClasificacionPrincipal);
        }
        #endregion

        //Mover carrousel a la izquierda
        private void izquierdaBuzon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if(!captura)
            MoveLeft();
        }

        //Mover carrousel a la derecha
        private void derechaBuzon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if(!captura)
            MoveRight();
        }


        //Ver ejemplo de un talento
        private void btnVoltear_Click(object sender, RoutedEventArgs e)
        {
                Image auxImage = new Image();
                //if (buzones.b1)
                if ((!capturaB1) && (SessionActual.Buzon1.activo) && (SessionActual.Buzon1.lstTalento.Count > 0) && SessionActual.Buzon1.lstTalento.Count > Convert.ToInt32(_targetB1))
                {
                    //for (int i = 0; i < buzones.lstImagebuzon1.Count; i++)
                    for (int i = 0; i < SessionActual.Buzon1.LstImagebuzon.Count; i++)
                    {
                        //LayoutRoot.Children.Remove(buzones.lstImagebuzon1[i]);
                        LayoutRoot.Children.Remove(SessionActual.Buzon1.LstImagebuzon[i]);
                    }
                    //String auxUrl = buzones.buzon1.lstTalento[Convert.ToInt32(_targetB1)].Image;
                    String auxUrl = SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].Image;
                    //buzones.buzon1.lstTalento[Convert.ToInt32(_targetB1)].Image = buzones.buzon1.lstTalento[Convert.ToInt32(_targetB1)].Example;
                    SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].Image = SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].Example;
                    //buzones.buzon1.lstTalento[Convert.ToInt32(_targetB1)].Example = auxUrl;
                    SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].Example = auxUrl;

                    addImages();
                    //SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].Example = SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].Image;
                    //SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].Image = auxUrl;
                }
                else
                    if ((!capturaB2) && (SessionActual.Buzon2.activo) && (SessionActual.Buzon2.lstTalento.Count > 0) && SessionActual.Buzon2.lstTalento.Count > Convert.ToInt32(_targetB2))//(buzones.b2)
                    {
                        //for (int i = 0; i < buzones.lstImagebuzon2.Count; i++)
                        for (int i = 0; i < SessionActual.Buzon2.LstImagebuzon.Count; i++)
                        {
                            //LayoutRoot.Children.Remove(buzones.lstImagebuzon2[i]);
                            LayoutRoot2.Children.Remove(SessionActual.Buzon2.LstImagebuzon[i]);
                        }
                        //String auxUrl = buzones.buzon2.lstTalento[Convert.ToInt32(_targetB2)].Image;
                        String auxUrl = SessionActual.Buzon2.lstTalento[Convert.ToInt32(_targetB2)].Image;
                        //buzones.buzon2.lstTalento[Convert.ToInt32(_targetB2)].Image = buzones.buzon2.lstTalento[Convert.ToInt32(_targetB2)].Example;
                        SessionActual.Buzon2.lstTalento[Convert.ToInt32(_targetB2)].Image = SessionActual.Buzon2.lstTalento[Convert.ToInt32(_targetB2)].Example;
                        //buzones.buzon2.lstTalento[Convert.ToInt32(_targetB2)].Example = auxUrl;
                        SessionActual.Buzon2.lstTalento[Convert.ToInt32(_targetB2)].Example = auxUrl;

                        addImages();
                        //SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].Example = SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].Image;
                        //SessionActual.Buzon1.lstTalento[Convert.ToInt32(_targetB1)].Image = auxUrl;
                    }
                    else
                        if ((!capturaB3) && (SessionActual.Buzon3.activo) && (SessionActual.Buzon3.lstTalento.Count > 0) && SessionActual.Buzon3.lstTalento.Count > Convert.ToInt32(_targetB3))//(buzones.b3)
                        {
                            //for (int i = 0; i < buzones.lstImagebuzon3.Count; i++)
                            for (int i = 0; i < SessionActual.Buzon3.LstImagebuzon.Count; i++)
                            {
                                //LayoutRoot.Children.Remove(buzones.lstImagebuzon3[i]);
                                LayoutRoot3.Children.Remove(SessionActual.Buzon3.LstImagebuzon[i]);
                            }
                            //String auxUrl = buzones.buzon3.lstTalento[Convert.ToInt32(_targetB3)].Image;
                            String auxUrl = SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].Image;
                            //buzones.buzon3.lstTalento[Convert.ToInt32(_targetB3)].Image = buzones.buzon3.lstTalento[Convert.ToInt32(_targetB3)].Example;
                            SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].Image = SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].Example;
                            //buzones.buzon3.lstTalento[Convert.ToInt32(_targetB3)].Example = auxUrl;
                            SessionActual.Buzon3.lstTalento[Convert.ToInt32(_targetB3)].Example = auxUrl;


                            addImages();
                        }
        }

        //Cuando se hace click sobre el buzón 1
        private void buzon1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SessionActual.Buzon1.activo) && (ppSeAcabo.Visibility == Visibility.Collapsed) && (!capturaB1 || !capturaB2 || !capturaB3))
            {
                SessionActual.Buzon3.activo = SessionActual.Buzon2.activo = false;
                SessionActual.Buzon1.activo = true;

                //if (SessionActual.Buzon1.lstTalento.Count != SessionActual.Buzon1.LstImagebuzon.Count)
                //{
                for (int i = 0; i < SessionActual.Buzon1.LstImagebuzon.Count; i++)
                    //canvasPuntajesBuzon1.Children.Remove(buzones.lstBuzon1Borde[i]);
                    LayoutRoot.Children.Remove(SessionActual.Buzon1.LstImagebuzon[i]);

                addImages();

                //}
                LayoutRoot.Visibility = Visibility.Visible;
                LayoutRoot2.Visibility = Visibility.Collapsed;
                LayoutRoot3.Visibility = Visibility.Collapsed;

                EfectoBuzonActual();

                contCantidadTalentos();
            }
        }

        //Cuando se hace click sobre el buzón 2
        private void buzon2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SessionActual.Buzon2.activo) && (ppSeAcabo.Visibility == Visibility.Collapsed) && (!capturaB1 || !capturaB2 || !capturaB3))
            {

                SessionActual.Buzon3.activo = SessionActual.Buzon1.activo = false;
                SessionActual.Buzon2.activo = true;




                //if (SessionActual.Buzon2.lstTalento.Count != SessionActual.Buzon2.LstImagebuzon.Count)
                //{
                for (int i = 0; i < SessionActual.Buzon2.LstImagebuzon.Count; i++)
                    //canvasPuntajesBuzon1.Children.Remove(buzones.lstBuzon1Borde[i]);
                    LayoutRoot2.Children.Remove(SessionActual.Buzon2.LstImagebuzon[i]);

                addImages();

                //}

                LayoutRoot2.Visibility = Visibility.Visible;
                LayoutRoot.Visibility = Visibility.Collapsed;
                LayoutRoot3.Visibility = Visibility.Collapsed;
                EfectoBuzonActual();

                contCantidadTalentos();
            }
        }

        //Cuando se hace click sobre el buzón 3
        private void buzon3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SessionActual.Buzon3.activo) && (ppSeAcabo.Visibility == Visibility.Collapsed) && (!capturaB1 || !capturaB2 || !capturaB3))
            {

                SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = false;
                SessionActual.Buzon3.activo = true;


                //if (SessionActual.Buzon3.lstTalento.Count != SessionActual.Buzon3.LstImagebuzon.Count)
                //{
                for (int i = 0; i < SessionActual.Buzon3.LstImagebuzon.Count; i++)
                    //canvasPuntajesBuzon1.Children.Remove(buzones.lstBuzon1Borde[i]);
                    LayoutRoot3.Children.Remove(SessionActual.Buzon3.LstImagebuzon[i]);

                addImages();

                //}

                LayoutRoot3.Visibility = Visibility.Visible;
                LayoutRoot2.Visibility = Visibility.Collapsed;
                LayoutRoot.Visibility = Visibility.Collapsed;
                EfectoBuzonActual();

                contCantidadTalentos();
            }
        }

        //contador de talentos existentes en cada buzón
        private void contCantidadTalentos()
        {
            if(SessionActual.Buzon1.LstImagebuzon.Count.Equals(1))
                txtCantidad.Text = SessionActual.Buzon1.LstImagebuzon.Count.ToString() + " talento";
            else
                txtCantidad.Text = SessionActual.Buzon1.LstImagebuzon.Count.ToString() + " talentos";

            if(SessionActual.Buzon2.LstImagebuzon.Count.Equals(1))
                txtCantidad2.Text = SessionActual.Buzon2.LstImagebuzon.Count.ToString() + " talento";
            else
                txtCantidad2.Text = SessionActual.Buzon2.LstImagebuzon.Count.ToString() + " talentos";

            if(SessionActual.Buzon3.LstImagebuzon.Count.Equals(1))
                txtCantidad3.Text = SessionActual.Buzon3.LstImagebuzon.Count.ToString() + " talento";
            else
                txtCantidad3.Text = SessionActual.Buzon3.LstImagebuzon.Count.ToString() + " talentos";
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SessionActual.Buzon1.lstTalento.Count.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && (SessionActual.Buzon3.lstTalento.Count.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS)))
            {
                for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
                    SessionActual.Buzon1.lstTalento[i].seleccionado = true;

                for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
                    SessionActual.Buzon3.lstTalento[i].seleccionado = true;

                SessionActual.paso1 = false;
                SessionActual.paso2 = true;
                SessionActual.paso3 = false;
                SessionActual.paso4 = false;
                SessionActual.pasoCorrec = false;

                SessionActual.terminoClasificacion = true;
                SessionActual.terminoSeleccion = true;

                _cambiarContenido.Invoke(Enumerador.Pagina.SeleccionarPrincipal);
                _cambiarInstruccion.Invoke(Enumerador.Instruccion.SegundaInstruccion);
            }
            else
            {
                SessionActual.paso1 = false;
                SessionActual.paso2 = false;
                SessionActual.paso3 = false;
                SessionActual.paso4 = false;
                SessionActual.pasoCorrec = true;

                SessionActual.terminoClasificacion = true;
                //todo: validar cuando en el buzon 1 hay 10 O en el buzon 3 hay 5
                _cambiarContenido.Invoke(Enumerador.Pagina.SeleccionarPrincipal);
                _cambiarInstruccion.Invoke(Enumerador.Instruccion.SegundaInstruccion);
            }
        }

        private void btnAceptarSeAcabo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtCantidad.Visibility = Visibility.Visible;
            MoveLeftButton.Visibility = Visibility.Visible;
            MoveRightButton.Visibility = Visibility.Visible;

            _cambiarInstruccion.Invoke(Enumerador.Instruccion.instruccionCorreccion);

            ppSeAcabo.Visibility = Visibility.Collapsed;

            EfectoBuzonActual();

            imgBuzon1.Visibility = Visibility.Visible;
            imgBuzon2.Visibility = Visibility.Visible;
            imgBuzon3.Visibility = Visibility.Visible;

            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = false;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrec = true;

            SessionActual.revisaClasif = true;

            _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);

            txtVolver1.Text = txtVolver2.Text = txtVolver3.Text = "Continuar >>  ";
        }

        private void btnCancelarSeAcabo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ActualizarResultados();   
        }

        //Actualiza resultados en la BD
        private void ActualizarResultados()
        {
            result_actualizar_completed(null, null);
        }

        public void result_actualizar_completed(object sender, TalentosReference.ResultadoActualizarCompletedEventArgs e)
        {
            if (SessionActual.Buzon1.lstTalento.Count.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && (SessionActual.Buzon3.lstTalento.Count.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS)))
            {
                for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
                    SessionActual.Buzon1.lstTalento[i].seleccionado = true;

                for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
                    SessionActual.Buzon3.lstTalento[i].seleccionado = true;

                SessionActual.paso1 = false;
                SessionActual.paso2 = false;
                SessionActual.paso3 = true;
                SessionActual.paso4 = false;

                SessionActual.terminoClasificacion = true;
                SessionActual.terminoSeleccion = true;

                resultUpdateToResultados_completed(null, null);
            }
            else
            {
                SessionActual.paso1 = false;
                SessionActual.paso2 = true;
                SessionActual.paso3 = false;
                SessionActual.paso4 = false;

                if (SessionActual.Buzon1.lstTalento.Count > Session.MAX_TALENTOS_MAS_DESARROLLADOS)
                {
                    SessionActual.Buzon1.activo = true;
                    SessionActual.Buzon2.activo = false;
                    SessionActual.Buzon3.activo = false;
                }
                else
                {
                    if (SessionActual.Buzon3.lstTalento.Count > Session.MAX_TALENTOS_MENOS_DESARROLLADOS)
                    {
                        SessionActual.Buzon1.activo = false;
                        SessionActual.Buzon2.activo = false;
                        SessionActual.Buzon3.activo = true;
                    }
                }

                SessionActual.terminoClasificacion = true;

                _cambiarContenido.Invoke(Enumerador.Pagina.SeleccionarPrincipal);

                _cambiarInstruccion.Invoke(Enumerador.Instruccion.SegundaInstruccion);
            }
        }

        public void resultUpdateToResultados_completed(object sender, TalentosReference.ResultadoActualizarCompletedEventArgs e)
        {
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = true;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrec = false;

            SessionActual.terminoClasificacion = true;
            SessionActual.terminoSeleccion = true;

            //_cambiarContenido.Invoke(Enumerador.Pagina.EnvioReporte);
            //_cambiarInstruccion.Invoke(Enumerador.Instruccion.InstruccionEnvioReporte);
        }

        private void txtNavegacion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock txt = (TextBlock)(sender as TextBlock);
            if (txt.Text.Contains("Continuar"))
            {
                SessionActual.Buzon1.activo = true;
                SessionActual.Buzon2.activo = false;

                ActualizarResultados();
            }
            else
            {
                SessionActual.paso1 = true;
                SessionActual.paso2 = false;
                SessionActual.paso3 = false;
                SessionActual.paso4 = false;
                SessionActual.pasoCorrec = false;

                _cambiarContenido.Invoke(Enumerador.Pagina.MasterRueda);
            }
        }
    }
}
