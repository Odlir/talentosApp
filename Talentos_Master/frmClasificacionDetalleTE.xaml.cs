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
using System.Windows.Threading;
using System.Windows.Media.Imaging;

namespace Talentos_Master
{
    public partial class frmClasificacionDetalleTE : IPaginaContenida
    {
        private Session SessionActual;
        private static double IMAGE_WIDTH = 170;        // Ancho de la Imagen
        private static double IMAGE_HEIGHT = 200;       // Altua de la Imagen
        private static double SPRINESS = 0.4;		    // Controla la velocidad de salto
        private static double DECAY = 0.5;			    // Controla  la velocidad de caida
        private static double SCALE_DOWN_FACTOR = 0.5;  // Scala entre imágenes
        private static double OFFSET_FACTOR = 100;      // Distancia entre imagenes
        private static double OPACITY_DOWN_FACTOR = 0.4;    // Alpha entre imagenes
        private static double MAX_SCALE = 1.5;            // Escala Maxima
        private static double CRITICAL_POINT = 0.001;

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

        private bool sobreB1;
        private bool sobreB2;
        private bool sobreB3;

        private int WIDTH = 650;
        private int HEIGHT = 340;

        bool b1, b2, b3;

        TranslateTransform img2transform;

        public frmClasificacionDetalleTE()
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

            int activo = 0;
            if (SessionActual.BuzonTEMas.activo)
                activo = 1;
            else if (SessionActual.BuzonTEIntermedio.activo)
                activo = 2;
            else if (SessionActual.BuzonTEMenos.activo)
                activo = 3;

            SessionActual.BuzonTEMas.activo = SessionActual.BuzonTEIntermedio.activo = SessionActual.BuzonTEMenos.activo = true;

            addImages();

            switch (activo)
            {
                case 1: SessionActual.BuzonTEMas.activo = true; SessionActual.BuzonTEIntermedio.activo = false; SessionActual.BuzonTEMenos.activo = false; break;
                case 2: SessionActual.BuzonTEIntermedio.activo = true; SessionActual.BuzonTEMas.activo = false; SessionActual.BuzonTEMenos.activo = false; break;
                case 3: SessionActual.BuzonTEMenos.activo = true; SessionActual.BuzonTEIntermedio.activo = false; SessionActual.BuzonTEMas.activo = false; break;
            }
            capturaB1 = capturaB2 = capturaB3 = false;
            sobreB1 = sobreB2 = sobreB3 = false;

            b1 = b2 = b3 = false;

            contCantidadTalentos();
            EfectoBuzonActual();

            if (SessionActual.lstTalentosEspecificos.Count.Equals(0))
            {
                if ((SessionActual.BuzonTEMas.lstTalento.Count + SessionActual.BuzonTEIntermedio.lstTalento.Count + SessionActual.BuzonTEMenos.lstTalento.Count).Equals(Session.MAX_TALENTOS_ESPECIFICOS))
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

        void EfectoBuzonTEMas()
        {
            ResourceDictionary rd = new temas.temaSelva.Tema();
            this.Resources = rd;

            if (SessionActual.BuzonTEMas.activo)
                imgBuzon1.Style = (Style)this.Resources["GlassBorderStyleMarron"];
            else if (SessionActual.BuzonTEIntermedio.activo)
                imgBuzon1_2.Style = (Style)this.Resources["GlassBorderStyleMarron"];
            else if (SessionActual.BuzonTEMenos.activo)
                imgBuzon1_3.Style = (Style)this.Resources["GlassBorderStyleMarron"];
        }

        void QuitarEfectoBuzonTEMas()
        {
            ResourceDictionary rd = new temas.temaSelva.Tema();
            this.Resources = rd;

            if (SessionActual.BuzonTEMas.activo)
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
            else if (SessionActual.BuzonTEIntermedio.activo)
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
            else if (SessionActual.BuzonTEMenos.activo)
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

        void EfectoBuzonTEIntermedio()
        {
            ResourceDictionary rd = new temas.temaSelva.Tema();
            this.Resources = rd;

            if (SessionActual.BuzonTEMas.activo)
                imgBuzon2.Style = (Style)this.Resources["GlassBorderStyleMarron"];
            else if (SessionActual.BuzonTEIntermedio.activo)
                imgBuzon2_2.Style = (Style)this.Resources["GlassBorderStyleMarron"];
            else if (SessionActual.BuzonTEMenos.activo)
                imgBuzon2_3.Style = (Style)this.Resources["GlassBorderStyleMarron"];
        }

        void QuitarEfectoBuzonTEIntermedio()
        {
            ResourceDictionary rd = new temas.temaSelva.Tema();
            this.Resources = rd;

            LinearGradientBrush blanco = new LinearGradientBrush();
            GradientStop b1 = new GradientStop();
            b1.Color = Colors.White;
            blanco.GradientStops.Add(b1);

            if (SessionActual.BuzonTEMas.activo)
            {
                imgBuzon2.Style = (Style)this.Resources["GlassBorderStyle"];
                txt12.Foreground = blanco;
                txt22.Foreground = blanco;
                txt32.Foreground = blanco;
            }
            else if (SessionActual.BuzonTEIntermedio.activo)
            {
                imgBuzon2_2.Style = (Style)this.Resources["GlassBorderStyle"];
                txt12_2.Foreground = blanco;
                txt22_2.Foreground = blanco;
                txt32_2.Foreground = blanco;
            }
            else if (SessionActual.BuzonTEMenos.activo)
            {
                imgBuzon2_3.Style = (Style)this.Resources["GlassBorderStyle"];
                txt12_3.Foreground = blanco;
                txt22_3.Foreground = blanco;
                txt32_3.Foreground = blanco;
            }
        }

        void EfectoBuzonTEMenos()
        {
            ResourceDictionary rd = new temas.temaSelva.Tema();
            this.Resources = rd;

            if (SessionActual.BuzonTEMas.activo)
                imgBuzon3.Style = (Style)this.Resources["GlassBorderStyleMarron"];
            else if (SessionActual.BuzonTEIntermedio.activo)
                imgBuzon3_2.Style = (Style)this.Resources["GlassBorderStyleMarron"];
            else if (SessionActual.BuzonTEMenos.activo)
                imgBuzon3_3.Style = (Style)this.Resources["GlassBorderStyleMarron"];
        }

        void QuitarEfectoBuzonTEMenos()
        {
            ResourceDictionary rd = new temas.temaSelva.Tema();
            this.Resources = rd;

            LinearGradientBrush blanco = new LinearGradientBrush();
            GradientStop b1 = new GradientStop();
            b1.Color = Colors.White;
            blanco.GradientStops.Add(b1);

            if (SessionActual.BuzonTEMas.activo)
            {
                imgBuzon3.Style = (Style)this.Resources["GlassBorderStyle"];
                txt13.Foreground = blanco;
                txt23.Foreground = blanco;
                txt33.Foreground = blanco;
            }
            else if (SessionActual.BuzonTEIntermedio.activo)
            {
                imgBuzon3_2.Style = (Style)this.Resources["GlassBorderStyle"];
                txt13_2.Foreground = blanco;
                txt23_2.Foreground = blanco;
                txt33_2.Foreground = blanco;
            }
            else if (SessionActual.BuzonTEMenos.activo)
            {
                imgBuzon3_3.Style = (Style)this.Resources["GlassBorderStyle"];
                txt13_3.Foreground = blanco;
                txt23_3.Foreground = blanco;
                txt33_3.Foreground = blanco;
            }
        }

        void EfectoBuzonActual()
        {
            if (SessionActual.BuzonTEMas.activo)
            {
                LayoutRoot.Visibility = Visibility.Visible;
                LayoutRoot2.Visibility = Visibility.Collapsed;
                LayoutRoot3.Visibility = Visibility.Collapsed;

                EfectoBuzonTEMas();
                QuitarEfectoBuzonTEIntermedio();
                QuitarEfectoBuzonTEMenos();
            }
            else
            {
                if (SessionActual.BuzonTEIntermedio.activo)
                {
                    LayoutRoot2.Visibility = Visibility.Visible;
                    LayoutRoot.Visibility = Visibility.Collapsed;
                    LayoutRoot3.Visibility = Visibility.Collapsed;

                    EfectoBuzonTEIntermedio();
                    QuitarEfectoBuzonTEMenos();
                    QuitarEfectoBuzonTEMas();
                }
                else
                {
                    if (SessionActual.BuzonTEMenos.activo)
                    {
                        LayoutRoot3.Visibility = Visibility.Visible;
                        LayoutRoot2.Visibility = Visibility.Collapsed;
                        LayoutRoot.Visibility = Visibility.Collapsed;

                        EfectoBuzonTEMenos();
                        QuitarEfectoBuzonTEMas();
                        QuitarEfectoBuzonTEIntermedio();
                    }
                }
            }
        }

        private void imgBuzon1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!SessionActual.BuzonTEMas.activo)
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
            if (!SessionActual.BuzonTEMas.activo)
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
            if (!SessionActual.BuzonTEIntermedio.activo)
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
            if (!SessionActual.BuzonTEIntermedio.activo)
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
            if (!SessionActual.BuzonTEMenos.activo)
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
            if (!SessionActual.BuzonTEMenos.activo)
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
            if (SessionActual.BuzonTEMas.activo && Math.Abs(_targetB1 - _currentB1) < CRITICAL_POINT) return;
            else
                if (SessionActual.BuzonTEIntermedio.activo && Math.Abs(_targetB2 - _currentB2) < CRITICAL_POINT) return;
                else
                    if (SessionActual.BuzonTEMenos.activo && Math.Abs(_targetB3 - _currentB3) < CRITICAL_POINT) return;

            Image image;
            for (int i = 0; i < SessionActual.BuzonTEMas.LstImagebuzon.Count && SessionActual.BuzonTEMas.activo; i++)
            {
                image = SessionActual.BuzonTEMas.LstImagebuzon[i];
                posImage(image, i);
            }
            for (int i = 0; i < SessionActual.BuzonTEIntermedio.LstImagebuzon.Count && SessionActual.BuzonTEIntermedio.activo; i++)
            {
                image = SessionActual.BuzonTEIntermedio.LstImagebuzon[i];
                posImage(image, i);
            }
            for (int i = 0; i < SessionActual.BuzonTEMenos.LstImagebuzon.Count && SessionActual.BuzonTEMenos.activo; i++)
            {
                image = SessionActual.BuzonTEMenos.LstImagebuzon[i];
                posImage(image, i);
            }

            // Calcula la posición actual
            // agrega el efecto spring
            if (SessionActual.BuzonTEMas.activo)
            {
                _springB1 = (_targetB1 - _currentB1) * SPRINESS + _springB1 * DECAY;
                _currentB1 += _springB1;
            }
            else
                if (SessionActual.BuzonTEIntermedio.activo)
                {
                    _springB2 = (_targetB2 - _currentB2) * SPRINESS + _springB2 * DECAY;
                    _currentB2 += _springB2;
                }
                else
                    if (SessionActual.BuzonTEMenos.activo)
                    {
                        _springB3 = (_targetB3 - _currentB3) * SPRINESS + _springB3 * DECAY;
                        _currentB3 += _springB3;
                    }
        }

        //agrega talentos a los buzones seleccionados
        private void addImages()
        {
            if (SessionActual.BuzonTEMas.activo)
                SessionActual.BuzonTEMas.LstImagebuzon.Clear();
            if (SessionActual.BuzonTEIntermedio.activo)
                SessionActual.BuzonTEIntermedio.LstImagebuzon.Clear();
            if (SessionActual.BuzonTEMenos.activo)
                SessionActual.BuzonTEMenos.LstImagebuzon.Clear();

            //agrega talentos al buzón 1  "Talentos más desarrollados"
            for (int i = 0; i < SessionActual.BuzonTEMas.lstTalento.Count && SessionActual.BuzonTEMas.activo; i++)
            {
                String url = SessionActual.BuzonTEMas.lstTalento[i].Image;
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
                SessionActual.BuzonTEMas.LstImagebuzon.Add(image);
            }

            //agrega talentos al buzón 2 "Talentos Intermedios"
            for (int i = 0; i < SessionActual.BuzonTEIntermedio.lstTalento.Count && SessionActual.BuzonTEIntermedio.activo; i++)
            {
                String url = SessionActual.BuzonTEIntermedio.lstTalento[i].Image;
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
                SessionActual.BuzonTEIntermedio.LstImagebuzon.Add(image);
            }

            //agrega talentos al buzón 3 "Talentos Menos Desarrollados"
            for (int i = 0; i < SessionActual.BuzonTEMenos.lstTalento.Count && SessionActual.BuzonTEMenos.activo; i++)
            {
                String url = SessionActual.BuzonTEMenos.lstTalento[i].Image;
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
                SessionActual.BuzonTEMenos.LstImagebuzon.Add(image);
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
            double diffFactor = 0;
            if (SessionActual.BuzonTEMas.activo)
                diffFactor = index - _currentB1;
            else if (SessionActual.BuzonTEIntermedio.activo)
                diffFactor = index - _currentB2;
            else if (SessionActual.BuzonTEMenos.activo)
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
            if ((SessionActual.BuzonTEMas.activo) && SessionActual.BuzonTEMas.LstImagebuzon.Count > Convert.ToInt32(_targetB1))
            {
                SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].CaptureMouse();
                capturaB1 = true;
                capturaB2 = false;
                capturaB3 = false;
            }
            else
                if ((SessionActual.BuzonTEIntermedio.activo) && SessionActual.BuzonTEIntermedio.LstImagebuzon.Count > Convert.ToInt32(_targetB2))
                {
                    SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].CaptureMouse();
                    capturaB2 = true;
                    capturaB1 = false;
                    capturaB3 = false;
                }
                else
                    if (SessionActual.BuzonTEMenos.LstImagebuzon.Count > Convert.ToInt32(_targetB3))
                    {
                        SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].CaptureMouse();
                        capturaB3 = true;
                        capturaB2 = false;
                        capturaB1 = false;
                    }

            QuitarEfectoBuzonTEMas();
            QuitarEfectoBuzonTEIntermedio();
            QuitarEfectoBuzonTEMenos();
            EfectoBuzonActual();
        }

        private void img_MouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (capturaB1 || capturaB2 || capturaB3)
            {
                if ((SessionActual.BuzonTEMas.activo) && SessionActual.BuzonTEMas.LstImagebuzon.Count > Convert.ToInt32(_targetB1))
                {
                    SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].ReleaseMouseCapture();

                    #region 1

                    if ((sobreB2 || sobreB3) && !b1)
                    {
                        if (SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].Image.Contains("a.png"))
                        {
                            string urlFrente = SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].Example;
                            string urlEspalda = SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].Image;

                            SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].Image = urlFrente;
                            SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].Example = urlEspalda;
                        }
                        if (b2)
                        {
                            if (!SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].puntaje.Equals(0))
                            {
                                SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].puntaje = 0;
                                if (SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].seleccionado)
                                {
                                    SessionActual.cantCalificadosBuzonTEMas--;
                                    SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].seleccionado = false;
                                    SessionActual.cantSeleccionadosBuzonTEMas--;
                                }
                                else
                                    SessionActual.cantCalificadosBuzonTEIntermedio--;
                            }
                            else
                            {
                                if (SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].seleccionado)
                                {
                                    SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].seleccionado = false;
                                    SessionActual.cantSeleccionadosBuzonTEMas--;
                                }
                            }

                            SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].seleccionado = false;
                            SessionActual.BuzonTEIntermedio.lstTalento.Add(SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)]);
                        }
                        else
                        {
                            if (!SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].puntaje.Equals(0))
                            {
                                SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].puntaje = 0;
                                if (SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].seleccionado)
                                {
                                    SessionActual.cantCalificadosBuzonTEMas--;
                                    SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].seleccionado = false;
                                    SessionActual.cantSeleccionadosBuzonTEMas--;
                                }
                                else
                                    SessionActual.cantCalificadosBuzonTEIntermedio--;
                            }
                            else
                            {
                                if (SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].seleccionado)
                                {
                                    SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].seleccionado = false;
                                    SessionActual.cantSeleccionadosBuzonTEMas--;
                                }
                            }

                            SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)].seleccionado = false;
                            SessionActual.BuzonTEMenos.lstTalento.Add(SessionActual.BuzonTEMas.lstTalento[Convert.ToInt32(_targetB1)]);
                        }

                        for (int i = 0; i < SessionActual.BuzonTEMas.LstImagebuzon.Count; i++)
                        {
                            LayoutRoot.Children.Remove(SessionActual.BuzonTEMas.LstImagebuzon[i]);
                        }

                        SessionActual.BuzonTEMas.lstTalento.RemoveAt(Convert.ToInt32(_targetB1));
                        SessionActual.BuzonTEMas.LstImagebuzon.RemoveAt(Convert.ToInt32(_targetB1));
                        addImages();

                        capturaB1 = false;
                        if ((SessionActual.BuzonTEMas.LstImagebuzon.Count > 0) && !Equals(SessionActual.BuzonTEMas.LstImagebuzon.Count, Convert.ToInt32(_targetB1)))
                        {
                            posImage(SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)], Convert.ToInt32(_targetB1));
                            SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].Width = 170;
                            SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].Height = 200;
                        }
                        else
                            MoveLeft();
                    }
                    else
                    {
                        img2transform = new TranslateTransform();

                        SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].Width = 170;//270;//356;
                        SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].Height = 200;//300;// 356;
                        SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].Opacity = 1.0;

                        posImage(SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)], Convert.ToInt32(_targetB1));
                    }
                    #endregion
                }
                else
                    if ((SessionActual.BuzonTEIntermedio.activo) && SessionActual.BuzonTEIntermedio.LstImagebuzon.Count > Convert.ToInt32(_targetB2))
                    {
                        SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].ReleaseMouseCapture();
                        #region 2

                        if ((sobreB1 || sobreB3) && !b2)
                        {
                            if (SessionActual.BuzonTEIntermedio.lstTalento[Convert.ToInt32(_targetB2)].Image.Contains("a.png"))
                            {
                                string urlFrente = SessionActual.BuzonTEIntermedio.lstTalento[Convert.ToInt32(_targetB2)].Example;
                                string urlEspalda = SessionActual.BuzonTEIntermedio.lstTalento[Convert.ToInt32(_targetB2)].Image;

                                SessionActual.BuzonTEIntermedio.lstTalento[Convert.ToInt32(_targetB2)].Image = urlFrente;
                                SessionActual.BuzonTEIntermedio.lstTalento[Convert.ToInt32(_targetB2)].Example = urlEspalda;
                            }
                            if (b1)
                            {
                                if (!SessionActual.BuzonTEIntermedio.lstTalento[Convert.ToInt32(_targetB2)].puntaje.Equals(0))
                                {
                                    SessionActual.BuzonTEIntermedio.lstTalento[Convert.ToInt32(_targetB2)].puntaje = 0;
                                    SessionActual.cantCalificadosBuzonTEIntermedio--;
                                }

                                SessionActual.BuzonTEIntermedio.lstTalento[Convert.ToInt32(_targetB2)].seleccionado = false;
                                SessionActual.BuzonTEMas.lstTalento.Add(SessionActual.BuzonTEIntermedio.lstTalento[Convert.ToInt32(_targetB2)]);
                            }
                            else
                            {
                                if (!SessionActual.BuzonTEIntermedio.lstTalento[Convert.ToInt32(_targetB2)].puntaje.Equals(0))
                                {
                                    SessionActual.BuzonTEIntermedio.lstTalento[Convert.ToInt32(_targetB2)].puntaje = 0;
                                    SessionActual.cantCalificadosBuzonTEIntermedio--;
                                }

                                SessionActual.BuzonTEIntermedio.lstTalento[Convert.ToInt32(_targetB2)].seleccionado = false;
                                SessionActual.BuzonTEMenos.lstTalento.Add(SessionActual.BuzonTEIntermedio.lstTalento[Convert.ToInt32(_targetB2)]);
                            }

                            for (int i = 0; i < SessionActual.BuzonTEIntermedio.LstImagebuzon.Count; i++)
                            {
                                LayoutRoot2.Children.Remove(SessionActual.BuzonTEIntermedio.LstImagebuzon[i]);
                            }

                            SessionActual.BuzonTEIntermedio.lstTalento.RemoveAt(Convert.ToInt32(_targetB2));

                            SessionActual.BuzonTEIntermedio.LstImagebuzon.RemoveAt(Convert.ToInt32(_targetB2));
                            addImages();

                            capturaB2 = false;
                            if ((SessionActual.BuzonTEIntermedio.LstImagebuzon.Count > 0) && !Equals(SessionActual.BuzonTEIntermedio.LstImagebuzon.Count, Convert.ToInt32(_targetB2))) //(buzones.lstImageBuzonTEIntermedio.Count > 0)
                            {
                                posImage(SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)], Convert.ToInt32(_targetB2));
                                SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].Width = 170;
                                SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].Height = 200;
                            }
                            else
                                MoveLeft();
                        }
                        else
                        {
                            img2transform = new TranslateTransform();

                            SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].Width = 170;//270;//356;
                            SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].Height = 200;//300;// 356;
                            SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].Opacity = 1.0;

                            posImage(SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)], Convert.ToInt32(_targetB2));
                        }

                        #endregion
                    }
                    else
                    {
                        if (SessionActual.BuzonTEMenos.LstImagebuzon.Count > Convert.ToInt32(_targetB3))
                        {
                            SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].ReleaseMouseCapture();

                            #region 3
                            if ((sobreB1 || sobreB2) && !b3)
                            {
                                if (SessionActual.BuzonTEMenos.lstTalento[Convert.ToInt32(_targetB3)].Image.Contains("a.png"))
                                {
                                    string urlFrente = SessionActual.BuzonTEMenos.lstTalento[Convert.ToInt32(_targetB3)].Example;
                                    string urlEspalda = SessionActual.BuzonTEMenos.lstTalento[Convert.ToInt32(_targetB3)].Image;

                                    SessionActual.BuzonTEMenos.lstTalento[Convert.ToInt32(_targetB3)].Image = urlFrente;
                                    SessionActual.BuzonTEMenos.lstTalento[Convert.ToInt32(_targetB3)].Example = urlEspalda;
                                }

                                if (b1)
                                {
                                    if (!SessionActual.BuzonTEMenos.lstTalento[Convert.ToInt32(_targetB3)].puntaje.Equals(0))
                                    {
                                        SessionActual.BuzonTEMenos.lstTalento[Convert.ToInt32(_targetB3)].puntaje = 0;

                                        if (SessionActual.BuzonTEMenos.lstTalento[Convert.ToInt32(_targetB3)].seleccionado)
                                        {
                                            SessionActual.BuzonTEMenos.lstTalento[Convert.ToInt32(_targetB3)].seleccionado = false;
                                            SessionActual.cantSeleccionadosBuzonTEMenos--;
                                        }
                                        else
                                            SessionActual.cantCalificadosBuzonTEIntermedio--;
                                    }
                                    SessionActual.BuzonTEMenos.lstTalento[Convert.ToInt32(_targetB3)].seleccionado = false;
                                    SessionActual.BuzonTEMas.lstTalento.Add(SessionActual.BuzonTEMenos.lstTalento[Convert.ToInt32(_targetB3)]);
                                }
                                else
                                {
                                    if (!SessionActual.BuzonTEMenos.lstTalento[Convert.ToInt32(_targetB3)].puntaje.Equals(0))
                                    {
                                        SessionActual.BuzonTEMenos.lstTalento[Convert.ToInt32(_targetB3)].puntaje = 0;

                                        if (SessionActual.BuzonTEMenos.lstTalento[Convert.ToInt32(_targetB3)].seleccionado)
                                        {
                                            SessionActual.BuzonTEMenos.lstTalento[Convert.ToInt32(_targetB3)].seleccionado = false;
                                            SessionActual.cantSeleccionadosBuzonTEMenos--;
                                        }
                                        else
                                            SessionActual.cantCalificadosBuzonTEIntermedio--;
                                    }
                                    SessionActual.BuzonTEMenos.lstTalento[Convert.ToInt32(_targetB3)].seleccionado = false;
                                    SessionActual.BuzonTEIntermedio.lstTalento.Add(SessionActual.BuzonTEMenos.lstTalento[Convert.ToInt32(_targetB3)]);
                                }

                                for (int i = 0; i < SessionActual.BuzonTEMenos.LstImagebuzon.Count; i++)
                                {
                                    LayoutRoot3.Children.Remove(SessionActual.BuzonTEMenos.LstImagebuzon[i]);
                                }

                                SessionActual.BuzonTEMenos.lstTalento.RemoveAt(Convert.ToInt32(_targetB3));
                                SessionActual.BuzonTEMenos.LstImagebuzon.RemoveAt(Convert.ToInt32(_targetB3));
                                addImages();

                                capturaB3 = false;

                                if ((SessionActual.BuzonTEMenos.LstImagebuzon.Count > 0) && !Equals(SessionActual.BuzonTEMenos.LstImagebuzon.Count, Convert.ToInt32(_targetB3)))
                                {
                                    posImage(SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)], Convert.ToInt32(_targetB3));
                                    SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].Width = 170;
                                    SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].Height = 200;
                                }
                                else
                                    MoveLeft();
                            }
                            else
                            {
                                img2transform = new TranslateTransform();

                                SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].Width = 170;//270;//356;
                                SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].Height = 200;//300;// 356;
                                SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].Opacity = 1.0;

                                posImage(SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)], Convert.ToInt32(_targetB3));
                            }

                            #endregion

                        }
                    }

                if (SessionActual.lstTalentosEspecificos.Count.Equals(0) && ((SessionActual.BuzonTEMas.lstTalento.Count.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && SessionActual.BuzonTEMenos.lstTalento.Count >= Session.MAX_TALENTOS_MENOS_DESARROLLADOS) ||
                    (SessionActual.BuzonTEMas.lstTalento.Count >= Session.MAX_TALENTOS_MAS_DESARROLLADOS && SessionActual.BuzonTEMenos.lstTalento.Count.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS)) ||
                    (SessionActual.BuzonTEMas.lstTalento.Count.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && SessionActual.BuzonTEMenos.lstTalento.Count.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS))))
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

                    if (!SessionActual.lstTalentosEspecificos.Count.Equals(0))
                    {
                        _cambiarInstruccion.Invoke(Enumerador.Instruccion.Anterior);
                        txtVolver1.Text = txtVolver2.Text = txtVolver3.Text = "<< Volver  ";
                    }
                    else
                    {
                        if (SessionActual.BuzonTEMas.lstTalento.Count < Session.MAX_TALENTOS_MAS_DESARROLLADOS || SessionActual.BuzonTEMenos.lstTalento.Count < Session.MAX_TALENTOS_MENOS_DESARROLLADOS)
                        {
                            _cambiarInstruccion.Invoke(Enumerador.Instruccion.SinNavegacion);
                            txtVolver1.Text = txtVolver2.Text = txtVolver3.Text = "";
                        }
                        else
                        {
                            if (SessionActual.BuzonTEMas.lstTalento.Count >= Session.MAX_TALENTOS_MAS_DESARROLLADOS && SessionActual.BuzonTEMenos.lstTalento.Count >= Session.MAX_TALENTOS_MENOS_DESARROLLADOS)
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

                QuitarEfectoBuzonTEMas();
                QuitarEfectoBuzonTEIntermedio();
                QuitarEfectoBuzonTEMenos();
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

                if ((capturaB1) && (SessionActual.BuzonTEMas.activo) && SessionActual.BuzonTEMas.LstImagebuzon.Count > Convert.ToInt32(_targetB1))//(buzones.b1)
                {
                    SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].Width = 156;//356;
                    SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].Height = 246;//356;
                    SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].RenderTransform = img2transform;
                    SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].Opacity = 1.0;

                    LayoutRoot.Children.Remove(SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)]);
                    LayoutRoot.Children.Add(SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)]);

                    #region 1
                    if (pFinal.X > 0 && pFinal.X <= 140 && pFinal.Y > 257 && pFinal.Y <= 480)
                    {
                        sobreB1 = true;

                        SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].Opacity = 0.5;
                        SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].Width = 156;//356;
                        SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].Height = 246;//356;

                        b1 = true;
                    }
                    else // para el buzon 2
                        if (pFinal.X > 300 && pFinal.X <= 425 && pFinal.Y > 227 && pFinal.Y <= 480)
                        {
                            sobreB2 = true;

                            SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].Opacity = 0.5;
                            SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].Width = 156;//356;
                            SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].Height = 246;//356;

                            b2 = true;

                            EfectoBuzonTEIntermedio();
                            QuitarEfectoBuzonTEMenos();
                        }
                        else
                        {
                            if (pFinal.X > 500 && pFinal.X <= 690 && pFinal.Y > 237 && pFinal.Y <= 480)
                            {
                                sobreB3 = true;

                                SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].Opacity = 0.5;
                                SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].Width = 156;//356;
                                SessionActual.BuzonTEMas.LstImagebuzon[Convert.ToInt32(_targetB1)].Height = 246;//356;

                                b3 = true;

                                EfectoBuzonTEMenos();
                                QuitarEfectoBuzonTEIntermedio();
                            }
                        }
                    #endregion
                }
                else
                    if ((capturaB2) && (SessionActual.BuzonTEIntermedio.activo) && SessionActual.BuzonTEIntermedio.LstImagebuzon.Count > Convert.ToInt32(_targetB2))//(buzones.b2)
                    {
                        SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].Width = 156;//356;
                        SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].Height = 246;//356;
                        SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].RenderTransform = img2transform;
                        SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].Opacity = 1.0;

                        LayoutRoot2.Children.Remove(SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)]);
                        LayoutRoot2.Children.Add(SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)]);

                        #region 2

                        if (pFinal.X > 0 && pFinal.X <= 240 && pFinal.Y > 237 && pFinal.Y <= 480)
                        {
                            sobreB1 = true;

                            SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].Opacity = 0.5;

                            SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].Width = 156;//356;
                            SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].Height = 246;
                            b1 = true;

                            EfectoBuzonTEMas();
                            QuitarEfectoBuzonTEMenos();
                        }
                        else // para el buzon 2
                            if (pFinal.X > 160 && pFinal.X <= 295 && pFinal.Y > 257 && pFinal.Y <= 480)
                            {
                                sobreB2 = true;

                                SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].Opacity = 0.5;
                                SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].Width = 156;//356;
                                SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].Height = 246;

                                b2 = true;
                            }
                            else
                                if (pFinal.X > 450 && pFinal.X <= 650 && pFinal.Y > 237 && pFinal.Y <= 480)
                                {
                                    sobreB3 = true;
                                    SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].Opacity = 0.5;

                                    SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].Width = 156;//356;
                                    SessionActual.BuzonTEIntermedio.LstImagebuzon[Convert.ToInt32(_targetB2)].Height = 246;
                                    b3 = true;

                                    EfectoBuzonTEMenos();
                                    QuitarEfectoBuzonTEMas();
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
                        if (capturaB3 && (SessionActual.BuzonTEMenos.LstImagebuzon.Count > Convert.ToInt32(_targetB3)))
                        {
                            SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].Width = 156;
                            SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].Height = 246;
                            SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].RenderTransform = img2transform;
                            SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].Opacity = 1.0;

                            LayoutRoot3.Children.Remove(SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)]);
                            LayoutRoot3.Children.Add(SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)]);

                            #region 3
                            if (pFinal.X > 0 && pFinal.X <= 140 && pFinal.Y > 257 && pFinal.Y <= 480)//407 && e.GetPosition(this).Y <= 480)
                            {
                                sobreB1 = true;

                                SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].Opacity = 0.5;
                                SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].Width = 156;
                                SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].Height = 246;
                                b1 = true;

                                EfectoBuzonTEMas();
                                QuitarEfectoBuzonTEIntermedio();
                            }
                            else // para el buzon 2
                                if (pFinal.X > 260 && pFinal.X <= 350 && pFinal.Y > 257 && pFinal.Y <= 480)
                                {
                                    sobreB2 = true;

                                    SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].Opacity = 0.5;
                                    SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].Width = 156;
                                    SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].Height = 246;

                                    b2 = true;

                                    EfectoBuzonTEIntermedio();
                                    QuitarEfectoBuzonTEMas();
                                }
                                else
                                    if (pFinal.X > 310 && pFinal.X <= 470 && pFinal.Y > 257 && pFinal.Y <= 480)
                                    {
                                        sobreB3 = true;

                                        SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].Opacity = 0.5;

                                        SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].Width = 156;
                                        SessionActual.BuzonTEMenos.LstImagebuzon[Convert.ToInt32(_targetB3)].Height = 246;
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
                if (SessionActual.BuzonTEMas.activo)//(buzones.b1)
                {
                    _targetB1 += value;
                    _targetB1 = Math.Max(0, _targetB1);
                    _targetB1 = Math.Min(SessionActual.BuzonTEMas.lstTalento.Count - 1, _targetB1);
                }
                else
                    if (SessionActual.BuzonTEIntermedio.activo)//(buzones.b2)
                    {
                        _targetB2 += value;
                        _targetB2 = Math.Max(0, _targetB2);
                        _targetB2 = Math.Min(SessionActual.BuzonTEIntermedio.lstTalento.Count - 1, _targetB2);
                    }
                    else
                        if (SessionActual.BuzonTEMenos.activo)//(buzones.b3)
                        {
                            _targetB3 += value;
                            _targetB3 = Math.Max(0, _targetB3);
                            _targetB3 = Math.Min(SessionActual.BuzonTEMenos.lstTalento.Count - 1, _targetB3);
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
            SessionActual.terminoClasificacionTE = true;
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
            MoveLeft();
        }

        //Mover carrousel a la derecha
        private void derechaBuzon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MoveRight();
        }

        //Cuando se hace click sobre el buzón 1
        private void BuzonTEMas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SessionActual.BuzonTEMas.activo) && (ppSeAcabo.Visibility == Visibility.Collapsed) && (!capturaB1 || !capturaB2 || !capturaB3))
            {
                SessionActual.BuzonTEMenos.activo = SessionActual.BuzonTEIntermedio.activo = false;
                SessionActual.BuzonTEMas.activo = true;

                for (int i = 0; i < SessionActual.BuzonTEMas.LstImagebuzon.Count; i++)
                    LayoutRoot.Children.Remove(SessionActual.BuzonTEMas.LstImagebuzon[i]);

                addImages();

                LayoutRoot.Visibility = Visibility.Visible;
                LayoutRoot2.Visibility = Visibility.Collapsed;
                LayoutRoot3.Visibility = Visibility.Collapsed;

                EfectoBuzonActual();

                contCantidadTalentos();
            }
        }

        //Cuando se hace click sobre el buzón 2
        private void BuzonTEIntermedio_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SessionActual.BuzonTEIntermedio.activo) && (ppSeAcabo.Visibility == Visibility.Collapsed) && (!capturaB1 || !capturaB2 || !capturaB3))
            {
                SessionActual.BuzonTEMenos.activo = SessionActual.BuzonTEMas.activo = false;
                SessionActual.BuzonTEIntermedio.activo = true;

                for (int i = 0; i < SessionActual.BuzonTEIntermedio.LstImagebuzon.Count; i++)
                    LayoutRoot2.Children.Remove(SessionActual.BuzonTEIntermedio.LstImagebuzon[i]);

                addImages();

                LayoutRoot2.Visibility = Visibility.Visible;
                LayoutRoot.Visibility = Visibility.Collapsed;
                LayoutRoot3.Visibility = Visibility.Collapsed;
                EfectoBuzonActual();

                contCantidadTalentos();
            }
        }

        //Cuando se hace click sobre el buzón 3
        private void BuzonTEMenos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SessionActual.BuzonTEMenos.activo) && (ppSeAcabo.Visibility == Visibility.Collapsed) && (!capturaB1 || !capturaB2 || !capturaB3))
            {
                SessionActual.BuzonTEMas.activo = SessionActual.BuzonTEIntermedio.activo = false;
                SessionActual.BuzonTEMenos.activo = true;

                for (int i = 0; i < SessionActual.BuzonTEMenos.LstImagebuzon.Count; i++)
                    LayoutRoot3.Children.Remove(SessionActual.BuzonTEMenos.LstImagebuzon[i]);

                addImages();

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
            if (SessionActual.BuzonTEMas.LstImagebuzon.Count.Equals(1))
                txtCantidad.Text = SessionActual.BuzonTEMas.LstImagebuzon.Count.ToString() + " talento";
            else
                txtCantidad.Text = SessionActual.BuzonTEMas.LstImagebuzon.Count.ToString() + " talentos";

            if (SessionActual.BuzonTEIntermedio.LstImagebuzon.Count.Equals(1))
                txtCantidad2.Text = SessionActual.BuzonTEIntermedio.LstImagebuzon.Count.ToString() + " talento";
            else
                txtCantidad2.Text = SessionActual.BuzonTEIntermedio.LstImagebuzon.Count.ToString() + " talentos";

            if (SessionActual.BuzonTEMenos.LstImagebuzon.Count.Equals(1))
                txtCantidad3.Text = SessionActual.BuzonTEMenos.LstImagebuzon.Count.ToString() + " talento";
            else
                txtCantidad3.Text = SessionActual.BuzonTEMenos.LstImagebuzon.Count.ToString() + " talentos";
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SessionActual.BuzonTEMas.lstTalento.Count.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && (SessionActual.BuzonTEMenos.lstTalento.Count.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS)))
            {
                for (int i = 0; i < SessionActual.BuzonTEMas.lstTalento.Count; i++)
                    SessionActual.BuzonTEMas.lstTalento[i].seleccionado = true;

                for (int i = 0; i < SessionActual.BuzonTEMenos.lstTalento.Count; i++)
                    SessionActual.BuzonTEMenos.lstTalento[i].seleccionado = true;

                SessionActual.paso1 = false;
                SessionActual.paso2 = true;
                SessionActual.paso3 = false;
                SessionActual.paso4 = false;
                SessionActual.pasoCorrec = false;

                SessionActual.terminoClasificacionTE = true;
                SessionActual.terminoSeleccionTE = true;

                _cambiarContenido.Invoke(Enumerador.Pagina.SeleccionarTalentosEspecificos);
                _cambiarInstruccion.Invoke(Enumerador.Instruccion.InstruccionTalentoEspecifico);
            }
            else
            {
                SessionActual.paso1 = false;
                SessionActual.paso2 = false;
                SessionActual.paso3 = false;
                SessionActual.paso4 = false;
                SessionActual.pasoCorrec = true;

                SessionActual.terminoClasificacionTE = true;
                //todo: validar cuando en el buzon 1 hay 10 O en el buzon 3 hay 5
                _cambiarContenido.Invoke(Enumerador.Pagina.SeleccionarTalentosEspecificos);
                _cambiarInstruccion.Invoke(Enumerador.Instruccion.InstruccionTalentoEspecifico);
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
            if (SessionActual.BuzonTEMas.lstTalento.Count.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && (SessionActual.BuzonTEMenos.lstTalento.Count.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS)))
            {
                for (int i = 0; i < SessionActual.BuzonTEMas.lstTalento.Count; i++)
                    SessionActual.BuzonTEMas.lstTalento[i].seleccionado = true;

                for (int i = 0; i < SessionActual.BuzonTEMenos.lstTalento.Count; i++)
                    SessionActual.BuzonTEMenos.lstTalento[i].seleccionado = true;

                SessionActual.paso1 = false;
                SessionActual.paso2 = false;
                SessionActual.paso3 = true;
                SessionActual.paso4 = false;

                SessionActual.terminoClasificacionTE = true;
                SessionActual.terminoSeleccionTE = true;
                SessionActual.revisaSelecTE = true;
            }
            else
            {
                SessionActual.paso1 = false;
                SessionActual.paso2 = true;
                SessionActual.paso3 = false;
                SessionActual.paso4 = false;

                if (SessionActual.BuzonTEMas.lstTalento.Count > Session.MAX_TALENTOS_MAS_DESARROLLADOS)
                {
                    SessionActual.BuzonTEMas.activo = true;
                    SessionActual.BuzonTEIntermedio.activo = false;
                    SessionActual.BuzonTEMenos.activo = false;
                }
                else
                {
                    if (SessionActual.BuzonTEMenos.lstTalento.Count > Session.MAX_TALENTOS_MENOS_DESARROLLADOS)
                    {
                        SessionActual.BuzonTEMas.activo = false;
                        SessionActual.BuzonTEIntermedio.activo = false;
                        SessionActual.BuzonTEMenos.activo = true;
                    }
                }

                SessionActual.terminoClasificacionTE = true;

                _cambiarContenido.Invoke(Enumerador.Pagina.SeleccionarVirtudes);
                _cambiarInstruccion.Invoke(Enumerador.Instruccion.SinNavegacion);
            }
        }

        public void resultUpdateToResultados_completed(object sender, TalentosReference.ResultadoActualizarCompletedEventArgs e)
        {
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = true;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrec = false;

            SessionActual.terminoClasificacionTE = true;
            SessionActual.terminoSeleccionTE = true;

            //_cambiarContenido.Invoke(Enumerador.Pagina.EnvioReporte);
            //_cambiarInstruccion.Invoke(Enumerador.Instruccion.InstruccionEnvioReporte);
        }

        private void txtNavegacion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock txt = (TextBlock)(sender as TextBlock);
            if (txt.Text.Contains("Continuar"))
            {
                SessionActual.BuzonTEMas.activo = true;

                SessionActual.BuzonTEIntermedio.activo = false;

                ActualizarResultados();
            }
            else
            {
                SessionActual.paso1 = true;
                SessionActual.paso2 = false;
                SessionActual.paso3 = false;
                SessionActual.paso4 = false;
                SessionActual.pasoCorrec = false;

                SessionActual.BuzonTEMas.activo = SessionActual.BuzonTEIntermedio.activo = SessionActual.BuzonTEMenos.activo = false;
                _cambiarContenido.Invoke(Enumerador.Pagina.SeleccionarTalentosEspecificos);
            }
        }
    }
}
