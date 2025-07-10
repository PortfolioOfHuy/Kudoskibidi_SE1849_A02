using KudoskibidiWPF.Pages.Admin;
using System.Windows;

namespace KudoskibidiWPF
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        private readonly Lazy<CustomerPage> _customerPage = new(() => new CustomerPage());
        private readonly Lazy<RoomPage> _roomPage = new(() => new RoomPage());
        private readonly Lazy<BookingPage> _bookingPage = new(() => new BookingPage());
        private readonly Lazy<ReportPage> _reportPage = new(() => new ReportPage());

        public AdminDashboard()
        {
            InitializeComponent();
            MainFrame.Content = new CustomerPage();
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_customerPage.Value);
        }

        private void btnRoom_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_roomPage.Value);
        }

        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_bookingPage.Value);
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_reportPage.Value);
        }
    }
}
