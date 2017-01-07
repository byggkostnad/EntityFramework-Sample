using System.Data.Entity.Migrations;

namespace EntityFramework.Sample.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<EntityFramework.Sample.CoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //SetSqlGenerator("System.Data.SQLite",);
        }

        protected override void Seed(EntityFramework.Sample.CoreDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
