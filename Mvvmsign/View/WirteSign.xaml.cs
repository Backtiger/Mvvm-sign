﻿using Mvvmsign.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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

        public WirteSign(string chartName,string path ,string cusnum ,string cusname)
        {
            InitializeComponent();
            SetImage(path.Trim());

            this.DataContext = new WriteSignVM(imageHeight, imageHeight, cusnum, cusname, chartName);
        }

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

        private void save()
        {

        }

        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
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