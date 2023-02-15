using consent2.Query;
using Mvvmsign.Model;
using Mvvmsign.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Mvvmsign.ViewModel
{
    internal class CustomerPopupVM : CustomerModel, INotifyPropertyChanged
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

        protected void OnPropertyChanged(string propname)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }

        public void InsertCustomer(object ABC)
        {
           DalCustoemr customer = new DalCustoemr();

            string birth = BirthDay.ToString("yyyy-MM-dd");
            int returnflag = customer.InsertCustomer(Name, birth, Sex, PhoneNumber, Address, Remark);

            if (returnflag > 0)
            {
                Name = null; OnPropertyChanged("Name");
                birth = null; OnPropertyChanged("birth");
                PhoneNumber = null; OnPropertyChanged("PhoneNumber");
                Sex = false; OnPropertyChanged("PhoneNumber");
                Address = null; OnPropertyChanged("Address");
                Remark = null; OnPropertyChanged("Remark");

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
