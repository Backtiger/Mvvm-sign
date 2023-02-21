using consent2.Query;
using Mvvmsign.Model;
using Mvvmsign.Util;
using Mvvmsign.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Mvvmsign.View
{
    /// <summary>
    /// WirteSign.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WirteSign : Window
    {
        System.Windows.Controls.Image stationImage = new System.Windows.Controls.Image();

        private Double zoomMax = 5;
        private Double zoomMin = 0.5;
        private Double zoomSpeed = 0.001;
        private Double zoom = 1;

        double imageHeight = 0;
        double imageWidth = 0;

        //string chartName;
        //string path;
        //string cusNum;
        //string cusName;

        ChartListModel ChartListM;
        CustomerModel CustomerM;



        public WirteSign(ChartListModel chartListModel , CustomerModel customerModel)
        {
            InitializeComponent();
            SetImage(chartListModel.ChartPath);
            this.ChartListM = chartListModel;
            this.CustomerM = customerModel;       

        }

        //public WirteSign(string chartname,string path ,string cusnum ,string cusname)
        //{
        //    InitializeComponent();
        //    SetImage(path.Trim());

        //    this.chartName = chartname;
        //    this.path = path;
        //    this.cusNum = cusnum;
        //    this.cusName = cusname;

        //    //this.DataContext = new WriteSignVM(imageHeight, imageHeight, cusnum, cusname, chartName);
        //}

        private void SetImage(string path)
        {
            stationImage.Source = new BitmapImage(new Uri(path));

            canvas.Children.Add(stationImage);

            Bitmap img = new Bitmap(path);

            imageHeight = img.Height;
            imageWidth = img.Width;


            canvas.Height = imageHeight;
            canvas.Width = imageWidth;
        }

        private void save()
        {
            DalMakeChart dalMakeChart = new DalMakeChart();
                       
            dalMakeChart.InsertMackeChartList(ChartListM, CustomerM);
            Saveimage();
        }

        private void Saveimage()
        {
            //UIElement inkcanvas = param as UIElement;

            canvas.RenderTransform = new ScaleTransform(1, 1, 0, 0);
            canvas.Refresh();

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)imageWidth, (int)imageHeight, 96d, 96d, PixelFormats.Default);

            renderTargetBitmap.Render(canvas);

            using (Stream stream = new FileStream(CreatePath(), FileMode.Create, FileAccess.Write, FileShare.None))
            {
                JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
                jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                jpegBitmapEncoder.Save(stream);
            }
        } 

        private string CreatePath()
        {
            if (!Directory.Exists("C:\\SignChart\\WritedSign"))
            {
                Directory.CreateDirectory("C:\\SignChart\\WritedSign");
            }

            string path = CustomerM.Number + "_" + CustomerM.Name + "-" + ChartListM.ChartName;

            return path;
        }

        

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = sender as System.Windows.Controls.Button;

            switch (btn.Name)
            {
                case "btnSelect": canvas.EditingMode = InkCanvasEditingMode.Select; break;

                case "btnEraser":
                    canvas.EraserShape = new RectangleStylusShape(50, 50);
                    canvas.EditingMode = InkCanvasEditingMode.EraseByPoint; break;

                case "btnDelete": canvas.EditingMode = InkCanvasEditingMode.EraseByStroke; break;

                case "btnDipose": canvas.EditingMode = InkCanvasEditingMode.None; break;

                case "btnDraw": canvas.EditingMode = InkCanvasEditingMode.Ink; break;

                case "btnSave": save(); break;
            }

        }


        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
            {
                zoom += zoomSpeed * e.Delta; // Ajust zooming speed (e.Delta = Mouse spin value )
                if (zoom < zoomMin) { zoom = zoomMin; } // Limit Min Scale
                if (zoom > zoomMax) { zoom = zoomMax; } // Limit Max Scale

                System.Windows.Point mousePos = e.GetPosition(scrollview);

                if (zoom > 1)
                {
                    canvas.RenderTransform = new ScaleTransform(zoom, zoom, mousePos.X, mousePos.Y); // transform Canvas size from mouse position
                }
                else
                {
                    canvas.RenderTransform = new ScaleTransform(zoom, zoom); // transform Canvas size
                }
            }
        }
    }
}
