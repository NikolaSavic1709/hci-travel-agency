using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using travelAgency.components;

namespace travelAgency.Dialogs
{
    public class ReportCardEventArgs : EventArgs
    {
        public List<ReportCard> ReportCards { get; }

        public ReportCardEventArgs(List<ReportCard> reportCards)
        {
            ReportCards = reportCards;
        }
    }
}
