using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using travelAgency.controls;
using travelAgency.Dialogs;
using travelAgency.repository;

namespace travelAgency.view;

/// <summary>
/// Interaction logic for ClientHome.xaml
/// </summary>
public partial class ClientHome : Window
{
    public TravelAgencyContext dbContext;
    public TripRepository tripRepository;
    public PlaceRepository placeRepository;
    public ClientHome()
    {
        InitializeComponent();
        dbContext = new TravelAgencyContext();
        tripRepository = new TripRepository(dbContext);
        placeRepository = new PlaceRepository(dbContext);
        HomeButton.IsClicked = "True";
        Main.Content = new ClientHomePage();
    }

    private IEnumerable<DependencyObject> GetChildren(DependencyObject parent)
    {
        if (parent == null)
            yield break;

        int childCount = VisualTreeHelper.GetChildrenCount(parent);
        for (int i = 0; i < childCount; i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            yield return child;
        }
    }

    private void NavbarButton_Click(object sender, RoutedEventArgs e)
    {
        NavbarButton button = (NavbarButton)sender;
        DeselectNavbarButtons();
        button.IsClicked = "True";
        switch (button.Name)
        {
            case "HomeButton":
                Main.Content = new ClientHomePage();
                break;
            case "HistoryButton":
                Main.Content = new HistoryPage(null);
                break;

            default:
                break;
        }
    }

    private void DeselectNavbarButtons()
    {
        if (NavbarButtons != null)
        {
            foreach (var child in GetChildren(NavbarButtons))
            {
                ((NavbarButton)child).IsClicked = "False";
            }
        }
    }

    private void LogoutButton_Click(object sender, RoutedEventArgs e)
    {
        var logoutDialog = new LogoutDialog();

        logoutDialog.DialogResultEvent += DialogWindow_DialogResultEvent;

        logoutDialog.ShowDialog();
    }

    private void DialogWindow_DialogResultEvent(object sender, DialogResultEventArgs e)
    {
        bool result = e.Result;
        if (result)
        {
            Window loginwindow = new RegistrationWindow();
            loginwindow.Show();
            this.Close();
        }
    }
}