using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvvmsign.Model
{
    internal class UserModel : INotifyPropertyChanged
    {
        public string Userid { get; set; }

        public string Userpw { get; set; }

        public string UserName { get; set; }

        public DateTime Makedate { get; set; }
        public string Remark { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propname)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
    }
}
