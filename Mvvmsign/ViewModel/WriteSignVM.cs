using Mvvmsign.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mvvmsign.ViewModel
{
    class WriteSignVM
    {
        double imageHeight = 0;
        double imageWidth = 0;

        string cusNum = null;
        string cusName = null;
        string chartName = null;

        public WriteSignVM(double imageHeight, double imageWidth,string cusNum ,string cusName ,string chartName)
        {
            this.imageHeight = imageHeight;
            this.imageWidth = imageWidth;

            this.cusNum = cusNum;
            this.cusName = cusName;
            this.chartName = chartName;
        }

        private ICommand _SaveCommand;
        public ICommand test
        {
            get
            {
                System.Windows.Forms.MessageBox.Show("Test");
                return _SaveCommand;
            }
        }
        
    
        private void ExecuteSave(object param)
        {
            UIElement inkcanvas = param as UIElement;

            inkcanvas.RenderTransform = new ScaleTransform(1, 1, 0, 0);
            inkcanvas.Refresh();

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)imageWidth, (int)imageHeight, 96d, 96d, PixelFormats.Default);

            renderTargetBitmap.Render(inkcanvas);

            using (Stream stream = new FileStream( CreatePath(), FileMode.Create, FileAccess.Write, FileShare.None))
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

            string path = cusNum + "_"+cusName+"-"+chartName;

            return path;
        }

    }
}
