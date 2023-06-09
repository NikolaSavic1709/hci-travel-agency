using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travelAgency.model;

namespace travelAgency.ViewModel
{
    class CreateTripViewModel: INotifyPropertyChanged
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
