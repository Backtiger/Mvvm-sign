using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvvmsign.Model
{
    public class MakeChartModel
    {
        public string MakeSeq { get; set; }

        public string CustoemrSeq { get; set; }

        public string CustomerName { get; set; }

        public string ChartSeq { get; set; }

        public string ChartName { get; set; }

        public string ChartPath { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime MakeDate { get; set; }

        public bool Delfalg { get; set; }

        public string Remark { get; set; }
    }
}
