using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Client.Model_Veiw.Base
{

    internal abstract class BaseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter) { throw new NotImplementedException(); }

        public virtual void Execute(object parameter) { throw new NotImplementedException(); }
    }
}
