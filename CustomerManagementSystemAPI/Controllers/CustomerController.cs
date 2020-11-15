using CustomerManagementDAL.Context;
using CustomerManagementDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CustomerManagementSystemAPI.Controllers
{
    public class CustomerController : ApiController
    {
        private ICustomerRepository customerRepository;
        public CustomerController(ICustomerRepository customerDataRepository)
        {
            this.customerRepository = customerDataRepository;
        }
        public CustomerController()
        {
            this.customerRepository = new CustomerRepository(new CustomerContext());
        }

        [HttpGet, Route("api/GetCustomers")]
        [ResponseType(typeof(IEnumerable<Models.Customer>))]
        [Authorize]
        public IHttpActionResult GetCustomers()
        {
            IHttpActionResult result = null;
            IEnumerable<Models.Customer> list;
            try
            {
                list = MapperHelper.DomainToModelMap<IEnumerable<CustomerManagementDAL.Customer>, IEnumerable<Models.Customer>>(customerRepository.GetCustomers());
            }
            catch (Exception)
            {
                return InternalServerError(new Exception("Error in get customer list.  Please try again."));
            }
           
            result = Ok(list);

            return result;
        }

        [HttpGet, Route("api/GetCustomers/{id}")]
        [ResponseType(typeof(IList<Models.Customer>))]
        public IHttpActionResult GetCustomers(int id)
        {
            IHttpActionResult result;
            Models.Customer customer = MapperHelper.DomainToModelMap<CustomerManagementDAL.Customer, Models.Customer>(customerRepository.GetCustomerByID(id));

            if (customer == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(customer);
            }

            return result;
        }

        [HttpGet, Route("api/GetCustomersByName/{name}")]
        [ResponseType(typeof(IList<Models.Customer>))]
        public IHttpActionResult GetCustomersByName(string name)
        {
            IHttpActionResult result;
            IList<Models.Customer> customer;
            try
            {
                customer = MapperHelper.DomainToModelMap<IList<CustomerManagementDAL.Customer>, IList<Models.Customer>>(customerRepository.GetCustomersByName(name));
            }
            catch (Exception)
            {
                return InternalServerError(new Exception("Error in get customer by name.  Please try again."));
            }
           
            if (customer == null)
            {
                result = NotFound();
            }
            else
            {
                result = Ok(customer);
            }

            return result;
        }
        // POST api/<controller>
        [HttpPost, Route("api/Customer/AddCustomer")]
        public IHttpActionResult AddCustomer(Models.Customer customer)
        {
            IHttpActionResult ret = null;
            if (Add(customer))
            {
                ret = Created<Models.Customer>(Request.RequestUri +
                                                customer.CustomersId.ToString(), customer);
            }
            else
            {
                ret = NotFound();
            }

            return ret;
        }

        private bool Add(Models.Customer customer)
        {
            var newcustomer = MapperHelper.ModelToDomainMap<Models.Customer, CustomerManagementDAL.Customer>(customer);
            // Add to list
            customerRepository.InsertCustomer(newcustomer);
            // TODO: Change this to false to test the NotFound()
            return true;
        }

        // PUT api/<controller>/5
        [HttpPost, Route("api/Customer/EditCustomer")]
        public IHttpActionResult EditCustomer(Models.Customer customer)
        {
            IHttpActionResult ret = null;

            if (Update(customer))
            {
                ret = Ok(customer);
            }
            else
            {
                ret = NotFound();
            }

            return ret;
        }

        private bool Update(Models.Customer customer)
        {
            var updateCustomer = MapperHelper.ModelToDomainMap<Models.Customer, CustomerManagementDAL.Customer>(customer);
            // Add to list
            customerRepository.UpdateCustomer(updateCustomer);

            return true;
        }

        // DELETE api/<controller>/5
        [HttpPost, Route("api/Customer/DeleteCustomer/{id}")]
        public IHttpActionResult DeleteCustomer(int id)
        {
            IHttpActionResult ret = null;

            if (DeleteCustomers(id))
            {
                ret = Ok(true);
            }
            else
            {
                ret = NotFound();
            }

            return ret;
        }

        private bool DeleteCustomers(int id)
        {
            customerRepository.DeleteCustomer(id);
            return true;
        }
    }
}
