using System;
using System.ComponentModel;
using travelAgency.model;

namespace travelAgency.ViewModel
{
    public class CreateTripViewModel : INotifyPropertyChanged
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
        public string PlaceName { get; set; }
        private DateTime? _date;
        public DateTime? Date { get { return _date; } set
            {
                _date = value;
            }
        }
        public DateTime? Time { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}