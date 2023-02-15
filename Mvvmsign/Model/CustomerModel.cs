using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvvmsign.Model
{
    public class CustomerModel
    {
        public string Number { get; set; }

        public string Name { get; set; }

        public DateTime BirthDay { get; set; }

        public bool Sex { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Remark { get; set; }
    }
}
