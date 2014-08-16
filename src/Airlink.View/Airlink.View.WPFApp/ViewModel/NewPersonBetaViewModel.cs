using Airlink.Model.Domain;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using Airlink.Model.Business;

namespace Airlink.View.WPFApp.ViewModel
{
    // Wrapper for New Person UI
    public class NewPersonBetaViewModel : WorkspaceViewModel
    {
        private Person _person;
        private PersonnelManager _persMgr;
        private string _labelMessage;
        RelayCommand _submit;
        RelayCommand _clear;

        public NewPersonBetaViewModel(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException("person");
            }

            _person = person;
        }

        public NewPersonBetaViewModel(Person person, PersonnelManager persMgr)
        {
            if (person == null)
            {
                throw new ArgumentNullException("person");
            }

            _person = person;
            _persMgr = persMgr;
        }

        // Props
        public string FirstName
        {
            get { return _person.FirstName; }
            set
            {
                if (value == _person.FirstName)
                    return;

                _person.FirstName = value;

                base.OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return _person.LastName; }
            set
            {
                if (value == _person.LastName)
                    return;

                _person.LastName = value;

                base.OnPropertyChanged("LastName");
            }
        }

        public string PhoneNumber
        {
            get { return _person.PhoneNumber; }
            set
            {
                if (value == _person.PhoneNumber)
                    return;

                _person.PhoneNumber = value;

                base.OnPropertyChanged("PhoneNumber");
            }
        }

        public string Email
        {
            get { return _person.Email; }
            set
            {
                if (value == _person.Email)
                    return;

                _person.Email = value;

                base.OnPropertyChanged("Email");
            }
        }

        // Gender type for selected item to bind to
        public Gender GenderType
        {
            get { return _person.Gender; }
            set
            {
                _person.Gender = value;
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
            get { return _person.Address.Street; }
            set
            {
                if (value == _person.Address.Street)
                    return;

                _person.Address.Street = value;

                base.OnPropertyChanged("Address.Street");
            }
        }

        public string City
        {
            get { return _person.Address.City; }
            set
            {
                if (value == _person.Address.City)
                    return;

                _person.Address.City = value;

                base.OnPropertyChanged("Address.City");
            }
        }

        public string State
        {
            get { return _person.Address.State; }
            set
            {
                if (value == _person.Address.State)
                    return;

                _person.Address.State = value;

                base.OnPropertyChanged("Address.State");
            }
        }

        public string ZipCode
        {
            get { return _person.Address.ZipCode; }
            set
            {
                if (value == _person.Address.ZipCode)
                    return;

                _person.Address.ZipCode = value;

                base.OnPropertyChanged("Address.ZipCode");
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

        // Saves the customer to the repository.  This method is invoked by the SaveCommand.
        private void Save()
        {
            if (!_person.Validate())
            {
                _labelMessage = "Cannot Save, Invalid Person";
                base.OnPropertyChanged("LabelMessage");
                return;
            }

            if (this.IsNewPerson)
            {
                if (_persMgr.SavePerson(_person))
                {
                    // Success
                    _labelMessage = String.Format("Saved {0} {1}", _person.FirstName, _person.LastName);
                    base.OnPropertyChanged("LabelMessage");
                }
                else
                {
                    // Error
                    _labelMessage = String.Format("Error Occured Saving Person");
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

        // Clears the customer in the view. This method is invoked by the ClearCommand
        private void Clear()
        {
            _person = new Person();
            base.OnPropertyChanged("FirstName");
            base.OnPropertyChanged("LastName");
            base.OnPropertyChanged("PhoneNumber");
            base.OnPropertyChanged("Email");
            base.OnPropertyChanged("GenderType");
            base.OnPropertyChanged("Street");
            base.OnPropertyChanged("City");
            base.OnPropertyChanged("State");
            base.OnPropertyChanged("ZipCode");

            // Clear labelMessage also
            _labelMessage = "";
            base.OnPropertyChanged("LabelMessage");
        }

        // Returns true if this person isn't in db yet, false if person already exists
        // Based upon first, last name
        bool IsNewPerson
        {
            get
            {
                if (_persMgr.GetPerson(_person.FirstName, _person.LastName) == null) { return true; }
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
