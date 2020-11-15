using CustomerManagementDAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerManagementDAL.Repositories
{
    public class CustomerRepository : ICustomerRepository, IDisposable
    {
        private readonly CustomerContext context;
        public CustomerRepository(CustomerContext context)
        {
            this.context = context;
        }
        public IEnumerable<Customer> GetCustomers()
        {
            return context.Customers.ToList();
        }

        public Customer GetCustomerByID(int id)
        {
            return context.Customers.Find(id);
        }

        public IList<Customer> GetCustomersByName(string name)
        {
            var customersList = context.Customers;
            var customers = (List<Customer>)customersList.Where(u => u.FirstName.StartsWith(name)).ToList();

            return customers;
        }

        public void InsertCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public void DeleteCustomer(int customerID)
        {
            Customer customer = context.Customers.Find(customerID);
            context.Customers.Remove(customer);
            Save();
        }

        public void UpdateCustomer(Customer customer)
        {
            Customer editCustomer = context.Customers.FirstOrDefault(f => f.CustomersId == customer.CustomersId);

            editCustomer.FirstName = customer.FirstName;
            editCustomer.LastName = customer.LastName;
            editCustomer.Email = customer.Email;
            editCustomer.PhoneNumber = customer.PhoneNumber;
            editCustomer.IsActive = customer.IsActive;
            //context.Entry(customer).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
