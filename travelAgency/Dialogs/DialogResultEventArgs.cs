using System;

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