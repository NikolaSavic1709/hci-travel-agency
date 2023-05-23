using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using travelAgency.model;
using travelAgency;
using Microsoft.EntityFrameworkCore;

namespace travelAgency
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (var db=new TravelAgencyContext())
            {
                /*db.Database.EnsureCreated();
                db.SaveChanges();*/
                int journeyId = 1; // Specify the journey ID
                Journey journey = db.Journeys.Include(j => j.JourneyPlaces)
                    .ThenInclude(jp=>jp.Place)
                   .FirstOrDefault(j => j.Id == journeyId);

                if (journey != null)
                {
                    List<Place> places = journey.JourneyPlaces.Select(jp => jp.Place).ToList();

                }
            }
        }
    }
}
