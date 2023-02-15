using consent2.Query;
using Mvvmsign.DBConnection;
using Mvvmsign.Model;
using Mvvmsign.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Mvvmsign.ViewModel
{
    public class UcChartListVM// :ChartListModel
    {
        public ObservableCollection<ChartListModel> ChartModelList { get; set; } = new ObservableCollection<ChartListModel>();

        private ChartListModel _SelectedchartList { get; set; }

        public ChartListModel SelectedchartList
        {
            get { return _SelectedchartList; }
            set
            {
                if (_SelectedchartList != value)
                    _SelectedchartList = value;
            }
        }

        private ICommand _SelectCommand;
        public ICommand SelectCommand
        {
            get
            {

                if (_SelectCommand == null)
                {
                    _SelectCommand = new RelayCommand(update => ExecuteSelect());

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
                    _InsertCommand = new RelayCommand(update => ExecuteInsert());

                }
                return _InsertCommand;

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

        DalChartList ChartRegister = new DalChartList();

        public UcChartListVM()
        {
            SelectChartList();
        }


        public void ExecuteSelect()
        {
            SelectChartList();

        }

        public void ExecuteUpdate(object param)
        {
            System.Windows.Forms.MessageBox.Show("Test");
        }

        public bool CanExecute(object param)
        {
            if (SelectedchartList != null)
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
            int returnflag = ChartRegister.DeleteChartList(SelectedchartList.ChartSeq);

            if (returnflag == 0)
            { 
                System.Windows.Forms.MessageBox.Show("삭제가 완료 되었습니다."); 
            }
        }

        public void ExecuteInsert()
        {
            List<string> filelist= GetFilePath();

            if (filelist != null)
            {
                if (filelist[0] != null)
                {
                    string filename = filelist[0];
                    string filepath = filelist[1];

                    int result = ChartRegister.InsertChartList(filename, filepath, " ");

                    if (result == 1)
                    {
                        MessageBox.Show("입력이 완료 되었습니다.");
                        SelectChartList();
                    }
                }
                else
                {
                    MessageBox.Show("취소 되었습니다.");
                }
            }
            else
            {
                MessageBox.Show("다시 선택 해주세요");
            }          
        }

        private List<string> GetFilePath() 
        {
            List<string> filelist= new List<string>();  

            if (!Directory.Exists("C:\\SignChart"))
            {
                Directory.CreateDirectory("C:\\SignChart");
            }

            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "한글파일|*hwp";
            open.Multiselect = false;

            DialogResult dialogResult = open.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                HWP hwp = new HWP();

                string hwppath= hwp.ImgConvert(open.FileName, "C:\\SignChart", 100);

                filelist.Add(open.SafeFileName.Substring(0, open.SafeFileName.LastIndexOf('.'))); //File Name
                filelist.Add(hwppath); //File Path

            }
            else 
            {
                filelist = null; 
            }
            
            return filelist;
        }

        private void SelectChartList() 
        {
            DataTable ChartList = ChartRegister.SelectChartList();

            ChartModelList = new ObservableCollection<ChartListModel>();

            foreach (DataRow dr in ChartList.Rows)
            {
                ChartModelList.Add(new ChartListModel
                {
                    ChartSeq = dr["CHART_SEQ"].ToString(),
                    ChartName = dr["CHART_NAME"].ToString(),
                    Makedate = Convert.ToDateTime(dr["CHART_MAKEDATE"]),
                    ChartPath = dr["CHART_PATH"].ToString(),
                    remark = dr["REMARK"].ToString()
                });
            }
            
        }


    }
}
