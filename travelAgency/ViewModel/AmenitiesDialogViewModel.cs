using System.Collections.Generic;
using System.Collections.ObjectModel;
using travelAgency.model;

namespace travelAgency.ViewModel
{
    public class AmenitiesDialogViewModel : ViewModelBase
    {
        public IconItemListingViewModel ActiveIconItemListingViewModel { get; set; }
        public IconItemListingViewModel RemainingIconItemListingViewModel { get; set; }

        public AmenitiesDialogViewModel(Stay stay)
        {

            IconItemListingViewModel activeIconItemListingViewModel = new IconItemListingViewModel();
            List<int> amenitiesIdxs = new List<int>();

            for (int i = 0; i < stay.StayAmenities.Count; i++)
            {
                int amenityIdx = (int)stay.StayAmenities[i].amenity;
                amenitiesIdxs.Add(amenityIdx);
                activeIconItemListingViewModel.AddTodoItem(new IconItemViewModel(amenityIdx));
            }
            ActiveIconItemListingViewModel = activeIconItemListingViewModel;

            IconItemListingViewModel remainingIconItemListingViewModel = new IconItemListingViewModel();
            for (int i = 0; i < 10; i++)
            {
                if (!amenitiesIdxs.Contains(i))
                {
                    remainingIconItemListingViewModel.AddTodoItem(new IconItemViewModel(i));
                }
                
            }
            RemainingIconItemListingViewModel = remainingIconItemListingViewModel;
        }

        public void RemoveAll()
        {
            ObservableCollection<IconItemViewModel> movedIconItemViewModels = ActiveIconItemListingViewModel.RemoveAllTodoItems();
            foreach (IconItemViewModel item in movedIconItemViewModels)
            {
                RemainingIconItemListingViewModel.AddTodoItem(item);
            }
        }

        public void AddAll()
        {
            ObservableCollection<IconItemViewModel> movedIconItemViewModels = RemainingIconItemListingViewModel.RemoveAllTodoItems();
            foreach (IconItemViewModel item in movedIconItemViewModels)
            {
                ActiveIconItemListingViewModel.AddTodoItem(item);
            }
        }
    }
}