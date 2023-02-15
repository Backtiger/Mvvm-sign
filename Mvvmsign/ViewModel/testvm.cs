using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mvvmsign.ViewModel
{
    class testvm
    {

        private ICommand _SelectCommand;
        public ICommand SelectCommand
        {
            get
            {

                if (_SelectCommand == null)
                {
                    System.Windows.Forms.MessageBox.Show("Test");

                }
                return _SelectCommand;

            }
        }
    }
}
