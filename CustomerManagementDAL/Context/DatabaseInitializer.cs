using System.Data.Entity;

namespace CustomerManagementDAL.Context
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<CustomerContext>
    {
        protected override void Seed(CustomerContext context)
        {
            context.Customers.Add(
                new Customer()
                {
                    CustomersId = 1,
                    FirstName = "Abhijeet",
                    LastName = "Rao",
                    Email = "abhijeet.rao@gmail.com",
                    IsActive = true,
                    PhoneNumber = "25"
                });

            context.Customers.Add(
                new Customer
                {
                    CustomersId = 2,
                    FirstName = "Seema",
                    LastName = "Rai",
                    Email = "seemarr@gmail.com",
                    IsActive = true,
                    PhoneNumber = "25"
                });

            context.Customers.Add(
                new Customer
                {
                    CustomersId = 3,
                    FirstName = "Gargi",
                    LastName = "Pawar",
                    Email = "gargi12@gmail.com",
                    IsActive = false,
                    PhoneNumber = "252344"
                });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
