using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace EntityFramework.Sample
{
    public partial class CoreDbContext : DbContext
    {
        public CoreDbContext():base("DefaultConnection")
        {
        }
        public CoreDbContext(DbConnection connString)
            : base(connString, true)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public Customer GetCustomer(int v)
        {
            return Customers.SingleOrDefault(c => c.Id == v);
        }
        public Product GetProduct(int v)
        {
            return Products.SingleOrDefault(p => p.Id == v);
        }
        public Order GetOrder(int v)
        {
            return Orders.SingleOrDefault(o => o.Id == v);
        }
    }
}
