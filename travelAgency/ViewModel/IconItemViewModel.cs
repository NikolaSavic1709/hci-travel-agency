using System;
using travelAgency.model;

namespace travelAgency.ViewModel
{
    public class IconItemViewModel : ViewModelBase
    {
        public IconItemViewModel(int kindValue)
        {
            if (kindValue > 9) throw new ArgumentNullException(nameof(kindValue));
            AmenityEnum amenity = (AmenityEnum)kindValue;
            AmenityIconKind amenityIconKind = (AmenityIconKind)kindValue;

            KindValue = kindValue;

            Kind = amenityIconKind.ToString();

            Aliases = amenity.ToString();
        }

        private int _kindValue;

        public int KindValue
        {
            get
            {
                return _kindValue;
            }
            set
            {
                _kindValue = value;
                OnPropertyChanged(nameof(KindValue));
            }
        }

        private string _kind;

        public string Kind
        {
            get
            {
                return _kind;
            }
            set
            {
                _kind = value;
                OnPropertyChanged(nameof(Kind));
            }
        }

        private string _aliases;

        public string Aliases
        {
            get
            {
                return _aliases;
            }
            set
            {
                _aliases = value;
                OnPropertyChanged(nameof(Aliases));
            }
        }
    }
}