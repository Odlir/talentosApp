using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading;

namespace Talentos_Master
{
    public partial class frmSeleccionarVirtudes : IPaginaContenida
    {
        private List<TalentoUC> TalentosBuzonVirtud = new List<TalentoUC>();
        private Session SessionActual;
        private bool isLoading;
        private bool isReverse;
        private string url;

        public frmSeleccionarVirtudes()
        {
            InitializeComponent();

            BusyWindow.IsBusy = true;

            SessionActual = Session.getInstance();
            ReasignarUrlsVirtudes();
            txtCantidadVirtud.Text = "0 seleccionados de " + Session.MAX_VIRTUDES.ToString();
            isLoading = true;

            if (SessionActual.BuzonVirtudes.lstTalento.Count.Equals(Session.MAX_VIRTUDES))
            {
                if (!SessionActual.cantCalificadosBuzonVirtud.Equals(Session.MAX_VIRTUDES))
                {
                    for (int i = 0; i < SessionActual.BuzonVirtudes.lstTalento.Count; i++)
                    {
                        SessionActual.BuzonVirtudes.lstTalento[i].seleccionado = true;
                    }
                }

                ppPuntajesBuzonVirtud.Visibility = Visibility.Collapsed;
                SessionActual.BuzonVirtudes.activo = false;

                LoadTalentos();
            }

            if (!SessionActual.BuzonVirtudes.lstTalento.Count.Equals(Session.MAX_VIRTUDES))
            {
                ppPuntajesBuzonVirtud.Visibility = Visibility.Visible;

                SessionActual.BuzonVirtudes.activo = true;

                SessionActual.cantSeleccionadosBuzonVirtudes = 0;
                LoadTalentos();
            }

            txtTotal.Text = SessionActual.BuzonVirtudes.lstTalento.Count.ToString() + " virtudes.";

            if (ActualizarContadoresBuzon(1).Equals(Session.MAX_VIRTUDES))
            {
                txtCantidadVirtud.Text = Session.MAX_VIRTUDES.ToString() + " seleccionados de " + Session.MAX_VIRTUDES.ToString();

                SessionActual.paso1 = false;
                SessionActual.paso2 = false;
                SessionActual.paso4 = false;
                SessionActual.pasoCorrec = false;
                SessionActual.paso3 = false;
                SessionActual.pasoTE = false;
                SessionActual.pasoVirtud = true;
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

        private int ActualizarContadoresBuzon(int idBuzon)
        {
            int contador = 0;
            if (idBuzon == 1)
            {
                for (int i = 0; i < SessionActual.BuzonVirtudes.lstTalento.Count; i++)
                {
                    if (SessionActual.BuzonVirtudes.lstTalento[i].seleccionado)
                        contador++;
                }
            }
            return contador;
        }

        private void ReasignarUrlsVirtudes()
        {
            for (int i = 0; i < SessionActual.BuzonVirtudes.lstTalento.Count; i++)
            {
                if (SessionActual.BuzonVirtudes.lstTalento[i].Image.Contains("a.png"))
                {
                    string urlEspalda = SessionActual.BuzonVirtudes.lstTalento[i].Image;
                    string urlFrente = SessionActual.BuzonVirtudes.lstTalento[i].Example;

                    SessionActual.BuzonVirtudes.lstTalento[i].Example = urlEspalda;
                    SessionActual.BuzonVirtudes.lstTalento[i].Image = urlFrente;
                }
            }
        }

        private void LoadTalentos()
        {
            TalentosBuzonVirtud.Clear();

            for (int i = 0; i < SessionActual.BuzonVirtudes.lstTalento.Count; i++)
            {
                TalentosBuzonVirtud.Add(new TalentoUC(SessionActual.BuzonVirtudes.lstTalento[i].IdTalento, SessionActual.BuzonVirtudes.lstTalento[i].Image, SessionActual.BuzonVirtudes.lstTalento[i].Nombre, SessionActual.BuzonVirtudes.lstTalento[i].Descripcion, SessionActual.BuzonVirtudes.lstTalento[i].seleccionado, i));
            }

            talentosListBoxVirtud.ItemsSource = TalentosBuzonVirtud;
        }

        //mostrar opción Seleccionar talento del buzón 1
        private void imgPuntajesBuzon1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!SessionActual.BuzonVirtudes.activo) && (ppSeAcabo.Visibility == Visibility.Collapsed))
            {
                SessionActual.BuzonVirtudes.activo = false;
                ppPuntajesBuzonVirtud.Visibility = Visibility.Visible;

                SessionActual.BuzonVirtudes.activo = true;

                LoadTalentos();

                _cambiarInstruccion.Invoke(Enumerador.Instruccion.SegundaInstruccion);

                if (ActualizarContadoresBuzon(1).Equals(Session.MAX_VIRTUDES))
                {
                    txtCantidadVirtud.Text = Session.MAX_VIRTUDES.ToString() + " seleccionados de " + Session.MAX_VIRTUDES.ToString();

                    SessionActual.paso1 = false;
                    SessionActual.paso2 = false;
                    SessionActual.paso4 = false;
                    SessionActual.pasoCorrec = false;
                    SessionActual.paso3 = false;
                    SessionActual.pasoTE = false;
                    SessionActual.pasoVirtud = true;

                    if (ppSeAcabo.Visibility == Visibility.Collapsed)
                        _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);
                }
            }
        }

        //Ir a la sgte página Silverlight
        private void sgte_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            resultUpdate_completed(sender, null);
        }

        private void reporteClient_GenerarReporteCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            
        }

        public void resultUpdate_completed(object sender, TalentosReference.InsertarResultadoCompletedEventArgs e)
        {
            ReportesReference.wsReporteSoapClient reporteClient = new Talentos_Master.ReportesReference.wsReporteSoapClient();
            reporteClient.EnviarMailResultadoCompleted+=new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(reporteClient_GenerarReporteCompleted);
            reporteClient.EnviarMailResultadoAsync(SessionActual.CodEvaluacion, SessionActual.Token);

            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = true;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrec = false;
            SessionActual.pasoTE = false;
            SessionActual.pasoVirtud = false;

            SessionActual.terminoClasificacion = true;
            SessionActual.terminoSeleccionFinal = true;

            _cambiarContenido.Invoke(Enumerador.Pagina.AgradecimientoJuego);
            _cambiarInstruccion.Invoke(Enumerador.Instruccion.EnBlanco);
        }

        public void ValidarSeleccionVirtudes()
        {
            if ((SessionActual.cantSeleccionadosBuzonVirtudes.Equals(Session.MAX_VIRTUDES)))
            {
                if (SessionActual.cantSeleccionadosBuzonVirtudes == 1)
                    txtCantidadVirtud.Text = SessionActual.cantSeleccionadosBuzonVirtudes.ToString() + " seleccionado de " + Session.MAX_VIRTUDES.ToString();
                else
                    txtCantidadVirtud.Text = SessionActual.cantSeleccionadosBuzonVirtudes.ToString() + " seleccionados de " + Session.MAX_VIRTUDES.ToString();

                SessionActual.paso1 = false;
                SessionActual.paso2 = false;
                SessionActual.paso4 = false;
                SessionActual.pasoCorrec = false;
                SessionActual.paso3 = false;
                SessionActual.pasoTE = false;
                SessionActual.pasoVirtud = true;

                if (!isReverse)
                {
                    if (!SessionActual.terminoSeleccionFinal)
                    {
                        txtMensaje.Text = "Has terminado de elegir tus " + Session.MAX_VIRTUDES.ToString() + " virtudes.";
                        //"Has terminado de elegir los 10 talentos que más te identifican y los 5 talentos que menos te identifican.";
                        ppSeAcabo.Visibility = Visibility.Visible;
                        canvasPuntajesNivelVirtud.Visibility = Visibility.Collapsed;

                        _cambiarInstruccion.Invoke(Enumerador.Instruccion.EnBlanco);

                        SessionActual.terminoSeleccionFinal = false;
                    }
                }

                if (ppSeAcabo.Visibility == Visibility.Collapsed)
                    _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);
            }
            else
            {
                if (SessionActual.BuzonVirtudes.activo)//(buzones.b1)
                {
                    if (SessionActual.cantSeleccionadosBuzonVirtudes == 1)
                        //if (cantSelec1 == 1)
                        txtCantidadVirtud.Text = SessionActual.cantSeleccionadosBuzonVirtudes.ToString() + " seleccionado de " + Session.MAX_VIRTUDES.ToString();
                    else
                        txtCantidadVirtud.Text = SessionActual.cantSeleccionadosBuzonVirtudes.ToString() + " seleccionados de " + Session.MAX_VIRTUDES.ToString();

                    if (!isReverse)
                    {
                        if (SessionActual.cantSeleccionadosBuzonVirtudes.Equals(Session.MAX_VIRTUDES) && (!SessionActual.terminoSeleccionFinal))
                        {
                            txtMensaje.Text = "Has terminado de elegir tus " + Session.MAX_VIRTUDES.ToString() + " virtudes.";
                            //"Has terminado de elegir los 10 talentos que más te identifican.";
                            ppSeAcabo.Visibility = Visibility.Visible;

                            _cambiarInstruccion.Invoke(Enumerador.Instruccion.EnBlanco);

                            SessionActual.terminoSeleccionFinal = false;
                        }
                    }
                    _cambiarInstruccion.Invoke(Enumerador.Instruccion.SinNavegacion);
                }
                else
                {
                    _cambiarInstruccion.Invoke(Enumerador.Instruccion.SinNavegacion);
                }
            }
        }

        //seleccionar talento(con el checkbox)
        private void chkSeleccionado_Checked(object sender, RoutedEventArgs e)
        {
            if (SessionActual.BuzonVirtudes.activo)
            {
                if (!SessionActual.BuzonVirtudes.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado && SessionActual.cantSeleccionadosBuzonVirtudes >= Session.MAX_VIRTUDES)
                { (sender as CheckBox).IsChecked = false; return; }
                else
                {
                    SessionActual.BuzonVirtudes.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado = true;

                    SessionActual.cantSeleccionadosBuzonVirtudes++;
                }
            }

            ValidarSeleccionVirtudes();
        }

        //desseleccionar talento(con el checkbox)
        private void chkSeleccionado_Unchecked(object sender, RoutedEventArgs e)
        {
            if (SessionActual.BuzonVirtudes.activo)
            {
                if (!SessionActual.BuzonVirtudes.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado)
                {
                    (sender as CheckBox).IsChecked = false;
                    return;
                }
                else
                {
                    SessionActual.BuzonVirtudes.lstTalento[Convert.ToInt16((sender as CheckBox).Tag)].seleccionado = false;
                    SessionActual.cantSeleccionadosBuzonVirtudes--;
                }
            }

            ValidarSeleccionVirtudes();
        }

        /// <summary>
        /// Registra el resultado al terminar el juego
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContinuarSeAcabo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SessionActual.cantSeleccionadosBuzonVirtudes.Equals(Session.MAX_VIRTUDES))
            {
                TalentosReference.ResultadoBE objResultadoBE = new Talentos_Master.TalentosReference.ResultadoBE();

                for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
                {
                    objResultadoBE.Seleccionado = string.Concat(objResultadoBE.Seleccionado, Convert.ToInt16(SessionActual.Buzon1.lstTalento[i].seleccionado).ToString(), ", ");
                    objResultadoBE.TendenciaId = string.Concat(objResultadoBE.TendenciaId, Convert.ToInt16(SessionActual.Buzon1.lstTalento[i].IdTendencia).ToString(), ", ");
                    objResultadoBE.TalentoId = objResultadoBE.TalentoId + SessionActual.Buzon1.lstTalento[i].IdTalento.ToString() + ", ";
                    objResultadoBE.BuzonId = objResultadoBE.BuzonId + "1, ";
                    if (SessionActual.Buzon1.lstTalento[i].seleccionado)
                        objResultadoBE.TipoDesarrollo = string.Concat(objResultadoBE.TipoDesarrollo, "1", ", ");
                    else
                        objResultadoBE.TipoDesarrollo = string.Concat(objResultadoBE.TipoDesarrollo, "0", ", ");
                }

                for (int i = 0; i < SessionActual.Buzon2.lstTalento.Count; i++)
                {
                    objResultadoBE.Seleccionado = string.Concat(objResultadoBE.Seleccionado, Convert.ToInt16(SessionActual.Buzon2.lstTalento[i].seleccionado).ToString(), ", ");
                    objResultadoBE.TendenciaId = string.Concat(objResultadoBE.TendenciaId, Convert.ToInt16(SessionActual.Buzon2.lstTalento[i].IdTendencia).ToString(), ", ");
                    objResultadoBE.TipoDesarrollo = string.Concat(objResultadoBE.TipoDesarrollo, "0", ", ");
                    objResultadoBE.TalentoId = objResultadoBE.TalentoId + SessionActual.Buzon2.lstTalento[i].IdTalento.ToString() + ", ";
                    objResultadoBE.BuzonId = objResultadoBE.BuzonId + "2, ";
                }

                for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
                {
                    objResultadoBE.Seleccionado = string.Concat(objResultadoBE.Seleccionado, Convert.ToInt16(SessionActual.Buzon3.lstTalento[i].seleccionado).ToString(), ", ");
                    objResultadoBE.TendenciaId = string.Concat(objResultadoBE.TendenciaId, Convert.ToInt16(SessionActual.Buzon3.lstTalento[i].IdTendencia).ToString(), ", ");
                    objResultadoBE.TalentoId = objResultadoBE.TalentoId + SessionActual.Buzon3.lstTalento[i].IdTalento.ToString() + ", ";
                    objResultadoBE.BuzonId = objResultadoBE.BuzonId + "3, ";
                    if (SessionActual.Buzon3.lstTalento[i].seleccionado)
                        objResultadoBE.TipoDesarrollo = string.Concat(objResultadoBE.TipoDesarrollo, "2", ", ");
                    else
                        objResultadoBE.TipoDesarrollo = string.Concat(objResultadoBE.TipoDesarrollo, "0", ", ");
                }

                for (int i = 0; i < SessionActual.BuzonTEMas.lstTalento.Count; i++)
                {
                    //rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.BuzonTEMas.lstTalento[i].seleccionado).ToString(), ", ");
                    objResultadoBE.Seleccionado = objResultadoBE.Seleccionado + "1, ";
                    objResultadoBE.TendenciaId = string.Concat(objResultadoBE.TendenciaId, Convert.ToInt16(SessionActual.BuzonTEMas.lstTalento[i].IdTendencia).ToString(), ", ");
                    objResultadoBE.TalentoId = objResultadoBE.TalentoId + SessionActual.BuzonTEMas.lstTalento[i].IdTalento.ToString() + ", ";
                    objResultadoBE.BuzonId = objResultadoBE.BuzonId + "4, ";
                    if (SessionActual.BuzonTEMas.lstTalento[i].seleccionado)
                        objResultadoBE.TipoDesarrollo = string.Concat(objResultadoBE.TipoDesarrollo, "3", ", ");
                    else
                        objResultadoBE.TipoDesarrollo = string.Concat(objResultadoBE.TipoDesarrollo, "0", ", ");
                }

                for (int i = 0; i < SessionActual.BuzonTEIntermedio.lstTalento.Count; i++)
                {
                    objResultadoBE.Seleccionado = string.Concat(objResultadoBE.Seleccionado, Convert.ToInt16(SessionActual.BuzonTEIntermedio.lstTalento[i].seleccionado).ToString(), ", ");
                    objResultadoBE.TendenciaId = string.Concat(objResultadoBE.TendenciaId, Convert.ToInt16(SessionActual.BuzonTEIntermedio.lstTalento[i].IdTendencia).ToString(), ", ");
                    objResultadoBE.TalentoId = objResultadoBE.TalentoId + SessionActual.BuzonTEIntermedio.lstTalento[i].IdTalento.ToString() + ", ";
                    objResultadoBE.BuzonId = objResultadoBE.BuzonId + "5, ";
                    if (SessionActual.BuzonTEIntermedio.lstTalento[i].seleccionado)
                        objResultadoBE.TipoDesarrollo = string.Concat(objResultadoBE.TipoDesarrollo, "4", ", ");
                    else
                        objResultadoBE.TipoDesarrollo = string.Concat(objResultadoBE.TipoDesarrollo, "0", ", ");
                }

                for (int i = 0; i < SessionActual.BuzonTEMenos.lstTalento.Count; i++)
                {
                    objResultadoBE.Seleccionado = string.Concat(objResultadoBE.Seleccionado, Convert.ToInt16(SessionActual.BuzonTEMenos.lstTalento[i].seleccionado).ToString(), ", ");
                    objResultadoBE.TendenciaId = string.Concat(objResultadoBE.TendenciaId, Convert.ToInt16(SessionActual.BuzonTEMenos.lstTalento[i].IdTendencia).ToString(), ", ");
                    objResultadoBE.TalentoId = objResultadoBE.TalentoId + SessionActual.BuzonTEMenos.lstTalento[i].IdTalento.ToString() + ", ";
                    objResultadoBE.BuzonId = objResultadoBE.BuzonId + "6, ";
                    if (SessionActual.BuzonTEMenos.lstTalento[i].seleccionado)
                        objResultadoBE.TipoDesarrollo = string.Concat(objResultadoBE.TipoDesarrollo, "5", ", ");
                    else
                        objResultadoBE.TipoDesarrollo = string.Concat(objResultadoBE.TipoDesarrollo, "0", ", ");
                }

                for (int i = 0; i < SessionActual.BuzonVirtudes.lstTalento.Count; i++)
                {
                    objResultadoBE.Seleccionado = string.Concat(objResultadoBE.Seleccionado, Convert.ToInt16(SessionActual.BuzonVirtudes.lstTalento[i].seleccionado).ToString(), ", ");
                    objResultadoBE.TendenciaId = string.Concat(objResultadoBE.TendenciaId, Convert.ToInt16(SessionActual.BuzonVirtudes.lstTalento[i].IdTendencia).ToString(), ", ");
                    objResultadoBE.TalentoId = objResultadoBE.TalentoId + SessionActual.BuzonVirtudes.lstTalento[i].IdTalento.ToString() + ", ";
                    objResultadoBE.BuzonId = objResultadoBE.BuzonId + "7, ";
                    if (SessionActual.BuzonVirtudes.lstTalento[i].seleccionado)
                        objResultadoBE.TipoDesarrollo = string.Concat(objResultadoBE.TipoDesarrollo, "6", ", ");
                    else
                        objResultadoBE.TipoDesarrollo = string.Concat(objResultadoBE.TipoDesarrollo, "0", ", ");
                }

                objResultadoBE.Participante_id = SessionActual.participante.UsuarioId;
                objResultadoBE.NombreParticipante = SessionActual.participante.Nombres + " " + SessionActual.participante.APaterno;
                objResultadoBE.Fecha = DateTime.Now;
                objResultadoBE.DNI = SessionActual.DNI;
                objResultadoBE.CorreoElectronico = SessionActual.CorreoParticipanteMasivo;
                objResultadoBE.EsMasivo = SessionActual.EsMasivo;
                objResultadoBE.CodEvaluacion = SessionActual.CodEvaluacion;

                SessionActual.resultado.Seleccionado = objResultadoBE.Seleccionado;
                SessionActual.resultado.TendenciaId = objResultadoBE.TendenciaId;
                SessionActual.resultado.TipoDesarrollo = objResultadoBE.TipoDesarrollo;
                SessionActual.resultado.TalentoId = objResultadoBE.TalentoId;
                SessionActual.resultado.BuzonId = objResultadoBE.BuzonId;

                SessionActual.revisaSelec = true;

                //guardar resultado del juego
                TalentosReference.WSTalentosSoapClient ws = new Talentos_Master.TalentosReference.WSTalentosSoapClient();
                ws.InsertarResultadoCompleted += new EventHandler<TalentosReference.InsertarResultadoCompletedEventArgs>(resultUpdate_completed);
                ws.InsertarResultadoAsync(objResultadoBE);
            }
        }

        private void btnRevisarSeAcabo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtCantidadVirtud.Visibility = Visibility.Visible;

            _cambiarInstruccion.Invoke(Enumerador.Instruccion.InstruccionVirtudes);

            ppSeAcabo.Visibility = Visibility.Collapsed;
            canvasPuntajesNivelVirtud.Visibility = Visibility.Visible;

            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrec = false;
            SessionActual.paso3 = false;
            SessionActual.pasoTE = false;
            SessionActual.pasoVirtud = true;

            LoadTalentos();

            SessionActual.revisaSelec = true;

            if (SessionActual.cantSeleccionadosBuzonVirtudes.Equals(Session.MAX_VIRTUDES))
            {
                _cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);
            }
        }

        //seleccionar talento del buzón Talento más desarrollado(haciendo clic sobre su imagen)
        private void talentosListBoxVirtud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TalentoUC uc = (TalentoUC)(talentosListBoxVirtud.SelectedItem);

            if ((uc != null))
            {
                if (!isReverse)
                {
                    if (SessionActual.BuzonVirtudes.lstTalento[uc.i].seleccionado)
                    {
                        SessionActual.BuzonVirtudes.lstTalento[uc.i].seleccionado = false;
                    }
                    else
                        if (SessionActual.cantSeleccionadosBuzonVirtudes < Session.MAX_VIRTUDES)
                        {
                            SessionActual.BuzonVirtudes.lstTalento[uc.i].seleccionado = true;
                        }
                        else
                        {
                            SessionActual.BuzonVirtudes.lstTalento[uc.i].seleccionado = false;
                        }

                    if (SessionActual.cantSeleccionadosBuzonVirtudes.Equals(1) && !SessionActual.BuzonVirtudes.lstTalento[uc.i].seleccionado)
                    {
                        txtCantidadVirtud.Text = "0 seleccionados de " + Session.MAX_VIRTUDES.ToString();
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
                        string nombreImagen = uc.source.Substring(pos + 1, uc.source.Length - pos - 1);
                        string nombreImagenEspalda = "Espalda " + nombreImagen;

                        url = uc.source.ToString().Replace(nombreImagen, nombreImagenEspalda);
                        url = url.Replace("talentos/images", "talentos/example");
                    }

                    SessionActual.BuzonVirtudes.lstTalento[uc.i].Image = url;
                    isReverse = false;
                }

                SessionActual.cantSeleccionadosBuzonVirtudes = 0;

                talentosListBoxVirtud.ItemsSource = null;
                LoadTalentos();
            }
        }
    }
}
