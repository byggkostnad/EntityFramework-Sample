using System.Collections.Generic;

namespace EntityFramework.Sample
{
    public class Product
    {
        public virtual float Cost { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<ProductOrder> ProductOrders { get; set; }

        public virtual int Id { get; set; }

        public virtual int Version { get; set; }
    }
}