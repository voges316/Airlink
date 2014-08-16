using Airlink.Model.Business;
using Airlink.Model.Domain;
using System;
using System.Windows.Data;
using System.Windows.Input;

namespace Airlink.View.WPFApp.ViewModel
{
    public class JobMenuViewModel : WorkspaceViewModel
    {
        private Job _job;
        private CollectionView _jobs;
        private Job _selectedJob;
        private JobManager _jobMgr;
        private string _labelMessage;

        RelayCommand _add;
        RelayCommand _delete;
        RelayCommand _clear;

        public JobMenuViewModel(JobManager jobManager)
        {
            if (jobManager == null)
            {
                throw new ArgumentNullException("job manager");
            }

            this._jobMgr = jobManager;
            this._job = new Job();
            this._jobs = new CollectionView(_jobMgr.GetAllJobs());
        }

        // Props
        public string Name
        {
            get { return _job.Name; }
            set
            {
                if (value == _job.Name)
                    return;

                _job.Name = value;

                base.OnPropertyChanged("Name");
            }
        }

        public CollectionView Jobs
        {
            get { return _jobs; }
        }

        public Job SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                if (value == _selectedJob)
                    return;

                _selectedJob = value;
                base.OnPropertyChanged("SelectedJob");
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

        // Commands
        public ICommand AddCommand
        {
            get
            {
                if (_add == null)
                {
                    _add = new RelayCommand(
                        param => this.Add()
                        );
                }
                return _add;
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

        private void Add()
        {
            if (!_job.Validate())
            {
                _labelMessage = "Cannot Save, Invalid Job";
                base.OnPropertyChanged("LabelMessage");
                return;
            }
            if (this.IsNewJob)
            {
                if (_jobMgr.SaveJob(_job))
                {
                    // Success
                    _labelMessage = String.Format("Saved {0}", _job.Name);
                    base.OnPropertyChanged("LabelMessage");

                    _jobs = new CollectionView(_jobMgr.GetAllJobs());
                    base.OnPropertyChanged("Jobs");

                    _job = new Job();
                    base.OnPropertyChanged("Name"); 
                }
                else
                {
                    // Error
                    _labelMessage = String.Format("Error Occured Saving Job");
                    base.OnPropertyChanged("LabelMessage");
                }
            }
            else
            {
                // Job already exists
                _labelMessage = String.Format("Job already exists");
                base.OnPropertyChanged("LabelMessage");
            }
        }

        private void Delete()
        {
            if (_selectedJob == null)
            {
                _labelMessage = String.Format("Please first select a valid person to delete");
                base.OnPropertyChanged("LabelMessage");
                return;
            }
            else if (_selectedJob.Validate())
            {
                string name = _selectedJob.Name;
                if (_jobMgr.DeleteJob(_selectedJob.Name))
                {
                    _jobs = new CollectionView(_jobMgr.GetAllJobs());
                    base.OnPropertyChanged("Jobs");
                    _selectedJob = null;

                    // Success
                    _labelMessage = String.Format("Deleted {0}", name);
                    base.OnPropertyChanged("LabelMessage");

                    return;
                }
                else
                {
                    // Error
                    _labelMessage = String.Format("Unable to Delete {0}", name);
                    base.OnPropertyChanged("LabelMessage");
                }
            }
        }

        private void Clear()
        {

        }

        // Returns true if this job isn't in db yet, false if job already exists
        // Based upon name
        bool IsNewJob
        {
            get
            {
                if (_jobMgr.GetJob(_job.Name) == null) { return true; }
                else { return false; }
            }
        }
    }
}
