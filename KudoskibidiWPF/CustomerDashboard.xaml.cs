using DAL.Entities;
using KudoskibidiWPF.Pages.Admin;
using KudoskibidiWPF.Pages.Customer;
using System.Windows;

namespace KudoskibidiWPF
{
    /// <summary>
    /// Interaction logic for CustomerDashboard.xaml
    /// </summary>
    public partial class CustomerDashboard : Window
    {
        private Customer _currentCustomer;

        public CustomerDashboard(Customer customer)
        {
            InitializeComponent();
            _currentCustomer = customer;
            MainFrame.Content = new ProfilePage(_currentCustomer);

        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow(); // Tên window đăng nhập của bạn
            loginWindow.Show();
            this.Close();
        }


        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ProfilePage(_currentCustomer);
        }

        private void btnBookingHistory_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
