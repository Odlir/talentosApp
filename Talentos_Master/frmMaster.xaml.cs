using System.Windows;
using System.Windows.Media;
using System;
using System.Windows.Browser;

namespace Talentos_Master
{
    //TODO: (ITERACION 1 - II ENTREGA) OPTIMIZAR NAVEGABILIDAD ENTRE PÁGINAS SILVERLIGHT
    //TODO: (ITERACION 1 - II ENTREGA) ELIMINAR PÁGINAS Y USER CONTROL DUPLICADOS
    //TODO: (ITERACION 2 - I ENTREGA) Incluir en la Secuencia del Juego

    public partial class frmMaster : IPaginaContenida
    {
        //private BuzonGlobal session; //para almacenar el nombre del usuario actual

        private Session SessionActual; //para almacenar el nombre del usuario actual

        public frmMaster()
        {
            InitializeComponent();
            SessionActual = Session.getInstance();
            
            if(SessionActual.participante.Sexo.Equals(1))
                txtfullname.Text = "Bienvenida " + SessionActual.participante.NickName;//string.Concat("Hola ",SessionActual.participante.NickName);
            else
                txtfullname.Text = "Bienvenido " + SessionActual.participante.NickName;//string.Concat("Hola ",SessionActual.participante.NickName);

            //borrar esto
            //SessionActual.paso3 = true;

            if (SessionActual.paso1)
            {
                brInstruc1.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)94, (byte)94, (byte)94));
                txtIndicaciones.Text = "Arrastra cada tarjeta a los buzones de abajo seleccionando los talentos según te identifiquen.";
                txtIndicaciones.FontSize = 14;

                line1.X1 = 0;
                line1.X2 = 165;

                brInstruc2.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
            }

            if (SessionActual.pasoCorrec)
            {
                brInstruc1.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)94, (byte)94, (byte)94));
                txtIndicaciones.Text = "Si deseas mover alguno de estos talentos, arrástralo hasta el buzón que desees.";
                txtIndicaciones.FontSize = 14;

                line1.X1 = 0;
                line1.X2 = 165;

                brInstruc2.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
            }

            if (SessionActual.paso2)
            {
                brInstruc2.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)94, (byte)94, (byte)94));

                if (SessionActual.Buzon1.lstTalento.Count.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && (SessionActual.Buzon3.lstTalento.Count.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS)))
                    txtIndicaciones.Text = "Del buzón \"Me identifica\", elige los " + Session.MAX_TALENTOS_MAS_DESARROLLADOS + " talentos que más te identifican. Del buzón \"No me identifica\", elige los " + Session.MAX_TALENTOS_MENOS_DESARROLLADOS + " talentos que menos te identifican. ";
                else
                {
                    if (SessionActual.Buzon1.lstTalento.Count.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS))
                        txtIndicaciones.Text = "Del buzón \"No me identifica\", elige los " + Session.MAX_TALENTOS_MENOS_DESARROLLADOS + " talentos que menos te identifican. ";
                    else
                    {
                        if(SessionActual.Buzon3.lstTalento.Count.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS))
                            txtIndicaciones.Text = "Del buzón \"Me identifica\", elige los " + Session.MAX_TALENTOS_MAS_DESARROLLADOS + " talentos que más te identifican.";
                    }
                }

                txtIndicaciones.FontSize = 14;

                line1.X1 = 0;
                line1.X2 = 180;

                brInstruc1.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
            }

            if (SessionActual.paso3)
            {
                line1.X1 = 10;
                line1.X2 = 165;

                brInstruc1.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                brInstruc2.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
            }

            if (SessionActual.paso4) 
            {
                brInstruc1.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                brInstruc2.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));

                line1.X1 = 10;
                line1.X2 = 165;
            }

            if (SessionActual.pasoTE)
            {
                brTalentosEspecificos.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)94, (byte)94, (byte)94));

                if ((SessionActual.BuzonTEMas.lstTalento.Count + SessionActual.BuzonTEIntermedio.lstTalento.Count + SessionActual.BuzonTEMenos.lstTalento.Count).Equals(SessionActual.lstTalentosEspecificos.Count))
                    txtIndicaciones.Text = "Elige los talentos especificos que más te identifican.";
                
                txtIndicaciones.FontSize = 14;

                line1.X1 = 0;
                line1.X2 = 165;

                brInstruc1.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                brInstruc2.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                brVirtudes.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
            }

            if (SessionActual.pasoVirtud)
            {
                brVirtudes.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)94, (byte)94, (byte)94));

                if (SessionActual.BuzonVirtudes.lstTalento.Count.Equals(Session.MAX_VIRTUDES))
                    txtIndicaciones.Text = "Elige las " + Session.MAX_VIRTUDES.ToString() + " virtudes que más te identifican.";

                txtIndicaciones.FontSize = 14;

                line1.X1 = 0;
                line1.X2 = 165;

                brInstruc1.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                brInstruc2.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                brTalentosEspecificos.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
            }
        }

        //Instrucciones según etapa  o página del juego actual
        public void CambiarInstruccion(Enumerador.Instruccion eInstruccion)
        {
            switch (eInstruccion)
            {
                case Enumerador.Instruccion.PrimeraInstruccion:

                    txtTitle1.Text = "  Agrupando";
                    txtTitle2.Text = "mis talentos";

                    brInstruc1.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)94, (byte)94, (byte)94));

                    txtIndicaciones.Text = "Arrastra cada tarjeta a los buzones de abajo seleccionando los talentos según te identifiquen. ";
                    txtIndicaciones.FontSize = 14;

                    line1.X1 = 10;
                    line1.X2 = 165;

                    txtTitle1.Text = "  Agrupando";
                    txtTitle2.Text = " mis talentos";

                    txtSubtitulo.Visibility = Visibility.Collapsed;
                    txtIndicaciones.Margin = new Thickness(22, 15, 0, 0);

                    brInstruc2.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                    brTalentosEspecificos.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                    brVirtudes.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));

                    break;
                case Enumerador.Instruccion.SegundaInstruccion:
                    txtTitle1.Text = "  Eligiendo";
                    txtTitle2.Text = " mis talentos";

                    brInstruc2.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)94, (byte)94, (byte)94));

                    if (SessionActual.Buzon1.activo)//(SessionActual.Buzon1.lstTalento.Count>10)
                    {
                        txtSubTitulo1.Text = "Talento";
                        txtSubTitulo2.Text = "más desarrollado";
                        
                        //txtIndicaciones.Text = "Estás en el buzón \"Me identifica\". Busca con las flechas los 10 talentos que más te identiquen y haz clic sobre cada uno para seleccionarlos. Si seleccionaste alguno por error, haz clic sobre él para deseleccionarlo y sigue buscando hasta completar tus 10 talentos.";
                        txtIndicaciones.Text = "Estás en el buzón \"Talento  más desarrollado\". Selecciona  tus " + Session.MAX_TALENTOS_MAS_DESARROLLADOS + " talentos más desarrollados.";
                    }
                    else if (SessionActual.Buzon3.activo)//(SessionActual.Buzon3.lstTalento.Count>5)
                    {
                        txtIndicaciones.Text = "Estás en el buzón \"Talento  menos desarrollado\". Selecciona tus " + Session.MAX_TALENTOS_MENOS_DESARROLLADOS + " talentos menos desarrollados.";
                            //"Estás en el buzón \"No me identifica\". Selecciona los 5 talentos que menos te identifiquen.";
                        txtSubTitulo1.Text = "Talento";
                        txtSubTitulo2.Text = "menos desarrollado";
                    }

                    txtSubtitulo.Visibility = Visibility.Visible;
                    txtIndicaciones.Margin = new Thickness(22, 45, 0, 0);

                    txtIndicaciones.FontSize = 14;

                    line1.X1 = 10;
                    line1.X2 = 180;

                    txtTitle1.Text = "  Eligiendo";
                    txtTitle2.Text = " mis talentos";

                    brInstruc1.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                    brTalentosEspecificos.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                    brVirtudes.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));

                    break;
                case Enumerador.Instruccion.InstruccionTalentoEspecifico:
                    txtTitle1.Text = "  Eligiendo";
                    txtTitle2.Text = " mis talentos específicos";

                    txtIndicaciones.Text = "Arrastra cada tarjeta a los buzones de abajo seleccionando los talentos específicos según te identifiquen. ";

                    brTalentosEspecificos.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)94, (byte)94, (byte)94));
                    brInstruc1.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                    brInstruc2.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                    brVirtudes.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                    break;
                case Enumerador.Instruccion.InstruccionVirtudes:
                    txtTitle1.Text = "  Eligiendo";
                    txtTitle2.Text = " mis virtudes más importantes";
                    txtIndicaciones.Text = "Elige tres (3) de las siguientes virtudes, aquellas que consideres más importantes para tu carrera.";

                    brVirtudes.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)94, (byte)94, (byte)94));
                    brTalentosEspecificos.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                    brInstruc1.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                    brInstruc2.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                    break;
                //case Enumerador.Instruccion.InstruccionEnvioReporte:
                //    txtTitle1.Text = "  Envio de resultados ";
                //    txtTitle2.Text = " a su correo electrónico";

                //    brVirtudes.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                //    brTalentosEspecificos.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                //    brInstruc1.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                //    brInstruc2.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                //    break;
                case Enumerador.Instruccion.instruccionCorreccion:

                    txtTitle1.Text = "  Agrupando";
                    txtTitle2.Text = " mis talentos";
                    brInstruc1.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)94, (byte)94, (byte)94));

                    txtIndicaciones.Text = "Si deseas mover alguno de estos talentos, arrástralo hasta el buzón que desees.";
                    txtIndicaciones.FontSize = 14;

                    line1.X1 = 10;
                    line1.X2 = 165;

                    brInstruc2.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                    brVirtudes.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                    brTalentosEspecificos.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));

                    txtSubtitulo.Visibility = Visibility.Collapsed;
                    txtIndicaciones.Margin = new Thickness(22, 15, 0, 0);

                    break;
                case Enumerador.Instruccion.instruccionCorreccionTE:

                    txtTitle1.Text = "  Agrupando";
                    txtTitle2.Text = " mis talentos específicos";
                    brInstruc1.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                    brInstruc2.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                    brVirtudes.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));
                    brTalentosEspecificos.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)94, (byte)94, (byte)94));

                    txtIndicaciones.Text = "Si deseas mover alguno de estos talentos específicos, arrástralo hasta el buzón que desees.";
                    txtIndicaciones.FontSize = 14;

                    line1.X1 = 10;
                    line1.X2 = 165;

                    brInstruc2.Background = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)144, (byte)144));

                    txtSubtitulo.Visibility = Visibility.Collapsed;
                    txtIndicaciones.Margin = new Thickness(22, 15, 0, 0);

                    break;
                case Enumerador.Instruccion.EnBlanco:
                    txtIndicaciones.Text = "";
                    txtSubTitulo1.Text = "";
                    txtSubTitulo2.Text = "";
                    break;

                case Enumerador.Instruccion.Siguiente:
                    txtNavegacion.Text = "Continuar >>  ";
                    break;

                case Enumerador.Instruccion.Anterior:
                    txtNavegacion.Text = "<< Volver  ";
                    break;

                case Enumerador.Instruccion.SinNavegacion:
                    txtNavegacion.Text = "";
                    break;
            }
        }

        //Navegación entre páginas Silverlight
        public void CambiarContenido(Enumerador.Pagina ePagina)
        {
            Contenido.Children.Clear();
            IPaginaContenida objPaginaContenida = null;

            Thickness marginThick = new Thickness();
            switch (ePagina)
            {
                case Enumerador.Pagina.ClasificacionPrincipal:
                    objPaginaContenida = new frmClasificacionPrincipal();
                    
                    marginThick = canvContenido.Margin;
                    marginThick.Top = 115;
                    canvContenido.Margin = marginThick;
                                        
                    break;
                case Enumerador.Pagina.ClasificacionDetalle:
                    CambiarInstruccion(Enumerador.Instruccion.instruccionCorreccion);
                   
                    objPaginaContenida = new frmClasificacionDetalle();
                    marginThick = canvContenido.Margin;
                    marginThick.Top = 115;
                    canvContenido.Margin = marginThick;
                    if (SessionActual.terminoClasificacion)
                        CambiarInstruccion(Enumerador.Instruccion.Siguiente);
                    else
                        CambiarInstruccion(Enumerador.Instruccion.SinNavegacion);

                    break;
                case Enumerador.Pagina.ClasificacionDetalleTE:
                    CambiarInstruccion(Enumerador.Instruccion.instruccionCorreccionTE);

                    objPaginaContenida = new frmClasificacionDetalleTE();
                    marginThick = canvContenido.Margin;
                    marginThick.Top = 115;
                    canvContenido.Margin = marginThick;
                    if (SessionActual.terminoClasificacion)
                        CambiarInstruccion(Enumerador.Instruccion.Siguiente);
                    else
                        CambiarInstruccion(Enumerador.Instruccion.SinNavegacion);

                    break;
                case Enumerador.Pagina.Calificacion:
                    objPaginaContenida = new frmCalificacion();

                    break;
                case Enumerador.Pagina.SeleccionarPrincipal:
                    CambiarInstruccion(Enumerador.Instruccion.SegundaInstruccion);
                    CambiarInstruccion(Enumerador.Instruccion.SinNavegacion);
                    objPaginaContenida = new frmSeleccionarPrincipal();
                    marginThick = canvContenido.Margin;
                    marginThick.Top = 115;
                    canvContenido.Margin = marginThick;

                    break;
                case Enumerador.Pagina.SeleccionarTalentosEspecificos:
                    CambiarInstruccion(Enumerador.Instruccion.InstruccionTalentoEspecifico);
                    CambiarInstruccion(Enumerador.Instruccion.SinNavegacion);

                    objPaginaContenida = new frmSeleccionarTalentosEspecificos();
                    marginThick = canvContenido.Margin;
                    marginThick.Top = 115;
                    canvContenido.Margin = marginThick;
                    break;
                case Enumerador.Pagina.SeleccionarVirtudes:
                    CambiarInstruccion(Enumerador.Instruccion.InstruccionVirtudes);
                    CambiarInstruccion(Enumerador.Instruccion.SinNavegacion);

                    objPaginaContenida = new frmSeleccionarVirtudes();
                    marginThick = canvContenido.Margin;
                    marginThick.Top = 115;
                    canvContenido.Margin = marginThick;
                    break;
                case Enumerador.Pagina.MasterRueda:
                    _cambiarContenido.Invoke(Enumerador.Pagina.MasterRueda);
                    return;
                case Enumerador.Pagina.ResultadoIndividual:
                    _cambiarContenido.Invoke(Enumerador.Pagina.ResultadoIndividual);
                    return;
                case Enumerador.Pagina.ResultadoEstadistico:
                    _cambiarContenido.Invoke(Enumerador.Pagina.ResultadoEstadistico);
                    //objPaginaContenida = new frmResultadoEstadistico();
                    //break;
                    return;
                case Enumerador.Pagina.SegundaEtapa:
                    _cambiarContenido.Invoke(Enumerador.Pagina.SegundaEtapa);
                    return;
                case Enumerador.Pagina.TerceraEtapa:
                    _cambiarContenido.Invoke(Enumerador.Pagina.TerceraEtapa);
                    return;
                case Enumerador.Pagina.ResultadoEstadisticoComponent:
                    _cambiarContenido.Invoke(Enumerador.Pagina.ResultadoEstadisticoComponent);
                    return;
                case Enumerador.Pagina.ResultadoRadar:
                    _cambiarContenido.Invoke(Enumerador.Pagina.ResultadoRadar);
                    return;
                case Enumerador.Pagina.Sugerencias:
                    CambiarInstruccion(Enumerador.Instruccion.TerceraInstruccion);
                    objPaginaContenida = new frmRecomendacionesFinales();
                    return;
                case Enumerador.Pagina.AgradecimientoJuego:
                    _cambiarContenido.Invoke(Enumerador.Pagina.AgradecimientoJuego);
                    //objPaginaContenida = new frmResultadoEstadistico();
                    return;
            }
            objPaginaContenida.SetDelegate(CambiarContenido);
            objPaginaContenida.SetDelegate(new Comun.CambiarInstruccion(CambiarInstruccion));
            
            //objPaginaContenida.SetDelegate(new Comun.CambiarNavegacion(CambiarNavegacion));
            Contenido.Children.Add(objPaginaContenida);
        }

        private void txtNavegacion_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (SessionActual.pasoCorrec || SessionActual.pasoCorrecTE || SessionActual.pasoVirtud)
            {
                if (txtNavegacion.Text.Contains("Continuar"))
                {
                    if (SessionActual.pasoCorrec)
                    {
                        SessionActual.Buzon1.activo = true;
                        SessionActual.Buzon2.activo = false;

                        resultUpdateCorrecion_completed(null, null);
                    }
                    else if (SessionActual.pasoCorrecTE)
                    {
                        CambiarInstruccion(Enumerador.Instruccion.Siguiente);

                        SessionActual.BuzonTEIntermedio.activo = SessionActual.BuzonTEMenos.activo = false;
                        SessionActual.BuzonTEMas.activo = true;
                        SessionActual.paso1 = false;
                        SessionActual.paso2 = false;
                        SessionActual.paso3 = false;
                        SessionActual.paso4 = false;
                        SessionActual.pasoCorrecTE = true;
                        CambiarInstruccion(Enumerador.Instruccion.SinNavegacion);
                        CambiarContenido(Enumerador.Pagina.SeleccionarVirtudes);
                    }
                    else if (SessionActual.pasoVirtud)
                    {
                        if (SessionActual.cantSeleccionadosBuzonVirtudes.Equals(Session.MAX_VIRTUDES))
                        {
                            TalentosReference.ResultadoBE rpta = new Talentos_Master.TalentosReference.ResultadoBE();
                            //rpta.Juego_id = SessionActual.juego.Juego_id;
                            rpta.Resultado_id = SessionActual.resultado.Resultado_id;

                            for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
                            {
                                rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.Buzon1.lstTalento[i].seleccionado).ToString(), ", ");
                                rpta.TendenciaId = string.Concat(rpta.TendenciaId, Convert.ToInt16(SessionActual.Buzon1.lstTalento[i].IdTendencia).ToString(), ", ");
                                rpta.TalentoId = rpta.TalentoId + SessionActual.Buzon1.lstTalento[i].IdTalento.ToString() + ", ";
                                rpta.BuzonId = rpta.BuzonId + "1, ";
                                if (SessionActual.Buzon1.lstTalento[i].seleccionado)
                                    rpta.TipoDesarrollo = string.Concat(rpta.TipoDesarrollo, "1", ", ");
                                else
                                    rpta.TipoDesarrollo = string.Concat(rpta.TipoDesarrollo, "0", ", ");
                            }

                            for (int i = 0; i < SessionActual.Buzon2.lstTalento.Count; i++)
                            {
                                rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.Buzon2.lstTalento[i].seleccionado).ToString(), ", ");
                                rpta.TendenciaId = string.Concat(rpta.TendenciaId, Convert.ToInt16(SessionActual.Buzon2.lstTalento[i].IdTendencia).ToString(), ", ");
                                rpta.TipoDesarrollo = string.Concat(rpta.TipoDesarrollo, "0", ", ");
                                rpta.TalentoId = rpta.TalentoId + SessionActual.Buzon2.lstTalento[i].IdTalento.ToString() + ", ";
                                rpta.BuzonId = rpta.BuzonId + "2, ";
                            }

                            for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
                            {
                                rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.Buzon3.lstTalento[i].seleccionado).ToString(), ", ");
                                rpta.TendenciaId = string.Concat(rpta.TendenciaId, Convert.ToInt16(SessionActual.Buzon3.lstTalento[i].IdTendencia).ToString(), ", ");
                                rpta.TalentoId = rpta.TalentoId + SessionActual.Buzon3.lstTalento[i].IdTalento.ToString() + ", ";
                                rpta.BuzonId = rpta.BuzonId + "3, ";
                                if (SessionActual.Buzon3.lstTalento[i].seleccionado)
                                    rpta.TipoDesarrollo = string.Concat(rpta.TipoDesarrollo, "2", ", ");
                                else
                                    rpta.TipoDesarrollo = string.Concat(rpta.TipoDesarrollo, "0", ", ");
                            }

                            for (int i = 0; i < SessionActual.BuzonTEMas.lstTalento.Count; i++)
                            {
                                //rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.BuzonTEMas.lstTalento[i].seleccionado).ToString(), ", ");
                                rpta.Seleccionado = rpta.Seleccionado + "1, ";
                                rpta.TendenciaId = string.Concat(rpta.TendenciaId, Convert.ToInt16(SessionActual.BuzonTEMas.lstTalento[i].IdTendencia).ToString(), ", ");
                                rpta.TalentoId = rpta.TalentoId + SessionActual.BuzonTEMas.lstTalento[i].IdTalento.ToString() + ", ";
                                rpta.BuzonId = rpta.BuzonId + "4, ";
                                if (SessionActual.BuzonTEMas.lstTalento[i].seleccionado)
                                    rpta.TipoDesarrollo = string.Concat(rpta.TipoDesarrollo, "3", ", ");
                                else
                                    rpta.TipoDesarrollo = string.Concat(rpta.TipoDesarrollo, "0", ", ");
                            }

                            for (int i = 0; i < SessionActual.BuzonTEIntermedio.lstTalento.Count; i++)
                            {
                                rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.BuzonTEIntermedio.lstTalento[i].seleccionado).ToString(), ", ");
                                rpta.TendenciaId = string.Concat(rpta.TendenciaId, Convert.ToInt16(SessionActual.BuzonTEIntermedio.lstTalento[i].IdTendencia).ToString(), ", ");
                                rpta.TalentoId = rpta.TalentoId + SessionActual.BuzonTEIntermedio.lstTalento[i].IdTalento.ToString() + ", ";
                                rpta.BuzonId = rpta.BuzonId + "5, ";
                                if (SessionActual.BuzonTEIntermedio.lstTalento[i].seleccionado)
                                    rpta.TipoDesarrollo = string.Concat(rpta.TipoDesarrollo, "4", ", ");
                                else
                                    rpta.TipoDesarrollo = string.Concat(rpta.TipoDesarrollo, "0", ", ");
                            }

                            for (int i = 0; i < SessionActual.BuzonTEMenos.lstTalento.Count; i++)
                            {
                                rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.BuzonTEMenos.lstTalento[i].seleccionado).ToString(), ", ");
                                rpta.TendenciaId = string.Concat(rpta.TendenciaId, Convert.ToInt16(SessionActual.BuzonTEMenos.lstTalento[i].IdTendencia).ToString(), ", ");
                                rpta.TalentoId = rpta.TalentoId + SessionActual.BuzonTEMenos.lstTalento[i].IdTalento.ToString() + ", ";
                                rpta.BuzonId = rpta.BuzonId + "6, ";
                                if (SessionActual.BuzonTEMenos.lstTalento[i].seleccionado)
                                    rpta.TipoDesarrollo = string.Concat(rpta.TipoDesarrollo, "5", ", ");
                                else
                                    rpta.TipoDesarrollo = string.Concat(rpta.TipoDesarrollo, "0", ", ");
                            }

                            for (int i = 0; i < SessionActual.BuzonVirtudes.lstTalento.Count; i++)
                            {
                                rpta.Seleccionado = string.Concat(rpta.Seleccionado, Convert.ToInt16(SessionActual.BuzonVirtudes.lstTalento[i].seleccionado).ToString(), ", ");
                                rpta.TendenciaId = string.Concat(rpta.TendenciaId, Convert.ToInt16(SessionActual.BuzonVirtudes.lstTalento[i].IdTendencia).ToString(), ", ");
                                rpta.TalentoId = rpta.TalentoId + SessionActual.BuzonVirtudes.lstTalento[i].IdTalento.ToString() + ", ";
                                rpta.BuzonId = rpta.BuzonId + "7, ";
                                if (SessionActual.BuzonVirtudes.lstTalento[i].seleccionado)
                                    rpta.TipoDesarrollo = string.Concat(rpta.TipoDesarrollo, "6", ", ");
                                else
                                    rpta.TipoDesarrollo = string.Concat(rpta.TipoDesarrollo, "0", ", ");
                            }

                            rpta.Participante_id = SessionActual.participante.UsuarioId;

                            rpta.Fecha = DateTime.Now;

                            SessionActual.resultado.Seleccionado = rpta.Seleccionado;
                            SessionActual.resultado.TendenciaId = rpta.TendenciaId;
                            SessionActual.resultado.TipoDesarrollo = rpta.TipoDesarrollo;
                            SessionActual.resultado.TalentoId = rpta.TalentoId;
                            SessionActual.resultado.BuzonId = rpta.BuzonId;

                            SessionActual.revisaSelec = true;

                            //guardar resultado del juego
                            TalentosReference.WSTalentosSoapClient ws = new Talentos_Master.TalentosReference.WSTalentosSoapClient();
                            ws.ResultadoActualizarCompleted += new EventHandler<TalentosReference.ResultadoActualizarCompletedEventArgs>(resultUpdate_completed);
                            ws.ResultadoActualizarAsync(rpta);
                        }
                    }
                    else
                    {
                        SessionActual.paso1 = true;
                        SessionActual.paso2 = false;
                        SessionActual.paso3 = false;
                        SessionActual.paso4 = false;
                        SessionActual.pasoCorrec = false;
                        SessionActual.pasoTE = false;
                        SessionActual.pasoVirtud = false;

                        SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = SessionActual.Buzon3.activo = false;
                        //_cambiarContenido.Invoke(Enumerador.Pagina.ClasificacionPrincipal);
                        _cambiarContenido.Invoke(Enumerador.Pagina.MasterRueda);
                    }
                }
                else if (txtNavegacion.Text.Contains("Volver"))
                {
                    if (SessionActual.pasoCorrec)
                    {
                        SessionActual.paso1 = true;
                        SessionActual.paso2 = false;
                        SessionActual.paso3 = false;
                        SessionActual.paso4 = false;
                        SessionActual.pasoCorrec = false;

                        CambiarContenido(Enumerador.Pagina.MasterRueda);
                    }
                    else if (SessionActual.pasoCorrecTE)
                    {
                        SessionActual.paso1 = false;
                        SessionActual.paso2 = false;
                        SessionActual.paso3 = false;
                        SessionActual.paso4 = false;
                        SessionActual.pasoCorrec = false;
                        SessionActual.pasoTE = true;
                        SessionActual.pasoVirtud = false;

                        SessionActual.Buzon1.activo = SessionActual.Buzon2.activo = SessionActual.Buzon3.activo = false;
                        CambiarContenido(Enumerador.Pagina.SeleccionarTalentosEspecificos);
                    }
                }
            }
            else
            {
                if (!SessionActual.pasoTE && SessionActual.lstTalentos.Count.Equals(0) && ActualizarContadoresBuzon(1).Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && ActualizarContadoresBuzon(3).Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS))
                {
                    SessionActual.paso1 = false;
                    SessionActual.paso2 = false;
                    SessionActual.paso3 = false;
                    SessionActual.paso4 = false;
                    SessionActual.pasoTE = true;
                    SessionActual.pasoVirtud = false;

                    CambiarContenido(Enumerador.Pagina.SeleccionarTalentosEspecificos);

                    CambiarInstruccion(Enumerador.Instruccion.InstruccionTalentoEspecifico);
                }
                if (!SessionActual.pasoVirtud && SessionActual.lstTalentos.Count.Equals(0) && ActualizarContadoresBuzon(1).Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && ActualizarContadoresBuzon(3).Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS)
                        && ActualizarContadoresBuzon(4).Equals(Session.MAX_TALENTOS_ESPECIFICOS))
                {
                    SessionActual.paso1 = false;
                    SessionActual.paso2 = false;
                    SessionActual.paso3 = false;
                    SessionActual.paso4 = false;
                    SessionActual.pasoTE = false;
                    SessionActual.pasoVirtud = true;

                    CambiarContenido(Enumerador.Pagina.SeleccionarVirtudes);

                    CambiarInstruccion(Enumerador.Instruccion.InstruccionVirtudes);
                }
                if (!SessionActual.pasoVirtud && SessionActual.lstTalentos.Count.Equals(0) && ActualizarContadoresBuzon(1).Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && ActualizarContadoresBuzon(3).Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS)
                        && ActualizarContadoresBuzon(4).Equals(Session.MAX_TALENTOS_ESPECIFICOS) && ActualizarContadoresBuzon(Session.MAX_TALENTOS_MENOS_DESARROLLADOS).Equals(Session.MAX_VIRTUDES)) //(SessionActual.paso2)
                {
                    if (txtNavegacion.Text.Contains("Continuar"))
                    {
                        TalentosReference.ResultadoBE rpta = new Talentos_Master.TalentosReference.ResultadoBE();
                        //rpta.Juego_id = SessionActual.juego.Juego_id;
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

                        //actualizar resultado del juego
                        TalentosReference.WSTalentosSoapClient ws = new Talentos_Master.TalentosReference.WSTalentosSoapClient();
                        ws.ResultadoActualizarCompleted += new EventHandler<TalentosReference.ResultadoActualizarCompletedEventArgs>(resultUpdatePaso2_completed);
                        ws.ResultadoActualizarAsync(rpta);

                        resultUpdatePaso2_completed(null, null);
                    }
                }
                //if (SessionActual.paso3)
                //{
                //    SessionActual.paso1 = false;
                //    SessionActual.paso2 = false;
                //    SessionActual.paso3 = true;
                //    SessionActual.paso4 = false;

                //    _cambiarContenido.Invoke(Enumerador.Pagina.Sugerencias);
                //}
                if (SessionActual.paso3)
                {
                    SessionActual.paso1 = false;
                    SessionActual.paso2 = false;
                    SessionActual.paso3 = true;
                    SessionActual.paso4 = false;
                    SessionActual.pasoTE = false;
                    SessionActual.pasoVirtud = false;

                    _cambiarContenido.Invoke(Enumerador.Pagina.Sugerencias);
                }
            }
        }

        public void resultUpdate_completed(object sender, TalentosReference.ResultadoActualizarCompletedEventArgs e)
        {
            ReportesReference.wsReporteSoapClient reporteClient = new Talentos_Master.ReportesReference.wsReporteSoapClient();
            reporteClient.EnviarMailResultadoCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(reporteClient_GenerarReporteCompleted);
            reporteClient.EnviarMailResultadoAsync(SessionActual.DNI, SessionActual.CodEvaluacion);

            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = true;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrec = false;
            SessionActual.pasoTE = false;
            SessionActual.pasoVirtud = false;

            SessionActual.terminoClasificacion = true;
            SessionActual.terminoSeleccion = true;

            CambiarContenido(Enumerador.Pagina.AgradecimientoJuego);
            CambiarInstruccion(Enumerador.Instruccion.EnBlanco);
        }

        private void reporteClient_GenerarReporteCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

        }

        public void resultUpdateCorrecion_completed(object sender, TalentosReference.ResultadoActualizarCompletedEventArgs e)
        {
            if (SessionActual.Buzon1.lstTalento.Count.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && (SessionActual.Buzon3.lstTalento.Count.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS)))
            {
                for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
                    SessionActual.Buzon1.lstTalento[i].seleccionado = true;

                for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
                    SessionActual.Buzon3.lstTalento[i].seleccionado = true;

                SessionActual.cantSeleccionadosBuzon1 = Session.MAX_TALENTOS_MAS_DESARROLLADOS;
                SessionActual.cantSeleccionadosBuzon3 = Session.MAX_TALENTOS_MENOS_DESARROLLADOS;

                SessionActual.paso1 = false;
                SessionActual.paso2 = false;
                SessionActual.paso3 = true;
                SessionActual.paso4 = false;
                SessionActual.pasoTE = false;
                SessionActual.pasoVirtud = false;

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

                CambiarContenido(Enumerador.Pagina.SeleccionarPrincipal);
                CambiarInstruccion(Enumerador.Instruccion.SegundaInstruccion);
            }
        }

        public void resultUpdateToResultados_completed(object sender, TalentosReference.ResultadoActualizarCompletedEventArgs e)
        {
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = true;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrec = false;
            SessionActual.pasoTE = false;
            SessionActual.pasoVirtud = false;

            SessionActual.terminoClasificacion = true;
            SessionActual.terminoSeleccion = true;

            //CambiarContenido(Enumerador.Pagina.EnvioReporte);
            //CambiarInstruccion(Enumerador.Instruccion.InstruccionEnvioReporte);
        }

        public void resultUpdatePaso2_completed(object sender, TalentosReference.ResultadoActualizarCompletedEventArgs e)
        {
            //MoveTalentosFromBuzon1ToBuzon2();
            //MoveTalentosFromBuzon3ToBuzon2();
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = true;
            SessionActual.paso4 = false;
            SessionActual.pasoTE = false;
            SessionActual.pasoVirtud = false;

            //CambiarContenido(Enumerador.Pagina.ResultadosClasificacion);
            //CambiarInstruccion(Enumerador.Instruccion.ResultInstrucciones);
            //CambiarContenido(Enumerador.Pagina.EnvioReporte);
            //CambiarInstruccion(Enumerador.Instruccion.InstruccionEnvioReporte);
        }

        private void txtCerrarSession_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //SessionActual = Session.deleteInstance();
            //_cambiarContenido.Invoke(Enumerador.Pagina.Login);
            HtmlPage.Window.Invoke("CloseWindow");
        }

        private void brInstruccion3_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int contador1, contador3;

            contador1 = ActualizarContadoresBuzon(1);
            contador3 = ActualizarContadoresBuzon(3);

            if (SessionActual.Buzon1.lstTalento.Count.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS))
            {
                SessionActual.cantSeleccionadosBuzon1 = Session.MAX_TALENTOS_MAS_DESARROLLADOS;
                contador1 = Session.MAX_TALENTOS_MAS_DESARROLLADOS;
            }

            if (SessionActual.Buzon3.lstTalento.Count.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS))
            {
                SessionActual.cantSeleccionadosBuzon3 = Session.MAX_TALENTOS_MENOS_DESARROLLADOS;
                contador3 = Session.MAX_TALENTOS_MENOS_DESARROLLADOS;
            }

            if (SessionActual.lstTalentos.Count.Equals(0) && SessionActual.Buzon1.lstTalento.Count >= Session.MAX_TALENTOS_MAS_DESARROLLADOS && SessionActual.Buzon3.lstTalento.Count >= Session.MAX_TALENTOS_MENOS_DESARROLLADOS && contador1.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && (contador3.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS)))
            {
                SessionActual.paso1 = false;
                SessionActual.paso2 = false;
                SessionActual.paso3 = true;
                SessionActual.paso4 = false;

                _cambiarContenido(Enumerador.Pagina.Sugerencias);
            }   
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //
            HtmlPage.Window.Navigate(new Uri("http://www.davidfischman.com/index.php?lang=es"), "_blank");
        }

        private void brInstruc1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!SessionActual.paso1)
            {
                SessionActual.Buzon1.activo = true;
                SessionActual.Buzon2.activo = SessionActual.Buzon3.activo = false;
                CambiarContenido(Enumerador.Pagina.ClasificacionDetalle);
                //CambiarInstruccion(Enumerador.Instruccion.Siguiente);
            }
        }

        private void brInstruc2_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (!SessionActual.paso2)
            if (SessionActual.lstTalentos.Count.Equals(0) && SessionActual.Buzon1.lstTalento.Count >= Session.MAX_TALENTOS_MAS_DESARROLLADOS && SessionActual.Buzon3.lstTalento.Count >= Session.MAX_TALENTOS_MENOS_DESARROLLADOS)
            {
                if (!SessionActual.Buzon1.lstTalento.Count.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && (!SessionActual.Buzon3.lstTalento.Count.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS)))
                {
                    SessionActual.Buzon1.activo = true;
                    SessionActual.Buzon2.activo = false;
                    SessionActual.Buzon3.activo = false;
                }
                else
                {
                    if (SessionActual.Buzon1.lstTalento.Count.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS))
                    {
                        SessionActual.Buzon1.activo = false;
                        SessionActual.Buzon2.activo = false;
                        SessionActual.Buzon3.activo = true;
                    }
                    else
                    {
                        if (SessionActual.Buzon3.lstTalento.Count.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS))
                        {
                            SessionActual.Buzon1.activo = true;
                            SessionActual.Buzon2.activo = false;
                            SessionActual.Buzon3.activo = false;
                        }
                    }
                }

                CambiarInstruccion(Enumerador.Instruccion.instruccionCorreccion);
                CambiarContenido(Enumerador.Pagina.SeleccionarPrincipal);
            }
        }

        private void brTalentosEspecificos_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (SessionActual.lstTalentos.Count.Equals(0) && (SessionActual.BuzonTEMas.lstTalento.Count + SessionActual.BuzonTEIntermedio.lstTalento.Count + SessionActual.BuzonTEMenos.lstTalento.Count).Equals(Session.MAX_TALENTOS_ESPECIFICOS))
            {
                SessionActual.Buzon1.activo = false;
                SessionActual.Buzon2.activo = false;
                SessionActual.Buzon3.activo = false;
                SessionActual.BuzonVirtudes.activo = false;
                SessionActual.BuzonTEMas.activo = true;

                CambiarInstruccion(Enumerador.Instruccion.instruccionCorreccionTE);
                CambiarContenido(Enumerador.Pagina.ClasificacionDetalleTE);
            }
        }

        private void brVirtud_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //SessionActual.BuzonVirtudes.lstTalento.Count
            if (SessionActual.lstTalentos.Count.Equals(0) && SessionActual.cantCalificadosBuzonVirtud >= Session.MAX_VIRTUDES)
            {
                SessionActual.Buzon1.activo = false;
                SessionActual.Buzon2.activo = false;
                SessionActual.Buzon3.activo = false;
                SessionActual.BuzonVirtudes.activo = true;
                SessionActual.BuzonTEMas.activo = false;
                SessionActual.BuzonTEIntermedio.activo = false;
                SessionActual.BuzonTEMenos.activo = false;

                CambiarContenido(Enumerador.Pagina.SeleccionarPrincipal);
            }
        }

        private void brInstrcResult_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int contador1, contador3;

            contador1 = ActualizarContadoresBuzon(1);
            contador3 = ActualizarContadoresBuzon(3);

            if (SessionActual.Buzon1.lstTalento.Count.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS))
            {
                SessionActual.cantSeleccionadosBuzon1 = Session.MAX_TALENTOS_MAS_DESARROLLADOS;
                contador1 = Session.MAX_TALENTOS_MAS_DESARROLLADOS;
            }

            if (SessionActual.Buzon3.lstTalento.Count.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS))
            {
                SessionActual.cantSeleccionadosBuzon3 = Session.MAX_TALENTOS_MENOS_DESARROLLADOS;
                contador3 = Session.MAX_TALENTOS_MENOS_DESARROLLADOS;
            }

            if (SessionActual.lstTalentos.Count.Equals(0) && SessionActual.Buzon1.lstTalento.Count >= Session.MAX_TALENTOS_MAS_DESARROLLADOS && SessionActual.Buzon3.lstTalento.Count >= Session.MAX_TALENTOS_MENOS_DESARROLLADOS && contador1.Equals(Session.MAX_TALENTOS_MAS_DESARROLLADOS) && (contador3.Equals(Session.MAX_TALENTOS_MENOS_DESARROLLADOS)))
            {
                //CambiarContenido(Enumerador.Pagina.ResultadosClasificacion);
                //CambiarContenido(Enumerador.Pagina.EnvioReporte);

                SessionActual.paso3 = true;
                SessionActual.paso2 = false;
                SessionActual.paso1 = false;
                SessionActual.paso4 = false;
            }   
        }

        private int ActualizarContadoresBuzon(int idBuzon)
        {
            int contador = 0;
            if (idBuzon == 1)
            {
                //SessionActual.cantSeleccionadosBuzon1 = 0;
                for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
                {
                    if (SessionActual.Buzon1.lstTalento[i].seleccionado)
                        contador++;
                        //SessionActual.cantSeleccionadosBuzon1++;
                }
            }

            if (idBuzon == 3)
            {
                //SessionActual.cantSeleccionadosBuzon3 = 0;
                for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
                {
                    if (SessionActual.Buzon3.lstTalento[i].seleccionado)
                        // SessionActual.cantSeleccionadosBuzon3++;
                        contador++;
                }
            }

            if (idBuzon == 4) // Talentos Especificos
            {
                foreach (var item in SessionActual.BuzonTEMas.lstTalento)
                {
                    if (item.seleccionado)
                    {
                        contador++;
                    }
                }
            }

            if (idBuzon == 5) // Talentos Especificos
            {
                foreach (var item in SessionActual.BuzonTEIntermedio.lstTalento)
                {
                    if (item.seleccionado)
                    {
                        contador++;
                    }
                }
            }

            if (idBuzon == 6) // Talentos Especificos
            {
                foreach (var item in SessionActual.BuzonTEMenos.lstTalento)
                {
                    if (item.seleccionado)
                    {
                        contador++;
                    }
                }
            }

            if (idBuzon == 7) // Virtudes
            {
                foreach (var item in SessionActual.BuzonVirtudes.lstTalento)
                {
                    if (item.seleccionado)
                    {
                        contador++;
                    }
                }
            }

            return contador;
        }
    }
}
