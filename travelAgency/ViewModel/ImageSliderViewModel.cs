using System.ComponentModel;
using travelAgency.model;

namespace travelAgency.ViewModel
{
    public class ImageSliderViewModel : INotifyPropertyChanged
    {
        private string _frontImageSource;

        public string FrontImageSource
        {
            get { return _frontImageSource; }
            set
            {
                _frontImageSource = value;
                OnPropertyChanged(nameof(FrontImageSource));
            }
        }

        private string _backImageSource;

        public string BackImageSource
        {
            get { return _backImageSource; }
            set
            {
                _backImageSource = value;
                OnPropertyChanged(nameof(BackImageSource));
            }
        }

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