﻿using System.IO;
using System.Xml.Linq;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using EntityFramework.Sample;
using EntityFramework.Sample.Migrations;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;

namespace SomeBasicEFApp.Tests
{
    [TestFixture]
    public class CustomerDataTests
    {
        private CoreDbContext _session;

        [Test]
        public void CanGetCustomerById()
        {
            var customer = _session.GetCustomer(1);

            Assert.IsNotNull(customer);
        }

        [Test]
        public void CanGetCustomerByFirstname()
        {
            var customers = _session.Customers
                .Where(c => c.Firstname == "Steve")
                .ToList();
            Assert.AreEqual(3, customers.Count);
        }

        [Test]
        public void CanGetProductById()
        {
            var product = _session.GetProduct(1);

            Assert.IsNotNull(product);
        }
        [Test]
        public void OrderContainsProduct()
        {
            Assert.True(_session.GetOrder(1).ProductOrders.Any(p => p.Product.Id == 1));
        }
        [Test]
        public void OrderHasACustomer()
        {
            Assert.That(_session.GetOrder(1).Customer.Firstname, Is.Not.Empty);
        }


        [SetUp]
        public void Setup()
        {
            _session = new CoreDbContext();
        }


        [TearDown]
        public void TearDown()
        {
            if (_session != null)
            {
                _session.Dispose();
            }
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var configuration = new Configuration();
            configuration.TargetDatabase = new DbConnectionInfo("DefaultConnection");

            var migrator = new DbMigrator(configuration);
            migrator.Update();

            var doc = XDocument.Load(Path.Combine("TestData", "TestData.xml"));
            var import = new XmlImport(doc, "http://tempuri.org/Database.xsd");
            var customer = new List<Customer>();
            using (var session = new CoreDbContext())
            {
                import.Parse(new[] { typeof(Customer), typeof(Order), typeof(Product) },
                                (type, obj) =>
                                {
                                    switch (type.Name)
                                    {
                                        case nameof(Customer):
                                            session.Customers.Add((Customer)obj);
                                            break;
                                        case nameof(Order):
                                            session.Orders.Add((Order)obj);
                                            break;
                                        case nameof(Product):
                                            session.Products.Add((Product)obj);
                                            break;
                                        default:
                                            break;
                                    }

                                });
                session.SaveChanges();
            }
            using (var session = new CoreDbContext())
            {
                import.ParseConnections("OrderProduct", "Product", "Order", (productId, orderId) =>
                {
                    var product = session.Products.Single(p => p.Id == productId);
                    var order = session.Orders.Single(o => o.Id == orderId);
                    session.ProductOrders.Add(new ProductOrder { Order = order, Product = product });
                });

                import.ParseIntProperty("Order", "Customer", (orderId, customerId) =>
                {
                    session.Orders.Single(o => o.Id == orderId).Customer = session.Customers.Single(c => c.Id == customerId);
                });

                session.SaveChanges();
            }
        }

    }
}
