using BLL.Services;
using DAL.Entities;
using System.Windows;
using System.Windows.Controls;

namespace KudoskibidiWPF.Pages.Admin
{
    /// <summary>
    /// Interaction logic for RoomPage.xaml
    /// </summary>
    public partial class RoomPage : Page
    {
        private readonly RoomInformationService _roomInformationService;
        private readonly RoomTypeService _roomTypeService;

        public RoomPage()
        {
            InitializeComponent();
            _roomInformationService = new RoomInformationService();
            _roomTypeService = new RoomTypeService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var rooms = _roomInformationService.GetAllRoomInformations();
                if (rooms != null)
                {
                    dgRoom.ItemsSource = rooms;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu phòng!");
                }

                var roomTypes = _roomTypeService.GetAllRoomTypes();
                cbRoomType.ItemsSource = roomTypes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }

        private void ClearForm()
        {
            txtRoomId.Text = "";
            txtRoomNumber.Text = "";
            txtRoomDetailDescription.Text = "";
            txtMaxCapacity.Text = "";
            cbRoomType.SelectedIndex = -1;
            cbStatus.SelectedIndex = -1;
            txtPrice.Text = "";
        }

        private void AutoFillInputFields(RoomInformation room)
        {
            txtRoomId.Text = room.RoomId.ToString();
            txtRoomNumber.Text = room.RoomNumber ?? string.Empty;
            txtRoomDetailDescription.Text = room.RoomDetailDescription ?? string.Empty;
            txtMaxCapacity.Text = room.RoomMaxCapacity?.ToString() ?? string.Empty;
            cbRoomType.SelectedValue = room.RoomType.RoomTypeId;
            cbStatus.SelectedValue = room.RoomStatus;
            txtPrice.Text = room.RoomPricePerDay?.ToString("F2") ?? string.Empty;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            dgRoom.ItemsSource = _roomInformationService.SearchRoom(txtSearch.Text ?? string.Empty);
            Window_Loaded(sender, e);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtRoomNumber.Text))
                {
                    MessageBox.Show("Please enter Room Number.");
                    return;
                }

                if (cbRoomType.SelectedValue == null)
                {
                    MessageBox.Show("Please select Room Type.");
                    return;
                }

                var room = new RoomInformation
                {
                    RoomNumber = txtRoomNumber.Text.Trim(),
                    RoomDetailDescription = txtRoomDetailDescription.Text.Trim(),
                    RoomMaxCapacity = int.TryParse(txtMaxCapacity.Text, out int cap) ? cap : null,
                    RoomTypeId = Convert.ToInt32(cbRoomType.SelectedValue),
                    RoomStatus = Convert.ToByte(((ComboBoxItem)cbStatus.SelectedItem)?.Tag ?? 1),
                    RoomPricePerDay = decimal.TryParse(txtPrice.Text, out decimal price) ? price : null
                };

                _roomInformationService.AddRoom(room);

                MessageBox.Show("Room added successfully!");

                Window_Loaded(sender, e); // Reload DataGrid
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding room: {ex.Message}");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRoomId.Text))
                {
                    MessageBox.Show("Please select a room to update.");
                    return;
                }

                int roomId = Convert.ToInt32(txtRoomId.Text);

                var updatedRoom = new RoomInformation
                {
                    RoomId = roomId,
                    RoomNumber = txtRoomNumber.Text.Trim(),
                    RoomDetailDescription = txtRoomDetailDescription.Text.Trim(),
                    RoomMaxCapacity = int.TryParse(txtMaxCapacity.Text, out int cap) ? cap : null,
                    RoomTypeId = Convert.ToInt32(cbRoomType.SelectedValue),
                    RoomStatus = Convert.ToByte(((ComboBoxItem)cbStatus.SelectedItem)?.Tag ?? 1),
                    RoomPricePerDay = decimal.TryParse(txtPrice.Text, out decimal price) ? price : null
                };

                _roomInformationService.UpdateRoom(updatedRoom);

                MessageBox.Show("Room updated successfully!");
                Window_Loaded(sender, e);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating room: {ex.Message}");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRoomId.Text))
                {
                    MessageBox.Show("Please select a room to delete.");
                    return;
                }

                int roomId = Convert.ToInt32(txtRoomId.Text);

                var result = MessageBox.Show("Are you sure you want to delete this room?",
                                             "Confirm Delete",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    _roomInformationService.DeleteRoom(roomId);
                    MessageBox.Show("Room deleted (or marked inactive) successfully.");

                    Window_Loaded(sender, e); // Reload DataGrid
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting room: {ex.Message}");
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void dgRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgRoom.SelectedItem is RoomInformation selectdRoom)
            {
                AutoFillInputFields(selectdRoom);
            }
            else
            {
                ClearForm();
            }
        }
    }
}
