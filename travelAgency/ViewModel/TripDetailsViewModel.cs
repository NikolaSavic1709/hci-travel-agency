using System.ComponentModel;
using travelAgency.model;

namespace travelAgency.ViewModel
{
    public class TripDetailsViewModel : INotifyPropertyChanged
    {
        private Trip _trip;

        public Trip Trip
        {
            get { return _trip; }
            set
            {
                _trip = value;
                OnPropertyChanged(nameof(Trip));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}