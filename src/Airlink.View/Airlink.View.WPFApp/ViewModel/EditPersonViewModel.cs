using Airlink.Model.Business;
using Airlink.Model.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Airlink.View.WPFApp.ViewModel
{
    public class EditPersonViewModel : WorkspaceViewModel
    {
        private Person _displayPerson;
        private Person _addFamilyPerson;
        private CollectionView _people;
        private CollectionView _familyOptions;
        private PersonnelManager _persMgr;
        private string _labelMessage;

        RelayCommand _delete;
        RelayCommand _clear;
        RelayCommand _addFamily;
        RelayCommand _removeFamily;

        public EditPersonViewModel(PersonnelManager persMgr)
        {
            if (persMgr == null)
            {
                throw new ArgumentNullException("personnel manager");
            }
            this._persMgr = persMgr;

            // Need two collection views for the main selector and the 'family options' listbox
            IList<Person> p = _persMgr.GetPeople();
            this._people = new CollectionView(p);
            this._familyOptions = new CollectionView(p);
            this._displayPerson = new Person();
            this._addFamilyPerson = new Person();
        }

        // Props
        public CollectionView People
        {
            get { return _people; }
        }

        public CollectionView FamilyOptions
        {
            get { return _familyOptions; }
        }

        public Person DisplayPerson
        {
            get { return _displayPerson; }
            set
            {
                if (value == _displayPerson)
                {
                    // No change to display, just exit method
                    return;
                }
                else if (value == null)
                {
                    // null value, possibly empty person list. 
                    // Update the screen to make it blank and exit
                    _displayPerson = new Person();
                    base.OnPropertyChanged("FirstName");
                    base.OnPropertyChanged("LastName");
                    base.OnPropertyChanged("PhoneNumber");
                    base.OnPropertyChanged("Email");
                    base.OnPropertyChanged("GenderType");
                    base.OnPropertyChanged("Street");
                    base.OnPropertyChanged("City");
                    base.OnPropertyChanged("State");
                    base.OnPropertyChanged("ZipCode");
                    base.OnPropertyChanged("Family");
                    AddFamilyPerson = new Person();
                    return;
                }
                // Valid person. Make changes and update screen
                _displayPerson = value;
                base.OnPropertyChanged("DisplayPerson");
                base.OnPropertyChanged("FirstName");
                base.OnPropertyChanged("LastName");
                base.OnPropertyChanged("PhoneNumber");
                base.OnPropertyChanged("Email");
                base.OnPropertyChanged("GenderType");
                base.OnPropertyChanged("Street");
                base.OnPropertyChanged("City");
                base.OnPropertyChanged("State");
                base.OnPropertyChanged("ZipCode");
                base.OnPropertyChanged("Family");
                AddFamilyPerson = new Person();
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

        public string FirstName
        {
            get { return _displayPerson.FirstName; }
            set
            {
                if (value == _displayPerson.FirstName)
                    return;

                _displayPerson.FirstName = value;

                base.OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return _displayPerson.LastName; }
            set
            {
                if (value == _displayPerson.LastName)
                    return;

                _displayPerson.LastName = value;

                base.OnPropertyChanged("LastName");
            }
        }

        public string PhoneNumber
        {
            get { return _displayPerson.PhoneNumber; }
            set
            {
                if (value == _displayPerson.PhoneNumber)
                    return;

                _displayPerson.PhoneNumber = value;

                base.OnPropertyChanged("PhoneNumber");
            }
        }

        public string Email
        {
            get { return _displayPerson.Email; }
            set
            {
                if (value == _displayPerson.Email)
                    return;

                _displayPerson.Email = value;

                base.OnPropertyChanged("Email");
            }
        }

        // Gender type for selected item to bind to
        public Gender GenderType
        {
            get { return _displayPerson.Gender; }
            set
            {
                _displayPerson.Gender = value;
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
            get { return _displayPerson.Address.Street; }
            set
            {
                if (value == _displayPerson.Address.Street)
                    return;

                _displayPerson.Address.Street = value;

                base.OnPropertyChanged("Address.Street");
            }
        }

        public string City
        {
            get { return _displayPerson.Address.City; }
            set
            {
                if (value == _displayPerson.Address.City)
                    return;

                _displayPerson.Address.City = value;

                base.OnPropertyChanged("Address.City");
            }
        }

        public string State
        {
            get { return _displayPerson.Address.State; }
            set
            {
                if (value == _displayPerson.Address.State)
                    return;

                _displayPerson.Address.State = value;

                base.OnPropertyChanged("Address.State");
            }
        }

        public string ZipCode
        {
            get { return _displayPerson.Address.ZipCode; }
            set
            {
                if (value == _displayPerson.Address.ZipCode)
                    return;

                _displayPerson.Address.ZipCode = value;

                base.OnPropertyChanged("Address.ZipCode");
            }
        }

        public CollectionView Family
        {
            get { return new CollectionView(_displayPerson.Family); }
            // Not sure if this actually does anything...
            /*set
            {
                if (value == _displayPerson.Family)
                    return;
                try
                {
                    _displayPerson.Family = (List<Person>)value;
                    base.OnPropertyChanged("Family");
                }
                catch (Exception e) 
                {
                    Console.WriteLine("Exception thrown in set Family method :(");
                }                

            }*/
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

        private void AddFamily()
        {
            // Fires, but needs to be implemented. If both valid, don't equal each other, and doesn't contain it already then add
            if (_displayPerson == null || _addFamilyPerson == null) return;
            if (_displayPerson.Equals(_addFamilyPerson)) return;
            if (_displayPerson.Family.Contains(_addFamilyPerson)) return;

            _persMgr.AddFamily(_displayPerson, _addFamilyPerson);
            base.OnPropertyChanged("Family");
        }

        private void RemoveFamily()
        {
            if (_displayPerson == null || _addFamilyPerson == null) return;
            if (_displayPerson.Equals(_addFamilyPerson)) return;
            if (_displayPerson.Family.Contains(_addFamilyPerson)) 
            {
                _persMgr.RemoveFamily(_displayPerson, _addFamilyPerson);
                base.OnPropertyChanged("Family");
            }
            return;
        }

        private void Delete()
        {
            try
            {
                // Store name to call the actual delete method
                string firstName = _displayPerson.FirstName;
                string lastName = _displayPerson.LastName;

                // Update the screen to make it blank
                _displayPerson = new Person();
                base.OnPropertyChanged("FirstName");
                base.OnPropertyChanged("LastName");
                base.OnPropertyChanged("PhoneNumber");
                base.OnPropertyChanged("Email");
                base.OnPropertyChanged("GenderType");
                base.OnPropertyChanged("Street");
                base.OnPropertyChanged("City");
                base.OnPropertyChanged("State");
                base.OnPropertyChanged("ZipCode");
                AddFamilyPerson = new Person();

                // Delete the person, update people collection, and fire change event
                if (_persMgr.DeletePerson(firstName, lastName))
                {
                    IList<Person> p = _persMgr.GetPeople();
                    _people = new CollectionView(p);
                    base.OnPropertyChanged("People");

                    _familyOptions = new CollectionView(p);
                    base.OnPropertyChanged("FamilyOptions");
                    AddFamilyPerson = new Person();

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
            // Just update collection list and display a blank person     
            IList<Person> p = _persMgr.GetPeople();
            _people = new CollectionView(p);
            base.OnPropertyChanged("People");

            _familyOptions = new CollectionView(p);
            base.OnPropertyChanged("FamilyOptions");

            DisplayPerson = new Person();
        }
    }
}
