using System.Data.Entity;

namespace CustomerManagementDAL.Context
{
    public class CustomerContext : DbContext
    {
        public CustomerContext() : base("name=DefaultConnection")
        {
            Database.SetInitializer<CustomerContext>(new DatabaseInitializer());
        }
        public DbSet<Customer> Customers { get; set; }
        
    }
}
