using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using travelAgency.model;
using DevExpress.XtraRichEdit.API.Native.Implementation;
using System.Collections.ObjectModel;

namespace travelAgency.ViewModel
{
    public class AmenitiesDialogViewModel: ViewModelBase
    {

        public IconItemListingViewModel ActiveIconItemListingViewModel { get; set; }
        public IconItemListingViewModel RemainingIconItemListingViewModel { get; set; }

        public AmenitiesDialogViewModel()
        {
            // :TODO add real data from db

            IconItemListingViewModel activeIconItemListingViewModel = new IconItemListingViewModel();
            for (int i = 0; i < 10; i++)
            {
                activeIconItemListingViewModel.AddTodoItem(new IconItemViewModel(i));
            }
            ActiveIconItemListingViewModel = activeIconItemListingViewModel;

            IconItemListingViewModel remainingIconItemListingViewModel = new IconItemListingViewModel();
            for (int i = 0; i < 10; i++)
            {
                remainingIconItemListingViewModel.AddTodoItem(new IconItemViewModel(i));
            }
            RemainingIconItemListingViewModel = remainingIconItemListingViewModel;

        }

        public void RemoveAll()
        {
            ObservableCollection<IconItemViewModel> movedIconItemViewModels = ActiveIconItemListingViewModel.RemoveAllTodoItems();
            foreach(IconItemViewModel item in movedIconItemViewModels)
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
