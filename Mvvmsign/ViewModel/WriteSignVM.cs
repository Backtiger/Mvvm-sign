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
        
    


  

    }
}
