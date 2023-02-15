using consent2.Query;
using Mvvmsign.DBConnection;
using Mvvmsign.Model;
using Mvvmsign.Util;
using Mvvmsign.Util.utilInterface;
using Mvvmsign.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Mvvmsign.ViewModel
{
    public class MainVM :INotifyPropertyChanged
    {
        DalChartList dalChartList= new DalChartList();  
        DalCustoemr dalCustoemr= new DalCustoemr();

        CollectionViewSource customerListview = new CollectionViewSource();
        CollectionViewSource chartListview = new CollectionViewSource();

        public ICollectionView CustomerListviewCollection => customerListview.View;
        public ICollectionView ChartListviewCollection => chartListview.View;


        public ObservableCollection<CustomerModel> customerList { get; set; } = new ObservableCollection<CustomerModel>();
        public ObservableCollection<ChartListModel> chartList { get; set; } = new ObservableCollection<ChartListModel>();


        private string _veiwCustomerName;
        public string ViewCustomerName
        {
            get { return _veiwCustomerName; }
            set
            {
                _veiwCustomerName=SelectedCustomer.Name;
                OnPropertyChanged("veiwCustomerName");
            }
        }

        private string _viewChartName;
        public string ViewChartName
        {
            get { return _viewChartName; }
            set
            {
                _viewChartName = SelectedChart.ChartName;
                OnPropertyChanged("veiwChartName");
            }
        }

        private CustomerModel _selectedCustomer;

        public CustomerModel SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (_selectedCustomer != value)
                    _selectedCustomer = value;
                OnPropertyChanged("SelectedCustomer");
            }
        }

        private ChartListModel _selectedchart;
        public ChartListModel SelectedChart
        {
            get { return _selectedchart; }
            set
            {
                if (_selectedchart != value)
                    _selectedchart = value;
                OnPropertyChanged("SelectedChart");

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private string _chartlistfilterText;
        public string ChartlistFilterText
        {
            get => _chartlistfilterText;
            set
            {
                _chartlistfilterText = value;
                chartListview.View.Refresh();
                OnPropertyChanged("FilterText");
            }
        }

        private string _customerfilterText;
        public string CustomerFilterText
        {
            get => _customerfilterText;
            set
            {
                _customerfilterText = value;
                customerListview.View.Refresh();
                OnPropertyChanged("FilterText");
            }
        }

        private ICommand _WriteCommand;
        public ICommand WirteCommand 
        { 
            get 
            {
                if (_WriteCommand == null)
                {
                    _WriteCommand = new RelayCommand(ExecuteShowWirte,CanExecuteShowWirte);
                }
                return _WriteCommand; 
            } 
        }

        public MainVM() 
        {
            SeletedCustomer();
            SelectChartList();
        }

        private void SeletedCustomer() 
        {
            customerList= new ObservableCollection<CustomerModel>();    

            DataTable dtcustomer = dalCustoemr.SelectCustomer();

            foreach (DataRow dr in dtcustomer.Rows)
            {
                Console.WriteLine(dr[0].ToString());
                customerList.Add(new CustomerModel
                {
                    Number = dr["CUSTOMER_SEQ"].ToString(),
                    Name = dr["CUSTOMER_NAME"].ToString(),
                    BirthDay = Convert.ToDateTime(dr["CUSTOMER_BIRTH"]),
                    Sex = Convert.ToBoolean(dr["CUSTOMER_SEX"]),
                    PhoneNumber = dr["CUSTOMER_PHONE"].ToString(),
                    Address = dr["CUSTOMER_ADDRESS"].ToString(),
                    Remark = dr["REMARK"].ToString()
                });
            }
                        
            customerListview = new CollectionViewSource { Source = customerList };
            customerListview.Filter += Filter;
        }

        private void SelectChartList()
        {
            DataTable ChartList = dalChartList.SelectChartList();

            chartList = new ObservableCollection<ChartListModel>();

            foreach (DataRow dr in ChartList.Rows)
            {
                chartList.Add(new ChartListModel
                {
                    ChartSeq = dr["CHART_SEQ"].ToString(),
                    ChartName = dr["CHART_NAME"].ToString(),
                    Makedate = Convert.ToDateTime(dr["CHART_MAKEDATE"]),
                    ChartPath = dr["CHART_PATH"].ToString(),
                    remark = dr["REMARK"].ToString()
                });
            }
            chartListview = new CollectionViewSource { Source = chartList };
            chartListview.Filter += Filter;
        }

        private void Filter(object sender, FilterEventArgs e)
      {
            string Search = null;
            string filtertext = null;
            if (string.IsNullOrEmpty(CustomerFilterText)&& string.IsNullOrEmpty(ChartlistFilterText))
            {
                e.Accepted = true;
                return;
            }

            if (e.Item is ChartListModel)
            {
                ChartListModel chartListModel= e.Item as ChartListModel;
                Search = chartListModel.ChartName;
                filtertext = ChartlistFilterText;

            }
            else if (e.Item is CustomerModel) 
            {
                CustomerModel customerModel = e.Item as CustomerModel;
                Search = customerModel.Name;
                filtertext = CustomerFilterText;
            }

            if (!string.IsNullOrEmpty(filtertext))
            {
                if (Search.ToUpper().Contains(filtertext.ToUpper()))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
        }
 

        private void ExecuteShowWirte(object param) 
        {
            
            WirteSign wirteSign = new WirteSign(SelectedChart.ChartName,SelectedChart.ChartPath ,SelectedCustomer.Number,SelectedCustomer.Name);
            wirteSign.Show();
            Sign sing = new Sign();
           // sing.Show();
        }

        private bool CanExecuteShowWirte(object param)
        {
            if (SelectedCustomer != null && SelectedChart != null)
            {
                return true;
            }
            return false;
        }

    }
}
