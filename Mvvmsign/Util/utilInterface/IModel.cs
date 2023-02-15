using Mvvmsign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvvmsign.Util.utilInterface
{
    internal interface IModel
    {
        ChartListModel ChartListModel { get; set; }  

        CustomerModel CustomerModel { get; set; }    
        MakeChartModel MakeChartModel { get; set; }
        UserModel UserModel { get; set; }    
    }
}
