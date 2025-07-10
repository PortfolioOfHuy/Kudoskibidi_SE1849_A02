using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
        }
        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAllCustomers();
        }

        public List<Customer> SearchCustomer(string keyword)
        {
            return _customerRepository.SearchCustomer(keyword);
        }

        public void AddCustomer(Customer customer)
        {
            _customerRepository.AddCustomer(customer);
        }

        public void DeleteCustomer(int id)
        {
            _customerRepository.DeleteCustomer(id);
        }

        public Customer LoginCustomer(string email, string password)
        {
            return _customerRepository.Login(email, password);
        }

        public bool UpdateCustomer(Customer customer)
        {
            return _customerRepository.UpdateCustomer(customer);
        }
    }
}
