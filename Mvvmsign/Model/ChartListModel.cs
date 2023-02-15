using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvvmsign.Model
{
    public class ChartListModel
    {    
        public string ChartSeq { get; set; }

        public string ChartName { get; set; }

        public DateTime Makedate { get; set; }

        public string ChartPath { get; set; }

        public string remark { get; set; }
    }
}
