﻿namespace EntityFramework.Sample
{
    public class ProductOrder
    {
        public virtual int Id { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}