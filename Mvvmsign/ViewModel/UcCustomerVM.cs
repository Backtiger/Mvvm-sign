using consent2.Query;
using Mvvmsign.Model;
using Mvvmsign.Util;
using Mvvmsign.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Mvvmsign.ViewModel
{
    public class UcCustomerVM 
    {
        DalCustoemr dalCustoemr = new DalCustoemr();
        public ObservableCollection<CustomerModel> customerList { get; set; } = new ObservableCollection<CustomerModel>();

        private CustomerModel _selectedCustomer { get; set; }

        public CustomerModel SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (_selectedCustomer != value)
                    _selectedCustomer = value;
            }
        }

        public UcCustomerVM()
        {
            SelectCustomer();
        }

        private ICommand _InsertCommand;
        public ICommand InsertCommand
        {
            get
            {
                if (_InsertCommand == null)
                {
                    _InsertCommand = new RelayCommand(add => ExecuteInsert());

                }
                return _InsertCommand;
            }
        }

        private ICommand _SelectCommand;
        public ICommand SelectCommand
        {
            get
            {
                if (_SelectCommand == null)
                {
                    _SelectCommand = new RelayCommand(search => new UcCustomerVM());

                }
                return _SelectCommand;
            }
        }

        private ICommand _UpdateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                if (_UpdateCommand == null)
                {
                    _UpdateCommand = new RelayCommand( ExecuteUpdate, CnaExecuteUpdate);

                }
                return _UpdateCommand;
            }
        }


        private ICommand _DeleteCommand;

        public event NotifyCollectionChangedEventHandler CollectionChanged;
  

        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new RelayCommand( ExecuteDelete, CnaExecuteUpdate);

                }
                return _DeleteCommand;
            }
        }

     
        public bool CnaExecuteUpdate(object param) 
        {
            if(SelectedCustomer!=null) 
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public void ExecuteUpdate(object param)
        {
            CustomerModel customerModel = new CustomerModel();

            customerModel.Name = SelectedCustomer.Name;
            customerModel.PhoneNumber = SelectedCustomer.PhoneNumber;
            customerModel.Sex = SelectedCustomer.Sex;
            customerModel.Remark = SelectedCustomer.Remark;
        }

        public void ExecuteDelete(object param)
        {
            int returnflag= dalCustoemr.DeleteCustomer(SelectedCustomer.Number);

            if (returnflag > 0)
            {
                System.Windows.Forms.MessageBox.Show("삭제가 완료 되었습니다.");
            }
        }

        public void ExecuteInsert()
        {
            CustomerPopupxaml insertpop = new CustomerPopupxaml();
            insertpop.Show();
        }

        private void SelectCustomer() 
        {
            customerList = new ObservableCollection<CustomerModel>();
            DataTable dtcustomer = dalCustoemr.SelectCustomer();    

            foreach (DataRow dr in dtcustomer.Rows)
            {
                Console.WriteLine(dr[0].ToString());
                customerList.Add(new CustomerModel
                {
                    Number = dr[0].ToString(),
                    Name = dr[1].ToString(),
                    BirthDay = Convert.ToDateTime(dr[2]),
                    Sex = Convert.ToBoolean(dr[3]),
                    PhoneNumber = dr[4].ToString(),
                    Address = dr[5].ToString(),
                    Remark = dr[6].ToString()
                });
            }
        }
    }
}
