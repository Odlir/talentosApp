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
using System.Windows.Media.Effects;
using System.Windows.Browser;
using FluxJpeg.Core;
using System.IO;
using Visifire.Charts;
using Visifire.Commons;

namespace Talentos_Master
{
    /// <summary>
    ///ESTA PÁGINA PERMITE ASIGNAR UNA CALIFICACION A LOS TALENTOS QUE FUERON DESTINADOS AL BUZÓN 1 Y 2 DE ACUERDO A LAS REGLAS DEL JUEGO
    /// <summary>

    //TODO: (ITERACION 1 - II ENTREGA) Modificar diseño de talentos  de cada talento
    //TODO: (ITERACION 1 - II ENTREGA) Instrucciones de pie de página deben ser más grandes
    //TODO: (ITERACION 1 - II ENTREGA) Modificar diseño de buzones. Agregar efecto que permita distinguir en qué buzón se encuentra el jugador actualmente.
    //TODO: (ITERACION 1 - II ENTREGA) Uniformizar efecto asignado a un talento que ya ha sido calificado


    //TODO: (ITERACION 2 - I ENTREGA) Incluir en la Secuencia del Juego


    //TODO: (ITERACION 2 - II ENTREGA)Corregir Bugs

    //TODO: (ITERACION 3) Implementar validación de cantidad de talentos calificados



    public partial class frmCalificacion : IPaginaContenida
    {
        private static double IMAGE_WIDTH = 170;        // Ancho de la Imagen
        private static double IMAGE_HEIGHT = 200;       // Altura de la Imagen
        private static double SPRINESS = 0.4;		    // Controlar la velocidad de salto
        private static double DECAY = 0.5;			    // Controlar la velocidad de caída
        private static double SCALE_DOWN_FACTOR = 0.5;  // Escala entre imágenes
        private static double OFFSET_FACTOR = 100;      // Distancia entre imágenes
        private static double OPACITY_DOWN_FACTOR = 0.4;    // Alpha entre imágenes
        private static double MAX_SCALE3 = 1.3;
        private static double CRITICAL_POINT = 0.001;

        //private BuzonGlobal buzones;

        private double _xCenter;
        private double _yCenter;


        //private double _current = 0;	// Posición actual
        private double _currentB1 = 0;
        private double _currentB2 = 0;
        //private double _spring = 0;		// Temp usado para almacenar el último movimiento
        private double _springB1 = 0;
        private double _springB2 = 0;

        private static int FPS = 24;                // fps of the on enter frame event
        private DispatcherTimer _timer = new DispatcherTimer(); // on enter frame simulator

        private int _targetB1;
        private int _targetB2;

        private Buzon buzon1;
        private Buzon buzon2;

        private Buzon buzon3;

        


        private Session SessionActual;

        public frmCalificacion()
        {
           

            InitializeComponent();

            // Guardar la posición del centro
            _xCenter = canvasPuntajesNivel2.Width / 2;
            _yCenter = canvasPuntajesNivel2.Height / 2;
           
            SessionActual = Session.getInstance();

            buzon3 = new Buzon();

            buzon2 = new Buzon();
            buzon1 = new Buzon();

            int orden= 1;
            for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
            {
                SessionActual.Buzon1.lstTalento[i].ordenInsercion = orden;

                //if (SessionActual.Buzon1.lstTalento[i].puntaje.Equals(1) || SessionActual.Buzon1.lstTalento[i].puntaje.Equals(2) || SessionActual.Buzon1.lstTalento[i].puntaje.Equals(3) || SessionActual.Buzon1.lstTalento[i].puntaje.Equals(4))
                //    SessionActual.Buzon1.lstTalento[i].puntaje = 0;

                if (SessionActual.Buzon1.lstTalento[i].seleccionado)
                {
                    if (SessionActual.Buzon1.lstTalento[i].puntaje.Equals(1)
                        || SessionActual.Buzon1.lstTalento[i].puntaje.Equals(2)
                        || SessionActual.Buzon1.lstTalento[i].puntaje.Equals(3)
                        || SessionActual.Buzon1.lstTalento[i].puntaje.Equals(4))
                        SessionActual.Buzon1.lstTalento[i].puntaje = 0;

                    buzon1.lstTalento.Add(SessionActual.Buzon1.lstTalento[i]);
                }
                else
                {
                    if (SessionActual.Buzon1.lstTalento[i].puntaje.Equals(1)
                        || SessionActual.Buzon1.lstTalento[i].puntaje.Equals(5)
                        || SessionActual.Buzon1.lstTalento[i].puntaje.Equals(6))
                        SessionActual.Buzon1.lstTalento[i].puntaje = 0;

                    buzon2.lstTalento.Add(SessionActual.Buzon1.lstTalento[i]);
                }
                
                orden++;
            }

            

            for (int i = 0; i < SessionActual.Buzon2.lstTalento.Count; i++)
            {
                

                SessionActual.Buzon2.lstTalento[i].ordenInsercion = orden;

                if (SessionActual.Buzon2.lstTalento[i].puntaje.Equals(1) 
                    || SessionActual.Buzon2.lstTalento[i].puntaje.Equals(5) 
                    || SessionActual.Buzon2.lstTalento[i].puntaje.Equals(6))
                    SessionActual.Buzon2.lstTalento[i].puntaje = 0;

                buzon2.lstTalento.Add(SessionActual.Buzon2.lstTalento[i]);
                orden++;

            }



            for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
            {
                

                SessionActual.Buzon3.lstTalento[i].ordenInsercion = orden;

                if (SessionActual.Buzon3.lstTalento[i].seleccionado)
                {
                    SessionActual.Buzon3.lstTalento[i].puntaje = 1;
                    buzon3.lstTalento.Add(SessionActual.Buzon3.lstTalento[i]);
                }
                else
                {
                    if (SessionActual.Buzon3.lstTalento[i].puntaje.Equals(1) 
                     || SessionActual.Buzon3.lstTalento[i].puntaje.Equals(5)
                    || SessionActual.Buzon3.lstTalento[i].puntaje.Equals(6))
                        SessionActual.Buzon3.lstTalento[i].puntaje = 0;

                    buzon2.lstTalento.Add(SessionActual.Buzon3.lstTalento[i]);
                }
               
                orden++;
            }

            ActualizaContadoresCalificacion();

            
            ppPuntajesBuzon1.Visibility = Visibility.Visible;
            
            ppPuntajesBuzon2.Visibility = Visibility.Collapsed;
            SessionActual.Buzon2.activo = false;
            SessionActual.Buzon1.activo = true;

            _targetB1 = _targetB2 = 0;

            addImages();

            Start();



            efectoBuzonActual();

            if (SessionActual.participante.Sexo.Equals(1))
                txtfullname.Text = "Bienvenida, " + SessionActual.participante.NickName;
            else
                txtfullname.Text = "Bienvenido, " + SessionActual.participante.NickName;

            
                if (SessionActual.cantCalificadosBuzon1 == 1)
                    txtCantidad1.Text = SessionActual.cantCalificadosBuzon1.ToString() + " calificado de " + Session.MAX_TALENTOS_MAS_DESARROLLADOS;
                else
                    txtCantidad1.Text = SessionActual.cantCalificadosBuzon1.ToString() + " calificados de " + Session.MAX_TALENTOS_MAS_DESARROLLADOS;


                ValidaCantidadCalificados();
            
        }

        private void ActualizaContadoresCalificacion()
        {
            SessionActual.cantCalificadosBuzon1=SessionActual.cantCalificadosBuzon2=0;
            for (int i = 0; i < buzon1.lstTalento.Count; i++)
            {
                if(!buzon1.lstTalento[i].puntaje.Equals(0))
                SessionActual.cantCalificadosBuzon1++;
            }

            for (int i = 0; i < buzon2.lstTalento.Count; i++)
            {
                if (!buzon2.lstTalento[i].puntaje.Equals(0))
                    SessionActual.cantCalificadosBuzon2++;
            }
        }

        //armar listas temporales para la calificación del segundo buzón 
        public List<TalentosReference.TalentoBE> RepartirTalentos()
        {
            List<TalentosReference.TalentoBE> talentos = new List<Talentos_Master.TalentosReference.TalentoBE>();
            talentos=talentos.Concat(buzon1.lstTalento).ToList();

            talentos= talentos.Concat(buzon2.lstTalento).ToList();

            talentos= talentos.Concat(buzon3.lstTalento).ToList();


            TalentosReference.TalentoBE auxili;
            int j;
            for (int i = 0; i <  talentos.Count; i++)
            {
                auxili = talentos[i];
                j = i - 1;
                while (j >= 0 && talentos[j].ordenInsercion > auxili.ordenInsercion)
                {
                    talentos[j + 1] = talentos[j];
                    j--;
                }
                talentos[j + 1] = auxili;
            }

            return talentos;

        }

        #region efecto buzones
        public void efectoBuzonActual()
        {
            if (SessionActual.Buzon1.activo)
            {
                EfectoBuzon1();


                QuitarEfectoBuzon2();

            }
            else
            {
                if (SessionActual.Buzon2.activo)
                {
                    EfectoBuzon2();

                    QuitarEfectoBuzon1();

                }
            }

        }


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

        #endregion


        void _timer_Tick(object sender, EventArgs e)
        {

            
            if (SessionActual.Buzon1.activo && Math.Abs(_targetB1 - _currentB1) < CRITICAL_POINT) return;
            else
                
                if (SessionActual.Buzon2.activo && Math.Abs(_targetB2 - _currentB2) < CRITICAL_POINT) return;

            Border borde;

           
            for (int i = 0; i < buzon1.LstBuzonBorde.Count && SessionActual.Buzon1.activo; i++)
            {
               
                borde = buzon1.LstBuzonBorde[i];

                posImage(borde, i);
            }
            
            for (int i = 0; i < buzon2.LstBuzonBorde.Count && SessionActual.Buzon2.activo; i++)
            {
               
                borde = buzon2.LstBuzonBorde[i];

                posImage(borde, i);
            }

            
            if (SessionActual.Buzon1.activo)
            {
                _springB1 = (_targetB1 - _currentB1) * SPRINESS + _springB1 * DECAY;
                _currentB1 += _springB1;
            }
            else
                if (SessionActual.Buzon2.activo)
                {
                    _springB2 = (_targetB2 - _currentB2) * SPRINESS + _springB2 * DECAY;
                    _currentB2 += _springB2;
                }

            
            

        }


        // agregar imágenes al carrousel
        private void addImages()
        {
            buzon1.LstImagebuzon.Clear();  
            buzon1.LstBuzonBorde.Clear();

            buzon2.LstImagebuzon.Clear();
            buzon2.LstBuzonBorde.Clear();
            
            for (int i = 0; i < buzon1.lstTalento.Count && SessionActual.Buzon1.activo; i++)
            {
                //if (buzon1.lstTalento[i].seleccionado)
                //{
                    Border borde = new Border();

                    //String url = buzones.buzon1.lstTalento[i].Image;
                    String url = buzon1.lstTalento[i].Image;
                    System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                    image.Source = new BitmapImage(new Uri(url, UriKind.Relative));
                    SolidColorBrush scb = new SolidColorBrush(Colors.Transparent);

                    borde.Background = scb;
                    borde.Height = 217;
                    borde.Width = 187;
                    borde.Child = image;
                    image.Width = 170;
                    image.Height = 200;
                    image.Cursor = System.Windows.Input.Cursors.Hand;
                    //if (buzones.buzon1.lstTalento[i].seleccionado)
                    if (!buzon1.lstTalento[i].puntaje.Equals(0))
                    {
                        scb = new SolidColorBrush(Color.FromArgb((byte)255, (byte)232, (byte)137, (byte)25));//Colors.Cyan);
                        Thickness t = new Thickness(5.0);
                        borde.BorderThickness = t;
                        borde.BorderBrush = scb;
                        scb.Opacity = 0.8;

                    }
                    canvasPuntajesBuzon1.Children.Add(borde);
                    posImage(borde, i);
                    image.MouseLeftButtonDown += new MouseButtonEventHandler(btnPuntaje1_Click);
                    //image.MouseEnter += new MouseEventHandler(img_MouseEnter);
                    //image.MouseLeave += new MouseEventHandler(img_MouseLeave);
                    
                    buzon1.LstImagebuzon.Add(image);
                    buzon1.LstBuzonBorde.Add(borde);
                //}
            }


            for (int i = 0; i < buzon2.lstTalento.Count && SessionActual.Buzon2.activo; i++)
            {
               
                    Border borde = new Border();
                    //String url = buzones.Buzon2.lstTalento[i].Image;
                    String url = buzon2.lstTalento[i].Image;
                    System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                    image.Source = new BitmapImage(new Uri(url, UriKind.Relative));
                    SolidColorBrush scb = new SolidColorBrush(Colors.Transparent);
                    borde.Background = scb;
                    borde.Height = 217;
                    borde.Width = 187;
                    borde.Child = image;
                    image.Width = 170;
                    image.Height = 200;
                    image.Cursor = System.Windows.Input.Cursors.Hand;
                    //if (buzones.Buzon2.lstTalento[i].seleccionado)
                    if (!buzon2.lstTalento[i].puntaje.Equals(0))
                    {
                        scb = new SolidColorBrush(Color.FromArgb((byte)255, (byte)232, (byte)137, (byte)25));//Colors.Cyan);
                        Thickness t = new Thickness(5.0);
                        borde.BorderThickness = t;
                        borde.BorderBrush = scb;
                        scb.Opacity = 0.8;
                    }
                    canvasPuntajesBuzon2.Children.Add(borde);
                    posImage(borde, i);
                    image.MouseLeftButtonDown += new MouseButtonEventHandler(btnPuntaje1_Click);
                    //image.MouseEnter += new MouseEventHandler(img_MouseEnter);
                    //image.MouseLeave += new MouseEventHandler(img_MouseLeave);
                    
                    buzon2.LstImagebuzon.Add(image);
                    
                    buzon2.LstBuzonBorde.Add(borde);
                
            }

 
        }

     

        //Calcular la posición de una imagen
        private void posImage(Border image, int index)
        {
            double diffFactor = 0;
            if (SessionActual.Buzon1.activo)
                diffFactor = index - _currentB1;
            else if (SessionActual.Buzon2.activo)
                diffFactor = index - _currentB2;

           // double diffFactor = index - _current;

            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = MAX_SCALE3 - Math.Abs(diffFactor) * SCALE_DOWN_FACTOR;
            scaleTransform.ScaleY = MAX_SCALE3 - Math.Abs(diffFactor) * SCALE_DOWN_FACTOR;
            image.RenderTransform = scaleTransform;

            // reposicionar la imagen
            double left = _xCenter - (IMAGE_WIDTH * scaleTransform.ScaleX) / 2 + diffFactor * OFFSET_FACTOR;
            double top = _yCenter - (IMAGE_HEIGHT * scaleTransform.ScaleY) / 2 - 120;
            image.Opacity = 1 - Math.Abs(diffFactor) * OPACITY_DOWN_FACTOR;

            image.SetValue(Canvas.LeftProperty, left);
            image.SetValue(Canvas.TopProperty, top);

            // ordenar el elemento por escala
            image.SetValue(Canvas.ZIndexProperty, (int)Math.Abs(scaleTransform.ScaleX * 100));
        }

        //Mostrar opción ptjes
        private void btnPuntaje1_Click(object sender, RoutedEventArgs e)
        {
            this.popup1.Visibility = Visibility.Collapsed;
          
                imgBuzon1.Visibility = Visibility.Visible;
                imgBuzon2.Visibility = Visibility.Visible;

                ppTerminoCalificacion.Visibility = Visibility.Collapsed;
                canvasPuntajesNivel2.Visibility = Visibility.Visible;
                ppConfirmaSalir.Visibility = Visibility.Collapsed;


                if (SessionActual.Buzon1.activo)//(buzones.b1)
                {
                    //ppPuntaje1.IsOpen = true;
                    ppPuntaje1.Visibility = Visibility.Visible;
                    //ppPuntaje2.IsOpen = false;
                    ppPuntaje2.Visibility = Visibility.Collapsed;
                }
                else
                    if (SessionActual.Buzon2.activo)//(buzones.b3)
                    {
                        //ppPuntaje2.IsOpen = true;
                        ppPuntaje2.Visibility = Visibility.Visible;
                        //ppPuntaje1.IsOpen = false;
                        ppPuntaje1.Visibility = Visibility.Collapsed;

                    }

        }


        //Iniciar el timer
        public void Start()
        {
            // start the enter frame event
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / FPS);
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Start();
        }


        #region Seleccionar talento

        //mostrar  buzón 1
        private void imgPuntajesBuzon1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SessionActual.Buzon1.activo) && (ppTerminoCalificacion.Visibility == Visibility.Collapsed))//(!ppTerminoCalificacion.IsOpen))
            {
                
                SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = false;
                
                ppPuntajesBuzon1.Visibility = Visibility.Visible;
                
                ppPuntajesBuzon2.Visibility = Visibility.Collapsed;
                
                SessionActual.Buzon1.activo = true;
                deleteImages();
                addImages();
                efectoBuzonActual();



            }
        }

        //mostrar  buzón 2
        private void imgPuntajesBuzon2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SessionActual.Buzon2.activo) && (ppTerminoCalificacion.Visibility == Visibility.Collapsed)) //(!ppTerminoCalificacion.IsOpen))
            {
                
                SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = false;
                
                ppPuntajesBuzon1.Visibility = Visibility.Collapsed;
                
                ppPuntajesBuzon2.Visibility = Visibility.Visible;
                
                SessionActual.Buzon2.activo = true;
                deleteImages();
                addImages();
                efectoBuzonActual();



            }
        }

        #endregion

        //Borrar imágenes de los buzones
        private void deleteImages()
        {
            
            for (int i = 0; i < buzon1.LstBuzonBorde.Count; i++)
               
                canvasPuntajesBuzon1.Children.Remove(buzon1.LstBuzonBorde[i]);

   

            for (int i = 0; i < buzon2.LstBuzonBorde.Count; i++)
                
                canvasPuntajesBuzon2.Children.Remove(buzon2.LstBuzonBorde[i]);


            if (SessionActual.Buzon1.activo)
            {
                if (SessionActual.cantCalificadosBuzon1 == 1)
                    txtCantidad1.Text = SessionActual.cantCalificadosBuzon1.ToString() + " calificado de " + Session.MAX_TALENTOS_MAS_DESARROLLADOS;
                else
                    txtCantidad1.Text = SessionActual.cantCalificadosBuzon1.ToString() + " calificados de " + Session.MAX_TALENTOS_MAS_DESARROLLADOS;
            }
            else
            {
                if (SessionActual.cantCalificadosBuzon2 == 1)
                    txtCantidad2.Text = SessionActual.cantCalificadosBuzon2.ToString() + " calificado de 27";
                else
                    txtCantidad2.Text = SessionActual.cantCalificadosBuzon2.ToString() + " calificados de 27";
            }
        }

        //Mover posición talento
        public void moveIndex(int value)
        {
            if (SessionActual.Buzon1.activo)//(buzones.b1)
            {
                _targetB1 += value;
                _targetB1 = Math.Max(0, _targetB1);
                //_targetB1 = Math.Min(buzones.lstImagebuzon1.Count - 1, _targetB1);
                _targetB1 = Math.Min(buzon1.LstImagebuzon.Count - 1, _targetB1);
            }
            else
                if (SessionActual.Buzon2.activo)//(buzones.b3)
                {
                    _targetB2 += value;
                    _targetB2 = Math.Max(0, _targetB2);
                    //_targetB3 = Math.Min(buzones.lstImageBuzon2.Count - 1, _targetB3);
                    _targetB2 = Math.Min(buzon2.LstImagebuzon.Count - 1, _targetB2);
                }
        }

        // Mover las imágenes a la izquierda
        public void MoveLeft()
        {
            
            ppPuntaje1.Visibility = Visibility.Collapsed;
            
            ppPuntaje2.Visibility = Visibility.Collapsed;
            moveIndex(-1);
            Start();
        }

        // Mover las imágenes a la derecha
        public void MoveRight()
        {
            
            ppPuntaje1.Visibility = Visibility.Collapsed;
           
            ppPuntaje2.Visibility = Visibility.Collapsed;
            moveIndex(1);
            Start();
        }

        #region Mover Carrousel para el buzón 1
        //MOver el carrousel del buzón 1 a la izquierda
        private void izquierdaB1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            MoveRight();
        }

        //Mover el carrousel del buzón 1 a la derecha
        private void derechaB1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MoveLeft();
        }

        #endregion

        #region Mover Carrousel para el buzón 2

        //MOver el carrousel del buzón 2 a la izquierda
        private void izquierdaB2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MoveRight();
        }

        //Mover el carrousel del buzón 2 a la derecha
        private void derechaB2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            MoveLeft();
        }

        #endregion

        #region evento de botones de los mensajes

        public void resultUpdate_completed(object sender, TalentosReference.ResultadoActualizarCompletedEventArgs e)
        {
            

        }

        public void resultUpdateSalir_completed(object sender, TalentosReference.ResultadoActualizarCompletedEventArgs e)
        {
           
            izquierdaB2.Visibility = Visibility.Collapsed;
            derechaB2.Visibility = Visibility.Collapsed;
            izquierdaB1.Visibility = Visibility.Collapsed;
            derechaB1.Visibility = Visibility.Collapsed;
            txtIndicaciones.Visibility = Visibility.Collapsed;
            
            imgBuzon2.Visibility = Visibility.Collapsed;
            imgBuzon1.Visibility = Visibility.Collapsed;
            btnSalir.Visibility = Visibility.Collapsed;

            
            ppPuntajesBuzon1.Visibility = Visibility.Collapsed;
            
            ppPuntajesBuzon2.Visibility = Visibility.Collapsed;

            
            ppConfirmaSalir.Visibility = Visibility.Visible;
            ppTerminoCalificacion.Visibility = Visibility.Collapsed;
            canvasPuntajesNivel2.Visibility = Visibility.Collapsed;

        }


        private void btnAceptarSeAcabo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = false;

            

            txtCantidad1.Visibility = Visibility.Visible;
            izquierdaB1.Visibility = Visibility.Visible;
            derechaB1.Visibility = Visibility.Visible;
            izquierdaB2.Visibility = Visibility.Visible;
            derechaB2.Visibility = Visibility.Visible;
            txtCantidad1.Visibility = Visibility.Visible;
            txtCantidad2.Visibility = Visibility.Visible;
            
            efectoBuzonActual();

            
            ppTerminoCalificacion.Visibility = Visibility.Collapsed;
            canvasPuntajesNivel2.Visibility = Visibility.Visible;
            ppConfirmaSalir.Visibility = Visibility.Collapsed;

            SessionActual.paso1 = false;
            SessionActual.paso2 = true;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrec = false;
            SessionActual.paso3 = false;

            if (buzon2.lstTalento.Count > Session.MAX_TALENTOS_MENOS_DESARROLLADOS)
            {
                imgBuzon2.Visibility = Visibility.Visible;
                SessionActual.Buzon1.activo = false;
                SessionActual.Buzon2.activo = true;
                
                ppPuntajesBuzon1.Visibility = Visibility.Collapsed;
               
                ppPuntajesBuzon2.Visibility = Visibility.Visible;
            }

            if (buzon1.lstTalento.Count > Session.MAX_TALENTOS_MAS_DESARROLLADOS)
            {
                imgBuzon1.Visibility = Visibility.Visible;
                SessionActual.Buzon1.activo = true;
                SessionActual.Buzon2.activo = false;
                
                ppPuntajesBuzon1.Visibility = Visibility.Visible;
                
                ppPuntajesBuzon2.Visibility = Visibility.Collapsed;
            }

            addImages();

            efectoBuzonActual();
        }

        private void btnSalirCompletamente_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _cambiarContenido.Invoke(Enumerador.Pagina.AgradecimientoFinal);

        }


        private void btnResultados_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //mostrar radar
            //HtmlPage.Window.Invoke("ShowRadar");

            int ptjeO1, ptjeO2, ptjeO3, ptjeO4, ptjeO5, ptjeO6;

            ptjeO1 = ptjeO2 = ptjeO3 = ptjeO4 = ptjeO5 = ptjeO6 = 0;

            //for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
            for (int i = 0; i < buzon1.lstTalento.Count; i++)
            {
                //switch (SessionActual.Buzon1.lstTalento[i].IdColor)
                switch (buzon1.lstTalento[i].IdColor)
                {
                    case 1: ptjeO1 += buzon1.lstTalento[i].puntaje; break;
                    case 2: ptjeO2 += buzon1.lstTalento[i].puntaje; break;
                    case 3: ptjeO3 += buzon1.lstTalento[i].puntaje; break;
                    case 4: ptjeO4 += buzon1.lstTalento[i].puntaje; break;
                    case 5: ptjeO5 += buzon1.lstTalento[i].puntaje; break;
                    case 6: ptjeO6 += buzon1.lstTalento[i].puntaje; break;

                }


            }

            //for (int i = 0; i < SessionActual.Buzon2.lstTalento.Count; i++)
            for (int i = 0; i < buzon2.lstTalento.Count; i++)
            
            {
                switch (buzon2.lstTalento[i].IdColor)
                {
                    case 1: ptjeO1 += buzon2.lstTalento[i].puntaje; break;
                    case 2: ptjeO2 += buzon2.lstTalento[i].puntaje; break;
                    case 3: ptjeO3 += buzon2.lstTalento[i].puntaje; break;
                    case 4: ptjeO4 += buzon2.lstTalento[i].puntaje; break;
                    case 5: ptjeO5 += buzon2.lstTalento[i].puntaje; break;
                    case 6: ptjeO6 += buzon2.lstTalento[i].puntaje; break;

                }


            }

            for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
            {
                if (SessionActual.Buzon3.lstTalento[i].seleccionado)
                {
                    switch (SessionActual.Buzon3.lstTalento[i].IdColor)
                    {
                        case 1: ptjeO1 += SessionActual.Buzon3.lstTalento[i].puntaje; break;
                        case 2: ptjeO2 += SessionActual.Buzon3.lstTalento[i].puntaje; break;
                        case 3: ptjeO3 += SessionActual.Buzon3.lstTalento[i].puntaje; break;
                        case 4: ptjeO4 += SessionActual.Buzon3.lstTalento[i].puntaje; break;
                        case 5: ptjeO5 += SessionActual.Buzon3.lstTalento[i].puntaje; break;
                        case 6: ptjeO6 += SessionActual.Buzon3.lstTalento[i].puntaje; break;

                    }
                }


            }

            List<int> puntajes= new List<int>();
            puntajes.Add(ptjeO1);
            puntajes.Add(ptjeO2);
            puntajes.Add(ptjeO3);
            puntajes.Add(ptjeO4);
            puntajes.Add(ptjeO5);
            puntajes.Add(ptjeO6);


            UpdateChart(puntajes,"");

            frmRadar.Visibility = Visibility.Visible;

            //HtmlPage.Window.Invoke("ShowRadar", "30", "27", "18", "25", "10", "35");
            //HtmlPage.Window.Invoke("ShowRadar", ptjeO1.ToString(), ptjeO2.ToString(), ptjeO3.ToString(), ptjeO4.ToString(), ptjeO5.ToString(), ptjeO6.ToString());

            //radar.Ptjes = puntajes;

            //radar.Visibility = Visibility.Visible;
        }

        private void btnSi_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SessionActual.cantCalificadosBuzon1.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && SessionActual.cantCalificadosBuzon2.Equals(27)) 
            UpdateResultados();

            _cambiarContenido.Invoke(Enumerador.Pagina.AgradecimientoFinal);
        }

        private void btnNo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //ppConfirmaSalir.IsOpen = false;
            ppConfirmaSalir.Visibility = Visibility.Collapsed;
            ppTerminoCalificacion.Visibility = Visibility.Collapsed;
            canvasPuntajesNivel2.Visibility = Visibility.Visible;
            txtVolvera.Visibility = Visibility.Visible;
            txtVolverRecomendaciones.Visibility = Visibility.Visible;

            //btnSalir.Visibility = Visibility.Visible;
            izquierdaB2.Visibility = Visibility.Visible;
            derechaB2.Visibility = Visibility.Visible;
            izquierdaB1.Visibility = Visibility.Visible;
            derechaB1.Visibility = Visibility.Visible;
            txtIndicaciones.Visibility = Visibility.Visible;
            ////txtCantidad.Visibility = Visibility.Visible;
            imgBuzon1.Visibility = Visibility.Visible;
            imgBuzon2.Visibility = Visibility.Visible;

            btnSalir.Visibility = Visibility.Visible;

            //if (SessionActual.cantCalificadosBuzon1.Equals(10))
            if(SessionActual.Buzon2.activo)
            {
                ppPuntajesBuzon1.Visibility = Visibility.Collapsed;

                ppPuntajesBuzon2.Visibility = Visibility.Visible;
            }
            else
            {
                if (SessionActual.Buzon2.activo)
                {
                    ppPuntajesBuzon1.Visibility = Visibility.Visible;

                    ppPuntajesBuzon2.Visibility = Visibility.Collapsed;
                }
            }
            ////ppPuntajesBuzon1.IsOpen = false;
            //ppPuntajesBuzon1.Visibility = Visibility.Collapsed;
            ////ppPuntajesBuzon2.IsOpen = false;
            //ppPuntajesBuzon2.Visibility = Visibility.Collapsed;

            deleteImages();
            addImages();

        }

        private void btnSalir_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
             ppConfirmaSalir.Visibility = Visibility.Visible;
             ppTerminoCalificacion.Visibility = Visibility.Collapsed;
             canvasPuntajesNivel2.Visibility = Visibility.Collapsed;
             txtVolvera.Visibility = Visibility.Collapsed;
             txtVolverRecomendaciones.Visibility = Visibility.Collapsed;

             if (!SessionActual.cantCalificadosBuzon1.Equals(0) || !SessionActual.cantCalificadosBuzon2.Equals(0))
             {
                 UpdateResultados();
             }

        }

        #endregion



        private void imgBuzon2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void imgBuzon1_mouseButtonLeftDown(object sender, MouseButtonEventArgs e)
        {
            

            
        }

        private void txtIndicaciones_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //ppPuntaje1.IsOpen = true;
            ppPuntaje1.Visibility = Visibility.Visible;
            //ppPuntaje2.IsOpen = false;
            ppPuntaje2.Visibility = Visibility.Collapsed;
        }


        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //ppPuntaje2.IsOpen = true;
            ppPuntaje2.Visibility = Visibility.Visible;
            //ppPuntaje1.IsOpen = false;
            ppPuntaje1.Visibility = Visibility.Collapsed;
        }


        //Asignar puntaje a talento
        private void btnPuntaje_Click(object sender, RoutedEventArgs e)
        {



            this.popup1.Visibility = Visibility.Collapsed;
            //ppPuntaje1.IsOpen = false;
            //ppPuntaje2.IsOpen = false;

            String nombreBoton = ((System.Windows.Controls.Button)sender).Name;
            int ptje = 0;


            switch (nombreBoton)
            {
                // Asignar puntaje al buzon 1
                case "btnPuntaje5": buzon1.PuntajeBuzon += 5; ptje = 5; break;//buzones.puntajeBuzon_1 += 5; ptje = 5; break;
                case "btnPuntaje6": buzon1.PuntajeBuzon += 6; ptje = 6; break;
                // Asignar puntaje al buzon 2
                case "btnPuntaje2": buzon2.PuntajeBuzon += 2; ptje = 2; break;
                case "btnPuntaje3": buzon2.PuntajeBuzon += 3; ptje = 3; break;
                case "btnPuntaje4": buzon2.PuntajeBuzon += 4; ptje = 4; break;
            }

            if (SessionActual.Buzon1.activo)//(buzones.b1)
            {

                SolidColorBrush scb = new SolidColorBrush(Colors.Orange);

                Thickness t = new Thickness(5.0);
                BlurEffect be = new BlurEffect();
                be.Radius = 20;
                //buzones.lstBuzon1Borde[Convert.ToInt32(_targetB1)].BorderBrush = scb;
                //buzones.lstBuzon1Borde[Convert.ToInt32(_targetB1)].BorderThickness = t;
                //buzones.buzon1.lstTalento[Convert.ToInt32(_targetB1)].puntaje = ptje;

                buzon1.LstBuzonBorde[Convert.ToInt32(_targetB1)].BorderBrush = scb;
                buzon1.LstBuzonBorde[Convert.ToInt32(_targetB1)].BorderThickness = t;

                if (buzon1.lstTalento[Convert.ToInt32(_targetB1)].puntaje.Equals(0))
                    SessionActual.cantCalificadosBuzon1++;


                buzon1.lstTalento[Convert.ToInt32(_targetB1)].puntaje = ptje;

                //if ((SessionActual.cantCalificadosBuzon1.Equals(SessionActual.Buzon1.lstTalento.Count)) && (txtNextSms.Visibility == Visibility.Collapsed))
                if ((SessionActual.cantCalificadosBuzon1.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS)) && (txtNextSms.Visibility == Visibility.Collapsed))
                {
                    txtVolvera.Visibility = Visibility.Collapsed;
                    txtVolverRecomendaciones.Visibility = Visibility.Collapsed;
                    txtMensaje.Text = "Has terminado de seleccionar el nivel de desarrollo para cada talento del buzón \"Talento más desarrollado\".";//\"Me identifica\".";
                    ppSeAcabo.Visibility = Visibility.Visible;
                    ppConfirmaSalir.Visibility = Visibility.Collapsed;
                    ppTerminoCalificacion.Visibility = Visibility.Collapsed;
                    canvasPuntajesNivel2.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ppSeAcabo.Visibility = Visibility.Collapsed;
                    ppConfirmaSalir.Visibility = Visibility.Collapsed;
                    ppTerminoCalificacion.Visibility = Visibility.Collapsed;
                    canvasPuntajesNivel2.Visibility = Visibility.Visible;
                    txtVolvera.Visibility = Visibility.Visible;
                    txtVolverRecomendaciones.Visibility = Visibility.Visible;
                }


            }
            else
            {
                if (SessionActual.Buzon2.activo)//(buzones.b2)
                {
                    SolidColorBrush scb = new SolidColorBrush(Colors.Orange);
                    Thickness t = new Thickness(5.0);
                    BlurEffect be = new BlurEffect();
                    be.Radius = 20;
                    //buzones.lstBuzon2Borde[Convert.ToInt32(_targetB2)].Background = scb;
                    //buzones.lstBuzon2Borde[Convert.ToInt32(_targetB2)].BorderThickness = t;
                    //buzones.buzon2.lstTalento[Convert.ToInt32(_targetB2)].puntaje = ptje;
                    buzon2.LstBuzonBorde[Convert.ToInt32(_targetB2)].BorderBrush = scb;
                    buzon2.LstBuzonBorde[Convert.ToInt32(_targetB2)].BorderThickness = t;

                    if (buzon2.lstTalento[Convert.ToInt32(_targetB2)].puntaje.Equals(0))
                        SessionActual.cantCalificadosBuzon2++;

                   buzon2.lstTalento[Convert.ToInt32(_targetB2)].puntaje = ptje;

                    //if ((SessionActual.cantCalificadosBuzon2.Equals(SessionActual.Buzon2.lstTalento.Count)) && (txtNextSms.Visibility == Visibility.Collapsed))
                    if ((SessionActual.cantCalificadosBuzon2.Equals(27)) && (txtNextSms.Visibility == Visibility.Collapsed))
                    {
                        txtMensaje.Text = "Has terminado de seleccionar el nivel de desarrollo para cada talento del buzón \"Talento intermedio\"."; //\"No estoy seguro\".";
                        txtVolvera.Visibility = Visibility.Collapsed;
                        txtVolverRecomendaciones.Visibility = Visibility.Collapsed;
                        ppSeAcabo.Visibility = Visibility.Visible;
                        ppConfirmaSalir.Visibility = Visibility.Collapsed;
                        ppTerminoCalificacion.Visibility = Visibility.Collapsed;
                        canvasPuntajesNivel2.Visibility = Visibility.Collapsed;

                        //UpdateResultados();
                    }
                    else
                    {
                        ppSeAcabo.Visibility = Visibility.Collapsed;
                        ppConfirmaSalir.Visibility = Visibility.Collapsed;
                        ppTerminoCalificacion.Visibility = Visibility.Collapsed;
                        canvasPuntajesNivel2.Visibility = Visibility.Visible;
                        txtVolvera.Visibility = Visibility.Visible;
                        txtVolverRecomendaciones.Visibility = Visibility.Visible;
                    }

                }
            }

            if (SessionActual.Buzon1.activo)//(buzones.b1)
            {
                if (SessionActual.cantCalificadosBuzon1 == 1)
                    //txtCantidad.Text = cant1.ToString() + " calificado de " + buzones.lstBuzon1Borde.Count.ToString();
                    txtCantidad1.Text = SessionActual.cantCalificadosBuzon1.ToString() + " calificado de " + buzon1.LstBuzonBorde.Count.ToString();
                else
                    txtCantidad1.Text = SessionActual.cantCalificadosBuzon1.ToString() + " calificados de " + buzon1.LstBuzonBorde.Count.ToString();
            }
            else
            {
                if (SessionActual.cantCalificadosBuzon2 == 1)
                    txtCantidad2.Text = SessionActual.cantCalificadosBuzon2.ToString() + " calificado de " + buzon2.LstBuzonBorde.Count.ToString();
                else
                    txtCantidad2.Text = SessionActual.cantCalificadosBuzon2.ToString() + " calificados de " + buzon2.LstBuzonBorde.Count.ToString();
            }


          


            //ppPuntaje2.IsOpen = false;
            ppPuntaje2.Visibility = Visibility.Collapsed;
            //ppPuntaje1.IsOpen = false;
            ppPuntaje1.Visibility = Visibility.Collapsed;

        }

        private void ValidaCantidadCalificados()
        {
            if ((SessionActual.cantCalificadosBuzon2.Equals(buzon2.lstTalento.Count) && (SessionActual.cantCalificadosBuzon1.Equals(buzon1.lstTalento.Count))))
            {

                ppTerminoCalificacion.Visibility = Visibility.Visible;
                canvasPuntajesNivel2.Visibility = Visibility.Collapsed;
                ppSeAcabo.Visibility = Visibility.Collapsed;
                txtNext.Visibility = Visibility.Collapsed;
                txtNextSms.Visibility = Visibility.Collapsed;
                txtVolvera.Visibility = Visibility.Collapsed;
                txtVolverRecomendaciones.Visibility = Visibility.Collapsed;

                UpdateResultados();
            }
            else
            {
                if ((SessionActual.cantCalificadosBuzon1.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS)) && (txtNextSms.Visibility == Visibility.Collapsed))
                {
                    txtVolvera.Visibility = Visibility.Collapsed;
                    txtVolverRecomendaciones.Visibility = Visibility.Collapsed;
                    txtMensaje.Text = "Has terminado de seleccionar el nivel de desarrollo para cada talento del buzón \"Talento más desarrollado\".";//\"Me identifica\".";
                    ppSeAcabo.Visibility = Visibility.Visible;
                    ppConfirmaSalir.Visibility = Visibility.Collapsed;
                    ppTerminoCalificacion.Visibility = Visibility.Collapsed;
                    canvasPuntajesNivel2.Visibility = Visibility.Collapsed;
                }
                else
                {
                    if ((SessionActual.cantCalificadosBuzon2.Equals(27)) && (txtNextSms.Visibility == Visibility.Collapsed))
                    {
                        txtMensaje.Text = "Has terminado de seleccionar el nivel de desarrollo para cada talento del buzón \"Talento intermedio\"."; //\"No estoy seguro\".";
                        txtVolvera.Visibility = Visibility.Collapsed;
                        txtVolverRecomendaciones.Visibility = Visibility.Collapsed;
                        ppSeAcabo.Visibility = Visibility.Visible;
                        ppConfirmaSalir.Visibility = Visibility.Collapsed;
                        ppTerminoCalificacion.Visibility = Visibility.Collapsed;
                        canvasPuntajesNivel2.Visibility = Visibility.Collapsed;

                        //UpdateResultados();
                    }

                    else
                    {
                        ppSeAcabo.Visibility = Visibility.Collapsed;
                        ppConfirmaSalir.Visibility = Visibility.Collapsed;
                        ppTerminoCalificacion.Visibility = Visibility.Collapsed;
                        canvasPuntajesNivel2.Visibility = Visibility.Visible;
                        txtVolvera.Visibility = Visibility.Visible;
                        txtVolverRecomendaciones.Visibility = Visibility.Visible;
                    }
                }
            }

        }

        //Actualiza resultados en la BD
        private void UpdateResultados()
        {
           

            TalentosReference.ResultadoBE rpta = new Talentos_Master.TalentosReference.ResultadoBE();
            //rpta.Juego_id = SessionActual.juego.Juego_id;
            rpta.Resultado_id = SessionActual.resultado.Resultado_id;

           


            List<TalentosReference.TalentoBE> talentos = new List<Talentos_Master.TalentosReference.TalentoBE>();
            talentos = RepartirTalentos();

            for (int i = 0; i < talentos.Count; i++)
            {
                rpta.Puntaje = string.Concat(rpta.Puntaje, talentos[i].puntaje.ToString(), ", ");
            }

            

            rpta.BuzonId = SessionActual.resultado.BuzonId;
            //rpta.Juego_id = SessionActual.resultado.Juego_id;
            rpta.TalentoId = SessionActual.resultado.TalentoId;
            rpta.Seleccionado = SessionActual.resultado.Seleccionado;
            rpta.Participante_id = SessionActual.participante.UsuarioId;

            rpta.Fecha = DateTime.Now;
            SessionActual.resultado.Puntaje = rpta.Puntaje;
            SessionActual.resultado.Fecha = rpta.Fecha;

            //guardar resultado del juego
            TalentosReference.WSTalentosSoapClient ws = new Talentos_Master.TalentosReference.WSTalentosSoapClient();
            ws.ResultadoActualizarCompleted += new EventHandler<TalentosReference.ResultadoActualizarCompletedEventArgs>(resultUpdate_completed);
            ws.ResultadoActualizarAsync(rpta);
        }

        private void txtCerrarSession_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SessionActual = Session.deleteInstance();
            _cambiarContenido.Invoke(Enumerador.Pagina.Login);
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HtmlPage.Window.Navigate(new Uri("http://www.davidfischman.com/index.php?lang=es"), "_blank");
        }

        private void btnCancelarSeAcabo_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            //continuar

            if((SessionActual.cantCalificadosBuzon2.Equals(buzon2.lstTalento.Count)&& (SessionActual.cantCalificadosBuzon1.Equals(buzon1.lstTalento.Count)) ))
            {
                ppTerminoCalificacion.Visibility = Visibility.Visible;
                canvasPuntajesNivel2.Visibility = Visibility.Collapsed;
                ppSeAcabo.Visibility = Visibility.Collapsed;
                txtNext.Visibility = Visibility.Collapsed;
                txtNextSms.Visibility = Visibility.Collapsed;
                txtVolvera.Visibility = Visibility.Collapsed;
                txtVolverRecomendaciones.Visibility = Visibility.Collapsed;
                
                UpdateResultados();
            }
            else
            {

                if ((SessionActual.Buzon1.activo) && (SessionActual.cantCalificadosBuzon1.Equals(buzon1.lstTalento.Count)))
                {
                    //buzones.b1 = buzones.b3 = false;
                    SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = false;
                    //ppPuntajesBuzon1.IsOpen = false;
                    ppPuntajesBuzon1.Visibility = Visibility.Collapsed;
                    //ppPuntajesBuzon2.IsOpen = true;
                    ppPuntajesBuzon2.Visibility = Visibility.Visible;
                    //buzones.b3 = true;
                    SessionActual.Buzon2.activo = true;
                    deleteImages();
                    addImages();
                    efectoBuzonActual();
                    canvasPuntajesNivel2.Visibility = Visibility.Visible;
                    ppSeAcabo.Visibility = Visibility.Collapsed;
                    ppTerminoCalificacion.Visibility = Visibility.Collapsed;
                    txtNext.Visibility = Visibility.Collapsed;
                    txtNextSms.Visibility = Visibility.Collapsed;

                    
                    txtVolvera.Visibility = Visibility.Visible;
                    txtVolverRecomendaciones.Visibility = Visibility.Visible;
                    UpdateResultados();
                }
                else if ((SessionActual.Buzon2.activo) && (SessionActual.cantCalificadosBuzon2.Equals(buzon2.lstTalento.Count)))
                {

                    //buzones.b1 = buzones.b3 = false;
                    SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = false;
                    //ppPuntajesBuzon1.IsOpen = false;
                    ppPuntajesBuzon2.Visibility = Visibility.Collapsed;
                    //ppPuntajesBuzon2.IsOpen = true;
                    ppPuntajesBuzon1.Visibility = Visibility.Visible;
                    //buzones.b3 = true;
                    SessionActual.Buzon1.activo = true;
                    deleteImages();
                    addImages();
                    efectoBuzonActual();
                    canvasPuntajesNivel2.Visibility = Visibility.Visible;
                    ppSeAcabo.Visibility = Visibility.Collapsed;
                    ppTerminoCalificacion.Visibility = Visibility.Collapsed;
                    txtNext.Visibility = Visibility.Collapsed;
                    txtNextSms.Visibility = Visibility.Collapsed;

                   
                    txtVolvera.Visibility = Visibility.Visible;
                    txtVolverRecomendaciones.Visibility = Visibility.Visible;
                    UpdateResultados();
                }
            }


            

        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if ((SessionActual.cantCalificadosBuzon1.Equals(buzon1.lstTalento.Count)) && (SessionActual.cantCalificadosBuzon2.Equals(buzon2.lstTalento.Count)))
            {
                ppTerminoCalificacion.Visibility = Visibility.Visible;
                canvasPuntajesNivel2.Visibility = Visibility.Collapsed;
                ppSeAcabo.Visibility = Visibility.Collapsed;
                txtNext.Visibility = Visibility.Collapsed;
                txtNextSms.Visibility = Visibility.Collapsed;
                //txtVolvera.Visibility = Visibility.Collapsed;
                //txtVolverRecomendaciones.Visibility = Visibility.Collapsed;
                UpdateResultados();
            }
            else
            {

                if ((SessionActual.Buzon1.activo) && (SessionActual.cantCalificadosBuzon1.Equals(buzon1.lstTalento.Count)))
                {
                    //buzones.b1 = buzones.b3 = false;
                    SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = false;
                    //ppPuntajesBuzon1.IsOpen = false;
                    ppPuntajesBuzon1.Visibility = Visibility.Collapsed;
                    //ppPuntajesBuzon2.IsOpen = true;
                    ppPuntajesBuzon2.Visibility = Visibility.Visible;
                    //buzones.b3 = true;
                    SessionActual.Buzon2.activo = true;
                    deleteImages();
                    addImages();
                    efectoBuzonActual();
                    canvasPuntajesNivel2.Visibility = Visibility.Visible;
                    ppSeAcabo.Visibility = Visibility.Collapsed;
                    ppTerminoCalificacion.Visibility = Visibility.Collapsed;
                    txtNext.Visibility = Visibility.Collapsed;
                    txtNextSms.Visibility = Visibility.Collapsed;
                    //txtVolvera.Visibility = Visibility.Collapsed;
                    //txtVolverRecomendaciones.Visibility = Visibility.Collapsed;
                    UpdateResultados();
                }
                else if ((SessionActual.Buzon2.activo) && (SessionActual.cantCalificadosBuzon2.Equals(buzon2.lstTalento.Count)))
                {
                    //buzones.b1 = buzones.b3 = false;
                    SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = false;
                    //ppPuntajesBuzon1.IsOpen = false;
                    ppPuntajesBuzon2.Visibility = Visibility.Collapsed;
                    //ppPuntajesBuzon2.IsOpen = true;
                    ppPuntajesBuzon1.Visibility = Visibility.Visible;
                    //buzones.b3 = true;
                    SessionActual.Buzon1.activo = true;
                    deleteImages();
                    addImages();
                    efectoBuzonActual();
                    canvasPuntajesNivel2.Visibility = Visibility.Visible;
                    ppSeAcabo.Visibility = Visibility.Collapsed;
                    ppTerminoCalificacion.Visibility = Visibility.Collapsed;
                    txtNext.Visibility = Visibility.Collapsed;
                    txtNextSms.Visibility = Visibility.Collapsed;
                    //txtVolvera.Visibility = Visibility.Collapsed;
                    //txtVolverRecomendaciones.Visibility = Visibility.Collapsed;
                    UpdateResultados();

                }
                
            }
        }

        private void btnAceptarSeAcabo_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {//revisar

            if ((SessionActual.Buzon1.activo) && (SessionActual.cantCalificadosBuzon1.Equals(buzon1.lstTalento.Count)))
            {
                canvasPuntajesNivel2.Visibility = Visibility.Visible;
                ppPuntajesBuzon1.Visibility = Visibility.Visible;
                ppPuntajesBuzon2.Visibility = Visibility.Collapsed;

                ppSeAcabo.Visibility = Visibility.Collapsed;
                ppTerminoCalificacion.Visibility = Visibility.Collapsed;
                txtNext.Visibility = Visibility.Visible;
                txtNextSms.Visibility = Visibility.Visible;
                txtVolvera.Visibility = Visibility.Visible;
                txtVolverRecomendaciones.Visibility = Visibility.Visible;


            
            }
            else if ((SessionActual.Buzon2.activo) && (SessionActual.cantCalificadosBuzon2.Equals(buzon2.lstTalento.Count)))
            {
                ppTerminoCalificacion.Visibility = Visibility.Collapsed;

                ppPuntajesBuzon1.Visibility = Visibility.Collapsed;
                ppPuntajesBuzon2.Visibility = Visibility.Visible;

                canvasPuntajesNivel2.Visibility = Visibility.Visible;
                ppSeAcabo.Visibility = Visibility.Collapsed;
                txtNext.Visibility = Visibility.Visible;
                txtNextSms.Visibility = Visibility.Visible;

                
                txtVolvera.Visibility = Visibility.Visible;
                txtVolverRecomendaciones.Visibility = Visibility.Visible;
                //UpdateResultados();
            }
               
        }

        private void ActualizaBuzones()
        {
            List<TalentosReference.TalentoBE> talentos = RepartirTalentos();

            int cant1 = SessionActual.Buzon1.lstTalento.Count;

            SessionActual.Buzon1.lstTalento.Clear();

            for (int i = 0; i < cant1; i++)
            {
                SessionActual.Buzon1.lstTalento.Add(talentos[i]);
            }

            int cant2 = SessionActual.Buzon2.lstTalento.Count;

            SessionActual.Buzon2.lstTalento.Clear();

            for (int i = cant1; i < cant1+ cant2; i++)
            {
                SessionActual.Buzon2.lstTalento.Add(talentos[i]);
            }

            int cant3 = SessionActual.Buzon3.lstTalento.Count;

            SessionActual.Buzon3.lstTalento.Clear();

            for (int i = cant1 + cant2; i < talentos.Count; i++)
            {
                SessionActual.Buzon3.lstTalento.Add(talentos[i]);
            }
        }

        //Volver a ver Mis recomendaciones
        private void txtVolverRecomendaciones_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SessionActual.paso3 = true;


            ActualizaBuzones();

            _cambiarContenido.Invoke(Enumerador.Pagina.Sugerencias);
        }

        //******************** Ver talentos del buzón Talento más desarrollado
        private void imgBuzon1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ppPuntaje1.Visibility = Visibility.Collapsed;
            ppPuntaje2.Visibility = Visibility.Collapsed;

            if ((!SessionActual.Buzon1.activo) && (ppTerminoCalificacion.Visibility == Visibility.Collapsed))//(!ppTerminoCalificacion.IsOpen))
            {
                
                SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = false;
                
                ppPuntajesBuzon2.Visibility = Visibility.Collapsed;
                
                ppPuntajesBuzon1.Visibility = Visibility.Visible;
                
                SessionActual.Buzon1.activo = true;
                deleteImages();
                addImages();
                efectoBuzonActual();
            }
        }

        //*************************** Ver talentos del buzón Talento menos desarrollado
        private void imgBuzon2_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            ppPuntaje1.Visibility = Visibility.Collapsed;
            ppPuntaje2.Visibility = Visibility.Collapsed;


            if ((!SessionActual.Buzon2.activo) && (ppTerminoCalificacion.Visibility == Visibility.Collapsed))//(!ppTerminoCalificacion.IsOpen))
            {
                
                SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = false;
               
                ppPuntajesBuzon1.Visibility = Visibility.Collapsed;
                
                ppPuntajesBuzon2.Visibility = Visibility.Visible;
                
                SessionActual.Buzon2.activo = true;
                deleteImages();
                addImages();
                efectoBuzonActual();
            }

        }


        #region piechart
        //** para mostrar resultados pie chart **//

        private void ButtonSeven_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            SaveToImage(MyChart);


        }

        //Mostrar pie chart con resultados
        public void UpdateChart(List<int> values, String titulo)
        {


            #region Setear Properties al Charter

            // Setear ancho y altura al chart
            Title title = new Title();
            title.Text = titulo;
            title.FontSize = 20;
            title.FontFamily = new FontFamily("Arial");
            title.FontColor = new SolidColorBrush(Color.FromArgb((byte)255, (byte)0, (byte)0, (byte)0));
            title.Margin = new Thickness(0, 40, 0, 0);

            //title.MouseLeftButtonDown += new MouseButtonEventHandler(title_MouseLeftButtonDown);



            // Agregar el título instanciado a la collección de títulos del chart
            MyChart.Titles.Clear();
            MyChart.Titles.Add(title);

            // Crear Axis X
            Axis axisX = new Axis();

            // Setear propiedades al axis X
            axisX.Title = "Tendencias";
            //axisX.MouseLeftButtonDown += new MouseButtonEventHandler(axis_MouseLeftButtonDown);
            axisX.TitleFontColor = new SolidColorBrush(Color.FromArgb((byte)255, (byte)0, (byte)0, (byte)0));
            axisX.TitleFontSize = 25;
            axisX.TitleFontFamily = new FontFamily("Arial");

            //axisX.AxisLabels.FontColor = new SolidColorBrush(Color.FromArgb((byte)255, (byte)0, (byte)0, (byte)0));

            axisX.AxisLabels.FontColor = new SolidColorBrush(Colors.Red);

            // Agregar el axis instanciado a la colección de AxesX 
            MyChart.AxesX.Clear();
            MyChart.AxesX.Add(axisX);

            // Crear axisY
            Axis axisY = new Axis();

            // Setear properties al axis Y
            axisY.Title = "Cantidad de Talentos";
            //axisY.MouseLeftButtonDown += new MouseButtonEventHandler(axis_MouseLeftButtonDown);
            axisY.TitleFontColor = new SolidColorBrush(Color.FromArgb((byte)255, (byte)47, (byte)79, (byte)79));
            axisY.TitleFontSize = 25;
            axisY.TitleFontFamily = new FontFamily("Arial");
            axisY.AxisLabels.FontColor = new SolidColorBrush(Color.FromArgb((byte)255, (byte)232, (byte)137, (byte)25));

            // Agregar axis a la colección de AxesY
            MyChart.AxesY.Clear();
            MyChart.AxesY.Add(axisY);





            #region Crear Datapoints para cada tendencia o color y agregarlos a DataSeries

            MyChart.Series.Clear();

            DataSeries dataSeries = new DataSeries();

            if (values[0] != 0)
            {
                // Crear un DataPoint
                DataPoint dataPoint1;

                dataPoint1 = new DataPoint();
                dataPoint1.AxisXLabel = "Orientado a la ejecución";
                dataPoint1.YValue = values[0];

                //// setear un color gradient
                LinearGradientBrush lgb = new LinearGradientBrush();
                GradientStopCollection lista = new GradientStopCollection();
                GradientStop stop = new GradientStop();
                stop.Color = Color.FromArgb((byte)255, (byte)234, (byte)102, (byte)0);
                stop.Offset = 1.0;
                lgb.GradientStops.Add(stop);

                GradientStop stop3 = new GradientStop();
                stop3.Color = Color.FromArgb((byte)255, (byte)255, (byte)102, (byte)0); //****
                stop3.Offset = 0.8;
                lgb.GradientStops.Add(stop3);
                GradientStop stop4 = new GradientStop();
                stop4.Color = Color.FromArgb((byte)255, (byte)255, (byte)122, (byte)0);
                stop4.Offset = 0.5;
                lgb.GradientStops.Add(stop4);




                //stop.Color = Color.FromArgb((byte)255, (byte)255, (byte)102, (byte)0);
                //stop.Offset = 1;
                //lgb.GradientStops.Add(stop);
                dataPoint1.Color = lgb;
                dataPoint1.Width = 50;

                //dataPoint1.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);


                dataSeries.DataPoints.Add(dataPoint1);

            }
            if (values[1] != 0)
            {
                DataPoint dataPoint2;

                dataPoint2 = new DataPoint();
                dataPoint2.AxisXLabel = "Orientado al pensamiento";
                dataPoint2.YValue = values[1];
                // set un color gradiente
                LinearGradientBrush Azul = new LinearGradientBrush();
                GradientStop stopAzul1 = new GradientStop();
                stopAzul1.Color = Color.FromArgb((byte)255, (byte)62, (byte)105, (byte)179);//Color.FromArgb((byte)255, (byte)0, (byte)0, (byte)255);
                stopAzul1.Offset = 0;
                Azul.GradientStops.Add(stopAzul1);
                //GradientStop stopAzul2 = new GradientStop();
                //stopAzul2.Color = Color.FromArgb((byte)255, (byte)0, (byte)0, (byte)205);
                //stopAzul2.Offset = 0.6;
                //Azul.GradientStops.Add(stopAzul2);
                //GradientStop stopAzul3 = new GradientStop();
                // stopAzul3.Color =Color.FromArgb((byte)255, (byte)65, (byte)105, (byte)225);
                //stopAzul3.Offset = 0.9;
                //Azul.GradientStops.Add(stopAzul3);
                GradientStop stopAzul4 = new GradientStop();
                //stopAzul4.Color = Color.FromArgb((byte)255, (byte)100, (byte)149, (byte)237);
                stopAzul4.Color = Color.FromArgb((byte)255, (byte)62, (byte)105, (byte)179);
                stopAzul4.Offset = 1.0;
                Azul.GradientStops.Add(stopAzul4);
                dataPoint2.Color = Azul;
                //dataPoint2.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);
                dataSeries.DataPoints.Add(dataPoint2);
            }

            if (values[2] != 0)
            {
                DataPoint dataPoint3;

                dataPoint3 = new DataPoint();
                dataPoint3.AxisXLabel = "Orientado a la innovación";
                dataPoint3.YValue = values[2];
                // setear un color degradado
                LinearGradientBrush Amarillo = new LinearGradientBrush();
                GradientStop stopAmarillo2 = new GradientStop();
                stopAmarillo2.Color = Color.FromArgb((byte)255, (byte)255, (byte)204, (byte)0);//*******
                stopAmarillo2.Offset = 0.45;
                Amarillo.GradientStops.Add(stopAmarillo2);
                GradientStop stopAmarillo3 = new GradientStop();
                stopAmarillo3.Color = Color.FromArgb((byte)255, (byte)255, (byte)251, (byte)0);
                stopAmarillo3.Offset = 0.75;
                Amarillo.GradientStops.Add(stopAmarillo3);
                GradientStop stopAmarillo4 = new GradientStop();
                stopAmarillo4.Color = Color.FromArgb((byte)255, (byte)255, (byte)230, (byte)0);
                stopAmarillo4.Offset = 1.0;
                Amarillo.GradientStops.Add(stopAmarillo4);
                dataPoint3.Color = Amarillo;
                //dataPoint3.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);

                dataSeries.DataPoints.Add(dataPoint3);
            }

            if (values[3] != 0)
            {
                DataPoint dataPoint4;

                dataPoint4 = new DataPoint();
                dataPoint4.AxisXLabel = "Orientado al liderazgo";
                dataPoint4.YValue = values[3];
                //setear un color degradado
                LinearGradientBrush Guinda = new LinearGradientBrush();
                GradientStop stopGuinda1 = new GradientStop();
                stopGuinda1.Color = Color.FromArgb((byte)255, (byte)125, (byte)0, (byte)54);
                stopGuinda1.Offset = 0.2;
                Guinda.GradientStops.Add(stopGuinda1);
                GradientStop stopGuinda2 = new GradientStop();
                stopGuinda2.Color = Color.FromArgb((byte)255, (byte)164, (byte)0, (byte)54);
                stopGuinda2.Offset = 0.4;
                Guinda.GradientStops.Add(stopGuinda2);
                GradientStop stopGuinda3 = new GradientStop();
                stopGuinda3.Color = Color.FromArgb((byte)255, (byte)185, (byte)0, (byte)54);
                stopGuinda3.Offset = 0.6;
                Guinda.GradientStops.Add(stopGuinda3);
                GradientStop stopGuinda4 = new GradientStop();
                stopGuinda4.Color = Color.FromArgb((byte)255, (byte)125, (byte)15, (byte)54);
                stopGuinda4.Offset = 0.9;
                Guinda.GradientStops.Add(stopGuinda4);
                dataPoint4.Color = Guinda;
                //dataPoint4.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);
                dataSeries.DataPoints.Add(dataPoint4);
            }

            if (values[4] != 0)
            {
                DataPoint dataPoint5;

                dataPoint5 = new DataPoint();
                dataPoint5.AxisXLabel = "Orientado a las personas";
                dataPoint5.YValue = values[4];

                //setear un color degradado
                LinearGradientBrush Rojo = new LinearGradientBrush();
                GradientStop stopRojo1 = new GradientStop();
                stopRojo1.Color = Color.FromArgb((byte)255, (byte)204, (byte)0, (byte)0);
                stopRojo1.Offset = 0.3;
                Rojo.GradientStops.Add(stopRojo1);
                GradientStop stopRojo4 = new GradientStop();
                stopRojo4.Color = Color.FromArgb((byte)255, (byte)255, (byte)0, (byte)0);
                stopRojo4.Offset = 0.65;
                Rojo.GradientStops.Add(stopRojo4);

                GradientStop stopRojo5 = new GradientStop();
                stopRojo5.Color = Color.FromArgb((byte)255, (byte)204, (byte)0, (byte)0);
                stopRojo5.Offset = 0.85;
                Rojo.GradientStops.Add(stopRojo5);

                dataPoint5.Color = Rojo;
                //dataPoint5.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);
                dataSeries.DataPoints.Add(dataPoint5);
            }

            if (values[5] != 0)
            {
                DataPoint dataPoint6;

                dataPoint6 = new DataPoint();
                dataPoint6.AxisXLabel = "Orientado a la estructura";
                dataPoint6.YValue = values[5];

                //setear un color degradado
                LinearGradientBrush Verde = new LinearGradientBrush();
                GradientStop stopVerde1 = new GradientStop();
                stopVerde1.Color = Color.FromArgb((byte)255, (byte)0, (byte)128, (byte)0);
                stopVerde1.Offset = 0;
                Verde.GradientStops.Add(stopVerde1);
                GradientStop stopVerde2 = new GradientStop();
                stopVerde2.Color = Color.FromArgb((byte)255, (byte)0, (byte)160, (byte)0);
                stopVerde2.Offset = 0.4;
                Verde.GradientStops.Add(stopVerde2);
                GradientStop stopVerde3 = new GradientStop();
                stopVerde3.Color = Color.FromArgb((byte)255, (byte)0, (byte)228, (byte)0);
                stopVerde3.Offset = 0.7;
                Verde.GradientStops.Add(stopVerde3);
                GradientStop stopVerde4 = new GradientStop();
                stopVerde4.Color = Color.FromArgb((byte)255, (byte)0, (byte)180, (byte)0);
                stopVerde4.Offset = 0.9;
                Verde.GradientStops.Add(stopVerde4);
                dataPoint6.Color = Verde;
                //dataPoint6.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);
                dataSeries.DataPoints.Add(dataPoint6);
            }
            #endregion

            dataSeries.RenderAs = RenderAs.Pie;
            dataSeries.Bevel = true;

            dataSeries.Height = 350;
            dataSeries.Width = 450;

            dataSeries.Margin = new Thickness(0, -200, 0, 0);

            // Agregar dataSeries a la colección de Series
            MyChart.Series.Add(dataSeries);

            #endregion

            // Agregar el chart a LayoutRoot
            //LayoutRoot.Children.Add(charter);


        }


        /// <summary>
        /// Save Visifire chart as Image
        /// </summary>
        /// <param name="visifire_Chart">Visifire.Charts.Chart</param>
        private void SaveToImage(Chart chart)
        {
            try
            {
                WriteableBitmap bitmap = new WriteableBitmap(chart, null);

                if (bitmap != null)
                {
                    SaveFileDialog saveDlg = new SaveFileDialog();
                    saveDlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|All Files (*.*)|*.*";
                    saveDlg.DefaultExt = ".jpeg";

                    if ((bool)saveDlg.ShowDialog())
                    {
                        using (Stream fs = saveDlg.OpenFile())
                        {
                            MemoryStream stream = GetImageStream(bitmap);

                            //Get Bytes from memory stream and write into IO stream
                            byte[] binaryData = new Byte[stream.Length];
                            long bytesRead = stream.Read(binaryData, 0, (int)stream.Length);
                            fs.Write(binaryData, 0, binaryData.Length);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Note: Please make sure that Height and Width of the chart is set properly.");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Get image MemoryStream from WriteableBitmap
        /// </summary>
        /// <param name="bitmap">WriteableBitmap</param>
        /// <returns>MemoryStream</returns>
        public static MemoryStream GetImageStream(WriteableBitmap bitmap)
        {
            byte[][,] raster = ReadRasterInformation(bitmap);
            return EncodeRasterInformationToStream(raster, ColorSpace.RGB);
        }

        /// <summary>
        /// Reads raster information from WriteableBitmap
        /// </summary>
        /// <param name="bitmap">WriteableBitmap</param>
        /// <returns>Array of bytes</returns>
        public static byte[][,] ReadRasterInformation(WriteableBitmap bitmap)
        {
            int width = bitmap.PixelWidth;
            int height = bitmap.PixelHeight;
            int bands = 3;
            byte[][,] raster = new byte[bands][,];

            for (int i = 0; i < bands; i++)
            {
                raster[i] = new byte[width, height];
            }

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    int pixel = bitmap.Pixels[width * row + column];
                    raster[0][column, row] = (byte)(pixel >> 16);
                    raster[1][column, row] = (byte)(pixel >> 8);
                    raster[2][column, row] = (byte)pixel;
                }
            }

            return raster;
        }

        /// <summary>
        /// Encode raster information to MemoryStream
        /// </summary>
        /// <param name="raster">Raster information (Array of bytes)</param>
        /// <param name="colorSpace">ColorSpace used</param>
        /// <returns>MemoryStream</returns>
        public static MemoryStream EncodeRasterInformationToStream(byte[][,] raster, ColorSpace colorSpace)
        {
            ColorModel model = new ColorModel { colorspace = ColorSpace.RGB };
            FluxJpeg.Core.Image img = new FluxJpeg.Core.Image(model, raster);

            //Encode the Image as a JPEG
            MemoryStream stream = new MemoryStream();
            FluxJpeg.Core.Encoder.JpegEncoder encoder = new FluxJpeg.Core.Encoder.JpegEncoder(img, 100, stream);
            encoder.Encode();

            // Back to the start
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        private void ButtonSeven_MouseMove(object sender, MouseEventArgs e)
        {
            ButtonSeven button = sender as ButtonSeven;
            if (button == null) return;

            textInfo.Text = button.Title;

            textInfo.FontWeight = FontWeights.Bold;

        }

        private void ButtonSeven_MouseLeave(object sender, MouseEventArgs e)
        {
            textInfo.Text = "";

        }

        private void ButtonSeven_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frmRadar.Visibility = Visibility.Collapsed;
        }

        #endregion

    }



}


