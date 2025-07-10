using BLL.Services;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KudoskibidiWPF.Windows
{
    public partial class AddBookingWindow : Window
    {
        private readonly CustomerService _customerService = new CustomerService();
        private readonly RoomInformationService _roomService = new RoomInformationService();

        public BookingReservation? NewBooking { get; private set; }
        private readonly List<BookingDetail> _bookingDetails = new List<BookingDetail>();

        public AddBookingWindow()
        {
            InitializeComponent();
            LoadCustomers();
            LoadRooms();
        }

        private void LoadCustomers()
        {
            cbCustomer.ItemsSource = _customerService.GetAllCustomers();
        }

        private void LoadRooms()
        {
            cbRoom.ItemsSource = _roomService.GetAllRoomInformations();
        }

        private void btnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbRoom.SelectedItem == null)
                {
                    MessageBox.Show("Please select a room.");
                    return;
                }

                RoomInformation selectedRoom = (RoomInformation)cbRoom.SelectedItem;
                DateOnly start = DateOnly.FromDateTime(dpStartDate.SelectedDate ?? DateTime.Today);
                DateOnly end = DateOnly.FromDateTime(dpEndDate.SelectedDate ?? DateTime.Today);

                if (end < start)
                {
                    MessageBox.Show("End date must be after start date.");
                    return;
                }

                decimal price = decimal.Parse(txtActualPrice.Text.Trim());

                BookingDetail detail = new BookingDetail
                {
                    RoomId = selectedRoom.RoomId,
                    StartDate = start,
                    EndDate = end,
                    ActualPrice = price,
                    Room = selectedRoom
                };

                _bookingDetails.Add(detail);

                dgBookingDetails.ItemsSource = null;
                dgBookingDetails.ItemsSource = _bookingDetails;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add room: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbCustomer.SelectedValue == null)
                {
                    MessageBox.Show("Please select a customer.");
                    return;
                }

                int customerId = (int)cbCustomer.SelectedValue;
                DateOnly bookingDate = DateOnly.FromDateTime(dpBookingDate.SelectedDate ?? DateTime.Today);
                byte status = byte.Parse(txtStatus.Text.Trim());
                decimal totalPrice = decimal.Parse(txtTotalPrice.Text.Trim());

                NewBooking = new BookingReservation
                {
                    CustomerId = customerId,
                    BookingDate = bookingDate,
                    BookingStatus = status,
                    TotalPrice = totalPrice,
                    BookingDetails = _bookingDetails.Select(d => new BookingDetail
                    {
                        RoomId = d.RoomId,
                        StartDate = d.StartDate,
                        EndDate = d.EndDate,
                        ActualPrice = d.ActualPrice
                        // KHÔNG gán BookingReservation
                    }).ToList()
                };

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
