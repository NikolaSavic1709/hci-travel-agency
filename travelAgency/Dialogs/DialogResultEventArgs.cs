using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelAgency.Dialogs
{
    public class DialogResultEventArgs : EventArgs
    {
        public bool Result { get; }

        public DialogResultEventArgs(bool result)
        {
            Result = result;
        }
    }
}
