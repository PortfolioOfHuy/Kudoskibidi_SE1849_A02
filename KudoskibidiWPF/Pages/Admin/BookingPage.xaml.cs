using BLL.Services;
using DAL.Entities;
using KudoskibidiWPF.Windows;
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
using System.Windows.Shapes;

namespace KudoskibidiWPF.Pages.Admin
{
    /// <summary>
    /// Interaction logic for BookingPage.xaml
    /// </summary>
    public partial class BookingPage : Page
    {
        private BookingService _bookingService;

        public BookingPage()
        {
            InitializeComponent();
            _bookingService = new BookingService();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dgBookings.ItemsSource = _bookingService.GetAllBookings();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                dgBookings.ItemsSource = _bookingService.GetAllBookings();
                return;
            }

            var result = _bookingService.GetAllBookings()
                        .Where(b =>
                            b.Customer.CustomerFullName.ToLower().Contains(keyword) ||
                            b.BookingReservationId.ToString().Contains(keyword))
                        .ToList();

            dgBookings.ItemsSource = result;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddBookingWindow
            {
                Owner = Window.GetWindow(this)
            };

            if (addWindow.ShowDialog() == true && addWindow.NewBooking != null)
            {
                _bookingService.AddBooking(addWindow.NewBooking); // bạn cần viết phương thức này
                dgBookings.ItemsSource = _bookingService.GetAllBookings(); // Refresh
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgBookings.SelectedItem as BookingReservation;
            if (selected == null)
            {
                MessageBox.Show("Please select a booking to delete.");
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this booking?",
                                          "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (confirm == MessageBoxResult.Yes)
            {
                _bookingService.DeleteBooking(selected.BookingReservationId);
                dgBookings.ItemsSource = _bookingService.GetAllBookings();
                dgBookingDetails.ItemsSource = null;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            dgBookings.UnselectAll();
            dgBookingDetails.ItemsSource = null;
            txtSearch.Text = "";
        }

        private void dgBookings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedBooking = dgBookings.SelectedItem as BookingReservation;
            if (selectedBooking != null)
            {
                dgBookingDetails.ItemsSource = selectedBooking.BookingDetails;
            }
        }
    }
}
