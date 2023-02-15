using Mvvmsign.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using static Mvvmsign.ViewModel.MenuItemVM;

namespace Mvvmsign.Util
{
    internal class NavigationVM : INotifyPropertyChanged
    {
        private CollectionViewSource MenuItemsCollection;

        public ICollectionView SourceCollection => MenuItemsCollection.View;

        private object _selectedViewModel;
        public object SelectedViewModel
        {
            get => _selectedViewModel;
            set { _selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }

        public NavigationVM()
        {
            ObservableCollection<MenuItems> menuItems = new ObservableCollection<MenuItems>
            {
                new MenuItems { MenuName = "메인화면" },
                new MenuItems { MenuName = "고객관리"},
                new MenuItems { MenuName = "양식관리"},
                new MenuItems { MenuName = "작성목록"},
                new MenuItems { MenuName = "test"}


            };

            MenuItemsCollection = new CollectionViewSource { Source = menuItems };
            SelectedViewModel = new MainVM();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }



        private ICommand _menucommand;
        public ICommand MenuCommand
        {
            get
            {
                if (_menucommand == null)
                {
                    _menucommand = new RelayCommand(param => SwitchViews(param));
                }
                return _menucommand;
            }
        }

        private ICommand _closeCommand;
        public ICommand CloseAppCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(p => CloseApp(p));
                }
                return _closeCommand;
            }
        }

        private ICommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new RelayCommand(p => Add());
                }
                return _AddCommand;
            }

        }

        public void SwitchViews(object parameter)
        {
            switch (parameter)
            {
                case "메인화면":
                    SelectedViewModel = new MainVM();
                    break;
                case "고객관리":
                    SelectedViewModel = new UcCustomerVM();
                    break;
                case "작성목록":
                    SelectedViewModel = new UcMakeChartVM();
                    break;
                case "양식관리":
                    SelectedViewModel = new UcChartListVM();
                    break;
           
                default:
                    SelectedViewModel = new MainVM();
                    break;
            }
        }

        public void CloseApp(object obj)
        {
            MainWindow win = obj as MainWindow;
            win.Close();
        }

        public void Add() 
        {
        
        }

    }
}
