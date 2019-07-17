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
using Visifire.Charts;
using Visifire.Commons;

using System.Collections.ObjectModel;
using Talentos_Master.TalentosReference;
using System.Windows.Media.Imaging;
using System.IO;
using FluxJpeg.Core;
using System.Windows.Browser;

/// <summary>
///Esta página muestra los resultados del usuario en gráficas estadísticas de los resultados
/// <summary>


namespace Talentos_Master
{
    //TODO: (ITERACION 2 - I ENTREGA) Incluir en la Secuencia del Juego

    //TODO: (ITERACION 2 - I ENTREGA) Desarrollar CU Guardar Resultados

    //TODO: (ITERACION 2 - I ENTREGA) Desarrollar CU Mostrar Gráficas Estadísticas de los Resultados

    //TODO: (ITERACION 2 - II ENTREGA)Corregir Bugs

    //TODO: (ITERACION 5) Sólo debe estar disponible la gráfica pie y el detalle de los talentos deben aparecer debajo de las gráficas estadísticas.


    public partial class frmResultadoEstadistico : IPaginaContenida
    {

        //BuzonGlobal session;

        //BuzonGlobal TalentosColores;

        private Session SessionActual;

        //
        private List<int> values = new List<int>(); //cantidad de talentos de cada tendencia. Esta lista es de longitud igual a 6



        private List<TalentoUC> ejecucion = new List<TalentoUC>();

        private List<TalentoUC> pensamiento = new List<TalentoUC>();

        private List<TalentoUC> innovacion = new List<TalentoUC>();

        private List<TalentoUC> liderazgo = new List<TalentoUC>();

        private List<TalentoUC> personas = new List<TalentoUC>();

        private List<TalentoUC> estructura = new List<TalentoUC>();

        private int n1, n3;

        TranslateTransform img2transform = new TranslateTransform();

        public frmResultadoEstadistico()
        {
            InitializeComponent();

            SessionActual = Session.getInstance();
            //txtfullname.Text = SessionActual.participante.NickName;

            if (SessionActual.participante.Sexo.Equals(1))
                txtfullname.Text = "Bienvenida, " + SessionActual.participante.NickName;//string.Concat("Hola ",SessionActual.participante.NickName);
            else
                txtfullname.Text = "Bienvenido, " + SessionActual.participante.NickName;//string.Concat("Hola ",SessionActual.participante.NickName);

            n1 = n3 = 0;
            LoadMasDesarrollados();
            LoadTendenciasBuzon1();

            //UpdateChart(values, "Talentos que más me identifican");
            UpdateChart(values, "Talentos más desarrollados");

            LoadTalentosListBox(getPosicionMayor(values));




            //values.Clear();
            //for (int i = 0; i < 6; i++)
            //{
            //    values.Add(0);
            //}

            //values[0] = 3;
            //values[1] = 3;
            //values[2] = 3;
            //values[3] = 3;
            //values[4] = 3;
            //values[5] = 3;



            //UpdateChart(values, "Talentos más desarrollados");

            //txtTitulo.Text = "Orientado al";
            //txtOrientacion.Text = "Logro";

            //List<TalentoUC> detalle = new List<TalentoUC>();

            //TalentoUC uc = null;
            //for (int i = 0; i < 7; i++)
            //{
            //    uc = new TalentoUC(5, "images/talentos/images/Image5.png", "aprendiz", "descripcion aprendiz");
            //    detalle.Add(uc);
            //}

            //talentosListBox.ItemsSource = detalle;



        }

        //////////////////////////////////////////////////////////////



        private void txbInicia_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //_cambiarContenido.Invoke(Enumerates.ContentPage.Inicio);
        }

        //Tooltip de cada buttonSeven
        private void ButtonSeven_MouseMove(object sender, MouseEventArgs e)
        {
            ButtonSeven button = sender as ButtonSeven;
            if (button == null) return;

            textInfo.Text = button.Title;
            textInfo2.Text = "";
            textInfo3.Text = "";
            textInfo.FontWeight = FontWeights.Bold;

        }

        public void CreateChart(int i, List<int> values, String titulo)
        {
            canv.Children.Clear();
            //Crear una nueva instancia de chart
            Chart charter = new Chart();

            
           

            #region Setear Properties al Charter

            // Setear ancho y altura al chart
            charter.Width = 700;
            charter.Height = 430;
            charter.AnimationEnabled = true;
            charter.View3D = true;
            charter.BorderThickness = new Thickness(10);
            //charter.VerticalAlignment = VerticalAlignment.Center;
            //charter.HorizontalAlignment = HorizontalAlignment.Center;

            // chart.Theme = “Theme1″;


            // Crear una nueva instancia de Título
            Title title = new Title();

            // Setear propiedades al título
            title.Text = titulo;
            title.FontSize = 30;
            title.FontFamily = new FontFamily("Arial");
            title.FontColor = new SolidColorBrush(Color.FromArgb((byte)255, (byte)47, (byte)79, (byte)79));
            title.MouseLeftButtonDown += new MouseButtonEventHandler(title_MouseLeftButtonDown);

            // Attach evento al Title
            // title.MouseLeftButtonDown += new MouseButtonEventHandler(title_MouseLeftButtonDown);

            // Agregar el título instanciado a la collección de títulos del chart
            charter.Titles.Add(title);

            // Crear Axis X
            Axis axisX = new Axis();

            // Setear propiedades al axis X
            axisX.Title = "Tendencias";
            axisX.MouseLeftButtonDown += new MouseButtonEventHandler(axis_MouseLeftButtonDown);
            axisX.TitleFontColor = new SolidColorBrush(Color.FromArgb((byte)255, (byte)47, (byte)79, (byte)79));
            axisX.TitleFontSize = 20;
            axisX.TitleFontFamily = new FontFamily("Arial");

            axisX.AxisLabels.FontColor = new SolidColorBrush(Color.FromArgb((byte)255, (byte)47, (byte)79, (byte)79));

            // Agregar el axis instanciado a la colección de AxesX 
            charter.AxesX.Add(axisX);

            // Crear axisY
            Axis axisY = new Axis();

            // Setear properties al axis Y
            axisY.Title = "Cantidad de Talentos";
            axisY.MouseLeftButtonDown += new MouseButtonEventHandler(axis_MouseLeftButtonDown);
            axisY.TitleFontColor = new SolidColorBrush(Color.FromArgb((byte)255, (byte)47, (byte)79, (byte)79));
            axisY.TitleFontSize = 20;
            axisY.TitleFontFamily = new FontFamily("Arial");
            axisY.AxisLabels.FontColor = new SolidColorBrush(Color.FromArgb((byte)255, (byte)47, (byte)79, (byte)79));

            // Agregar axis a la colección de AxesY
            charter.AxesY.Add(axisY);


            // Crear una nueva instancia de DataSeries
            DataSeries dataSeries = new DataSeries();

            // Setear properties al DataSeries
            switch (i)
            {

                case 1: dataSeries.RenderAs = RenderAs.Column; break;
                case 2: dataSeries.RenderAs = RenderAs.Bubble; break;
                case 3: dataSeries.RenderAs = RenderAs.Pie; break;
                case 4: dataSeries.RenderAs = RenderAs.Doughnut; break;
                case 5: dataSeries.RenderAs = RenderAs.StackedBar; break;
                case 6: dataSeries.RenderAs = RenderAs.Line;
                    dataSeries.Color = new SolidColorBrush(Color.FromArgb((byte)255, (byte)47, (byte)79, (byte)79)); break;

            }

            dataSeries.Bevel = true;

            dataSeries.Height = 400;
            dataSeries.Width = 450;

            #region Crear Datapoints para cada tendencia o color y agregarlos a DataSeries

            if (values[0] != 0)
            {
                // Crear un DataPoint
                DataPoint dataPoint1;

                dataPoint1 = new DataPoint();
                dataPoint1.AxisXLabel = "Orientado a la ejecución";
                dataPoint1.YValue = values[0];

                // setear un color gradient
                LinearGradientBrush lgb = new LinearGradientBrush();
                GradientStopCollection lista = new GradientStopCollection();
                GradientStop stop = new GradientStop();
                stop.Color = Color.FromArgb((byte)255, (byte)255, (byte)69, (byte)0);
                stop.Offset = 0.5;
                lgb.GradientStops.Add(stop);

                GradientStop stop3 = new GradientStop();
                stop3.Color = Color.FromArgb((byte)255, (byte)255, (byte)165, (byte)0);
                stop3.Offset = 0.9;
                lgb.GradientStops.Add(stop3);
                GradientStop stop4 = new GradientStop();
                stop4.Color = Color.FromArgb((byte)255, (byte)255, (byte)140, (byte)0);
                stop4.Offset = 1.0;
                lgb.GradientStops.Add(stop4);
                dataPoint1.Color = lgb;
                dataPoint1.Width = 50;


                dataPoint1.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);


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
                stopAzul1.Color = Color.FromArgb((byte)255, (byte)0, (byte)0, (byte)255);
                stopAzul1.Offset = 0;
                Azul.GradientStops.Add(stopAzul1);
                GradientStop stopAzul2 = new GradientStop();
                stopAzul2.Color = Color.FromArgb((byte)255, (byte)0, (byte)0, (byte)205);
                stopAzul2.Offset = 0.4;
                Azul.GradientStops.Add(stopAzul2);
                GradientStop stopAzul3 = new GradientStop();
                stopAzul3.Color = Color.FromArgb((byte)255, (byte)65, (byte)105, (byte)225);
                stopAzul3.Offset = 0.8;
                Azul.GradientStops.Add(stopAzul3);
                GradientStop stopAzul4 = new GradientStop();
                stopAzul4.Color = Color.FromArgb((byte)255, (byte)100, (byte)149, (byte)237);
                stopAzul4.Offset = 1.0;
                Azul.GradientStops.Add(stopAzul4);
                dataPoint2.Color = Azul;
                dataPoint2.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);
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
                stopAmarillo2.Color = Color.FromArgb((byte)255, (byte)255, (byte)255, (byte)0);
                stopAmarillo2.Offset = 0.7;
                Amarillo.GradientStops.Add(stopAmarillo2);
                GradientStop stopAmarillo3 = new GradientStop();
                stopAmarillo3.Color = Color.FromArgb((byte)255, (byte)255, (byte)250, (byte)205);
                stopAmarillo3.Offset = 0.9;
                Amarillo.GradientStops.Add(stopAmarillo3);
                GradientStop stopAmarillo4 = new GradientStop();
                stopAmarillo4.Color = Color.FromArgb((byte)255, (byte)250, (byte)250, (byte)210);
                stopAmarillo4.Offset = 1.0;
                Amarillo.GradientStops.Add(stopAmarillo4);
                dataPoint3.Color = Amarillo;
                dataPoint3.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);

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
                stopGuinda1.Color = Color.FromArgb((byte)255, (byte)139, (byte)0, (byte)0);
                stopGuinda1.Offset = 0;
                Guinda.GradientStops.Add(stopGuinda1);
                GradientStop stopGuinda2 = new GradientStop();
                stopGuinda2.Color = Color.FromArgb((byte)255, (byte)128, (byte)0, (byte)0);
                stopGuinda2.Offset = 0.4;
                Guinda.GradientStops.Add(stopGuinda2);
                GradientStop stopGuinda3 = new GradientStop();
                stopGuinda3.Color = Color.FromArgb((byte)255, (byte)178, (byte)34, (byte)34);
                stopGuinda3.Offset = 0.8;
                Guinda.GradientStops.Add(stopGuinda3);
                GradientStop stopGuinda4 = new GradientStop();
                stopGuinda4.Color = Color.FromArgb((byte)255, (byte)165, (byte)42, (byte)42);
                stopGuinda4.Offset = 1.0;
                Guinda.GradientStops.Add(stopGuinda4);
                dataPoint4.Color = Guinda;
                dataPoint4.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);
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
                stopRojo1.Color = Color.FromArgb((byte)255, (byte)255, (byte)0, (byte)0);
                stopRojo1.Offset = 0.7;
                Rojo.GradientStops.Add(stopRojo1);
                GradientStop stopRojo4 = new GradientStop();
                stopRojo4.Color = Color.FromArgb((byte)255, (byte)240, (byte)128, (byte)128);
                stopRojo4.Offset = 0.9;
                Rojo.GradientStops.Add(stopRojo4);

                dataPoint5.Color = Rojo;
                dataPoint5.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);
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
                stopVerde1.Color = Color.FromArgb((byte)255, (byte)0, (byte)100, (byte)0);
                stopVerde1.Offset = 0;
                Verde.GradientStops.Add(stopVerde1);
                GradientStop stopVerde2 = new GradientStop();
                stopVerde2.Color = Color.FromArgb((byte)255, (byte)0, (byte)128, (byte)0);
                stopVerde2.Offset = 0.4;
                Verde.GradientStops.Add(stopVerde2);
                GradientStop stopVerde3 = new GradientStop();
                stopVerde3.Color = Color.FromArgb((byte)255, (byte)34, (byte)139, (byte)34);
                stopVerde3.Offset = 0.6;
                Verde.GradientStops.Add(stopVerde3);
                GradientStop stopVerde4 = new GradientStop();
                stopVerde4.Color = Color.FromArgb((byte)255, (byte)50, (byte)205, (byte)50);
                stopVerde4.Offset = 0.8;
                Verde.GradientStops.Add(stopVerde4);
                dataPoint6.Color = Verde;
                dataPoint6.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);
                dataSeries.DataPoints.Add(dataPoint6);
            }
            #endregion

            // Agregar dataSeries a la colección de Series
            charter.Series.Add(dataSeries);

            #endregion

            // Agregar el chart a LayoutRoot
            //LayoutRoot.Children.Add(charter);
            canv.Children.Add(charter);

        }

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

            title.MouseLeftButtonDown += new MouseButtonEventHandler(title_MouseLeftButtonDown);

            //txtTitleChart.Text = titulo;
            //txtTitleChart.MouseLeftButtonDown += new MouseButtonEventHandler(title_MouseLeftButtonDown);
            // Attach evento al Title
            // title.MouseLeftButtonDown += new MouseButtonEventHandler(title_MouseLeftButtonDown);

            // Agregar el título instanciado a la collección de títulos del chart
            MyChart.Titles.Clear();
            MyChart.Titles.Add(title);

            // Crear Axis X
            Axis axisX = new Axis();

            // Setear propiedades al axis X
            axisX.Title = "Tendencias";
            axisX.MouseLeftButtonDown += new MouseButtonEventHandler(axis_MouseLeftButtonDown);
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
            axisY.MouseLeftButtonDown += new MouseButtonEventHandler(axis_MouseLeftButtonDown);
            axisY.TitleFontColor = new SolidColorBrush(Color.FromArgb((byte)255, (byte)47, (byte)79, (byte)79));
            axisY.TitleFontSize = 25;
            axisY.TitleFontFamily = new FontFamily("Arial");
            axisY.AxisLabels.FontColor = new SolidColorBrush(Color.FromArgb((byte)255, (byte)232, (byte)137, (byte)25));

            // Agregar axis a la colección de AxesY
            MyChart.AxesY.Clear();
            MyChart.AxesY.Add(axisY);


            #region Crear Datapoints para cada tendencia o color y agregarlos a DataSeries

            //MyChart.Series.Clear();

            //DataSeries dataSeries = new DataSeries();

            //if (values[0] != 0)
            //{
            //    // Crear un DataPoint
            //    DataPoint dataPoint1;

            //    dataPoint1 = new DataPoint();
            //    dataPoint1.AxisXLabel = "Orientado a la ejecución";
            //    dataPoint1.YValue = values[0];

            //    //// setear un color gradient
            //    //LinearGradientBrush lgb = new LinearGradientBrush();
            //    //GradientStopCollection lista = new GradientStopCollection();
            //    //GradientStop stop = new GradientStop();
            //    //stop.Color = Color.FromArgb((byte)255, (byte)255, (byte)69, (byte)0);
            //    //stop.Offset = 0.5;
            //    //lgb.GradientStops.Add(stop);

            //    //GradientStop stop3 = new GradientStop();
            //    //stop3.Color = Color.FromArgb((byte)255, (byte)255, (byte)165, (byte)0);
            //    //stop3.Offset = 0.9;
            //    //lgb.GradientStops.Add(stop3);
            //    //GradientStop stop4 = new GradientStop();
            //    //stop4.Color = Color.FromArgb((byte)255, (byte)255, (byte)140, (byte)0);
            //    //stop4.Offset = 1.0;
            //    //lgb.GradientStops.Add(stop4);
            //    //dataPoint1.Color = lgb;
            //    //dataPoint1.Width = 50;


            //    LinearGradientBrush lgb = new LinearGradientBrush();
            //    GradientStopCollection lista = new GradientStopCollection();
            //    GradientStop stop = new GradientStop();
            //    stop.Color = Color.FromArgb((byte)255, (byte)255, (byte)102, (byte)0);
            //    stop.Offset = 1;
            //    lgb.GradientStops.Add(stop);
            //    dataPoint1.Color = lgb;
            //    dataPoint1.Width = 50;

            //    dataPoint1.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);


            //    dataSeries.DataPoints.Add(dataPoint1);

            //}
            //if (values[1] != 0)
            //{
            //    DataPoint dataPoint2;

            //    dataPoint2 = new DataPoint();
            //    dataPoint2.AxisXLabel = "Orientado al pensamiento";
            //    dataPoint2.YValue = values[1];
            //    // set un color gradiente
            //    LinearGradientBrush Azul = new LinearGradientBrush();
            //    GradientStop stopAzul1 = new GradientStop();
            //    stopAzul1.Color = Color.FromArgb((byte)255, (byte)0, (byte)102, (byte)255);
            //    //stopAzul1.Offset = 0;
            //    stopAzul1.Offset = 1;
            //    Azul.GradientStops.Add(stopAzul1);
            //    //GradientStop stopAzul2 = new GradientStop();
            //    //stopAzul2.Color = Color.FromArgb((byte)255, (byte)0, (byte)0, (byte)205);
            //    //stopAzul2.Offset = 0.4;
            //    //Azul.GradientStops.Add(stopAzul2);
            //    //GradientStop stopAzul3 = new GradientStop();
            //    //stopAzul3.Color = Color.FromArgb((byte)255, (byte)65, (byte)105, (byte)225);
            //    //stopAzul3.Offset = 0.8;
            //    //Azul.GradientStops.Add(stopAzul3);
            //    //GradientStop stopAzul4 = new GradientStop();
            //    //stopAzul4.Color = Color.FromArgb((byte)255, (byte)100, (byte)149, (byte)237);
            //    //stopAzul4.Offset = 1.0;
            //    //Azul.GradientStops.Add(stopAzul4);
            //    dataPoint2.Color = Azul;
            //    dataPoint2.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);
            //    dataSeries.DataPoints.Add(dataPoint2);
            //}

            //if (values[2] != 0)
            //{
            //    DataPoint dataPoint3;

            //    dataPoint3 = new DataPoint();
            //    dataPoint3.AxisXLabel = "Orientado a la innovación";
            //    dataPoint3.YValue = values[2];
            //    // setear un color degradado
            //    LinearGradientBrush Amarillo = new LinearGradientBrush();
            //    GradientStop stopAmarillo2 = new GradientStop();
            //    stopAmarillo2.Color = Color.FromArgb((byte)255, (byte)255, (byte)204, (byte)0);
            //    stopAmarillo2.Offset = 0.7;
            //    Amarillo.GradientStops.Add(stopAmarillo2);
            //    //GradientStop stopAmarillo3 = new GradientStop();
            //    //stopAmarillo3.Color = Color.FromArgb((byte)255, (byte)255, (byte)250, (byte)205);
            //    //stopAmarillo3.Offset = 0.9;
            //    //Amarillo.GradientStops.Add(stopAmarillo3);
            //    //GradientStop stopAmarillo4 = new GradientStop();
            //    //stopAmarillo4.Color = Color.FromArgb((byte)255, (byte)250, (byte)250, (byte)210);
            //    //stopAmarillo4.Offset = 1.0;
            //    //Amarillo.GradientStops.Add(stopAmarillo4);
            //    dataPoint3.Color = Amarillo;
            //    dataPoint3.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);

            //    dataSeries.DataPoints.Add(dataPoint3);
            //}

            //if (values[3] != 0)
            //{
            //    DataPoint dataPoint4;

            //    dataPoint4 = new DataPoint();
            //    dataPoint4.AxisXLabel = "Orientado al liderazgo";
            //    dataPoint4.YValue = values[3];
            //    //setear un color degradado
            //    LinearGradientBrush Guinda = new LinearGradientBrush();
            //    GradientStop stopGuinda1 = new GradientStop();
            //    stopGuinda1.Color = Color.FromArgb((byte)255, (byte)125, (byte)0, (byte)54);
            //    stopGuinda1.Offset = 0;
            //    Guinda.GradientStops.Add(stopGuinda1);
            //    //GradientStop stopGuinda2 = new GradientStop();
            //    //stopGuinda2.Color = Color.FromArgb((byte)255, (byte)128, (byte)0, (byte)0);
            //    //stopGuinda2.Offset = 0.4;
            //    //Guinda.GradientStops.Add(stopGuinda2);
            //    //GradientStop stopGuinda3 = new GradientStop();
            //    //stopGuinda3.Color = Color.FromArgb((byte)255, (byte)178, (byte)34, (byte)34);
            //    //stopGuinda3.Offset = 0.8;
            //    //Guinda.GradientStops.Add(stopGuinda3);
            //    //GradientStop stopGuinda4 = new GradientStop();
            //    //stopGuinda4.Color = Color.FromArgb((byte)255, (byte)165, (byte)42, (byte)42);
            //    //stopGuinda4.Offset = 1.0;
            //    //Guinda.GradientStops.Add(stopGuinda4);
            //    dataPoint4.Color = Guinda;
            //    dataPoint4.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);
            //    dataSeries.DataPoints.Add(dataPoint4);
            //}

            //if (values[4] != 0)
            //{
            //    DataPoint dataPoint5;

            //    dataPoint5 = new DataPoint();
            //    dataPoint5.AxisXLabel = "Orientado a las personas";
            //    dataPoint5.YValue = values[4];

            //    //setear un color degradado
            //    LinearGradientBrush Rojo = new LinearGradientBrush();
            //    GradientStop stopRojo1 = new GradientStop();
            //    stopRojo1.Color = Color.FromArgb((byte)255, (byte)204, (byte)0, (byte)0);
            //    stopRojo1.Offset = 1;
            //    Rojo.GradientStops.Add(stopRojo1);
            //    //GradientStop stopRojo4 = new GradientStop();
            //    //stopRojo4.Color = Color.FromArgb((byte)255, (byte)240, (byte)128, (byte)128);
            //    //stopRojo4.Offset = 0.9;
            //    //Rojo.GradientStops.Add(stopRojo4);

            //    dataPoint5.Color = Rojo;
            //    dataPoint5.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);
            //    dataSeries.DataPoints.Add(dataPoint5);
            //}

            //if (values[5] != 0)
            //{
            //    DataPoint dataPoint6;

            //    dataPoint6 = new DataPoint();
            //    dataPoint6.AxisXLabel = "Orientado a la estructura";
            //    dataPoint6.YValue = values[5];

            //    //setear un color degradado
            //    LinearGradientBrush Verde = new LinearGradientBrush();
            //    GradientStop stopVerde1 = new GradientStop();
            //    stopVerde1.Color = Color.FromArgb((byte)255, (byte)0, (byte)128, (byte)0);
            //    stopVerde1.Offset = 0;
            //    Verde.GradientStops.Add(stopVerde1);
            //    //GradientStop stopVerde2 = new GradientStop();
            //    //stopVerde2.Color = Color.FromArgb((byte)255, (byte)0, (byte)128, (byte)0);
            //    //stopVerde2.Offset = 0.4;
            //    //Verde.GradientStops.Add(stopVerde2);
            //    //GradientStop stopVerde3 = new GradientStop();
            //    //stopVerde3.Color = Color.FromArgb((byte)255, (byte)34, (byte)139, (byte)34);
            //    //stopVerde3.Offset = 0.6;
            //    //Verde.GradientStops.Add(stopVerde3);
            //    //GradientStop stopVerde4 = new GradientStop();
            //    //stopVerde4.Color = Color.FromArgb((byte)255, (byte)50, (byte)205, (byte)50);
            //    //stopVerde4.Offset = 0.8;
            //    //Verde.GradientStops.Add(stopVerde4);
            //    dataPoint6.Color = Verde;
            //    dataPoint6.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);
            //    dataSeries.DataPoints.Add(dataPoint6);
            //}
            #endregion


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

                dataPoint1.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);


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
                dataPoint2.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);
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
                dataPoint3.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);

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
                dataPoint4.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);
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
                dataPoint5.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);
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
                dataPoint6.MouseLeftButtonDown += new MouseButtonEventHandler(DataPoint_MouseEnter);
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

        

        //efecto para el título
        void title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if ((sender as Title).FontSize == 28)
                (sender as Title).FontSize = 26;
            else
                (sender as Title).FontSize = 28;
        }

        //Efecto para el axis
        void axis_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if ((sender as Axis).TitleFontSize == 20)
                (sender as Axis).TitleFontSize = 25;
            else
                (sender as Axis).TitleFontSize = 20;
        }

        //Efecto para el datapoint: mostrar el detalle de talentos por tendencias
        void DataPoint_MouseEnter(object sender, MouseButtonEventArgs e)
        {
            Zoom.Visibility = Visibility.Collapsed;
            if ((sender as DataPoint).Opacity == 0.699999988079071)
                (sender as DataPoint).Opacity = 1;
            else
                (sender as DataPoint).Opacity = 0.699999988079071;

            //TalentosTendencia.IsOpen = false;
            //TalentosTendencia.IsOpen = true;
            //switch ((sender as DataPoint).Name.ToString().Substring(0, 10))
            switch ((sender as DataPoint).LegendText.ToString())
            {
                case "Orientado a la ejecución":
                    //Aqui.Children.Clear();
                    txtTitulo.Text = "Orientado a la ";
                    txtOrientacion.Text = "ejecución";
                    imgTalento.ImageSource = new BitmapImage(new Uri("images/Orientaciones/ejecucion.png", UriKind.Relative));
                    talentosListBox.ItemsSource = ejecucion;
                    
                    break;
                case "Orientado al pensamiento":
                    //Aqui.Children.Clear();
                    txtTitulo.Text = "Orientado al ";
                    txtOrientacion.Text = "pensamiento";
                    imgTalento.ImageSource = new BitmapImage(new Uri("images/Orientaciones/pensamiento.png", UriKind.Relative));
                    talentosListBox.ItemsSource = pensamiento;
                    break;
                case "Orientado a la innovación": //"DataPoint2":
                    //Aqui.Children.Clear();
                    txtTitulo.Text = "Orientado a la ";
                    txtOrientacion.Text = "innovación";
                    imgTalento.ImageSource = new BitmapImage(new Uri("images/Orientaciones/innovacion.png", UriKind.Relative));
                    talentosListBox.ItemsSource = innovacion; 
                    break;
                case "Orientado al liderazgo":
                    //Aqui.Children.Clear();
                    txtTitulo.Text = "Orientado al ";
                    txtOrientacion.Text = "liderazgo";
                    imgTalento.ImageSource = new BitmapImage(new Uri("images/Orientaciones/liderazgo.png", UriKind.Relative));
                    talentosListBox.ItemsSource = liderazgo;
                    break;
                case "Orientado a las personas":
                    //Aqui.Children.Clear();
                    txtTitulo.Text = "Orientado a las ";
                    txtOrientacion.Text = "personas";
                    imgTalento.ImageSource = new BitmapImage(new Uri("images/Orientaciones/personas.png", UriKind.Relative));
                    talentosListBox.ItemsSource = personas;
                    break;
                case "Orientado a la estructura":
                    //Aqui.Children.Clear();
                    txtTitulo.Text = "Orientado a la ";
                    txtOrientacion.Text = "estructura";
                    imgTalento.ImageSource = new BitmapImage(new Uri("images/Orientaciones/estructura.png", UriKind.Relative));
                    talentosListBox.ItemsSource = estructura;
                    break;
            }

        }

        //Efecto opacity sobre cada datapoint
        void DataPoint_MouseLeave(object sender, MouseButtonEventArgs e)
        {
            (sender as DataPoint).Opacity = 1;
        }


  

        private void LoadTendenciasBuzon1()
        {

            ejecucion.Clear();
            pensamiento.Clear();
            innovacion.Clear();
            liderazgo.Clear();
            personas.Clear();
            estructura.Clear();

            TalentoUC talentito = null;

            for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
            {
                talentito = null;
                if (SessionActual.Buzon1.lstTalento[i].seleccionado)
                {
                    talentito = new TalentoUC(SessionActual.Buzon1.lstTalento[i].IdTalento, SessionActual.Buzon1.lstTalento[i].Image, SessionActual.Buzon1.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].Descripcion);
                    switch (SessionActual.Buzon1.lstTalento[i].IdColor)
                    {
                        case 1:

                            //talentito = new TalentitoMapaUC("OrangeRed", "tttt");
                            ejecucion.Add(talentito); break;
                        case 2:
                            //talentito = new TalentoUC("Blue", SessionActual.Buzon1.lstTalento[i].Nombre);//"tttt");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            pensamiento.Add(talentito); break;
                        case 3:
                            //talentito = new TalentoUC("Yellow", SessionActual.Buzon1.lstTalento[i].Nombre); //"Con Sentido del Humor");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            innovacion.Add(talentito); break;
                        case 4:
                            //talentito = new TalentoUC("DarkRed", SessionActual.Buzon1.lstTalento[i].Nombre);//"Con Sentido del Humor");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            liderazgo.Add(talentito); break;
                        case 5:
                            //talentito = new TalentoUC("Red", SessionActual.Buzon1.lstTalento[i].Nombre); //"tttt");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            personas.Add(talentito); break;
                        case 6:
                            //talentito = new TalentoUC("#FF065F1B", SessionActual.Buzon1.lstTalento[i].Nombre); //"tttt");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            estructura.Add(talentito); break;
                    }
                }

            }

    
        }

        private void LoadTendenciasBuzon3()
        {
 
            ejecucion.Clear();
            pensamiento.Clear();
            innovacion.Clear();
            liderazgo.Clear();
            personas.Clear();
            estructura.Clear();

            TalentoUC talentito = null;

            for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
            {
                talentito = null;
                if (SessionActual.Buzon3.lstTalento[i].seleccionado)
                {
                    talentito = new TalentoUC(SessionActual.Buzon3.lstTalento[i].IdTalento, SessionActual.Buzon3.lstTalento[i].Image, SessionActual.Buzon3.lstTalento[i].Nombre, SessionActual.Buzon1.lstTalento[i].Descripcion);
                    switch (SessionActual.Buzon3.lstTalento[i].IdColor)
                    {
                        case 1:
                            //talentito = new TalentoUC("OrangeRed", SessionActual.Buzon3.lstTalento[i].Nombre);
                            ejecucion.Add(talentito); break;
                        case 2:
                            //talentito = new TalentoUC("Blue", SessionActual.Buzon3.lstTalento[i].Nombre);//"tttt");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            pensamiento.Add(talentito); break;
                        case 3:
                            //talentito = new TalentoUC("Yellow", SessionActual.Buzon3.lstTalento[i].Nombre); //"Con Sentido del Humor");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            innovacion.Add(talentito); break;
                        case 4:
                            //talentito = new TalentoUC("DarkRed", SessionActual.Buzon3.lstTalento[i].Nombre);//"Con Sentido del Humor");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            liderazgo.Add(talentito); break;
                        case 5:
                            //talentito = new TalentoUC("Red", SessionActual.Buzon3.lstTalento[i].Nombre); //"tttt");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            personas.Add(talentito); break;
                        case 6:
                            //talentito = new TalentoUC("#FF065F1B", SessionActual.Buzon3.lstTalento[i].Nombre); //"tttt");// SessionActual.Buzon1.lstTalento[i].Descripcion);
                            estructura.Add(talentito); break;
                    }
                }

            }

        }


        ////Mueve los talentos no seleccionados del buzon me identifica al buzon no estoy seguro
        //private void MoveTalentosFromBuzon1ToBuzon2()
        //{
        //    ObservableCollection<TalentoBE> lstTalento = new ObservableCollection<TalentoBE>();

        //    for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
        //    //for (int i = 0; i < buzonColores.buzon1.lstTalento.Count; i++)
        //    {
        //        //if (!buzonColores.buzon1.lstTalento[i].seleccionado)
        //        if (!SessionActual.Buzon1.lstTalento[i].seleccionado)
        //        {
        //            //buzonColores.buzon2.lstTalento.Add(buzonColores.buzon1.lstTalento[i]);
        //            //lstTalento.Add(buzonColores.buzon1.lstTalento[i]);
        //            SessionActual.Buzon2.lstTalento.Add(SessionActual.Buzon1.lstTalento[i]);
        //            lstTalento.Add(SessionActual.Buzon1.lstTalento[i]);

        //        }
        //    }

        //    //for (int i = 0; i < lstTalento.Count - 1; i++)
        //    //    //buzonColores.buzon2.lstTalento.Add(lstTalento[i]);
        //    //    SessionActual.Buzon2.lstTalento.Add(lstTalento[i]);

        //    for (int i = 0; i < lstTalento.Count; i++)
        //    {
        //        // buzonColores.buzon1.lstTalento.Remove(lstTalento[i]);
        //        SessionActual.Buzon1.lstTalento.Remove(lstTalento[i]);
        //    }
        //}

        //private void MoveTalentosFromBuzon3ToBuzon2()
        //{
        //    ObservableCollection<TalentoBE> lstTalento = new ObservableCollection<TalentoBE>();

        //    for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
        //    {

        //        if (!SessionActual.Buzon3.lstTalento[i].seleccionado)
        //        {
        //            SessionActual.Buzon2.lstTalento.Add(SessionActual.Buzon3.lstTalento[i]);
        //            lstTalento.Add(SessionActual.Buzon3.lstTalento[i]);

        //        }
        //    }


        //    for (int i = 0; i < lstTalento.Count; i++)
        //    {
        //        SessionActual.Buzon3.lstTalento.Remove(lstTalento[i]);
        //    }
        //}



        private void ButtonSeven_MouseLeftButtonDown_6(object sender, MouseButtonEventArgs e)
        {
          

            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = false;
            SessionActual.paso4 = true;

           _cambiarContenido.Invoke(Enumerador.Pagina.Sugerencias);
            
        }

        //Ir a la sgte etapa
        private void SiguienteEtapa(object sender, RoutedEventArgs e)
        {
            SessionActual.paso1 = false;
            SessionActual.paso2 = false;
            SessionActual.paso3 = false;
            SessionActual.paso4 = true;
            _cambiarContenido.Invoke(Enumerador.Pagina.Sugerencias);
        }

        private void ButtonSeven_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Zoom.Visibility = Visibility.Collapsed;
            LoadMasDesarrollados();
            LoadTendenciasBuzon1();
            //CreateChart(3, values, "Talentos Más Desarrollados");
            UpdateChart(values, "Talentos más desarrollados");//"Talentos que más me identifican");

            LoadTalentosListBox(getPosicionMayor(values));
            //TalentosTendencia.IsOpen = true;
        }

        private void ButtonSeven_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            Zoom.Visibility = Visibility.Collapsed;
            LoadMenosDesarrollados();
            LoadTendenciasBuzon3();
            //CreateChart(3, values, "Talentos Menos Desarrollados");
            UpdateChart( values,"Talentos menos desarrollados" );//"Talentos que menos me identifican");

            LoadTalentosListBox(getPosicionMayor(values));
            //TalentosTendencia.IsOpen = true;
        }

        void LoadMasDesarrollados()
        {
            values.Clear();
            for (int i = 0; i < 6; i++)
            {
                values.Add(0);
            }

           for (int i = 0; i < SessionActual.Buzon1.lstTalento.Count; i++)
            {
                if (SessionActual.Buzon1.lstTalento[i].seleccionado)
                {
                    switch (SessionActual.Buzon1.lstTalento[i].IdTendencia)
                    {
                        case 1: values[0] += 1; break;
                        case 2: values[1] += 1; break;
                        case 3: values[2] += 1; break;
                        case 4: values[3] += 1; break;
                        case 5: values[4] += 1; break;
                        case 6: values[5] += 1; break;

                    }
                }

            }
            
        }

        void LoadMenosDesarrollados()
        {
            values.Clear();
            for (int i = 0; i < 6; i++)
            {
                values.Add(0);
            }

            for (int i = 0; i < SessionActual.Buzon3.lstTalento.Count; i++)
            {
                if (SessionActual.Buzon3.lstTalento[i].seleccionado)
                {
                    switch (SessionActual.Buzon3.lstTalento[i].IdTendencia)
                    {
                        case 1: values[0] += 1; break;
                        case 2: values[1] += 1; break;
                        case 3: values[2] += 1; break;
                        case 4: values[3] += 1; break;
                        case 5: values[4] += 1; break;
                        case 6: values[5] += 1; break;

                    }
                }

            }
        }

        int getPosicionMayor(List<int> lista)
        {
            int posMayor = 0;
            int mayor = lista[0];
            for (int i = 1; i < lista.Count; i++)
            {
                if (lista[i] > mayor)
                {
                    posMayor = i;
                    mayor = lista[i];
                }
            }
            return posMayor;

        }

        void LoadTalentosListBox(int idTendencia)
        {
            switch (idTendencia)
            {
                case 0: talentosListBox.ItemsSource = ejecucion; txtTitulo.Text = "Orientados a la "; txtOrientacion.Text = "ejecución"; imgTalento.ImageSource = new BitmapImage(new Uri("images/Orientaciones/ejecucion.png", UriKind.Relative)); ; break;
                case 1: talentosListBox.ItemsSource = pensamiento; txtTitulo.Text = "Orientados al "; txtOrientacion.Text = "pensamiento"; imgTalento.ImageSource = new BitmapImage(new Uri("images/Orientaciones/pensamiento.png", UriKind.Relative)); ; break;
                case 2: talentosListBox.ItemsSource = innovacion; txtTitulo.Text = "Orientados a la "; txtOrientacion.Text = "innovación"; imgTalento.ImageSource = new BitmapImage(new Uri("images/Orientaciones/innovacion.png", UriKind.Relative)); break;
                case 3: talentosListBox.ItemsSource = liderazgo; txtTitulo.Text = "Orientados al "; txtOrientacion.Text = "liderazgo"; imgTalento.ImageSource = new BitmapImage(new Uri("images/Orientaciones/liderazgo.png", UriKind.Relative)); break;
                case 4: talentosListBox.ItemsSource = personas; txtTitulo.Text = "Orientados a las "; txtOrientacion.Text = "personas"; imgTalento.ImageSource = new BitmapImage(new Uri("images/Orientaciones/personas.png", UriKind.Relative)); break;
                case 5: talentosListBox.ItemsSource = estructura; txtTitulo.Text = "Orientados a la "; txtOrientacion.Text="estructura" ; imgTalento.ImageSource = new BitmapImage(new Uri("images/Orientaciones/estructura.png", UriKind.Relative)); break;

            }
            
        }

        private void sgtDerecha_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _cambiarContenido.Invoke(Enumerador.Pagina.SegundaEtapa);
        }

        private void sgtDerecha_MouseEnter(object sender, MouseEventArgs e)
        {
            _cambiarContenido.Invoke(Enumerador.Pagina.SegundaEtapa);
        }

        private void ButtonSeven_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            Zoom.Visibility = Visibility.Collapsed;
            SaveToImage(MyChart);

         
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

        private void ButtonSeven_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            SessionActual.paso1 = false;
            SessionActual.paso2 = true;
            SessionActual.paso3 = false;
            SessionActual.paso4 = false;
            SessionActual.pasoCorrec = false;
            //_cambiarInstruccion.Invoke(Enumerador.Instruccion.Siguiente);
            
            _cambiarContenido.Invoke(Enumerador.Pagina.ResultadosClasificacion);
        }

        private void ButtonSeven_MouseLeave(object sender, MouseEventArgs e)
        {
            textInfo.Text = "Haz clic sobre cada porción del ";
            textInfo2.Text="pie chart ";
            textInfo3.Text="para conocer la orientación de tus talentos.";
            textInfo.FontWeight = FontWeights.Normal;
            textInfo2.FontWeight = FontWeights.Normal;
            textInfo3.FontWeight = FontWeights.Normal;
        }

        private void txtCerrarSession_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SessionActual = Session.deleteInstance();
            _cambiarContenido.Invoke(Enumerador.Pagina.Login);
        }

        private void brInstruccion3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _cambiarContenido.Invoke(Enumerador.Pagina.Sugerencias);
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HtmlPage.Window.Navigate(new Uri("http://www.davidfischman.com/index.php?lang=es"), "_blank");
        }

        private void brInstruc1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!SessionActual.paso1)
            {

                SessionActual.Buzon1.activo = true;
                SessionActual.Buzon2.activo = SessionActual.Buzon3.activo = false;
                _cambiarContenido.Invoke(Enumerador.Pagina.ClasificacionDetalle);
            }
        }

        private void brInstruc2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SessionActual.lstTalentos.Count.Equals(0) && SessionActual.Buzon1.lstTalento.Count >= 10 && SessionActual.Buzon3.lstTalento.Count >= 5)
                _cambiarContenido(Enumerador.Pagina.SeleccionarPrincipal);
        }

        private void brInstrcResult_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (SessionActual.Buzon1.lstTalento.Count >= 10 && SessionActual.Buzon3.lstTalento.Count >= 5 && SessionActual.cantSeleccionadosBuzon1.Equals(10) && (SessionActual.cantSeleccionadosBuzon3.Equals(5)))
                _cambiarContenido.Invoke(Enumerador.Pagina.ResultadosClasificacion);
        }

        private void ucTalentoEstadistico_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ucTalentoEstadistico uc = (ucTalentoEstadistico)(sender as ucTalentoEstadistico);
            string url = uc.img.Tag.ToString();



            Point aqui = e.GetPosition(this);
            TalentoZoom.imgTalento.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            TalentoZoom.imgTalento.Tag = url;



            img2transform = new TranslateTransform();


            img2transform.X = aqui.X - 300 ;

            img2transform.Y = aqui.Y-350;

            if (aqui.X > 1020)
                img2transform.X = aqui.X - 465;

            //if (aqui.Y > 167 && aqui.Y <= 360)//291)
            //    img2transform.Y = aqui.Y - 175;

            if (aqui.Y > 787)
                img2transform.Y = aqui.Y - 400;

            Zoom.RenderTransform = img2transform;

            Zoom.Visibility = Visibility.Visible;
        }

        private void btnCerrarZoom_Click(object sender, RoutedEventArgs e)
        {
            Zoom.Visibility = Visibility.Collapsed;
        }
    }
}





