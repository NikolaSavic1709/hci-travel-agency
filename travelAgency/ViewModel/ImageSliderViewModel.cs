using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelAgency.ViewModel
{
    public class ImageSliderViewModel :INotifyPropertyChanged
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
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
