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
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private readonly CustomerService _customerService;
        private DAL.Entities.Customer _currentCustomer;

        public ProfilePage(DAL.Entities.Customer customer)
        {
            InitializeComponent();
            _customerService = new CustomerService();
            _currentCustomer = customer;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_currentCustomer != null)
                {
                    txtFullName.Text = _currentCustomer.CustomerFullName;
                    txtEmail.Text = _currentCustomer.EmailAddress;
                    txtTelephone.Text = _currentCustomer.Telephone;
                    dpBirthday.SelectedDate = _currentCustomer.CustomerBirthday?.ToDateTime(TimeOnly.MinValue);
                    pbPassword.Password = _currentCustomer.Password;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị dữ liệu: {ex.Message}");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
