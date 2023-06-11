using System.ComponentModel;
using travelAgency.model;

namespace travelAgency.ViewModel;

internal class PlaceDetailsViewModel : INotifyPropertyChanged
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