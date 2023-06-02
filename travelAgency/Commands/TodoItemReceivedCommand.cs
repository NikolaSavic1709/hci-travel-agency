using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travelAgency.ViewModel;

namespace travelAgency.Commands
{
    public class TodoItemReceivedCommand : CommandBase
    {
        private readonly IconItemListingViewModel _todoItemListingViewModel;

        public TodoItemReceivedCommand(IconItemListingViewModel todoItemListingViewModel)
        {
            _todoItemListingViewModel = todoItemListingViewModel;
        }

        public override void Execute(object parameter)
        {
            _todoItemListingViewModel.AddTodoItem(_todoItemListingViewModel.IncomingTodoItemViewModel);
        }
    }
}
