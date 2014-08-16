using Airlink.Model.Business;
using Airlink.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace Airlink.View.WPFApp.ViewModel
{
    public class EditEmployeeViewModel : WorkspaceViewModel
    {
        private Employee _displayEmployee;
        private Person _addFamilyPerson;
        private CollectionView _employees;
        private CollectionView _familyOptions;
        private PersonnelManager _persMgr;
        private string _labelMessage;
        // Job Values
        private Job _selectedJobOption;
        private CollectionView _jobOptions;
        private JobManager _jobMgr;

        RelayCommand _delete;
        RelayCommand _clear;
        RelayCommand _addFamily;
        RelayCommand _removeFamily;
        RelayCommand _assignJob;
        RelayCommand _removeJob;

        public EditEmployeeViewModel(PersonnelManager persMgr, JobManager jobMgr)
        {
            if (persMgr == null)
            {
                throw new ArgumentNullException("personnel manager");
            }
            this._persMgr = persMgr;
            this._jobMgr = jobMgr;

            // Need three collection views for the main selector, 'family options', & 'job options' listbox
            IList<Employee> e = _persMgr.GetEmployees();
            IList<Person> p = _persMgr.GetPeople();
            IList<Job> j = _jobMgr.GetAllJobs();

            this._employees = new CollectionView(e);
            this._familyOptions = new CollectionView(p);
            this._jobOptions = new CollectionView(j);

            this._displayEmployee = new Employee();
            this._addFamilyPerson = new Person();
            this._selectedJobOption = new Job();
        }

        // Props
        public CollectionView Employees
        {
            get { return _employees; }
        }

        public CollectionView FamilyOptions
        {
            get { return _familyOptions; }
        }

        public CollectionView JobOptions
        {
            get { return _jobOptions; }
        }

        public Employee DisplayEmployee
        {
            get { return _displayEmployee; }
            set
            {
                if (value == _displayEmployee)
                {
                    return;
                }
                else if (value == null)
                {
                    // null value, possibly empty employee list. 
                    // Update the screen to make it blank and exit
                    _displayEmployee = new Employee();
                    base.OnPropertyChanged("FirstName");
                    base.OnPropertyChanged("LastName");
                    base.OnPropertyChanged("PhoneNumber");
                    base.OnPropertyChanged("Email");
                    base.OnPropertyChanged("GenderType");
                    base.OnPropertyChanged("Street");
                    base.OnPropertyChanged("City");
                    base.OnPropertyChanged("State");
                    base.OnPropertyChanged("ZipCode");
                    base.OnPropertyChanged("RankType");
                    base.OnPropertyChanged("Family");
                    base.OnPropertyChanged("EmployeesJobs");
                    AddFamilyPerson = new Person();
                    SelectedJobOption = new Job();
                    return;
                }
                // Valid employee. Make changes and update screen
                _displayEmployee = value;
                base.OnPropertyChanged("DisplayEmployee");
                base.OnPropertyChanged("FirstName");
                base.OnPropertyChanged("LastName");
                base.OnPropertyChanged("PhoneNumber");
                base.OnPropertyChanged("Email");
                base.OnPropertyChanged("GenderType");
                base.OnPropertyChanged("Street");
                base.OnPropertyChanged("City");
                base.OnPropertyChanged("State");
                base.OnPropertyChanged("ZipCode");
                base.OnPropertyChanged("RankType");
                base.OnPropertyChanged("Family");
                base.OnPropertyChanged("EmployeesJobs");
                AddFamilyPerson = new Person();
                SelectedJobOption = new Job();
            }
        }

        public Person AddFamilyPerson
        {
            get { return _addFamilyPerson; }
            set
            {
                if (value == _addFamilyPerson)
                    return;

                _addFamilyPerson = value;
                base.OnPropertyChanged("AddFamilyPerson");
            }
        }

        public Job SelectedJobOption
        {
            get { return _selectedJobOption; }
            set
            {
                if (value == _selectedJobOption)
                    return;

                _selectedJobOption = value;
                base.OnPropertyChanged("SelectedJobOption");
            }
        }

        public string FirstName
        {
            get { return _displayEmployee.FirstName; }
            set
            {
                if (value == _displayEmployee.FirstName)
                    return;

                _displayEmployee.FirstName = value;

                base.OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return _displayEmployee.LastName; }
            set
            {
                if (value == _displayEmployee.LastName)
                    return;

                _displayEmployee.LastName = value;

                base.OnPropertyChanged("LastName");
            }
        }

        public string PhoneNumber
        {
            get { return _displayEmployee.PhoneNumber; }
            set
            {
                if (value == _displayEmployee.PhoneNumber)
                    return;

                _displayEmployee.PhoneNumber = value;

                base.OnPropertyChanged("PhoneNumber");
            }
        }

        public string Email
        {
            get { return _displayEmployee.Email; }
            set
            {
                if (value == _displayEmployee.Email)
                    return;

                _displayEmployee.Email = value;

                base.OnPropertyChanged("Email");
            }
        }

        // Gender type for selected item to bind to
        public Gender GenderType
        {
            get { return _displayEmployee.Gender; }
            set
            {
                _displayEmployee.Gender = value;
                base.OnPropertyChanged("GenderType");
            }
        }

        // Gender values for ItemSource for binding
        public IEnumerable<Gender> GenderValues
        {
            get
            {
                return Enum.GetValues(typeof(Gender)).Cast<Gender>();
            }
        }

        public string Street
        {
            get { return _displayEmployee.Address.Street; }
            set
            {
                if (value == _displayEmployee.Address.Street)
                    return;

                _displayEmployee.Address.Street = value;

                base.OnPropertyChanged("Address.Street");
            }
        }

        public string City
        {
            get { return _displayEmployee.Address.City; }
            set
            {
                if (value == _displayEmployee.Address.City)
                    return;

                _displayEmployee.Address.City = value;

                base.OnPropertyChanged("Address.City");
            }
        }

        public string State
        {
            get { return _displayEmployee.Address.State; }
            set
            {
                if (value == _displayEmployee.Address.State)
                    return;

                _displayEmployee.Address.State = value;

                base.OnPropertyChanged("Address.State");
            }
        }

        public string ZipCode
        {
            get { return _displayEmployee.Address.ZipCode; }
            set
            {
                if (value == _displayEmployee.Address.ZipCode)
                    return;

                _displayEmployee.Address.ZipCode = value;

                base.OnPropertyChanged("Address.ZipCode");
            }
        }

        // Rank type for selected item to bind to
        public Rank RankType
        {
            get { return _displayEmployee.EmpRank; }
            set
            {
                _displayEmployee.EmpRank = value;
                base.OnPropertyChanged("RankType");
            }
        }

        // Rank values for ItemSource for binding
        public IEnumerable<Rank> RankValues
        {
            get
            {
                return Enum.GetValues(typeof(Rank)).Cast<Rank>();
            }
        }

        public CollectionView Family
        {
            get { return new CollectionView(_displayEmployee.Family); }
        }

        public CollectionView EmployeesJobs
        {
            get { return new CollectionView(_displayEmployee.Jobs); }
        }

        public string LabelMessage
        {
            get { return _labelMessage; }
            set
            {
                if (value == _labelMessage)
                    return;

                _labelMessage = value;

                base.OnPropertyChanged("LabelMessage");
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_delete == null)
                {
                    _delete = new RelayCommand(
                        param => this.Delete()
                        );
                }
                return _delete;
            }
        }

        public ICommand ClearCommand
        {
            get
            {
                if (_clear == null)
                {
                    _clear = new RelayCommand(
                        param => this.Clear()
                        );
                }
                return _clear;
            }
        }

        public ICommand AddFamilyCommand
        {
            get
            {
                if (_addFamily == null)
                {
                    _addFamily = new RelayCommand(
                        param => this.AddFamily()
                        );
                }
                return _addFamily;
            }
        }

        public ICommand RemoveFamilyCommand
        {
            get
            {
                if (_removeFamily == null)
                {
                    _removeFamily = new RelayCommand(
                        param => this.RemoveFamily()
                        );
                }
                return _removeFamily;
            }
        }

        public ICommand AssignJobCommand
        {
            get
            {
                if (_assignJob == null)
                {
                    _assignJob = new RelayCommand(
                        param => this.AssignJob()
                        );
                }
                return _assignJob;
            }
        }

        public ICommand RemoveJobCommand
        {
            get
            {
                if (_removeJob == null)
                {
                    _removeJob = new RelayCommand(
                        param => this.RemoveJob()
                        );
                }
                return _removeJob;
            }
        }

        private void AssignJob()
        {
            if (_displayEmployee == null || _selectedJobOption == null) return;
            if (_displayEmployee.Jobs.Contains(_selectedJobOption)) return;

            _persMgr.AddJob(_displayEmployee, _selectedJobOption);
            base.OnPropertyChanged("EmployeesJobs");
        }

        private void RemoveJob()
        {
            if (_displayEmployee == null || _selectedJobOption == null) return;
            if (_displayEmployee.Jobs.Contains(_selectedJobOption))
            {
                _persMgr.RemoveJob(_displayEmployee, _selectedJobOption);
                base.OnPropertyChanged("EmployeesJobs");
            }
            return;
        }

        private void AddFamily()
        {
            // Fires, but needs to be implemented. If both valid, don't equal each other, and doesn't contain it already then add
            if (_displayEmployee == null || _addFamilyPerson == null) return;
            if (_displayEmployee.Equals(_addFamilyPerson)) return;
            if (_displayEmployee.Family.Contains(_addFamilyPerson)) return;

            _persMgr.AddFamily(_displayEmployee, _addFamilyPerson);
            base.OnPropertyChanged("Family");
        }

        private void RemoveFamily()
        {
            if (_displayEmployee == null || _addFamilyPerson == null) return;
            if (_displayEmployee.Equals(_addFamilyPerson)) return;
            if (_displayEmployee.Family.Contains(_addFamilyPerson))
            {
                _persMgr.RemoveFamily(_displayEmployee, _addFamilyPerson);
                base.OnPropertyChanged("Family");
            }
            return;
        }

        private void Delete()
        {
            try
            {
                // Store name to call the actual delete method
                string firstName = _displayEmployee.FirstName;
                string lastName = _displayEmployee.LastName;

                // Update the screen to make it blank
                _displayEmployee = new Employee();
                base.OnPropertyChanged("FirstName");
                base.OnPropertyChanged("LastName");
                base.OnPropertyChanged("PhoneNumber");
                base.OnPropertyChanged("Email");
                base.OnPropertyChanged("GenderType");
                base.OnPropertyChanged("Street");
                base.OnPropertyChanged("City");
                base.OnPropertyChanged("State");
                base.OnPropertyChanged("ZipCode");
                base.OnPropertyChanged("RankType");
                base.OnPropertyChanged("EmployeesJobs");
                AddFamilyPerson = new Person();
                SelectedJobOption = new Job();

                // Delete the employee, update employees collection, and fire change event
                if (_persMgr.DeletePerson(firstName, lastName))
                {
                    _employees = new CollectionView(_persMgr.GetEmployees());
                    base.OnPropertyChanged("Employees");
                    _familyOptions = new CollectionView(_persMgr.GetPeople());
                    base.OnPropertyChanged("FamilyOptions");
                    _jobOptions = new CollectionView(_jobMgr.GetAllJobs());
                    base.OnPropertyChanged("JobOptions");

                    // Success
                    _labelMessage = String.Format("Deleted {0} {1}", firstName, lastName);
                    base.OnPropertyChanged("LabelMessage");
                }
                else
                {
                    // Error
                    _labelMessage = String.Format("Unable to Delete {0} {1}", firstName, lastName);
                    base.OnPropertyChanged("LabelMessage");
                }

            }
            catch (Exception e)
            {
                _labelMessage = String.Format("Error in Delete Message");
                base.OnPropertyChanged("LabelMessage");
            }
        }

        private void Clear()
        {
            // Just update collection list and display a blank employee            
            _employees = new CollectionView(_persMgr.GetEmployees());
            base.OnPropertyChanged("Employees");

            _familyOptions = new CollectionView(_persMgr.GetPeople());
            base.OnPropertyChanged("FamilyOptions");

            _jobOptions = new CollectionView(_jobMgr.GetAllJobs());
            base.OnPropertyChanged("JobOptions");

            DisplayEmployee = new Employee();
        }
    }
}
