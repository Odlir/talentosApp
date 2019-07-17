using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Threading;

namespace Talentos_Master //TODO: (ITERACION 5) CAMBIAR BUZON 2 POR BUZON 3!!!!
{
    public partial class frmSeleccionarPrincipal : IPaginaContenida
    {
        private List<TalentoUC> Talentosbuzon1 = new List<TalentoUC>();
        private List<TalentoUC> Talentosbuzon3 = new List<TalentoUC>();
        private Session SessionActual;
        private bool isLoading;
        private bool isReverse;
        private string url;

        public frmSeleccionarPrincipal()
        {
            InitializeComponent();

            if (ppSeAcabo.Visibility == Visibility.Collapsed)
                BusyWindow.IsBusy = true;

            SessionActual = Session.getInstance();

            txtCantidad1.Text = "0 seleccionados de " + Session.MAX_TALENTOS_MAS_DESARROLLADOS.ToString();

            //TODO: VALIDAR CUANDO EN EL BUZON 1 HAY 10 O CUANDO EN EL BUZON 3 HAY 5

            ReasignarUrlsTalentos();

            isLoading = true;

            if (SessionActual.Buzon3.lstTalento.Count.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS))
            {
                if (!SessionActual.cantSeleccionadosBuzon3.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS))
                {
                    for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
                    {
                        SessionActual.Buzon3.lstTalento[i].seleccionado = true;
                    }
                }
               
                ppPuntajesBuzon1.Visibility = Visibility.Visible;
                
                ppPuntajesBuzon2.Visibility = Visibility.Collapsed;
                SessionActual.Buzon3.activo = false;
                SessionActual.Buzon1.activo = true;

                SessionActual.cantSeleccionadosBuzon1 = Session.MAX_TALENTOS_MAS_DESARROLLADOS;
                LoadTalentos();

                if(!SessionActual.terminoSeleccion)
                txtVerBuzon3.Visibility = Visibility.Collapsed;      
            }

            if (SessionActual.Buzon1.lstTalento.Count.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS))
            {
                if (!SessionActual.cantCalificadosBuzon1.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS))
                {
                    for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
                    {
                        SessionActual.Buzon1.lstTalento[i].seleccionado = true;
                    }
                }
                ppPuntajesBuzon2.Visibility = Visibility.Visible;
                
                ppPuntajesBuzon1.Visibility = Visibility.Collapsed;
                SessionActual.Buzon3.activo = true;
                SessionActual.Buzon1.activo = false;

                SessionActual.cantSeleccionadosBuzon3 = 0;
                LoadTalentos();

                if (!SessionActual.terminoSeleccion)
                txtVerBuzon1.Visibility = Visibility.Collapsed;
            }

            if (!SessionActual.Buzon1.lstTalento.Count.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && !SessionActual.Buzon3.lstTalento.Count.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS))
            {
                ppPuntajesBuzon2.Visibility = Visibility.Collapsed;
               
                ppPuntajesBuzon1.Visibility = Visibility.Visible;

                SessionActual.Buzon3.activo = false;
                SessionActual.Buzon1.activo = true;

                SessionActual.cantSeleccionadosBuzon1 = 0;
                LoadTalentos();
            }

            txtTotal.Text = SessionActual.Buzon1.lstTalento.Count.ToString() + " talentos.";
            txtTotal2.Text = SessionActual.Buzon3.lstTalento.Count.ToString() + " talentos.";

            if (ActualizarContadoresBuzon(1).Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && ActualizarContadoresBuzon(3).Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS))
            {
                txtCantidad1.Text = Session.MAX_TALENTOS_MAS_DESARROLLADOS.ToString() + " seleccionados de " + Session.MAX_TALENTOS_MAS_DESARROLLADOS.ToString();
                txtCantidad2.Text = Session.MAX_TALENTOS_MENOS_DESARROLLADOS.ToString() + " seleccionados de " + Session.MAX_TALENTOS_MENOS_DESARROLLADOS.ToString();

                SessionActual.paso1 = false;
                SessionActual.paso2 = true;
                SessionActual.paso4 = false;
                SessionActual.pasoCorrec = false;
                SessionActual.paso3 = false;
                SessionActual.pasoTE = false;
                SessionActual.pasoVirtud = false;
            }

            if (ppSeAcabo.Visibility == Visibility.Collapsed)
            {
                int busyDisplay = 8;
                int delayDisplay = 600;
                BusyWindow.DisplayAfter = TimeSpan.FromMilliseconds(delayDisplay);
                ThreadPool.QueueUserWorkItem((state) =>
                {
                    Thread.Sleep(busyDisplay * 1000);
                    Dispatcher.BeginInvoke(() => BusyWindow.IsBusy = false);
                });
            }
        }

        void OnLoad(object sender, RoutedEventArgs e)
        {
            if (SessionActual.cantSeleccionadosBuzon3.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS) && SessionActual.cantSeleccionadosBuzon1.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS))
            {
                _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);
            }
        }

        private int ActualizarContadoresBuzon(int idBuzon)
        {
            int contador = 0;
            if (idBuzon == 1)
            {
                for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
                {
                    if (SessionActual.Buzon1.lstTalento[i].seleccionado)
                        contador++;
                }
            }

            if (idBuzon == 3)
            {
                for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
                {
                    if (SessionActual.Buzon3.lstTalento[i].seleccionado)
                        contador++;
                }
            }

            return contador;
        }

        private void ReasignarUrlsTalentos()
        {
            for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
            {
                if (SessionActual.Buzon1.lstTalento[i].Image.Contains("a.png"))
                {
                    string urlEspalda = SessionActual.Buzon1.lstTalento[i].Image;
                    string urlFrente = SessionActual.Buzon1.lstTalento[i].Example;

                    SessionActual.Buzon1.lstTalento[i].Example = urlEspalda;
                    SessionActual.Buzon1.lstTalento[i].Image = urlFrente;
                }
            }

            for (int i = 0; i < SessionActual.Buzon2.lstTalento.Count; i++)
            {
                if (SessionActual.Buzon2.lstTalento[i].Image.Contains("a.png"))
                {
                    string urlEspalda = SessionActual.Buzon2.lstTalento[i].Image;
                    string urlFrente = SessionActual.Buzon2.lstTalento[i].Example;

                    SessionActual.Buzon2.lstTalento[i].Example = urlEspalda;
                    SessionActual.Buzon2.lstTalento[i].Image = urlFrente;
                }
            }

            for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
            {
                if (SessionActual.Buzon3.lstTalento[i].Image.Contains("a.png"))
                {
                    string urlEspalda = SessionActual.Buzon3.lstTalento[i].Image;
                    string urlFrente = SessionActual.Buzon3.lstTalento[i].Example;

                    SessionActual.Buzon3.lstTalento[i].Example = urlEspalda;
                    SessionActual.Buzon3.lstTalento[i].Image = urlFrente;
                }
            }
        }
       
        private void LoadTalentos()
        {
            Talentosbuzon1.Clear();

            for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
            {
                Talentosbuzon1.Add(new TalentoUC(SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image, SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].Descripcion, SessionActual.Buzon1.lstTalento[i].seleccionado, i));
            }

            talentosListBox1.ItemsSource = Talentosbuzon1;
                
            Talentosbuzon3.Clear();

            for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
            {
                Talentosbuzon3.Add(new TalentoUC(SessionActual.Buzon3.lstTalento[i].IdTalento, SessionActual.Buzon3.lstTalento[i].Image, SessionActual.Buzon3.lstTalento[i].Nombre, SessionActual.Buzon3.lstTalento[i].Descripcion, SessionActual.Buzon3.lstTalento[i].seleccionado, i));
            }

            talentosListBox3.ItemsSource = Talentosbuzon3;
        }

        //mostrar opción Seleccionar talento del buzón 1
        private void imgPuntajesBuzon1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SessionActual.Buzon1.activo) && (ppSeAcabo.Visibility==Visibility.Collapsed))
            {
                SessionActual.Buzon1.activo = SessionActual.Buzon3.activo = false;
                ppPuntajesBuzon1.Visibility = Visibility.Visible;
                ppPuntajesBuzon2.Visibility = Visibility.Collapsed;
                
                SessionActual.Buzon1.activo = true;

                LoadTalentos();

                _cambiarInstruccion.Invoke(Enumerador.Instruccion.SegundaInstruccion);

                if (ActualizarContadoresBuzon(1).Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && ActualizarContadoresBuzon(3).Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS))
                {
                    txtCantidad1.Text = Session.MAX_TALENTOS_MAS_DESARROLLADOS.ToString() + " seleccionados de " + Session.MAX_TALENTOS_MAS_DESARROLLADOS.ToString();
                    txtCantidad2.Text = Session.MAX_TALENTOS_MENOS_DESARROLLADOS.ToString() + " seleccionados de " + Session.MAX_TALENTOS_MENOS_DESARROLLADOS.ToString();

                    SessionActual.paso1 = false;
                    SessionActual.paso2 = true;
                    SessionActual.paso4 = false;
                    SessionActual.pasoCorrec = false;
                    SessionActual.paso3 = false;

                    if (ppSeAcabo.Visibility == Visibility.Collapsed)
                        _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);
                }
            }
        }

        //mostrar opción Seleccionar talento para el buzón 2
        private void imgPuntajesBuzon2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SessionActual.Buzon3.activo) && (ppSeAcabo.Visibility==Visibility.Collapsed))
            {
                SessionActual.Buzon1.activo = SessionActual.Buzon3.activo = false;
                ppPuntajesBuzon1.Visibility = Visibility.Collapsed;
                ppPuntajesBuzon2.Visibility = Visibility.Visible;
                SessionActual.Buzon3.activo = true;

                if (isLoading)
                {
                    SessionActual.cantSeleccionadosBuzon3 = 0;
                    isLoading = false;
                }

                LoadTalentos();

                _cambiarInstruccion.Invoke(Enumerador.Instruccion.SegundaInstruccion);

                if (ActualizarContadoresBuzon(1).Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && ActualizarContadoresBuzon(3).Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS))
                {
                    txtCantidad1.Text = Session.MAX_TALENTOS_MAS_DESARROLLADOS.ToString() + " seleccionados de " + Session.MAX_TALENTOS_MAS_DESARROLLADOS.ToString();
                    txtCantidad2.Text = Session.MAX_TALENTOS_MENOS_DESARROLLADOS.ToString() + " seleccionados de " + Session.MAX_TALENTOS_MENOS_DESARROLLADOS.ToString();

                    SessionActual.paso1 = false;
                    SessionActual.paso2 = true;
                    SessionActual.paso4 = false;
                    SessionActual.pasoCorrec = false;
                    SessionActual.paso3 = false;

                    if (ppSeAcabo.Visibility == Visibility.Collapsed)
                        _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);
                }
            }
        }

        //Ir a la sgte página Silverlight
        private void sgte_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TalentosReference.ResultadoBE rpta = new Talentos_Master.TalentosReference.ResultadoBE();
            rpta.Resultado_id = SessionActual.resultado.Resultado_id;

            for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
            {
                rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.Buzon1.lstTalento[i].seleccionado).ToString(), ", ");
            }

            for (int i = 0; i < SessionActual.Buzon2.lstTalento.Count; i++)
            {
                rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.Buzon2.lstTalento[i].seleccionado).ToString(), ", ");
            }

            for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
            {
                rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.Buzon3.lstTalento[i].seleccionado).ToString(), ", ");
            }

            rpta.BuzonId = SessionActual.resultado.BuzonId;
            rpta.Participante_id = SessionActual.participante.UsuarioId;
            rpta.TalentoId = SessionActual.resultado.TalentoId;

            rpta.Fecha = DateTime.Now;

            SessionActual.resultado.Seleccionado = rpta.Seleccionado;

            //guardar resultado del juego
            TalentosReference.WSTalentosSoapClient ws = new Talentos_Master.TalentosReference.WSTalentosSoapClient();
            ws.ResultadoActualizarCompleted += new EventHandler<TalentosReference.ResultadoActualizarCompletedEventArgs>(resultUpdate_completed);
            ws.ResultadoActualizarAsync(rpta); 
        }

        public void resultUpdate_completed(object sender, TalentosReference.ResultadoActualizarCompletedEventArgs e)
        {
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = false;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrec = false;
            SessionActual.pasoTE = true;
            SessionActual.pasoVirtud = false;

            SessionActual.terminoClasificacion = true;
            SessionActual.terminoSeleccion = true;

            _cambiarContenido.Invoke(Enumerador.Pagina.SeleccionarTalentosEspecificos);
            _cambiarInstruccion.Invoke(Enumerador.Instruccion.InstruccionTalentoEspecifico);
        }

        public void ValidarSeleccionTalentos()
        {
            if ((SessionActual.cantSeleccionadosBuzon3.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS)) && (SessionActual.cantSeleccionadosBuzon1.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS)))
            {
                if (SessionActual.cantSeleccionadosBuzon1 == 1)
                    txtCantidad1.Text = SessionActual.cantSeleccionadosBuzon1.ToString() + " seleccionado de " + Session.MAX_TALENTOS_MAS_DESARROLLADOS.ToString();
                else
                    txtCantidad1.Text = SessionActual.cantSeleccionadosBuzon1.ToString() + " seleccionados de " + Session.MAX_TALENTOS_MAS_DESARROLLADOS.ToString();

                if (SessionActual.cantSeleccionadosBuzon3 == 1)
                    txtCantidad2.Text = SessionActual.cantSeleccionadosBuzon3.ToString() + " seleccionado de " + Session.MAX_TALENTOS_MENOS_DESARROLLADOS.ToString();
                else
                    txtCantidad2.Text = SessionActual.cantSeleccionadosBuzon3.ToString() + " seleccionados de " + Session.MAX_TALENTOS_MENOS_DESARROLLADOS.ToString();

                SessionActual.paso1 = false;
                SessionActual.paso2 = true;
                SessionActual.paso4 = false;
                SessionActual.pasoCorrec = false;
                SessionActual.paso3 = false;

                if (!isReverse)
                {
                    if (!SessionActual.terminoSeleccion)
                    {
                        txtMensaje.Text = "Has terminado de elegir tus " + Session.MAX_TALENTOS_MAS_DESARROLLADOS.ToString() + " talentos más desarrollados y tus " + Session.MAX_TALENTOS_MENOS_DESARROLLADOS.ToString() + " talentos menos desarrollados.";
                        //"Has terminado de elegir los 10 talentos que más te identifican y los 5 talentos que menos te identifican.";
                        ppSeAcabo.Visibility = Visibility.Visible;
                        canvasPuntajesNivel2.Visibility = Visibility.Collapsed;

                        _cambiarInstruccion.Invoke(Enumerador.Instruccion.EnBlanco);

                        SessionActual.terminoSeleccion = false;
                    }
                }

                if (ppSeAcabo.Visibility == Visibility.Collapsed)
                    _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);
            }
            else
            {
                if (SessionActual.Buzon1.activo)//(buzones.b1)
                {
                    if (SessionActual.cantSeleccionadosBuzon1 == 1)
                        txtCantidad1.Text = SessionActual.cantSeleccionadosBuzon1.ToString() + " seleccionado de " + Session.MAX_TALENTOS_MAS_DESARROLLADOS.ToString();
                    else
                        txtCantidad1.Text = SessionActual.cantSeleccionadosBuzon1.ToString() + " seleccionados de " + Session.MAX_TALENTOS_MAS_DESARROLLADOS.ToString();

                    if (!isReverse)
                    {
                        if (SessionActual.cantSeleccionadosBuzon1.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && (!SessionActual.terminoSeleccion))
                        {
                            txtMensaje.Text = "Has terminado de elegir tus " + Session.MAX_TALENTOS_MAS_DESARROLLADOS.ToString() + " talentos más desarrollados.";
                            //"Has terminado de elegir los 10 talentos que más te identifican.";
                            ppSeAcabo.Visibility = Visibility.Visible;
                            canvasPuntajesNivel2.Visibility = Visibility.Collapsed;

                            _cambiarInstruccion.Invoke(Enumerador.Instruccion.EnBlanco);

                            SessionActual.terminoSeleccion = false;
                        }
                    }
                        _cambiarInstruccion.Invoke(Enumerador.Instruccion.SinNavegacion);
                }
                else
                {
                    if (SessionActual.cantSeleccionadosBuzon3 == 1)
                        txtCantidad2.Text = SessionActual.cantSeleccionadosBuzon3.ToString() + " seleccionado de " + Session.MAX_TALENTOS_MENOS_DESARROLLADOS.ToString();
                    else
                        txtCantidad2.Text = SessionActual.cantSeleccionadosBuzon3.ToString() + " seleccionados de " + Session.MAX_TALENTOS_MENOS_DESARROLLADOS.ToString();

                    if (!isReverse)
                    {
                        if (SessionActual.cantSeleccionadosBuzon3.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS) && (!SessionActual.terminoSeleccion))
                        {
                            txtMensaje.Text = "Has terminado de elegir tus " + Session.MAX_TALENTOS_MENOS_DESARROLLADOS.ToString() + " talentos menos desarrollados.";
                            //"Has terminado de elegir los 5 talentos que menos te identifican.";
                            ppSeAcabo.Visibility = Visibility.Visible;
                            canvasPuntajesNivel2.Visibility = Visibility.Collapsed;

                            _cambiarInstruccion.Invoke(Enumerador.Instruccion.EnBlanco);

                            SessionActual.terminoSeleccion = false;
                        }
                    }

                    _cambiarInstruccion.Invoke(Enumerador.Instruccion.SinNavegacion);
                }
            }
        }

        //seleccionar talento(con el checkbox)
        private void chkSeleccionado_Checked(object sender, RoutedEventArgs e)
        {
            if (SessionActual.Buzon1.activo)
            {
                if (!SessionActual.Buzon1.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado && SessionActual.cantSeleccionadosBuzon1 >= Session.MAX_TALENTOS_MAS_DESARROLLADOS)
                { (sender as CheckBox).IsChecked = false; return; }
                else
                {
                    SessionActual.Buzon1.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado = true;
                    SessionActual.cantSeleccionadosBuzon1++;
                }
            }
            else
            {
                if (SessionActual.Buzon3.activo)
                {
                    if (!SessionActual.Buzon3.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado && SessionActual.cantSeleccionadosBuzon3 >= Session.MAX_TALENTOS_MENOS_DESARROLLADOS)
                    { (sender as CheckBox).IsChecked = false; return; }
                    else
                    {
                        SessionActual.Buzon3.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado = true;
                        SessionActual.cantSeleccionadosBuzon3++;
                    }
                }
            }

            ValidarSeleccionTalentos();
        }

        //desseleccionar talento(con el checkbox)
        private void chkSeleccionado_Unchecked(object sender, RoutedEventArgs e)
        {
                if (SessionActual.Buzon1.activo)
                {
                    if (!SessionActual.Buzon1.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado)
                    {
                        (sender as CheckBox).IsChecked = false;
                        return;
                    }
                    else
                    {
                        SessionActual.Buzon1.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado = false;
                        SessionActual.cantSeleccionadosBuzon1--;
                    }
                }
                else
                {
                    if (SessionActual.Buzon3.activo)
                    {
                        if (!SessionActual.Buzon3.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado)
                        { (sender as CheckBox).IsChecked = false; return; }
                        else
                        {
                            SessionActual.Buzon3.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado = false;     
                            SessionActual.cantSeleccionadosBuzon3--;
                        }
                    }
                }

            ValidarSeleccionTalentos();
        }

        private void btnContinuarSeAcabo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SessionActual.cantSeleccionadosBuzon1.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && SessionActual.cantSeleccionadosBuzon3.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS))
            {
                if ((SessionActual.BuzonTEMas.lstTalento.Count + SessionActual.BuzonTEMenos.lstTalento.Count + SessionActual.BuzonTEIntermedio.lstTalento.Count).Equals(Session.MAX_TALENTOS_ESPECIFICOS))
                {
                    SessionActual.pasoCorrecTE = true;
                    SessionActual.pasoVirtud = false;
                    SessionActual.paso1 = false;
                    SessionActual.paso2 = false;
                    _cambiarInstruccion.Invoke(Enumerador.Instruccion.instruccionCorreccionTE);
                    _cambiarContenido.Invoke(Enumerador.Pagina.ClasificacionDetalleTE);
                }
                else
                {
                    TalentosReference.ResultadoBE rpta = new Talentos_Master.TalentosReference.ResultadoBE();
                    rpta.Resultado_id = SessionActual.resultado.Resultado_id;

                    for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
                    {
                        rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.Buzon1.lstTalento[i].seleccionado).ToString(), ", ");
                    }

                    for (int i = 0; i < SessionActual.Buzon2.lstTalento.Count; i++)
                    {
                        rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.Buzon2.lstTalento[i].seleccionado).ToString(), ", ");
                    }

                    for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
                    {
                        rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.Buzon3.lstTalento[i].seleccionado).ToString(), ", ");
                    }

                    rpta.BuzonId = SessionActual.resultado.BuzonId;
                    rpta.Participante_id = SessionActual.participante.UsuarioId;
                    rpta.TalentoId = SessionActual.resultado.TalentoId;

                    rpta.Fecha = DateTime.Now;

                    SessionActual.resultado.Seleccionado = rpta.Seleccionado;

                    SessionActual.revisaSelec = true;

                    //TODO: Se agrego el codigo para que llame a la selecicon de talentos especificos
                    _cambiarContenido.Invoke(Enumerador.Pagina.SeleccionarTalentosEspecificos);
                    _cambiarInstruccion.Invoke(Enumerador.Instruccion.InstruccionTalentoEspecifico);
                }
            }
            else
            {
                if (SessionActual.cantSeleccionadosBuzon1.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && SessionActual.cantSeleccionadosBuzon3 < Session.MAX_TALENTOS_MENOS_DESARROLLADOS)
                {
                    SessionActual.Buzon1.activo = false;
                    SessionActual.Buzon3.activo = true;
                    ppPuntajesBuzon2.Visibility = Visibility.Visible;
                    ppPuntajesBuzon1.Visibility = Visibility.Collapsed;
                    ppSeAcabo.Visibility = Visibility.Collapsed;
                    canvasPuntajesNivel2.Visibility = Visibility.Visible;
                }
                else if (SessionActual.cantSeleccionadosBuzon3.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS) && SessionActual.cantSeleccionadosBuzon1 < Session.MAX_TALENTOS_MAS_DESARROLLADOS)
                {
                    SessionActual.Buzon3.activo = false;
                    SessionActual.Buzon1.activo = true;
                    ppPuntajesBuzon1.Visibility = Visibility.Visible;
                    ppPuntajesBuzon2.Visibility = Visibility.Collapsed;
                    ppSeAcabo.Visibility = Visibility.Collapsed;
                    canvasPuntajesNivel2.Visibility = Visibility.Visible;
                }

                _cambiarInstruccion.Invoke(Enumerador.Instruccion.SegundaInstruccion);

                LoadTalentos();
            }
        }

        private void btnRevisarSeAcabo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BusyWindow.IsBusy = true;

            txtCantidad1.Visibility = Visibility.Visible;
    
            txtCantidad1.Visibility = Visibility.Visible;
            txtCantidad2.Visibility = Visibility.Visible;
            _cambiarInstruccion.Invoke(Enumerador.Instruccion.SegundaInstruccion);
            
            ppSeAcabo.Visibility = Visibility.Collapsed;
            canvasPuntajesNivel2.Visibility = Visibility.Visible;

            SessionActual.paso1 = false;
            SessionActual.paso2 = true;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrec = false;
            SessionActual.paso3 = false;

            if (SessionActual.Buzon1.activo)
            {
                SessionActual.Buzon3.activo = false;
                ppPuntajesBuzon1.Visibility = Visibility.Visible;
                ppPuntajesBuzon2.Visibility = Visibility.Collapsed;
            }
            else
                if (SessionActual.Buzon3.activo)
                {
                    SessionActual.Buzon1.activo = false;
                    ppPuntajesBuzon1.Visibility = Visibility.Collapsed;
                    ppPuntajesBuzon2.Visibility = Visibility.Visible;
                }
          
            LoadTalentos();

            SessionActual.revisaSelec = true;

            _cambiarInstruccion.Invoke(Enumerador.Instruccion.SegundaInstruccion);

            if (SessionActual.cantSeleccionadosBuzon3.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS) && SessionActual.cantSeleccionadosBuzon1.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS))
            {
                _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);
            }

            int busyDisplay = 8;
            int delayDisplay = 600;
            BusyWindow.DisplayAfter = TimeSpan.FromMilliseconds(delayDisplay);
            ThreadPool.QueueUserWorkItem((state) =>
            {
                Thread.Sleep(busyDisplay * 1000);
                Dispatcher.BeginInvoke(() => BusyWindow.IsBusy = false);
            });
        }

        //seleccionar talento del buzón Talento más desarrollado(haciendo clic sobre su imagen)
        private void talentosListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TalentoUC uc = (TalentoUC)(talentosListBox1.SelectedItem);

            if ((uc != null) )
            {
                if (!isReverse)
                {
                    if (SessionActual.Buzon1.lstTalento[uc.i].seleccionado)
                    {
                        SessionActual.Buzon1.lstTalento[uc.i].seleccionado = false;
                    }
                    else
                        if (SessionActual.cantSeleccionadosBuzon1 < Session.MAX_TALENTOS_MAS_DESARROLLADOS)
                        {
                            SessionActual.Buzon1.lstTalento[uc.i].seleccionado = true;
                        }
                        else
                        {
                            SessionActual.Buzon1.lstTalento[uc.i].seleccionado = false;
                        }

                    if (SessionActual.cantSeleccionadosBuzon1.Equals(1) && !SessionActual.Buzon1.lstTalento[uc.i].seleccionado)
                    {
                        txtCantidad1.Text = "0 seleccionados de " + Session.MAX_TALENTOS_MAS_DESARROLLADOS.ToString();
                    }

                    isReverse = false;
                }
                else
                {
                    if (uc.source.ToString().Contains("Espalda"))
                    {
                        url = uc.source.ToString().Replace("Espalda ", "");
                        url = url.Replace("talentos/example", "talentos/images");
                    }
                    else
                    {
                        int pos = uc.source.LastIndexOf("/");
                        string nombreImagen = uc.source.Substring(pos + 1, uc.source.Length - pos -1);
                        string nombreImagenEspalda = "Espalda " + nombreImagen;

                        url = uc.source.ToString().Replace(nombreImagen, nombreImagenEspalda);
                        url = url.Replace("talentos/images", "talentos/example");
                    }

                    SessionActual.Buzon1.lstTalento[uc.i].Image = url;
                    isReverse = false;
                }

                SessionActual.cantSeleccionadosBuzon1 = 0;

                talentosListBox1.ItemsSource = null;
                LoadTalentos();
            }
        }

        //seleccionar talento del buzón Talento menos desarrollado(haciendo clic sobre su imagen)
        private void talentosListBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TalentoUC uc = (TalentoUC)(talentosListBox3.SelectedItem);

            if (uc != null)
            {
                if (!isReverse)
                {
                    if (SessionActual.Buzon3.lstTalento[uc.i].seleccionado)
                    {
                        SessionActual.Buzon3.lstTalento[uc.i].seleccionado = false;
                    }
                    else
                        if (SessionActual.cantSeleccionadosBuzon3 < Session.MAX_TALENTOS_MENOS_DESARROLLADOS)
                        {
                            SessionActual.Buzon3.lstTalento[uc.i].seleccionado = true;
                        }
                        else
                        {
                            SessionActual.Buzon3.lstTalento[uc.i].seleccionado = false;
                        }

                    if (SessionActual.cantSeleccionadosBuzon3.Equals(1) && !SessionActual.Buzon3.lstTalento[uc.i].seleccionado)
                    {
                        txtCantidad2.Text = "0 seleccionados de " + Session.MAX_TALENTOS_MENOS_DESARROLLADOS.ToString();
                    }

                    isReverse = false;
                }
                else
                {
                    if (uc.source.ToString().Contains("a.png"))
                    {
                        url = uc.source.ToString().Replace("a.png", ".png");
                        url = url.Replace("talentos/example", "talentos/images");
                    }
                    else
                    {
                        url = uc.source.ToString().Replace(".png", "a.png");
                        url = url.Replace("talentos/images", "talentos/example");
                    }

                    SessionActual.Buzon3.lstTalento[uc.i].Image = url;
                    isReverse = false;
                }

                SessionActual.cantSeleccionadosBuzon3 = 0;

                talentosListBox3.ItemsSource = null;
                LoadTalentos();
            } 
        }     

        //voltear talento
        private void btnVoltear_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isReverse = true;
        }
    }
}
