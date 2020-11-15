using System;
using System.Collections.Generic;

namespace CustomerManagementDAL.Repositories
{
    public interface ICustomerRepository : IDisposable
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomerByID(int customerId);
        IList<Customer> GetCustomersByName(string name);
        void InsertCustomer(Customer customer);
        void DeleteCustomer(int customerID);
        void UpdateCustomer(Customer customer);
        void Save();
    }
}
