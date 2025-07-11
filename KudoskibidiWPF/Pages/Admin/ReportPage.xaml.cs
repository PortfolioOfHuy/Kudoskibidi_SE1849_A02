using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DAL.Entities; // namespace chứa BookingDetail
using BLL.Services; // namespace chứa BookingReservationService hoặc BookingService

namespace KudoskibidiWPF.Pages.Admin
{
    /// <summary>
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        private readonly BookingService _bookingService;

        public ReportPage()
        {
            InitializeComponent();
            _bookingService = new BookingService();
        }

        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dpStartDate.SelectedDate == null || dpEndDate.SelectedDate == null)
                {
                    MessageBox.Show("Please select both Start Date and End Date.");
                    return;
                }

                DateOnly startDate = DateOnly.FromDateTime(dpStartDate.SelectedDate.Value);
                DateOnly endDate = DateOnly.FromDateTime(dpEndDate.SelectedDate.Value);

                var reportData = _bookingService.GetBookingReport(startDate, endDate);

                dgReport.ItemsSource = reportData;

                decimal totalRevenue = reportData.Sum(x => x.ActualPrice ?? 0);
                txtTotalRevenue.Text = $"{totalRevenue:C}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}");
            }
        }
    }
}
