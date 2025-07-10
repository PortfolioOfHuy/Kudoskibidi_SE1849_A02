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

        public RoomPage()
        {
            InitializeComponent();
            _roomInformationService = new RoomInformationService();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }

        private void ClearForm()
        {
            txtRoomNumber.Text = "";
            txtRoomDetailDescription.Text = "";
            txtMaxCapacity = null;
            cbRoomType.SelectedIndex = -1;
            cbStatus.SelectedIndex = -1;
            txtPrice.Text = "";
        }

        private void AutoFillInputFields(RoomInformation room)
        {
            txtRoomNumber.Text = room.RoomNumber ?? string.Empty;
            txtRoomDetailDescription.Text = room.RoomDetailDescription ?? string.Empty;
            txtMaxCapacity.Text = room.RoomMaxCapacity?.ToString() ?? string.Empty;
            cbRoomType.SelectedValue = room.RoomType?.RoomTypeId;
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

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

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
