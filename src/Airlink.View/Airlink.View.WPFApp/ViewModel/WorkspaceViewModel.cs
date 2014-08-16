using System;
using System.Windows.Input;

namespace Airlink.View.WPFApp.ViewModel
{
    // This class is removed from the UI when the the closeCommand executes
    public abstract class WorkspaceViewModel : ViewModelBase
    {
        private RelayCommand _closeCommand;
        // Raised when this workspace should be removed from the UI.
        public event EventHandler RequestClose;

        protected WorkspaceViewModel() { }

        // Returns the command that, when invoked, attempts
        // to remove this workspace from the user interface.
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new RelayCommand(param => this.OnRequestClose());

                return _closeCommand;
            }
        }

        private void OnRequestClose()
        {
            Console.WriteLine("Inside OnRequestClose Method {0}", this.ToString());
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
