using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Talentos_Master
{
    public partial class frmSeleccionarTalentosEspecificos_2 : IPaginaContenida
    {
        private List<TalentoUC> TalentosBuzonTE = new List<TalentoUC>();
        private Session SessionActual;
        private bool isLoading;
        private bool isReverse;
        private string url;

        public frmSeleccionarTalentosEspecificos_2()
        {
            //InitializeComponent();

            //SessionActual = Session.getInstance();
            //ReasignarUrlsTalentos();
            //txtCantidadTE.Text = "0 seleccionados de " + Session.MAX_TALENTOS_ESPECIFICOS.ToString();
            //isLoading = true;

            //if (SessionActual.BuzonTE.lstTalento.Count.Equals(Session.MAX_TALENTOS_ESPECIFICOS))
            //{
            //    if (!SessionActual.cantCalificadosBuzon1.Equals(Session.MAX_TALENTOS_ESPECIFICOS))
            //    {
            //        for (int i = 0; i < SessionActual.BuzonTE.lstTalento.Count; i++)
            //        {
            //            SessionActual.BuzonTE.lstTalento[i].seleccionado = true;
            //        }
            //    }

            //    ppPuntajesBuzonTE.Visibility = Visibility.Collapsed;
            //    SessionActual.BuzonTE.activo = false;

            //    LoadTalentos();

            //    //if (!SessionActual.terminoSeleccion)
            //    //    txtVerBuzonTE.Visibility = Visibility.Collapsed;
            //}

            //if (!SessionActual.BuzonTE.lstTalento.Count.Equals(Session.MAX_TALENTOS_ESPECIFICOS))
            //{
            //    ppPuntajesBuzonTE.Visibility = Visibility.Visible;

            //    SessionActual.BuzonTE.activo = true;

            //    SessionActual.cantSeleccionadosBuzonTE = 0;
            //    LoadTalentos();
            //}

            //txtTotal.Text = SessionActual.BuzonTE.lstTalento.Count.ToString() + " talentos.";

            //if (ActualizarContadoresBuzon(1).Equals(Session.MAX_TALENTOS_ESPECIFICOS))
            //{
            //    txtCantidadTE.Text = Session.MAX_TALENTOS_ESPECIFICOS.ToString() + " seleccionados de " + Session.MAX_TALENTOS_ESPECIFICOS.ToString();

            //    SessionActual.paso1 = false;
            //    SessionActual.paso2 = false;
            //    SessionActual.paso4 = false;
            //    SessionActual.pasoCorrec = false;
            //    SessionActual.paso3 = false;
            //    SessionActual.pasoTE = true;
            //    SessionActual.pasoVirtud = false;
            //}
        }

        private int ActualizarContadoresBuzon(int idBuzon)
        {
            int contador = 0;
            //if (idBuzon == 1)
            //{
            //    for (int i = 0; i < SessionActual.BuzonTE.lstTalento.Count; i++)
            //    {
            //        if (SessionActual.BuzonTE.lstTalento[i].seleccionado)
            //            contador++;
            //    }
            //}
            return contador;
        }

        private void ReasignarUrlsTalentos()
        {
            //for (int i = 0; i < SessionActual.BuzonTE.lstTalento.Count; i++)
            //{
            //    if (SessionActual.BuzonTE.lstTalento[i].Image.Contains("a.png"))
            //    {
            //        string urlEspalda = SessionActual.BuzonTE.lstTalento[i].Image;
            //        string urlFrente = SessionActual.BuzonTE.lstTalento[i].Example;

            //        SessionActual.BuzonTE.lstTalento[i].Example = urlEspalda;
            //        SessionActual.BuzonTE.lstTalento[i].Image = urlFrente;
            //    }
            //}
        }

        private void LoadTalentos()
        {
            //TalentosBuzonTE.Clear();

            //for (int i = 0; i < SessionActual.BuzonTE.lstTalento.Count; i++)
            //{
            //    TalentosBuzonTE.Add(new TalentoUC(SessionActual.BuzonTE.lstTalento[i].IdTalento, SessionActual.BuzonTE.lstTalento[i].Image, SessionActual.BuzonTE.lstTalento[i].Nombre, SessionActual.BuzonTE.lstTalento[i].Descripcion, SessionActual.BuzonTE.lstTalento[i].seleccionado, i));
            //}

            //talentosListBoxTE.ItemsSource = TalentosBuzonTE;
        }

        //mostrar opción Seleccionar talento del buzón 1
        private void imgPuntajesBuzon1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if ((!SessionActual.BuzonTE.activo) && (ppSeAcabo.Visibility == Visibility.Collapsed))
            //{
            //    SessionActual.BuzonTE.activo = false;
            //    ppPuntajesBuzonTE.Visibility = Visibility.Visible;

            //    SessionActual.BuzonTE.activo = true;

            //    LoadTalentos();

            //    _cambiarInstruccion.Invoke(Enumerador.Instruccion.SegundaInstruccion);

            //    if (ActualizarContadoresBuzon(1).Equals(Session.MAX_TALENTOS_ESPECIFICOS))
            //    {
            //        txtCantidadTE.Text = Session.MAX_TALENTOS_ESPECIFICOS.ToString() + " seleccionados de " + Session.MAX_TALENTOS_ESPECIFICOS.ToString();

            //        SessionActual.paso1 = false;
            //        SessionActual.paso2 = false;
            //        SessionActual.paso4 = false;
            //        SessionActual.pasoCorrec = false;
            //        SessionActual.paso3 = false;
            //        SessionActual.pasoTE = true;
            //        SessionActual.pasoVirtud = false;

            //        if (ppSeAcabo.Visibility == Visibility.Collapsed)
            //            _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);
            //    }
            //}
        }

        //Ir a la sgte página Silverlight
        private void sgte_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //TalentosReference.ResultadoBE rpta = new Talentos_Master.TalentosReference.ResultadoBE();
            ////rpta.Juego_id = SessionActual.juego.Juego_id;
            //rpta.Resultado_id = SessionActual.resultado.Resultado_id;

            //for (int i = 0; i < SessionActual.BuzonTE.lstTalento.Count; i++)
            //{
            //    rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.BuzonTE.lstTalento[i].seleccionado).ToString(), ", ");
            //}

            //rpta.BuzonId = SessionActual.resultado.BuzonId;
            //rpta.Participante_id = SessionActual.participante.UsuarioId;
            //rpta.TalentoId = SessionActual.resultado.TalentoId;

            //rpta.Fecha = DateTime.Now;

            //SessionActual.resultado.Seleccionado = rpta.Seleccionado;

            ////guardar resultado del juego
            //TalentosReference.WSTalentosSoapClient ws = new Talentos_Master.TalentosReference.WSTalentosSoapClient();
            //ws.ResultadoActualizarCompleted += new EventHandler<TalentosReference.ResultadoActualizarCompletedEventArgs>(resultUpdate_completed);
            //ws.ResultadoActualizarAsync(rpta);
        }

        public void resultUpdate_completed(object sender, TalentosReference.ResultadoActualizarCompletedEventArgs e)
        {
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = false;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrec = false;
            SessionActual.pasoTE = false;
            SessionActual.pasoVirtud = true;

            SessionActual.terminoClasificacion = false;
            SessionActual.terminoSeleccion = false;

            _cambiarContenido.Invoke(Enumerador.Pagina.SeleccionarVirtudes);
            _cambiarInstruccion.Invoke(Enumerador.Instruccion.InstruccionVirtudes);
        }

        public void ValidarSeleccionTalentos()
        {
            //if ((SessionActual.cantSeleccionadosBuzonTE.Equals(Session.MAX_TALENTOS_ESPECIFICOS)))
            //{
            //    if (SessionActual.cantSeleccionadosBuzonTE == 1)
            //        txtCantidadTE.Text = SessionActual.cantSeleccionadosBuzonTE.ToString() + " seleccionado de " + Session.MAX_TALENTOS_ESPECIFICOS.ToString();
            //    else
            //        txtCantidadTE.Text = SessionActual.cantSeleccionadosBuzonTE.ToString() + " seleccionados de " + Session.MAX_TALENTOS_ESPECIFICOS.ToString();

            //    SessionActual.paso1 = false;
            //    SessionActual.paso2 = false;
            //    SessionActual.paso4 = false;
            //    SessionActual.pasoCorrec = false;
            //    SessionActual.paso3 = false;
            //    SessionActual.pasoTE = true;
            //    SessionActual.pasoVirtud = false;

            //    //ppSeAcabo.IsOpen = true;
            //    //malena
            //    if (!isReverse)
            //    {
            //        if (!SessionActual.terminoSeleccion)
            //        {
            //            txtMensaje.Text = "Has terminado de elegir tus " + Session.MAX_TALENTOS_ESPECIFICOS.ToString() + " talentos especificos.";
            //            //"Has terminado de elegir los 10 talentos que más te identifican y los 5 talentos que menos te identifican.";
            //            ppSeAcabo.Visibility = Visibility.Visible;
            //            canvasPuntajesNivelTE.Visibility = Visibility.Collapsed;

            //            _cambiarInstruccion.Invoke(Enumerador.Instruccion.EnBlanco);

            //            SessionActual.terminoSeleccion = false;
            //        }
            //    }

            //    if (ppSeAcabo.Visibility == Visibility.Collapsed)
            //        _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);
            //}
            //else
            //{
            //    if (SessionActual.BuzonTE.activo)//(buzones.b1)
            //    {
            //        if (SessionActual.cantSeleccionadosBuzonTE == 1)
            //            //if (cantSelec1 == 1)
            //            txtCantidadTE.Text = SessionActual.cantSeleccionadosBuzonTE.ToString() + " seleccionado de " + Session.MAX_TALENTOS_ESPECIFICOS.ToString();
            //        else
            //            txtCantidadTE.Text = SessionActual.cantSeleccionadosBuzonTE.ToString() + " seleccionados de " + Session.MAX_TALENTOS_ESPECIFICOS.ToString();

            //        if (!isReverse)
            //        {
            //            if (SessionActual.cantSeleccionadosBuzonTE.Equals(Session.MAX_TALENTOS_ESPECIFICOS) && (!SessionActual.terminoSeleccion))
            //            {
            //                txtMensaje.Text = "Has terminado de elegir tus " + Session.MAX_TALENTOS_ESPECIFICOS.ToString() + " talentos más desarrollados.";
            //                //"Has terminado de elegir los 10 talentos que más te identifican.";
            //                ppSeAcabo.Visibility = Visibility.Visible;

            //                _cambiarInstruccion.Invoke(Enumerador.Instruccion.EnBlanco);

            //                SessionActual.terminoSeleccion = false;
            //            }
            //        }
            //        _cambiarInstruccion.Invoke(Enumerador.Instruccion.SinNavegacion);
            //    }
            //    else
            //    {
            //        _cambiarInstruccion.Invoke(Enumerador.Instruccion.SinNavegacion);
            //    }
            //}
        }

        //seleccionar talento(con el checkbox)
        private void chkSeleccionado_Checked(object sender, RoutedEventArgs e)
        {
            //if (SessionActual.BuzonTE.activo)
            //{
            //    if (!SessionActual.BuzonTE.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado && SessionActual.cantSeleccionadosBuzonTE >= Session.MAX_TALENTOS_ESPECIFICOS)
            //    { (sender as CheckBox).IsChecked = false; return; }
            //    else
            //    {
            //        SessionActual.BuzonTE.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado = true;

            //        SessionActual.cantSeleccionadosBuzonTE++;
            //    }
            //}

            //ValidarSeleccionTalentos();
        }

        //desseleccionar talento(con el checkbox)
        private void chkSeleccionado_Unchecked(object sender, RoutedEventArgs e)
        {
            //if (SessionActual.BuzonTE.activo)
            //{
            //    if (!SessionActual.BuzonTE.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado)
            //    {
            //        (sender as CheckBox).IsChecked = false;
            //        return;
            //    }
            //    else
            //    {
            //        SessionActual.BuzonTE.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado = false;
            //        SessionActual.cantSeleccionadosBuzonTE--;
            //    }
            //}

            //ValidarSeleccionTalentos();
        }

        private void btnContinuarSeAcabo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (SessionActual.cantSeleccionadosBuzonTE.Equals(Session.MAX_TALENTOS_ESPECIFICOS))
            //{
            //    TalentosReference.ResultadoBE rpta = new Talentos_Master.TalentosReference.ResultadoBE();
            //    //rpta.Juego_id = SessionActual.juego.Juego_id;
            //    rpta.Resultado_id = SessionActual.resultado.Resultado_id;

            //    for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
            //    {
            //        rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.Buzon1.lstTalento[i].seleccionado).ToString(), ", ");
            //    }

            //    for (int i = 0; i < SessionActual.BuzonTE.lstTalento.Count; i++)
            //    {
            //        rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.BuzonTE.lstTalento[i].seleccionado).ToString(), ", ");
            //    }

            //    for (int i = 0; i < SessionActual.Buzon2.lstTalento.Count; i++)
            //    {
            //        rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.Buzon2.lstTalento[i].seleccionado).ToString(), ", ");
            //    }

            //    for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
            //    {
            //        rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.Buzon3.lstTalento[i].seleccionado).ToString(), ", ");
            //    }

            //    rpta.BuzonId = SessionActual.resultado.BuzonId;
            //    rpta.Participante_id = SessionActual.participante.UsuarioId;
            //    rpta.TalentoId = SessionActual.resultado.TalentoId;

            //    rpta.Fecha = DateTime.Now;

            //    SessionActual.resultado.Seleccionado = rpta.Seleccionado;

            //    SessionActual.revisaSelec = true;

            //    //TODO: Se agrego el codigo para que llame a la selecicon de virtudes
            //    _cambiarContenido.Invoke(Enumerador.Pagina.SeleccionarVirtudes);
            //    _cambiarInstruccion.Invoke(Enumerador.Instruccion.InstruccionVirtudes);
            //}
        }

        private void btnRevisarSeAcabo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtCantidadTE.Visibility = Visibility.Visible;

            _cambiarInstruccion.Invoke(Enumerador.Instruccion.InstruccionTalentoEspecifico);

            ppSeAcabo.Visibility = Visibility.Collapsed;
            canvasPuntajesNivelTE.Visibility = Visibility.Visible;

            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrec = false;
            SessionActual.paso3 = false;
            SessionActual.pasoTE = true;
            SessionActual.pasoVirtud = false;

            LoadTalentos();

            SessionActual.revisaSelec = true;

            _cambiarInstruccion.Invoke(Enumerador.Instruccion.InstruccionTalentoEspecifico);

            if (SessionActual.cantSeleccionadosBuzonTE.Equals(Session.MAX_TALENTOS_ESPECIFICOS))
            {
                _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);
            }
        }

        //seleccionar talento del buzón Talento más desarrollado(haciendo clic sobre su imagen)
        private void talentosListBoxTE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TalentoUC uc = (TalentoUC)(talentosListBoxTE.SelectedItem);

            //if ((uc != null))
            //{
            //    if (!isReverse)
            //    {
            //        if (SessionActual.BuzonTE.lstTalento[uc.i].seleccionado)
            //        {
            //            SessionActual.BuzonTE.lstTalento[uc.i].seleccionado = false;
            //        }
            //        else
            //            if (SessionActual.cantSeleccionadosBuzonTE < Session.MAX_TALENTOS_ESPECIFICOS)
            //            {
            //                SessionActual.BuzonTE.lstTalento[uc.i].seleccionado = true;
            //            }
            //            else
            //            {
            //                SessionActual.BuzonTE.lstTalento[uc.i].seleccionado = false;
            //            }

            //        if (SessionActual.cantSeleccionadosBuzonTE.Equals(1) && !SessionActual.BuzonTE.lstTalento[uc.i].seleccionado)
            //        {
            //            txtCantidadTE.Text = "0 seleccionados de " + Session.MAX_TALENTOS_ESPECIFICOS.ToString();
            //        }

            //        isReverse = false;
            //    }
            //    else
            //    {
            //        if (uc.source.ToString().Contains("a.png"))
            //        {
            //            url = uc.source.ToString().Replace("a.png", ".png");
            //            url = url.Replace("talentos/example", "talentos/images");
            //        }
            //        else
            //        {
            //            url = uc.source.ToString().Replace(".png", "a.png");
            //            url = url.Replace("talentos/images", "talentos/example");
            //        }

            //        SessionActual.BuzonTE.lstTalento[uc.i].Image = url;
            //        isReverse = false;
            //    }

            //    SessionActual.cantSeleccionadosBuzonTE = 0;

            //    talentosListBoxTE.ItemsSource = null;
            //    LoadTalentos();
            //}
        }

        //voltear talento
        private void btnVoltear_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isReverse = true;
        }
    }
}
