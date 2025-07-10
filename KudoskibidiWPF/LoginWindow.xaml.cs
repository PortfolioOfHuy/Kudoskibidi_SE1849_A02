using BLL.Services;
using KudoskibidiWPF.Utils;
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

namespace KudoskibidiWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly CustomerService _customerService;
        public LoginWindow()
        {
            InitializeComponent();
            _customerService = new CustomerService();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = pbPassword.Password;

            var customer = _customerService.LoginCustomer(email, password);

            if (email == ConfigurationImport.AdminEmail &&
                password == ConfigurationImport.AdminPassword)
            {
                var adminDashboard = new AdminDashboard();
                adminDashboard.Show();
                this.Close();
                return;
            }
            else if (customer != null)
            {
                var customerDashboard = new CustomerDashboard(customer);
                customerDashboard.Show();
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show("Email hoặc mật khẩu không đúng!");
            }
        }
    }
}
