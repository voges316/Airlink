using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airlink.Model.Business;
using Airlink.Model.Domain;
using System.Windows.Input;
using System.Windows.Data;

namespace Airlink.View.WPFApp.ViewModel
{
    // Wrapper for New Employee UI
    public class NewEmployeeViewModel : WorkspaceViewModel
    {
        private Employee _employee;
        private CollectionView _flights;
        private PersonnelManager _persMgr;
        private FlightManager _fltMgr;
        private string _labelMessage;

        RelayCommand _submit;
        RelayCommand _clear;

        public NewEmployeeViewModel(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("employee");
            }

            _employee = employee;
        }

        public NewEmployeeViewModel(Employee employee, PersonnelManager persMgr, FlightManager fltMgr)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("employee");
            }

            _employee = employee;
            _persMgr = persMgr;
            _fltMgr = fltMgr;
            
            IList<Flight> f = _fltMgr.GetAllFlights();
            this._flights = new CollectionView(f);
        }

        // Props
        public CollectionView Flights
        {
            get { return _flights; }
        }

        public string FirstName
        {
            get { return _employee.FirstName; }
            set
            {
                if (value == _employee.FirstName)
                    return;

                _employee.FirstName = value;

                base.OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return _employee.LastName; }
            set
            {
                if (value == _employee.LastName)
                    return;

                _employee.LastName = value;

                base.OnPropertyChanged("LastName");
            }
        }

        public string PhoneNumber
        {
            get { return _employee.PhoneNumber; }
            set
            {
                if (value == _employee.PhoneNumber)
                    return;

                _employee.PhoneNumber = value;

                base.OnPropertyChanged("PhoneNumber");
            }
        }

        public string Email
        {
            get { return _employee.Email; }
            set
            {
                if (value == _employee.Email)
                    return;

                _employee.Email = value;

                base.OnPropertyChanged("Email");
            }
        }

        // Gender type for selected item to bind to
        public Gender GenderType
        {
            get { return _employee.Gender; }
            set
            {
                _employee.Gender = value;
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
            get { return _employee.Address.Street; }
            set
            {
                if (value == _employee.Address.Street)
                    return;

                _employee.Address.Street = value;

                base.OnPropertyChanged("Address.Street");
            }
        }

        public string City
        {
            get { return _employee.Address.City; }
            set
            {
                if (value == _employee.Address.City)
                    return;

                _employee.Address.City = value;

                base.OnPropertyChanged("Address.City");
            }
        }

        public string State
        {
            get { return _employee.Address.State; }
            set
            {
                if (value == _employee.Address.State)
                    return;

                _employee.Address.State = value;

                base.OnPropertyChanged("Address.State");
            }
        }

        public string ZipCode
        {
            get { return _employee.Address.ZipCode; }
            set
            {
                if (value == _employee.Address.ZipCode)
                    return;

                _employee.Address.ZipCode = value;

                base.OnPropertyChanged("Address.ZipCode");
            }
        }

        // Rank type for selected item to bind to
        public Rank RankType
        {
            get { return _employee.EmpRank; }
            set
            {
                _employee.EmpRank = value;
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

        public ICommand SubmitCommand
        {
            get
            {
                if (_submit == null)
                {
                    _submit = new RelayCommand(
                        param => this.Save(),
                        param => this.CanSave
                        );
                }
                return _submit;
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

        // Saves the employee to the repository.  This method is invoked by the SaveCommand.
        private void Save()
        {
            if (!_employee.Validate())
            {
                _labelMessage = "Cannot Save, Invalid Employee";
                base.OnPropertyChanged("LabelMessage");
                return;
            }                

            if (this.IsNewPerson)
            {
                if (_persMgr.SavePerson(_employee))
                {
                    // Success
                    _labelMessage = String.Format("Saved {0} {1}", _employee.FirstName, _employee.LastName);
                    base.OnPropertyChanged("LabelMessage");
                }
                else
                {
                    // Error
                    _labelMessage = String.Format("Error Occured Saving Employee");
                    base.OnPropertyChanged("LabelMessage");
                }
            }
            else
            {
                // Person already exists
                _labelMessage = String.Format("Person already exists");
                base.OnPropertyChanged("LabelMessage");
            }

        }

        // Clears the employee in the view. This method is invoked by the ClearCommand
        private void Clear()
        {
            _employee = new Employee();
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

            // Clear labelMessage also
            _labelMessage = "";
            base.OnPropertyChanged("LabelMessage");
        }

        // Returns true if this person isn't in db yet, false if person already exists
        // Based upon first, last name
        bool IsNewPerson
        {
            // Checks for person in db
            get
            {
                if (_persMgr.GetPerson(_employee.FirstName, _employee.LastName) == null) { return true; }
                else { return false; }
            }
        }

        // Returns true if the customer is valid and can be saved.
        private bool CanSave
        {
            get { return true; /* String.IsNullOrEmpty(this.ValidateCustomerType()) && _customer.IsValid;*/ }
        }
    }
}
