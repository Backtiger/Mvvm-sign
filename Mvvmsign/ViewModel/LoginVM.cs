using consent2.Query;
using Mvvmsign;
using Mvvmsign.Model;
using Mvvmsign.Util;
using Mvvmsign.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace consent2
{
    class LoginVM :INotifyPropertyChanged
    {
        

        private ICommand _LoginCommand;

        public ICommand LoginCommand
        {
            get
            {
                if (_LoginCommand == null)
                {
                    _LoginCommand = new RelayCommand(ExecuteLogin);

                }
                return _LoginCommand;
            }
        }

        private ICommand _CloseCommand;

        public ICommand CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                {
                    _CloseCommand = new RelayCommand(ExecuteClose);

                }
                return _CloseCommand;
            }
        }
        
        private ICommand _RegisterCommand;

        public ICommand RegisterCommand
        {
            get
            {
                if (_RegisterCommand == null)
                {
                    _RegisterCommand = new RelayCommand(InsertUser, CanInsertUser);

                }
                return _RegisterCommand;
            }
        }

        private ICommand _SignUpCommand;

        public ICommand SignUpCommand
        {
            get
            {
                if (_SignUpCommand == null)
                {
                    _SignUpCommand = new RelayCommand(ShowRegister);

                }
                return _SignUpCommand;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propname)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }

        public void ExecuteLogin(object param)
        {
            DalUser dalUser = new DalUser();
            PasswordBox passwordBox = new PasswordBox();
            object closeform = new object();
            TextBox txtid = null;

            foreach (object ob in (object[])param)
            {
                if (ob is PasswordBox)
                    passwordBox = ob as PasswordBox;
                else if (ob is TextBox)
                    txtid = ob as TextBox;
                else
                    closeform = ob;
            }

            var password = passwordBox.Password;

            DataTable dtloginuser = dalUser.Login(txtid.Text, password.ToString());

            if (dtloginuser.Rows.Count > 0)
            {
                UserModel.Userid = txtid.Text;
                MainWindow win = new MainWindow();
                ExecuteClose(closeform);
                win.Show();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("로그인에 실패 하였습니다.");
            }
        }
        private void ExecuteClose(object obj)
        {
            Window Win = obj as Window;
            Win.Close();

        }

        private void InsertUser(object obj)
        {            
            DalUser dalUser = new DalUser();

            string today =(DateTime.Now.Year +"-"+ DateTime.Now.Month+ "-" + DateTime.Now.Day).ToString();
            int success = dalUser.InsertUser(UserModel.Userid, UserModel.Userpw, UserModel.UserName, today);

            if (success==1)
            {
                UserModel.UserName = null; OnPropertyChanged("UserName");
                UserModel.Userid = null; OnPropertyChanged("Userid");
                UserModel.Userpw = null; OnPropertyChanged("Userpw");

                System.Windows.Forms.MessageBox.Show("계정이 생성되었습니다.");
            }
        }

        private bool CanInsertUser(object obj)
        {
            bool flag;

            if (string.IsNullOrEmpty(UserModel.Userid) || string.IsNullOrEmpty(UserModel.UserName) || string.IsNullOrEmpty(UserModel.Userpw))
                flag = false;
            else
                flag = true;
            return flag;
        }

        private void ShowRegister(object obj)
        {
            UserRegister userRegister = new UserRegister();

            userRegister.ShowDialog();
        }
    }
}
