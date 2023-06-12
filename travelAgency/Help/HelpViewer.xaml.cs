using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace travelAgency.Help
{
    /// <summary>
    /// Interaction logic for HelpViewer.xaml
    /// </summary>
    public partial class HelpViewer : Window
    {
        public HelpViewer(string key, Window originator)
        {
            InitializeComponent();
            string curDir = Directory.GetCurrentDirectory();

            int index = 1;
            for (int i = 0; i < 3; i++)
            {
                index = curDir.LastIndexOf('\\');
                if (index == -1)
                    break;
                curDir = curDir.Substring(0, index);
            }
            curDir += "\\Help\\docs\\";
            string path = curDir + key + ".htm";
            if (!File.Exists(path))
            {
                key = "error";
            }
            Uri u = new Uri("file:///"+path);

            wbHelp.Navigate(u);
        }

        private void wbHelp_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {

        }
    }
}
