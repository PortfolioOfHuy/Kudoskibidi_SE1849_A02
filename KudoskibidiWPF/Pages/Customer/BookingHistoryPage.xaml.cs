using BLL.Services;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace KudoskibidiWPF.Pages.Customer
{
    /// <summary>
    /// Interaction logic for BookingHistoryPage.xaml
    /// </summary>
    public partial class BookingHistoryPage : Page
    {
        private readonly BookingService _bookingService;
        private readonly DAL.Entities.Customer _currentCustomer;

        public BookingHistoryPage(DAL.Entities.Customer customer)
        {
            InitializeComponent();
            _currentCustomer = customer;
            _bookingService = new BookingService();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var bookings = _bookingService.GetBookingsByCustomerId(_currentCustomer.CustomerId);
            dgBookings.ItemsSource = bookings;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClearSelection_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgBookings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgBookings.SelectedItem is BookingReservation selected)
            {
                dgBookingDetails.ItemsSource = selected.BookingDetails;
            }
            else
            {
                dgBookingDetails.ItemsSource = null;
            }
        }
    }
}
