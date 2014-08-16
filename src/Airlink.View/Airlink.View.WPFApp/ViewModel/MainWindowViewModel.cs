using Airlink.Model.Business;
using Airlink.Model.Domain;
using System.Windows.Input;

namespace Airlink.View.WPFApp.ViewModel
{
    public class MainWindowViewModel : WorkspaceViewModel
    {
        // The viewmodel currently for display
        private WorkspaceViewModel _currentViewModel;
        // Viewmodels instances for the main display
        private WorkspaceViewModel _homePageViewModel =  new HomePageViewModel();
        private NewPersonViewModel _newPersonViewModel;
        private NewEmployeeViewModel _newEmployeeViewModel;
        private EditPersonViewModel _editPersonViewModel;
        private EditEmployeeViewModel _editEmployeeViewModel;
        private JobMenuViewModel _jobMenuViewModel;

        private RelayCommand _loadHomePageCommand;
        private RelayCommand _loadNewPersonCommand;
        private RelayCommand _loadEditPersonCommand;
        private RelayCommand _loadNewEmployeeCommand;
        private RelayCommand _loadEditEmployeeCommand;
        private RelayCommand _loadJobMenuCommand;

        private PersonnelManager persMgr;
        private JobManager jobMgr;
        private FlightManager fltMgr;

        // Constructor with setup
        public MainWindowViewModel()
        {
            this.LoadHomePage();
            persMgr = PersonnelManager.Instance;
            jobMgr = JobManager.Instance;
            fltMgr = FlightManager.Instance;
        }

        public WorkspaceViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set 
            {
                _currentViewModel = value;                
                this.OnPropertyChanged("CurrentViewModel");
            }
        }

        // Returns a command that loads the basic home page
        public ICommand LoadHomePageCommand
        {
            get
            {
                if (_loadHomePageCommand == null)
                {
                    _loadHomePageCommand = new RelayCommand(
                        param => this.LoadHomePage()
                        );
                }
                return _loadHomePageCommand;
            }
        }

        // Returns a command that opens the new person UI.
        public ICommand LoadNewPersonCommand
        {
            get
            {
                if (_loadNewPersonCommand == null)
                {
                    _loadNewPersonCommand = new RelayCommand(
                        param => this.LoadNewPersonPage()
                        );
                }
                return _loadNewPersonCommand;
            }
        }

        // Returns a command that opens the new employee UI.
        public ICommand LoadNewEmployeeCommand
        {
            get
            {
                if (_loadNewEmployeeCommand == null)
                {
                    _loadNewEmployeeCommand = new RelayCommand(
                        param => this.LoadNewEmployeePage()
                        );
                }
                return _loadNewEmployeeCommand;
            }
        }

        // Returns a command that loads the edit person page
        public ICommand LoadEditPersonCommand
        {
            get
            {
                if (_loadEditPersonCommand == null)
                {
                    _loadEditPersonCommand = new RelayCommand(
                        param => this.LoadEditPerson()
                        );
                }
                return _loadEditPersonCommand;
            }
        }

        // Returns a command that loads the edit employee page
        public ICommand LoadEditEmployeeCommand
        {
            get
            {
                if (_loadEditEmployeeCommand == null)
                {
                    _loadEditEmployeeCommand = new RelayCommand(
                        param => this.LoadEditEmployee()
                        );
                }
                return _loadEditEmployeeCommand;
            }
        }

        // Returns a command that loads the job menu page
        public ICommand LoadJobMenuCommand
        {
            get
            {
                if (_loadJobMenuCommand == null)
                {
                    _loadJobMenuCommand = new RelayCommand(
                        param => this.LoadJobMenuPage()
                        );
                }
                return _loadJobMenuCommand;
            }
        }

        // Private helper methods
        private void LoadHomePage()
        {
            CurrentViewModel = _homePageViewModel;
        }

        private void LoadNewPersonPage()
        {
            // Create the viewmodel instance if it doesn't exist yet
            if (_newPersonViewModel == null)
            {
                Person newPerson = new Person();
                _newPersonViewModel = new NewPersonViewModel(newPerson, persMgr);
            }
            else
            {
                // Just clear out the viewmodel data
                _newPersonViewModel.ClearCommand.Execute(null);
            }            
            
            // Display UI
            CurrentViewModel = _newPersonViewModel;

        }

        private void LoadEditPerson()
        {
            if (_editPersonViewModel == null)
            {
                _editPersonViewModel = new EditPersonViewModel(persMgr);
            }
            else
            {
                _editPersonViewModel.ClearCommand.Execute(null);
            }

            CurrentViewModel = _editPersonViewModel;

        }

        private void LoadNewEmployeePage()
        {
            // Create viewmodel instance if it doesn't exist yet
            if (_newEmployeeViewModel == null)
            {
                Employee newEmployee = new Employee();
                _newEmployeeViewModel = new NewEmployeeViewModel(newEmployee, persMgr, fltMgr);
            }
            else
            {
                // Just clear out the viewmodel data
                _newEmployeeViewModel.ClearCommand.Execute(null);
            }

            // Display UI
            CurrentViewModel = _newEmployeeViewModel;
        }

        private void LoadEditEmployee()
        {
            if (_editEmployeeViewModel == null)
            {
                _editEmployeeViewModel = new EditEmployeeViewModel(persMgr, jobMgr);
            }
            else
            {
                _editEmployeeViewModel.ClearCommand.Execute(null);
            }

            CurrentViewModel = _editEmployeeViewModel;
        }

        private void LoadJobMenuPage()
        {
            // Create the viewmodel instance if it doesn't exist yet
            if (_jobMenuViewModel == null)
            {
                _jobMenuViewModel = new JobMenuViewModel(jobMgr);
            }
            else
            {
                // Just clear out the viewmodel data
                // to be implemented...
            }

            // Display UI
            CurrentViewModel = _jobMenuViewModel;
        }
    }
}
