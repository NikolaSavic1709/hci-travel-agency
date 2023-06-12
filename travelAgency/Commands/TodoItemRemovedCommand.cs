using travelAgency.ViewModel;

namespace travelAgency.Commands
{
    public class TodoItemRemovedCommand : CommandBase
    {
        private readonly IconItemListingViewModel _todoItemListingViewModel;

        public TodoItemRemovedCommand(IconItemListingViewModel todoItemListingViewModel)
        {
            _todoItemListingViewModel = todoItemListingViewModel;
        }

        public override void Execute(object parameter)
        {
            _todoItemListingViewModel.RemoveTodoItem(_todoItemListingViewModel.RemovedTodoItemViewModel);
        }
    }
}