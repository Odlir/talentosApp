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
using System.Threading;

namespace Talentos_Master
{
    public partial class frmSeleccionarTalentosEspecificos : IPaginaContenida
    {
        private Session SessionActual;
        private const int TOTAL = 9;
        private static double IMAGE_WIDTH = 170;//128;        // Ancho de la Imagen
        private static double IMAGE_HEIGHT = 200;//200;//128;       // Altura de la Imagen       
        private static double SPRINESS = 0.4;		    // Controla la velocidad de salto
        private static double DECAY = 0.5;			     // Controla  la velocidad de caida
        private static double SCALE_DOWN_FACTOR = 0.5;  // Scala entre imágenes
        private static double OFFSET_FACTOR = 100;      // Distancia entre imagenes
        private static double OPACITY_DOWN_FACTOR = 0.4;    // Alpha between imagenes
        private static double MAX_SCALE = 1.5;            // Escala Maxima
        private static double CRITICAL_POINT = 0.001;
        private int items;
        private int cantSeleccionada;
        private int cantTotal;
        private double _xCenter;
        private double _yCenter;
        // Display something at the cetner first
        private double _target;		// Target moving position
        private double _current = 0;	// Current position
        private double _spring = 0;		// Temp used to store last moving 		

        private static int FPS = 24;                // fps of the on enter frame event
        private DispatcherTimer _timer = new DispatcherTimer(); // on enter frame simulator

        private bool captura;
        //private Point pos;
        //private Point posOrgin;

        private bool sobre;

        bool b1, b2;//, b3;

        private int WIDTH = 650;
        private int HEIGHT = 340;

        TranslateTransform img2transform;

        double ancho, alto;

        Point pInicio;

        public frmSeleccionarTalentosEspecificos()
        {
            InitializeComponent();

            BusyWindow.IsBusy = true;
            
            // Save the center position
            _xCenter = WIDTH / 2;
            _yCenter = HEIGHT / 2;// Height / 2;
            _target = 0;
            items = 0;

            pInicio = new Point();
            pInicio.X = pInicio.Y = 0;

            SessionActual = Session.getInstance();

            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = false;
            SessionActual.paso4 = false;
            SessionActual.pasoTE = true;
            SessionActual.pasoVirtud = false;

            addImages();
            cantTotal = TOTAL;

            cantSeleccionada = TOTAL - SessionActual.lstImagesTE.Count;

            if (cantSeleccionada == 1)
                txtCantidad.Text = cantSeleccionada.ToString() + " clasificado de " + cantTotal.ToString();
            else
                txtCantidad.Text = cantSeleccionada.ToString() + " clasificados de " + cantTotal.ToString();
            captura = false;
            sobre = false;

            b1 = b2 = false;// = b3 = false;

            txtppVerificarCantidad.Text = "El buzón \"Talento más desarrollado\" debe contener un mínimo de " + Session.MAX_TALENTOS_MAS_DESARROLLADOS.ToString() + " talentos y el buzón \"Talento menos desarrollado\", un mínimo de 5 talentos.";
            ancho = 170;
            alto = 200;

            int busyDisplay = 8;
            int delayDisplay = 600;
            BusyWindow.DisplayAfter = TimeSpan.FromMilliseconds(delayDisplay);
            ThreadPool.QueueUserWorkItem((state) =>
            {
                Thread.Sleep(busyDisplay * 1000);
                Dispatcher.BeginInvoke(() => BusyWindow.IsBusy = false);
            });
        }

        #region efecto buzones
        void EfectoBuzon1()
        {
            ResourceDictionary rd = new temas.temaSelva.Tema();
            this.Resources = rd;

            imgBuzon1.Style = (Style)this.Resources["GlassBorderStyleMarron"];
        }

        void QuitarEfectoBuzon1()
        {
            ResourceDictionary rd = new temas.temaSelva.Tema();
            this.Resources = rd;

            imgBuzon1.Style = (Style)this.Resources["GlassBorderStyle"];

            LinearGradientBrush blanco = new LinearGradientBrush();
            GradientStop b1 = new GradientStop();
            b1.Color = Colors.White;
            blanco.GradientStops.Add(b1);

            txt11.Foreground = blanco;
            txt21.Foreground = blanco;
            txt31.Foreground = blanco;
        }

        void EfectoBuzon2()
        {
            ResourceDictionary rd = new temas.temaSelva.Tema();
            this.Resources = rd;

            imgBuzon2.Style = (Style)this.Resources["GlassBorderStyleMarron"];
        }

        void QuitarEfectoBuzon2()
        {
            ResourceDictionary rd = new temas.temaSelva.Tema();
            this.Resources = rd;

            imgBuzon2.Style = (Style)this.Resources["GlassBorderStyle"];

            LinearGradientBrush blanco = new LinearGradientBrush();
            GradientStop b1 = new GradientStop();
            b1.Color = Colors.White;
            blanco.GradientStops.Add(b1);

            txt12.Foreground = blanco;
            txt22.Foreground = blanco;
            txt32.Foreground = blanco;
        }

        void EfectoBuzon3()
        {
            ResourceDictionary rd = new temas.temaSelva.Tema();
            this.Resources = rd;

            imgBuzon3.Style = (Style)this.Resources["GlassBorderStyleMarron"];
        }

        void QuitarEfectoBuzon3()
        {
            ResourceDictionary rd = new temas.temaSelva.Tema();
            this.Resources = rd;

            imgBuzon3.Style = (Style)this.Resources["GlassBorderStyle"];

            LinearGradientBrush blanco = new LinearGradientBrush();
            GradientStop b1 = new GradientStop();
            b1.Color = Colors.White;
            blanco.GradientStops.Add(b1);

            txt13.Foreground = blanco;
            txt23.Foreground = blanco;
            txt33.Foreground = blanco;
        }

        #endregion

        void _timer_Tick(object sender, EventArgs e)
        {
            if (Math.Abs(_target - _current) < CRITICAL_POINT) return;

            Image image;
            for (int i = 0; i < SessionActual.lstTalentosEspecificos.Count; i++)
            {
                image = SessionActual.lstImagesTE[i];
                posImage(image, i);
            }

            _spring = (_target - _current) * SPRINESS + _spring * DECAY;

            _current += _spring;
        }

        //agregar talentos al carrusel
        private void addImages()
        {
            if (SessionActual.lstImagesTE.Count > 0)
                SessionActual.lstImagesTE.Clear();

            if (SessionActual.lstImgEspaldaTE.Count > 0)
                SessionActual.lstImgEspaldaTE.Clear();

            for (int i = 0; i < SessionActual.lstTalentosEspecificos.Count; i++)
            {
                string url = SessionActual.lstTalentosEspecificos[i].Image;

                Image image = new Image();
                image.Source = new BitmapImage(new Uri(url, UriKind.Absolute));
                image.Width = 170;//128;
                image.Height = 200;//190;//200;//128;
                image.Cursor = System.Windows.Input.Cursors.Hand;
                LayoutRoot.Children.Add(image);
                posImage(image, i);

                // espalda
                Image imagenEsp = new Image();
                url = SessionActual.lstTalentosEspecificos[i].Example;
                imagenEsp.Source = new BitmapImage(new Uri(url, UriKind.Absolute));
                LayoutRoot.Children.Add(imagenEsp);
                posImage(imagenEsp, i);

                imagenEsp.Visibility = Visibility.Collapsed;

                items++;

                image.MouseLeftButtonDown += new MouseButtonEventHandler(img_MouseButtonDown);
                image.MouseLeftButtonUp += new MouseButtonEventHandler(img_MouseButtonUp);
                image.MouseMove += new MouseEventHandler(img_MouseMove);
                SessionActual.lstImagesTE.Add(image);
                SessionActual.lstImgEspaldaTE.Add(imagenEsp);
            }
        }

        //posicionar un talento
        private void posImage(Image image, int index)
        {
            double diffFactor = index - _current;

            // scale and position the image according to their index and current position
            // the one who closer to the _current has the larger scale
            if (image != null)
            {
                ScaleTransform scaleTransform = new ScaleTransform();
                scaleTransform.ScaleX = MAX_SCALE - Math.Abs(diffFactor) * SCALE_DOWN_FACTOR;
                scaleTransform.ScaleY = MAX_SCALE - Math.Abs(diffFactor) * SCALE_DOWN_FACTOR;
                image.RenderTransform = scaleTransform;

                // reposition the image
                double left = _xCenter - (IMAGE_WIDTH * scaleTransform.ScaleX) / 2 + diffFactor * OFFSET_FACTOR;
                double top = _yCenter - (IMAGE_HEIGHT * scaleTransform.ScaleY) / 2 - 40;
                image.Opacity = 1 - Math.Abs(diffFactor) * OPACITY_DOWN_FACTOR;

                image.SetValue(Canvas.LeftProperty, left);
                image.SetValue(Canvas.TopProperty, top);

                // order the element by the scaleX
                image.SetValue(Canvas.ZIndexProperty, (int)Math.Abs(scaleTransform.ScaleX * 100));

                if (pInicio.X != 0)
                {
                    pInicio.X = left;
                    pInicio.Y = top;
                }
            }
        }

        #region Eventos talento
        private void img_MouseEnter(object sender, MouseEventArgs e)
        {
            this.popup1.Visibility = Visibility.Visible;
        }

        private void img_MouseLeave(object sender, MouseEventArgs e)
        {
            this.popup1.Visibility = Visibility.Collapsed;
        }

        private void img_MouseButtonDown(object sender, MouseEventArgs e)
        {
            SessionActual.lstImagesTE[Convert.ToInt32(_target)].CaptureMouse();
            captura = true;

            QuitarEfectoBuzon1();
            QuitarEfectoBuzon2();
            QuitarEfectoBuzon3();
        }

        private void img_MouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (SessionActual.lstImagesTE.Count > Convert.ToInt32(_target))
            {
                if (captura)
                {
                    SessionActual.lstImagesTE[Convert.ToInt32(_target)].ReleaseMouseCapture();

                    if (sobre)
                    {
                        if (SessionActual.lstTalentosEspecificos[Convert.ToInt32(_target)].Image.Contains("a.png"))
                        {
                            string urlFrente = SessionActual.lstTalentosEspecificos[Convert.ToInt32(_target)].Example;
                            string urlEspalda = SessionActual.lstTalentosEspecificos[Convert.ToInt32(_target)].Image;

                            SessionActual.lstTalentosEspecificos[Convert.ToInt32(_target)].Image = urlFrente;
                            SessionActual.lstTalentosEspecificos[Convert.ToInt32(_target)].Example = urlEspalda;
                        }

                        bool quitar = false;
                        if (b1)
                        {
                            if (SessionActual.BuzonTEMas.lstTalento.Count < 3)
                            {
                                SessionActual.BuzonTEMas.lstTalento.Add(SessionActual.lstTalentosEspecificos[Convert.ToInt32(_target)]);
                                quitar = true;
                            }
                        }
                        else
                            if (b2)
                            {
                                SessionActual.BuzonTEIntermedio.lstTalento.Add(SessionActual.lstTalentosEspecificos[Convert.ToInt32(_target)]);
                                quitar = true;
                            }
                            else if (SessionActual.BuzonTEMenos.lstTalento.Count < 3)
                            {
                                SessionActual.BuzonTEMenos.lstTalento.Add(SessionActual.lstTalentosEspecificos[Convert.ToInt32(_target)]);
                                quitar = true;
                            }
                        b1 = b2 = false;//b3 = false;
                        for (int i = 0; i < SessionActual.lstImagesTE.Count; i++)
                        {
                            LayoutRoot.Children.Remove(SessionActual.lstImagesTE[i]);
                        }
                        if (quitar)
                        {
                            SessionActual.lstTalentosEspecificos.RemoveAt(Convert.ToInt32(_target));
                            cantSeleccionada++;
                        }
                        addImages();

                        if ((SessionActual.lstImagesTE.Count > 0) && !Equals(SessionActual.lstImagesTE.Count, Convert.ToInt32(_target)))
                            posImage(SessionActual.lstImagesTE[Convert.ToInt32(_target)], Convert.ToInt32(_target));
                        else
                            MoveLeft();

                        sobre = false;

                        if (cantSeleccionada == 1)
                            txtCantidad.Text = cantSeleccionada.ToString() + " clasificado de " + cantTotal.ToString();
                        else
                            txtCantidad.Text = cantSeleccionada.ToString() + " clasificados de " + cantTotal.ToString();

                        if (SessionActual.BuzonTEMas.lstTalento.Count >= 0 && SessionActual.BuzonTEMenos.lstTalento.Count >= 0)
                        {
                            if (SessionActual.lstImagesTE.Count.Equals(0))
                            {
                                LayoutRoot.Children.Remove(txtCantidad);
                                LayoutRoot.Children.Remove(MoveLeftButton);
                                LayoutRoot.Children.Remove(MoveRightButton);
                                LayoutRoot.Children.Remove(buttseven);
                                _cambiarInstruccion.Invoke(Enumerador.Instruccion.EnBlanco);
                                imgBuzon1.Visibility = Visibility.Collapsed;
                                imgBuzon2.Visibility = Visibility.Collapsed;
                                imgBuzon3.Visibility = Visibility.Collapsed;
                                ppSeAcabo.Visibility = Visibility.Visible;
                                LayoutRoot.Visibility = Visibility.Collapsed;
                            }
                        }
                        else
                        {
                            if (SessionActual.lstImagesTE.Count.Equals(0))
                            {
                                LayoutRoot.Children.Remove(txtCantidad);
                                LayoutRoot.Children.Remove(MoveLeftButton);
                                LayoutRoot.Children.Remove(MoveRightButton);
                                LayoutRoot.Children.Remove(buttseven);
                                imgBuzon1.Visibility = Visibility.Collapsed;
                                imgBuzon2.Visibility = Visibility.Collapsed;
                                imgBuzon3.Visibility = Visibility.Collapsed;
                                _cambiarInstruccion.Invoke(Enumerador.Instruccion.EnBlanco);
                                ppVerificarCantidad.Visibility = Visibility.Visible;
                                LayoutRoot.Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                    else
                    {
                        img2transform = new TranslateTransform();

                        img2transform.X = pInicio.X;//_xCenter/4;
                        img2transform.Y = pInicio.Y;//_yCenter/4;

                        SessionActual.lstImagesTE[Convert.ToInt32(_target)].Width = ancho;//270;//356;
                        SessionActual.lstImagesTE[Convert.ToInt32(_target)].Height = alto;//300;// 356;
                        SessionActual.lstImagesTE[Convert.ToInt32(_target)].RenderTransform = img2transform;
                        SessionActual.lstImagesTE[Convert.ToInt32(_target)].Opacity = 1.0;

                        posImage(SessionActual.lstImagesTE[Convert.ToInt32(_target)], Convert.ToInt32(_target));
                    }

                    captura = false;

                    QuitarEfectoBuzon1();
                    QuitarEfectoBuzon2();
                    QuitarEfectoBuzon3();
                }
            }
        }

        private void img_MouseMove(object sender, MouseEventArgs e)
        {
            Point pFinal = e.GetPosition(this);

            if (SessionActual.lstImagesTE.Count > Convert.ToInt32(_target))
            {
                if (captura)
                {
                    LayoutRoot.Children.Remove(SessionActual.lstImagesTE[Convert.ToInt32(_target)]);

                    img2transform = new TranslateTransform();

                    img2transform.X = pFinal.X - 300;//250;
                    img2transform.Y = pFinal.Y - 100;

                    SessionActual.lstImagesTE[Convert.ToInt32(_target)].Width = 156;//356;
                    SessionActual.lstImagesTE[Convert.ToInt32(_target)].Height = 246;// 356;
                    SessionActual.lstImagesTE[Convert.ToInt32(_target)].RenderTransform = img2transform;
                    SessionActual.lstImagesTE[Convert.ToInt32(_target)].Opacity = 1.0;

                    LayoutRoot.Children.Add(SessionActual.lstImagesTE[Convert.ToInt32(_target)]);

                    #region Ingreso a Buzones

                    if (pFinal.X > 10 && pFinal.X <= 170 && pFinal.Y > 260 && pFinal.Y <= 480)
                    {
                        sobre = true;

                        SessionActual.lstImagesTE[Convert.ToInt32(_target)].Opacity = 0.5;
                        SessionActual.lstImagesTE[Convert.ToInt32(_target)].Width = 156;
                        SessionActual.lstImagesTE[Convert.ToInt32(_target)].Height = 246;

                        b1 = b2 = false;
                        b1 = true;

                        EfectoBuzon1();
                        QuitarEfectoBuzon2();
                        QuitarEfectoBuzon3();
                    }
                    else // para el buzon 2
                    {
                        if (pFinal.X > 260 && pFinal.X <= 390 && pFinal.Y > 260 && pFinal.Y <= 480)
                        {
                            sobre = true;

                            SessionActual.lstImagesTE[Convert.ToInt32(_target)].Opacity = 0.5;

                            SessionActual.lstImagesTE[Convert.ToInt32(_target)].Width = 156;

                            SessionActual.lstImagesTE[Convert.ToInt32(_target)].Height = 246;

                            b1 = b2 = false;// b3 = false;
                            b2 = true;

                            EfectoBuzon2();
                            QuitarEfectoBuzon1();
                            QuitarEfectoBuzon3();
                        }
                        else
                        {
                            if (pFinal.X > 450 && pFinal.X <= 620 && pFinal.Y > 260 && pFinal.Y <= 480)
                            {
                                sobre = true;

                                SessionActual.lstImagesTE[Convert.ToInt32(_target)].Opacity = 0.5;

                                SessionActual.lstImagesTE[Convert.ToInt32(_target)].Width = 156;

                                SessionActual.lstImagesTE[Convert.ToInt32(_target)].Height = 246;

                                b1 = b2 = false; // b3 = false;

                                EfectoBuzon3();
                                QuitarEfectoBuzon1();
                                QuitarEfectoBuzon2();
                            }
                        }
                    }

                    #endregion

                    if (pFinal.X > 650 || pFinal.Y > 480 || pFinal.X < 0 || pFinal.Y < 0)
                    {
                        img2transform = new TranslateTransform();

                        SessionActual.lstImagesTE[Convert.ToInt32(_target)].Width = ancho;//356;
                        SessionActual.lstImagesTE[Convert.ToInt32(_target)].Height = alto;// 356;
                        SessionActual.lstImagesTE[Convert.ToInt32(_target)].Opacity = 1.0;

                        posImage(SessionActual.lstImagesTE[Convert.ToInt32(_target)], Convert.ToInt32(_target));
                    }
                }
            }
        }
        #endregion

        public void moveIndex(int value)
        {
            _target += value;
            _target = Math.Max(0, _target);
            _target = Math.Min(SessionActual.lstTalentosEspecificos.Count - 1, _target);
        }

        public void Start()
        {
            // start the enter frame event
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / FPS);
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Start();
        }

        // Mover talentos a la izquierda
        public void MoveLeft()
        {
            moveIndex(-1);
            Start();
        }

        // Mover talentos a la derecha
        public void MoveRight()
        {
            moveIndex(1);
            Start();
        }

        private void btnCancelarSeAcabo_Click(object sender, RoutedEventArgs e)
        {
            ppSeAcabo.Visibility = Visibility.Collapsed;
            LayoutRoot.Visibility = Visibility.Visible;

            imgBuzon1.Visibility = Visibility.Visible;
            imgBuzon2.Visibility = Visibility.Visible;
            imgBuzon3.Visibility = Visibility.Visible;

            SessionActual.BuzonTEMas.activo = true;
            SessionActual.BuzonTEIntermedio.activo = false;

            resultUpdate_completed();
        }

        private void resultUpdate_completed()
        {
            if ((SessionActual.BuzonTEMas.lstTalento.Count + SessionActual.BuzonTEMenos.lstTalento.Count + SessionActual.BuzonTEIntermedio.lstTalento.Count).Equals(SessionActual.lstTalentosEspecificos))
            {
                for (int i = 0; i < SessionActual.BuzonTEMas.lstTalento.Count; i++)
                    SessionActual.BuzonTEMas.lstTalento[i].seleccionado = true;

                for (int i = 0; i < SessionActual.BuzonTEMenos.lstTalento.Count; i++)
                    SessionActual.BuzonTEMenos.lstTalento[i].seleccionado = true;

                SessionActual.paso1 = false;
                SessionActual.paso2 = false;
                SessionActual.paso3 = true;
                SessionActual.paso4 = false;

                SessionActual.terminoClasificacion = true;
                SessionActual.terminoSeleccion = true;

                SessionActual.revisaSelec = true;

                resultUpdateToResultados_completed();
            }
            else
            {
                SessionActual.paso1 = false;
                SessionActual.paso2 = false;
                SessionActual.paso3 = false;
                SessionActual.paso4 = false;

                if (SessionActual.BuzonTEMas.lstTalento.Count > 0)
                {
                    SessionActual.BuzonTEMas.activo = true;
                    SessionActual.BuzonTEIntermedio.activo = false;
                    SessionActual.BuzonTEMenos.activo = false;
                }
                else
                {
                    if (SessionActual.BuzonTEMenos.lstTalento.Count > 0)
                    {
                        SessionActual.BuzonTEMas.activo = false;
                        SessionActual.BuzonTEIntermedio.activo = false;
                        SessionActual.BuzonTEMenos.activo = true;
                    }
                }

                SessionActual.terminoClasificacion = true;
                _cambiarContenido.Invoke(Enumerador.Pagina.SeleccionarVirtudes);
                _cambiarInstruccion.Invoke(Enumerador.Instruccion.SinNavegacion);
            }
        }

        private void resultUpdateToResultados_completed()
        {
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = true;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrec = false;

            SessionActual.terminoClasificacionTE = true;
            SessionActual.terminoSeleccionTE = true;

            _cambiarContenido.Invoke(Enumerador.Pagina.SeleccionarVirtudes);
            _cambiarInstruccion.Invoke(Enumerador.Instruccion.SinNavegacion);
        }

        private void btnAceptarSeAcabo_Click(object sender, RoutedEventArgs e)
        {
            _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);

            SessionActual.BuzonTEMas.activo = SessionActual.Buzon2.activo = SessionActual.BuzonTEMenos.activo = false;

            SessionActual.BuzonTEMas.activo = true;
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = false;
            SessionActual.paso4 = false;
            SessionActual.pasoTE = false;
            SessionActual.pasoCorrecTE = true;
            _cambiarContenido.Invoke(Enumerador.Pagina.SeleccionarVirtudes);
            _cambiarInstruccion.Invoke(Enumerador.Instruccion.SinNavegacion);
        }

        //ver talentos destinados al buzón 1
        private void buzon1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SessionActual.BuzonTEMas.activo) && (!SessionActual.lstTalentosEspecificos.Count.Equals(0)) && (!captura))
            {
                SessionActual.BuzonTEIntermedio.activo = SessionActual.BuzonTEMenos.activo = false;
                SessionActual.BuzonTEMas.activo = true;
                SessionActual.pasoCorrecTE = true;
                SessionActual.paso1 = false;
                SessionActual.paso2 = false;
                SessionActual.paso3 = false;
                SessionActual.paso4 = false;
                _cambiarContenido.Invoke(Enumerador.Pagina.ClasificacionDetalleTE);
                _cambiarInstruccion.Invoke(Enumerador.Instruccion.Anterior);
            }
        }

        //ver talentos destinados al buzón 2
        private void buzon2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SessionActual.BuzonTEIntermedio.activo) && (!SessionActual.lstTalentosEspecificos.Count.Equals(0)) && (!captura))
            {
                SessionActual.BuzonTEMas.activo = SessionActual.BuzonTEMenos.activo = false;
                SessionActual.BuzonTEIntermedio.activo = true;
                SessionActual.pasoCorrecTE = true;
                SessionActual.paso1 = false;
                SessionActual.paso2 = false;
                SessionActual.paso3 = false;
                SessionActual.paso4 = false;
                _cambiarContenido.Invoke(Enumerador.Pagina.ClasificacionDetalleTE);
                _cambiarInstruccion.Invoke(Enumerador.Instruccion.Anterior);
            }
        }

        //ver talentos destinados al buzón 3
        private void buzon3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SessionActual.BuzonTEMenos.activo) && (!SessionActual.lstTalentosEspecificos.Count.Equals(0)) && (!captura))
            {
                SessionActual.BuzonTEMas.activo = SessionActual.BuzonTEIntermedio.activo = false;
                SessionActual.BuzonTEMenos.activo = true;
                SessionActual.pasoCorrecTE = true;
                SessionActual.paso1 = false;
                SessionActual.paso2 = false;
                SessionActual.paso3 = false;
                SessionActual.paso4 = false;
                _cambiarContenido.Invoke(Enumerador.Pagina.ClasificacionDetalleTE);
                _cambiarInstruccion.Invoke(Enumerador.Instruccion.Anterior);
            }
        }

        //evento mover carrusel a la izquierda
        private void izquierdaBuzon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!captura)
                MoveLeft();
        }

        //evento mover carrusel a la derecha
        private void derechaBuzon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!captura)
                MoveRight();
        }

        #region eventos buzones

        private void imgBuzon1_MouseLeave(object sender, MouseEventArgs e)
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

        private void imgBuzon1_MouseEnter(object sender, MouseEventArgs e)
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

        private void imgBuzon2_MouseLeave(object sender, MouseEventArgs e)
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

        private void imgBuzon2_MouseEnter(object sender, MouseEventArgs e)
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

        private void imgBuzon3_MouseLeave(object sender, MouseEventArgs e)
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

        private void imgBuzon3_MouseEnter(object sender, MouseEventArgs e)
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

        #endregion

        private void btnAceptarSeAcabo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);

            SessionActual.BuzonTEIntermedio.activo = SessionActual.BuzonTEMenos.activo = false;
            SessionActual.BuzonTEMas.activo = true;
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = false;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrecTE = true;

            SessionActual.revisaClasif = true;
            _cambiarContenido.Invoke(Enumerador.Pagina.ClasificacionDetalleTE);
            _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);
        }

        private void btnCancelarSeAcabo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ppSeAcabo.Visibility = Visibility.Collapsed;
            LayoutRoot.Visibility = Visibility.Visible;

            SessionActual.BuzonTEMas.activo = true;
            SessionActual.BuzonTEIntermedio.activo = false;

            resultUpdate_completed();
        }

        private void btnAceptarCorregir_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);

            SessionActual.BuzonTEIntermedio.activo = SessionActual.BuzonTEMenos.activo = false;
            SessionActual.BuzonTEMas.activo = true;
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = false;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrecTE = true;
            _cambiarInstruccion.Invoke(Enumerador.Instruccion.SinNavegacion);
            _cambiarContenido.Invoke(Enumerador.Pagina.SeleccionarVirtudes);
        }
    }
}
