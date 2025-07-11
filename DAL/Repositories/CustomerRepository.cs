﻿using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CustomerRepository
    {
        private readonly FuminiHotelManagementContext _db;

        public CustomerRepository()
        {
            _db = new FuminiHotelManagementContext();
        }

        public void AddCustomer(Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var customer = _db.Customers.Where(c => c.CustomerId == id);
            if (customer.Any())
            {
                _db.Customers.Remove(customer.First());
                _db.SaveChanges();
            }
        }

        public List<Customer> GetAllCustomers()
        {
            try
            {
                return _db.Customers
                    .Where(c => c.CustomerFullName != null && c.Telephone != null)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllCustomers: {ex.Message}");
                return new List<Customer>();
            }
        }

        public List<Customer> SearchCustomer(string keyword)
        {
            try
            {
                if (string.IsNullOrEmpty(keyword)) return _db.Customers.ToList();

                return _db.Customers
                    .Where(c =>
                        (c.CustomerFullName != null && c.CustomerFullName.Contains(keyword)) ||
                        (c.Telephone != null && c.Telephone.Contains(keyword)) ||
                        (c.EmailAddress != null && c.EmailAddress.Contains(keyword)))
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchCustomer: {ex.Message}");
                return new List<Customer>();
            }
        }

        public Customer Login(string email, string password)
        {
            try
            {
                return _db.Customers.FirstOrDefault(x => x.EmailAddress == email && x.Password == password);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Login: {ex.Message}");
                return new Customer();
            }
        }

        public bool UpdateCustomer(Customer updatedCustomer)
        {
            try
            {
                var existingCustomer = _db.Customers.FirstOrDefault(x => x.CustomerId == updatedCustomer.CustomerId);

                if (existingCustomer == null)
                    return false;

                // Cập nhật các thuộc tính cần thiết
                existingCustomer.CustomerFullName = updatedCustomer.CustomerFullName;
                existingCustomer.EmailAddress = updatedCustomer.EmailAddress;
                existingCustomer.Telephone = updatedCustomer.Telephone;
                existingCustomer.CustomerBirthday = updatedCustomer.CustomerBirthday;
                existingCustomer.Password = updatedCustomer.Password;

                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateCustomer: {ex.Message}");
                return false;
            }
        }

    }
}
