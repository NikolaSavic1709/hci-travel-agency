using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using travelAgency.Commands;

namespace travelAgency.ViewModel
{
    public class IconItemListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<IconItemViewModel> _todoItemViewModels;

        public IEnumerable<IconItemViewModel> TodoItemViewModels => _todoItemViewModels;

        private IconItemViewModel _incomingTodoItemViewModel;

        public IconItemViewModel IncomingTodoItemViewModel
        {
            get
            {
                return _incomingTodoItemViewModel;
            }
            set
            {
                _incomingTodoItemViewModel = value;
                OnPropertyChanged(nameof(IncomingTodoItemViewModel));
            }
        }

        private IconItemViewModel _removedTodoItemViewModel;

        public IconItemViewModel RemovedTodoItemViewModel
        {
            get
            {
                return _removedTodoItemViewModel;
            }
            set
            {
                _removedTodoItemViewModel = value;
                OnPropertyChanged(nameof(RemovedTodoItemViewModel));
            }
        }

        private IconItemViewModel _insertedTodoItemViewModel;

        public IconItemViewModel InsertedTodoItemViewModel
        {
            get
            {
                return _insertedTodoItemViewModel;
            }
            set
            {
                _insertedTodoItemViewModel = value;
                OnPropertyChanged(nameof(InsertedTodoItemViewModel));
            }
        }

        private IconItemViewModel _targetTodoItemViewModel;

        public IconItemViewModel TargetTodoItemViewModel
        {
            get
            {
                return _targetTodoItemViewModel;
            }
            set
            {
                _targetTodoItemViewModel = value;
                OnPropertyChanged(nameof(TargetTodoItemViewModel));
            }
        }

        public ICommand TodoItemReceivedCommand { get; }
        public ICommand TodoItemRemovedCommand { get; }
        public ICommand TodoItemInsertedCommand { get; }

        public IconItemListingViewModel()
        {
            _todoItemViewModels = new ObservableCollection<IconItemViewModel>();

            TodoItemReceivedCommand = new TodoItemReceivedCommand(this);
            TodoItemRemovedCommand = new TodoItemRemovedCommand(this);
            TodoItemInsertedCommand = new TodoItemInsertedCommand(this);
        }

        public void AddTodoItem(IconItemViewModel item)
        {
            if (!_todoItemViewModels.Contains(item))
            {
                _todoItemViewModels.Add(item);
            }
        }

        public ObservableCollection<IconItemViewModel> RemoveAllTodoItems()
        {
            ObservableCollection<IconItemViewModel> newTodoItemViewModels = new ObservableCollection<IconItemViewModel>();
            foreach (IconItemViewModel ii in _todoItemViewModels)
            {
                newTodoItemViewModels.Add(ii);
            }
            _todoItemViewModels.Clear();
            return newTodoItemViewModels;
        }

        public void InsertTodoItem(IconItemViewModel insertedTodoItem, IconItemViewModel targetTodoItem)
        {
            if (insertedTodoItem == targetTodoItem)
            {
                return;
            }

            int oldIndex = _todoItemViewModels.IndexOf(insertedTodoItem);
            int nextIndex = _todoItemViewModels.IndexOf(targetTodoItem);

            if (oldIndex != -1 && nextIndex != -1)
            {
                _todoItemViewModels.Move(oldIndex, nextIndex);
            }
        }

        public void RemoveTodoItem(IconItemViewModel item)
        {
            _todoItemViewModels.Remove(item);
        }
    }
}