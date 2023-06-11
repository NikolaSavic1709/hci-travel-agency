using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travelAgency.model;

namespace travelAgency.ViewModel;

class PlaceDetailsViewModel : INotifyPropertyChanged
{
    private Place _place;
    public Place Trip
    {
        get { return _place; }
        set
        {
            _place = value;
            OnPropertyChanged(nameof(Trip));
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
