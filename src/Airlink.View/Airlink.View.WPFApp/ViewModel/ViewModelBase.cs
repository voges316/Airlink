using System;
using System.ComponentModel;

namespace Airlink.View.WPFApp.ViewModel
{
    // Base class for all viewmodels. Allows propertychange notification
    // and disposing that child classes can implement
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        protected ViewModelBase() { }

        // Raised when property on the object has new value
        public event PropertyChangedEventHandler PropertyChanged;

        // Raises the obj's PropertyChanged event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        // Invoked when being removed and will be subject to GC
        public void Dispose()
        {
            this.OnDispose();
        }

        // For child classes to clean up and remove event handlers
        protected virtual void OnDispose()
        {
            Console.WriteLine("Inside OnDispose method: {0}", this.ToString());
        }
    }
}
