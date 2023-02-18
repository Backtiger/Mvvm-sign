using consent2.Query;
using Mvvmsign.Model;
using Mvvmsign.Util;
using Mvvmsign.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mvvmsign.ViewModel
{
    public class UcMakeChartVM
    {

        DalMakeChart dalMakeChart = new DalMakeChart();

        private MakeChartModel _SelectedMakeChart { get; set; }

        public MakeChartModel SelectedMakeChart
        {
            get { return _SelectedMakeChart; }
            set
            {
                if (_SelectedMakeChart != value)
                    _SelectedMakeChart = value;
            }
        }

        public ObservableCollection<MakeChartModel> MakeChartList { get; set; } = new ObservableCollection<MakeChartModel>();   

        public UcMakeChartVM()
        {
            ExecuteSelect();
        }

        private ICommand _SelectCommand;
        public ICommand SelectCommand
        {
            get
            {
                if (_SelectCommand == null)
                {
                    _SelectCommand = new RelayCommand(add => ExecuteSelect());

                }
                return _SelectCommand;
            }
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

        private ICommand _UpdateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                if (_UpdateCommand == null)
                {
                    _UpdateCommand = new RelayCommand(ExecuteUpdate, CanExecute);

                }
                return _UpdateCommand;
            }
        }

        private ICommand _DeleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new RelayCommand(ExecuteDelete, CanExecute);

                }
                return _DeleteCommand;
            }
        }

        public void ExecuteSelect() 
        {
       

            MakeChartList = new ObservableCollection<MakeChartModel>();

            DataTable dtMakeChart = dalMakeChart.SelectMackeChartList();

            foreach (DataRow dr in dtMakeChart.Rows)
            {
                Console.WriteLine(dr["MAKE_SEQ"].ToString());
                MakeChartList.Add(new MakeChartModel
                {
                   
                    MakeSeq = dr["MAKE_SEQ"].ToString(),
                    CustoemrSeq = dr["CUSTOMER_SEQ"].ToString(),
                    CustomerName = dr["CUSTOMER_NAME"].ToString(),
                    ChartSeq = dr["CHART_SEQ"].ToString(),
                    ChartName = dr["CHART_NAME"].ToString(),
                    ChartPath = dr["CHART_PATH"].ToString(),
                    UserId = dr["USER_ID"].ToString(),               
                    MakeDate = Convert.ToDateTime(dr["MAKE_DATE"])
                });
            }
        }

        public void ExecuteUpdate(object param)
        {
            System.Windows.Forms.MessageBox.Show("Test");
        }
        public bool CanExecute(object param)
        {
            if (SelectedMakeChart != null)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public void ExecuteDelete(object param)
        {
            dalMakeChart.DeleteMakeChart(SelectedMakeChart.MakeSeq);
        }
        public bool CanExecuteDelete(object param)
        {
            if (SelectedMakeChart is null)
                return false;
            else
                return true;
        }

        public void ExecuteInsert()
        {
        }
    }
}
