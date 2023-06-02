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

namespace travelAgency.ViewModel
{
    public class AmenitiesDialogViewModel: ViewModelBase
    {
        private readonly Lazy<IEnumerable<PackIconKindGroup>> _activePackIconKinds;
        private readonly Lazy<IEnumerable<PackIconKindGroup>> _remainingPackIconKinds;

        public AmenitiesDialogViewModel()
        {

            //_packIconKinds = new Lazy<IEnumerable<PackIconKindGroup>>(() =>
            //    Enum.GetNames(typeof(AmenityIconKind))
            //        .Select(g => new PackIconKindGroup(g))
            //        .ToList());

            _activePackIconKinds = new Lazy<IEnumerable<PackIconKindGroup>>(() =>
               Enumerable.Range(0, 10)
                   .Select(g => new PackIconKindGroup(g))
                   .ToList());

            _remainingPackIconKinds = new Lazy<IEnumerable<PackIconKindGroup>>(() =>
               Enumerable.Range(0, 10)
                   .Select(g => new PackIconKindGroup(g))
                   .ToList());
        }


        private IEnumerable<PackIconKindGroup>? _activeKinds;
        private PackIconKindGroup? _activeGroup;
        private string? _activeKind;
        private PackIconKind _activePackIconKind;

        public IEnumerable<PackIconKindGroup> ActiveKinds
        {
            get => _activeKinds ??= _activePackIconKinds.Value;
            set => SetProperty(ref _activeKinds, value);
        }

        public PackIconKindGroup? ActiveGroup
        {
            get => _activeGroup;
            set
            {
                if (SetProperty(ref _activeGroup, value))
                {
                    ActiveKind = value?.Kind;
                }
            }
        }

        public string? ActiveKind
        {
            get => _activeKind;
            set
            {
                if (SetProperty(ref _activeKind, value))
                {
                    ActivePackIconKind = value != null ? (PackIconKind)Enum.Parse(typeof(PackIconKind), value) : default;
                }
            }
        }

        public PackIconKind ActivePackIconKind
        {
            get => _activePackIconKind;
            set => SetProperty(ref _activePackIconKind, value);
        }


        //////////////////////////////////////// reamining

        private IEnumerable<PackIconKindGroup>? _remainingKinds;
        private PackIconKindGroup? _remainingGroup;
        private string? _remainingKind;
        private PackIconKind _remainingPackIconKind;

        public IEnumerable<PackIconKindGroup> RemainingKinds
        {
            get => _remainingKinds ??= _remainingPackIconKinds.Value;
            set => SetProperty(ref _remainingKinds, value);
        }

        public PackIconKindGroup? RemainingGroup
        {
            get => _remainingGroup;
            set
            {
                if (SetProperty(ref _remainingGroup, value))
                {
                    RemainingKind = value?.Kind;
                }
            }
        }

        public string? RemainingKind
        {
            get => _remainingKind;
            set
            {
                if (SetProperty(ref _remainingKind, value))
                {
                    RemainingPackIconKind = value != null ? (PackIconKind)Enum.Parse(typeof(PackIconKind), value) : default;
                }
            }
        }

        public PackIconKind RemainingPackIconKind
        {
            get => _remainingPackIconKind;
            set => SetProperty(ref _remainingPackIconKind, value);
        }


    }
}
