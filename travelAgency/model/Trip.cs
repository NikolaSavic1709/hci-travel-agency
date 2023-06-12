using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace travelAgency.model
{

    public class Trip : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string description;
        private double price;
        private ObservableCollection<TripSchedule> schedules;

        public event PropertyChangedEventHandler PropertyChanged;

        [Key]
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public bool IsDeleted { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public double Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public ObservableCollection<TripSchedule> Schedules
        {
            get { return schedules; }
            set
            {
                schedules = value;
                OnPropertyChanged(nameof(Schedules));
            }
        }

        public Trip()
        {
            Schedules = new ObservableCollection<TripSchedule>();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddTripSchedule(TripSchedule tripSchedule)
        {
            this.Schedules.Add(tripSchedule);
            //this.Schedules = new ObservableCollection<TripSchedule>(this.Schedules.OrderBy(ts => ts.DateTime));
        }
    }
}