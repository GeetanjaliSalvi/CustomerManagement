using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerManagementDAL;
using CustomerManagementDAL.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace CustomerManagementUnitTesting
{
    [TestClass]
    public class CustomerRepositorAPItest
    {
        ///<summary>
        /// Our Mock Customers Repository for use in testing
        ///</summary>
        public readonly ICustomerRepository MockCustomersRepository;

        public CustomerRepositorAPItest()
        {
            // create some mock customers to play with
            IList<Customer> customers = new List<Customer>
            {

                    new Customer { CustomersId = 1,
                        FirstName = "Abhijeet",
                        LastName = "Rao",
                        Email = "abhijeet.rao@gmail.com",
                        IsActive = true,
                        PhoneNumber = "25" },

                    new Customer { CustomersId = 2,
                        FirstName = "Sushant",
                        LastName = "Rao",
                        Email = "abhijeet.rao@gmail.com",
                        IsActive = true,
                        PhoneNumber = "25" },

                    new Customer { CustomersId = 3,
                        FirstName = "Rahe",
                        LastName = "Rao",
                        Email = "abhijeet.rao@gmail.com",
                        IsActive = true,
                        PhoneNumber = "25" }

                };

            // Mock the Customers Repository using Moq

            Mock<ICustomerRepository> mockCustomerRepository = new Mock<ICustomerRepository>();

            // Return all the customers

            mockCustomerRepository.Setup(mr => mr.GetCustomers()).Returns(customers);


            // return a customer by Id

            mockCustomerRepository.Setup(mr => mr.GetCustomerByID(It.IsAny<int>())).Returns((int i) => customers.First(x => x.CustomersId == i));


            // Allows us to test saving a customer

            mockCustomerRepository.Setup(mr => mr.InsertCustomer(It.IsAny<Customer>()));

            // Complete the setup of our Mock Customer Repository

            this.MockCustomersRepository = mockCustomerRepository.Object;
        }
        ///<summary>
        /// Can we return a customer By Id?
        ///</summary>
        [TestMethod]
        public void CanReturnCustomerById()
        {
            // Try finding a customer by id
            Customer testCustomer = this.MockCustomersRepository.GetCustomerByID(2);
            Assert.IsNotNull(testCustomer); // Test if null
            Assert.IsInstanceOfType(testCustomer, typeof(Customer)); // Test type
            Assert.AreEqual("Sushant", testCustomer.FirstName); // Verify it is the right customer
        }

        ///<summary>
        /// Can we return all customers?
        ///</summary>
        [TestMethod]
        public void CanReturnAllCustomers()
        {
            // Try finding all customers
            IEnumerable<Customer> testCustomers = this.MockCustomersRepository.GetCustomers();

            Assert.IsNotNull(testCustomers); // Test if null

            Assert.AreEqual(3, testCustomers.ToList().Count); // Verify the correct Number

        }


        ///<summary>
        /// Can we insert a new customer?
        ///</summary>
        [TestMethod]
        public void CanInsertCustomer()
        {
            // Create a new customer, not I do not supply an id
            Customer newCustomer = new Customer

            { CustomersId = 4, FirstName = "Rahul", LastName = "Sharma", Email = "rahul.sharma@gmail.com", PhoneNumber = "122222" };

            int customerCount = this.MockCustomersRepository.GetCustomers().ToList().Count;
            Assert.AreEqual(3, customerCount); // Verify the expected Number pre-insert

            // try saving our new customer
            this.MockCustomersRepository.InsertCustomer(newCustomer);
            // demand a recount
            customerCount = this.MockCustomersRepository.GetCustomers().ToList().Count;
            Assert.AreEqual(3, customerCount); // Verify the expected Number post-insert
        }

        ///<summary>
        /// Can we update a customer?
        ///</summary>
        [TestMethod]
        public void CanUpdateCustomer()
        {
            // Find a customer by id
            Customer testCustomer = this.MockCustomersRepository.GetCustomerByID(1);

            // Change one of its properties
            testCustomer.FirstName = "Geetanjali";

            // Save our changes.
            this.MockCustomersRepository.UpdateCustomer(testCustomer);

            // Verify the change
            Assert.AreEqual("Geetanjali", this.MockCustomersRepository.GetCustomerByID(1).FirstName);

        }
    }
}
