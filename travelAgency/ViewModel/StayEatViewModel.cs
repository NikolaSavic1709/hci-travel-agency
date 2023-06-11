using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travelAgency.model;

namespace travelAgency.ViewModel
{
    public class StayEatViewModel:INotifyPropertyChanged
    {
        private Place _place;
        public Place Place
        {
            get { return _place; }
            set
            {
                _place = value;
                OnPropertyChanged(nameof(Place));
            }
        }

        private readonly ObservableCollection<IconItemViewModel> _todoItemViewModels;
        public IEnumerable<IconItemViewModel> TodoItemViewModels => _todoItemViewModels;

        public void AddTodoItem(IconItemViewModel item)
        {
            if (!_todoItemViewModels.Contains(item))
            {
                _todoItemViewModels.Add(item);
            }
        }

        public void ClearTodoItemViewModels()
        {
            _todoItemViewModels.Clear();
        }

        public StayEatViewModel(Place place)
        {
            _todoItemViewModels = new ObservableCollection<IconItemViewModel>();
            _place = place;
        }

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
