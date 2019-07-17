using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Visifire.Charts;
using Visifire.Commons;
using System.Collections.Generic;
using FluxJpeg.Core;
using System.IO;
using System.Windows.Media.Imaging;

namespace Talentos_Master
{
	public partial class frmRadarxaml : UserControl
	{

        private List<int> ptjes;

        public List<int> Ptjes
        {
            get { return ptjes; }
            set { ptjes = value; }
        }

		public frmRadarxaml()
		{
			// Required to initialize variables
			InitializeComponent();

            //List<int> ptjes = new List<int>();
            ptjes.Add(30);
            ptjes.Add(40);
            ptjes.Add(60);
            ptjes.Add(20);
            ptjes.Add(10);
            ptjes.Add(30);



            UpdateChart(ptjes, "Resultados");

		}

        private void ButtonSeven_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            SaveToImage(MyChart);


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
            this.Visibility = Visibility.Visible;
        }
	}
}