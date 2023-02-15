using Mvvmsign.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Input;
using System.Xml.Linq;
using consent2.Query;
using Mvvmsign.Model;

namespace Mvvmsign.ViewModel
{
    internal class UserVM : CustomerModel, INotifyPropertyChanged
    {        
        private ICommand _InsertCustomerCommand;
        public ICommand InsertCustomerCommand
        {
            get
            {
                if (_InsertCustomerCommand == null)
                {
                    _InsertCustomerCommand = new RelayCommand(InsertCustomer, CanExcuteInsertCustomer);
                }
                return _InsertCustomerCommand;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void InsertCustomer(object ABC)
        {
           DalCustoemr customer = new DalCustoemr();

            string birth = BirthDay.ToString("yyyy-MM-dd");
            int returnflag = customer.InsertCustomer(Name, birth, Sex, PhoneNumber, Address, Remark);

            if (returnflag > 0)
            {
                System.Windows.Forms.MessageBox.Show("저장이 완료 되었습니다.");
            }
        }

        public bool CanExcuteInsertCustomer(object param)
        {
            bool flag = false;

            if (Name != null && Address != null && BirthDay != null && PhoneNumber != null && Sex != null)
            {
                flag = true;
            }

            return flag;
        }
    }
}
