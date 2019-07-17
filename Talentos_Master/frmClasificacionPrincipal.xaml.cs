using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.Threading;

namespace Talentos_Master
{
    /// <summary>
    //ESTA PÁGINA CONTIENE EL CARROUSEL PRINCIPAL DE TALENTOS EN EL QUE SE DEBEN CLASIFICAR ESTOS TALENTOS (REGLAS DEL JUEGO)
    /// <summary>
    public partial class frmClasificacionPrincipal : IPaginaContenida
    {
        private const int TOTAL = 54;
        
        private static double IMAGE_WIDTH = 170;//128;        // Ancho de la Imagen
        private static double IMAGE_HEIGHT = 200;//200;//128;       // Altura de la Imagen       
        private static double SPRINESS = 0.4;		    // Controla la velocidad de salto
        private static double DECAY = 0.5;			     // Controla  la velocidad de caida
        private static double SCALE_DOWN_FACTOR = 0.5;  // Scala entre imágenes
        private static double OFFSET_FACTOR = 100;      // Distancia entre imagenes
        private static double OPACITY_DOWN_FACTOR = 0.4;    // Alpha between imagenes
        private static double MAX_SCALE = 1.5;            // Escala Maxima
        private static double CRITICAL_POINT = 0.001;

        private Session SessionActual;
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
        private bool sobre;

        bool b1, b2;//, b3;

        private int WIDTH = 650;
        private int HEIGHT = 340;

        TranslateTransform img2transform;
        
        double ancho, alto;

        Point pInicio;

        public frmClasificacionPrincipal()
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
                        
            SessionActual.paso2 = false;
            SessionActual.paso3 = false;
            SessionActual.paso4 = false;

            addImages();
            cantTotal = TOTAL;

            cantSeleccionada = TOTAL - SessionActual.lstImages.Count;

            if (cantSeleccionada == 1)
                txtCantidad.Text = cantSeleccionada.ToString() + " clasificado de " + TOTAL.ToString();
            else
                txtCantidad.Text = cantSeleccionada.ToString() + " clasificados de " + TOTAL.ToString();
            captura = false;
            sobre = false;
            
            b1 = b2 = false;// = b3 = false;

            txtppVerificarCantidad.Text = "El buzón \"Talento más desarrollado\" debe contener un mínimo de " + Session.MAX_TALENTOS_MAS_DESARROLLADOS.ToString() + " talentos y el buzón \"Talento menos desarrollado\", un mínimo de 5 talentos.";
            ancho = 170;
            alto = 200;

            //InsertarJuego();

            int busyDisplay = 8;
            int delayDisplay = 600;
            BusyWindow.DisplayAfter = TimeSpan.FromMilliseconds(delayDisplay);
            ThreadPool.QueueUserWorkItem((state) =>
            {
                Thread.Sleep(busyDisplay * 1000);
                Dispatcher.BeginInvoke(() => BusyWindow.IsBusy = false);
            });
        }

        #region Insertar resultado en la BD
        private void InsertarJuego()
        {
            TalentosReference.ResultadoBE objResultadoBE = new Talentos_Master.TalentosReference.ResultadoBE();
            Session sesion = Session.getInstance();

            objResultadoBE.Participante_id = SessionActual.participante.UsuarioId;
            objResultadoBE.Fecha = DateTime.Now;
            objResultadoBE.EsMasivo = sesion.EsMasivo;
            objResultadoBE.DNI = SessionActual.DNI;
            objResultadoBE.CorreoElectronico = SessionActual.CorreoParticipanteMasivo;
            objResultadoBE.CodEvaluacion = SessionActual.CodEvaluacion;
            objResultadoBE.NombreParticipante = sesion.participante.Nombres + " " + sesion.participante.APaterno;
            TalentosReference.WSTalentosSoapClient ws = new Talentos_Master.TalentosReference.WSTalentosSoapClient();

            ws.InsertarResultadoCompleted += new EventHandler<Talentos_Master.TalentosReference.InsertarResultadoCompletedEventArgs>(ResultadoInsert_completed);
            ws.InsertarResultadoAsync(objResultadoBE);
        }

        private void ResultadoInsert_completed(object sender, TalentosReference.InsertarResultadoCompletedEventArgs e)
        {
            SessionActual.resultado.Resultado_id = e.Result;
            BusyWindow.IsBusy = false;
        }

        #endregion

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
            for (int i = 0; i < SessionActual.lstTalentos.Count; i++)
            {
                image = SessionActual.lstImages[i];
                posImage(image, i);
            }

            _spring = (_target - _current) * SPRINESS + _spring * DECAY;

            _current += _spring;
        }

        //agregar talentos al carrusel
        private void addImages()
        {
            if(SessionActual.lstImages.Count>0)
            SessionActual.lstImages.Clear();

            if(SessionActual.lstImgEspalda.Count>0)
            SessionActual.lstImgEspalda.Clear();

            for (int i = 0; i < SessionActual.lstTalentos.Count; i++)
            {
                string url = SessionActual.lstTalentos[i].Image;

                Image image = new Image();
                image.Source = new BitmapImage(new Uri(url, UriKind.Absolute));
                image.Width = 170;//128;
                image.Height = 200;//190;//200;//128;
                image.Cursor = System.Windows.Input.Cursors.Hand;
                LayoutRoot.Children.Add(image);
                posImage(image, i);

                // espalda
                Image imagenEsp = new Image();
                url = SessionActual.lstTalentos[i].Example;
                imagenEsp.Source = new BitmapImage(new Uri(url, UriKind.Absolute));
                LayoutRoot.Children.Add(imagenEsp);
                posImage(imagenEsp, i);

                imagenEsp.Visibility = Visibility.Collapsed;

                items++;

                image.MouseLeftButtonDown += new MouseButtonEventHandler(img_MouseButtonDown);
                image.MouseLeftButtonUp += new MouseButtonEventHandler(img_MouseButtonUp);
                image.MouseMove += new MouseEventHandler(img_MouseMove);
                SessionActual.lstImages.Add(image);
                SessionActual.lstImgEspalda.Add(imagenEsp);
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
            SessionActual.lstImages[Convert.ToInt32(_target)].CaptureMouse();
            captura = true;

            QuitarEfectoBuzon1();
            QuitarEfectoBuzon2();
            QuitarEfectoBuzon3();
        }

        private void img_MouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Point pFinal = e.GetPosition(this);

            if (SessionActual.lstImages.Count > Convert.ToInt32(_target))
            {
                if (captura)
                {
                    SessionActual.lstImages[Convert.ToInt32(_target)].ReleaseMouseCapture();

                    if (sobre)
                    {
                        if (SessionActual.lstTalentos[Convert.ToInt32(_target)].Image.Contains("a.png"))
                        {
                            string urlFrente = SessionActual.lstTalentos[Convert.ToInt32(_target)].Example;
                            string urlEspalda = SessionActual.lstTalentos[Convert.ToInt32(_target)].Image;

                            SessionActual.lstTalentos[Convert.ToInt32(_target)].Image = urlFrente;
                            SessionActual.lstTalentos[Convert.ToInt32(_target)].Example = urlEspalda;
                        }

                        if (b1)
                            //buzones.buzon1.lstTalento.Add(talento.lstTalento[Convert.ToInt32(_target)]);
                            SessionActual.Buzon1.lstTalento.Add(SessionActual.lstTalentos[Convert.ToInt32(_target)]);
                        else
                            if (b2)
                                // buzones.buzon2.lstTalento.Add(talento.lstTalento[Convert.ToInt32(_target)]);
                                SessionActual.Buzon2.lstTalento.Add(SessionActual.lstTalentos[Convert.ToInt32(_target)]);
                            else
                                // buzones.buzon3.lstTalento.Add(talento.lstTalento[Convert.ToInt32(_target)]);
                                SessionActual.Buzon3.lstTalento.Add(SessionActual.lstTalentos[Convert.ToInt32(_target)]);
                        b1 = b2 = false;//b3 = false;
                        //for (int i = 0; i < talento.lstImagesFrente.Count; i++)
                        for (int i = 0; i < SessionActual.lstImages.Count; i++)
                        {
                            //LayoutRoot.Children.Remove(talento.lstImagesFrente[i]);
                            LayoutRoot.Children.Remove(SessionActual.lstImages[i]);
                        }
                        //talento.lstTalento.RemoveAt(Convert.ToInt32(_target));
                        SessionActual.lstTalentos.RemoveAt(Convert.ToInt32(_target));
                        cantSeleccionada++;
                        addImages();


                    if ((SessionActual.lstImages.Count > 0) && !Equals(SessionActual.lstImages.Count, Convert.ToInt32(_target)))
                        posImage(SessionActual.lstImages[Convert.ToInt32(_target)], Convert.ToInt32(_target));
                    else
                        MoveLeft();

                    sobre = false;

                    if (cantSeleccionada == 1)
                        txtCantidad.Text = cantSeleccionada.ToString() + " clasificado de " + cantTotal.ToString();
                    else
                        txtCantidad.Text = cantSeleccionada.ToString() + " clasificados de " + cantTotal.ToString();

                
                    if (SessionActual.Buzon1.lstTalento.Count >= Session.MAX_TALENTOS_MAS_DESARROLLADOS && SessionActual.Buzon3.lstTalento.Count >= Session.MAX_TALENTOS_MENOS_DESARROLLADOS)
                    {
                        if (SessionActual.lstImages.Count.Equals(0))
                        {
                            LayoutRoot.Children.Remove(txtCantidad);
                            LayoutRoot.Children.Remove(MoveLeftButton);
                            LayoutRoot.Children.Remove(MoveRightButton);
                            LayoutRoot.Children.Remove(buttseven);
                            _cambiarInstruccion.Invoke(Enumerador.Instruccion.EnBlanco);
                            imgBuzon1.Visibility = Visibility.Collapsed;
                            imgBuzon2.Visibility = Visibility.Collapsed;
                            imgBuzon3.Visibility = Visibility.Collapsed;
                            //ppSeAcabo.IsOpen = true;
                            ppSeAcabo.Visibility = Visibility.Visible;
                            LayoutRoot.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                            if (SessionActual.lstImages.Count.Equals(0))
                            {
                                LayoutRoot.Children.Remove(txtCantidad);
                                LayoutRoot.Children.Remove(MoveLeftButton);
                                LayoutRoot.Children.Remove(MoveRightButton);
                                LayoutRoot.Children.Remove(buttseven);
                                imgBuzon1.Visibility = Visibility.Collapsed;
                                imgBuzon2.Visibility = Visibility.Collapsed;
                                imgBuzon3.Visibility = Visibility.Collapsed;
                                _cambiarInstruccion.Invoke(Enumerador.Instruccion.EnBlanco);
                                //ppVerificarCantidad.IsOpen = true;
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

                        SessionActual.lstImages[Convert.ToInt32(_target)].Width = ancho;//270;//356;
                        SessionActual.lstImages[Convert.ToInt32(_target)].Height = alto;//300;// 356;
                        SessionActual.lstImages[Convert.ToInt32(_target)].RenderTransform = img2transform;
                        SessionActual.lstImages[Convert.ToInt32(_target)].Opacity = 1.0;

                        posImage(SessionActual.lstImages[Convert.ToInt32(_target)], Convert.ToInt32(_target));
                     }
                        //SessionActual.lstImages[Convert.ToInt32(_target)].ReleaseMouseCapture();

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

            if (SessionActual.lstImages.Count > Convert.ToInt32(_target)) 
            {
                if (captura) 
                {
                    LayoutRoot.Children.Remove(SessionActual.lstImages[Convert.ToInt32(_target)]);

                     img2transform = new TranslateTransform();

                     img2transform.X = pFinal.X - 300;//250;
                     img2transform.Y = pFinal.Y-100 ;
                 
                     SessionActual.lstImages[Convert.ToInt32(_target)].Width = 156;//356;
                     SessionActual.lstImages[Convert.ToInt32(_target)].Height = 246;// 356;
                     SessionActual.lstImages[Convert.ToInt32(_target)].RenderTransform = img2transform;
                     SessionActual.lstImages[Convert.ToInt32(_target)].Opacity = 1.0;
                     
                     LayoutRoot.Children.Add(SessionActual.lstImages[Convert.ToInt32(_target)]);

                     #region Ingreso a Buzones
            //         // Para el buzon 1 

                     if (pFinal.X > 10 && pFinal.X <= 170 && pFinal.Y > 260 && pFinal.Y <= 480)
                     {
                         sobre = true;

                         SessionActual.lstImages[Convert.ToInt32(_target)].Opacity = 0.5;
                         SessionActual.lstImages[Convert.ToInt32(_target)].Width = 156;

                         SessionActual.lstImages[Convert.ToInt32(_target)].Height = 246;

                         b1 = b2 = false;// b3 = false;
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

                             SessionActual.lstImages[Convert.ToInt32(_target)].Opacity = 0.5;

                             SessionActual.lstImages[Convert.ToInt32(_target)].Width = 156;

                             SessionActual.lstImages[Convert.ToInt32(_target)].Height = 246;

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

                                 SessionActual.lstImages[Convert.ToInt32(_target)].Opacity = 0.5;

                                 SessionActual.lstImages[Convert.ToInt32(_target)].Width = 156;

                                 SessionActual.lstImages[Convert.ToInt32(_target)].Height = 246;

                                 b1 = b2 = false; // b3 = false;

                                 EfectoBuzon3();
                                 QuitarEfectoBuzon1();
                                 QuitarEfectoBuzon2();
                             }
                         }
                     }
                     
                    #endregion

                     if (pFinal.X > 650 || pFinal.Y > 480 || pFinal.X < 0 || pFinal.Y < 0)
                     // return;
                     {
                         img2transform = new TranslateTransform();

                         SessionActual.lstImages[Convert.ToInt32(_target)].Width = ancho;//356;
                         SessionActual.lstImages[Convert.ToInt32(_target)].Height = alto;// 356;
                         SessionActual.lstImages[Convert.ToInt32(_target)].Opacity = 1.0;
                         
                         posImage(SessionActual.lstImages[Convert.ToInt32(_target)], Convert.ToInt32(_target));
                     }
                }
           }
        }
        #endregion

        public void moveIndex(int value)
        {
            _target += value;
            _target = Math.Max(0, _target);
           // _target = Math.Min(talento.lstTalento.Count - 1, _target);
            _target = Math.Min(SessionActual.lstTalentos.Count - 1, _target);
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
          
            SessionActual.Buzon1.activo = true;
            SessionActual.Buzon2.activo = false;

            TalentosReference.ResultadoBE rpta = new Talentos_Master.TalentosReference.ResultadoBE();
            rpta.Resultado_id = SessionActual.resultado.Resultado_id;

            for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
            {
                rpta.TalentoId = string.Concat(rpta.TalentoId ,SessionActual.Buzon1.lstTalento[i].IdTalento.ToString(), ", ");
                rpta.BuzonId = string.Concat(rpta.BuzonId, "1, ");
            }

            for (int i = 0; i < SessionActual.Buzon2.lstTalento.Count; i++)
            {
                rpta.TalentoId = string.Concat(rpta.TalentoId ,SessionActual.Buzon2.lstTalento[i].IdTalento.ToString(), ", ");
                rpta.BuzonId = string.Concat(rpta.BuzonId, "2, ");
            }

            for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
            {
                rpta.TalentoId = string.Concat(rpta.TalentoId, SessionActual.Buzon3.lstTalento[i].IdTalento.ToString(), ", ");
                rpta.BuzonId = string.Concat(rpta.BuzonId, "3, ");
            }

                rpta.Fecha = DateTime.Now;
                rpta.Seleccionado = "";
                rpta.Puntaje = "";
                rpta.Participante_id = SessionActual.resultado.Participante_id;

                SessionActual.resultado.TalentoId = rpta.TalentoId;
                SessionActual.resultado.BuzonId = rpta.BuzonId;
                SessionActual.resultado.Fecha = rpta.Fecha;
                SessionActual.resultado.Resultado_id = rpta.Resultado_id;

                resultUpdate_completed();
        }

    private void resultUpdate_completed()    
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

                //SessionActual.revisaSelec = true;

                //resultUpdateToResultados_completed();

                //SessionActual.paso1 = false;
                //SessionActual.paso2 = true;
                //SessionActual.paso3 = false;
                //SessionActual.paso4 = false;

                SessionActual.Buzon1.activo = false;
                SessionActual.Buzon2.activo = false;
                SessionActual.Buzon3.activo = true;

                //SessionActual.terminoClasificacion = true;
                _cambiarContenido.Invoke(Enumerador.Pagina.SeleccionarPrincipal);
                _cambiarInstruccion.Invoke(Enumerador.Instruccion.SegundaInstruccion);
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

    private void resultUpdateToResultados_completed()    
    {
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = true;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrec = false;

            SessionActual.terminoClasificacion = true;
            SessionActual.terminoSeleccion = true;

            _cambiarContenido.Invoke(Enumerador.Pagina.SeleccionarPrincipal);
            _cambiarInstruccion.Invoke(Enumerador.Instruccion.SegundaInstruccion);
        }
        
        private void btnAceptarSeAcabo_Click(object sender, RoutedEventArgs e)
        {
            _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);
           
            SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = SessionActual.Buzon3.activo = false;
         
            SessionActual.Buzon1.activo = true;
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = false;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrec = true;
            _cambiarContenido.Invoke(Enumerador.Pagina.ClasificacionDetalle);
            _cambiarInstruccion.Invoke(Enumerador.Instruccion.SinNavegacion);
        }

        //ver talentos destinados al buzón 1
        private void buzon1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SessionActual.Buzon1.activo)&& (!SessionActual.lstTalentos.Count.Equals(0)) && (!captura))
            {
                SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = SessionActual.Buzon3.activo = false;
                SessionActual.Buzon1.activo = true;
                SessionActual.pasoCorrec = true;
                SessionActual.paso1 = false;
                SessionActual.paso2 = false;
                SessionActual.paso3 = false;
                SessionActual.paso4 = false;
                _cambiarContenido.Invoke(Enumerador.Pagina.ClasificacionDetalle);
                _cambiarInstruccion.Invoke(Enumerador.Instruccion.Anterior);
            }
        }

        //ver talentos destinados al buzón 2
        private void buzon2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SessionActual.Buzon2.activo) && (!SessionActual.lstTalentos.Count.Equals(0)) &&(!captura))
            {
                SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = SessionActual.Buzon3.activo = false;
                //buzones.b2 = true;
                SessionActual.Buzon2.activo = true;
                SessionActual.pasoCorrec = true;
                SessionActual.paso1 = false;
                SessionActual.paso2 = false;
                SessionActual.paso3 = false;
                SessionActual.paso4 = false;
                _cambiarContenido.Invoke(Enumerador.Pagina.ClasificacionDetalle);
                _cambiarInstruccion.Invoke(Enumerador.Instruccion.Anterior);
            }
        }

        //ver talentos destinados al buzón 3
        private void buzon3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SessionActual.Buzon3.activo) && (!SessionActual.lstTalentos.Count.Equals(0)) && (!captura))
            {
                SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = SessionActual.Buzon3.activo = false;
                
                SessionActual.Buzon3.activo = true;
                SessionActual.pasoCorrec = true;
                SessionActual.paso1 = false;
                SessionActual.paso2 = false;
                SessionActual.paso3 = false;
                SessionActual.paso4 = false;
                _cambiarContenido.Invoke(Enumerador.Pagina.ClasificacionDetalle);
                _cambiarInstruccion.Invoke(Enumerador.Instruccion.Anterior);
            }
        }

        //evento mover carrusel a la izquierda
        private void izquierdaBuzon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(!captura)
            MoveLeft();
        }

        //evento mover carrusel a la derecha
        private void derechaBuzon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!captura)
            MoveRight();
        }

        //voltear talento
        private void btnVoltear_Click(object sender, RoutedEventArgs e)
        {
            if ((SessionActual.lstTalentos.Count > 0) && SessionActual.lstTalentos.Count > Convert.ToInt32(_target) && (!captura))
            {
                Image auxImage = new Image();
                
                String auxUrl = SessionActual.lstTalentos[Convert.ToInt32(_target)].Image;
                String auxUrl2 = SessionActual.lstTalentos[Convert.ToInt32(_target)].Example;
                
                SessionActual.lstTalentos[Convert.ToInt32(_target)].Image = auxUrl2;
                SessionActual.lstTalentos[Convert.ToInt32(_target)].Example = auxUrl;
                
                for (int i = 0; i < SessionActual.lstTalentos.Count; i++)
                {
                    LayoutRoot.Children.Remove(SessionActual.lstImages[i]);
                    LayoutRoot.Children.Remove(SessionActual.lstImgEspalda[i]);
                }
                items = 0;
                addImages();
            }
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
           
            SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = SessionActual.Buzon3.activo = false;
            
            SessionActual.Buzon1.activo = true;
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = false;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrec = true;

            SessionActual.revisaClasif = true;
            _cambiarContenido.Invoke(Enumerador.Pagina.ClasificacionDetalle);
            _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);
        }

        private void btnCancelarSeAcabo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ppSeAcabo.Visibility = Visibility.Collapsed;
            LayoutRoot.Visibility = Visibility.Visible;
            
            SessionActual.Buzon1.activo = true;
            SessionActual.Buzon2.activo = false;

            resultUpdate_completed();
        }

        private void btnAceptarCorregir_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);
            
            SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = SessionActual.Buzon3.activo = false;
            
            SessionActual.Buzon1.activo = true;
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = false;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrec = true;
            _cambiarInstruccion.Invoke(Enumerador.Instruccion.SinNavegacion);
            _cambiarContenido.Invoke(Enumerador.Pagina.ClasificacionDetalle); 
        }
    }
}
