using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travelAgency.ViewModel;

namespace travelAgency.Commands
{
    public class TodoItemInsertedCommand : CommandBase
    {
        private readonly IconItemListingViewModel _viewModel;

        public TodoItemInsertedCommand(IconItemListingViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.InsertTodoItem(_viewModel.InsertedTodoItemViewModel, _viewModel.TargetTodoItemViewModel);
        }
    }
}
