using System.Data.Common;
using System.IO;

namespace EntityFramework.Sample
{
    public class SqlLiteSession
    {
        private readonly IMapPath _mapPath;

        public SqlLiteSession(IMapPath mapPath)
        {
            _mapPath = mapPath;
        }
        public DbConnection CreateConnection(string file)
        {
            DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SQLite");
            DbConnection cnn = fact.CreateConnection();
            cnn.ConnectionString = "Data Source=" + Path.Combine(Directory.GetCurrentDirectory(), file);
            cnn.Open();
            return cnn;
        }
        public CoreDbContext CreateSession(string file)
        {
            return new CoreDbContext(CreateConnection(file));
        }
    }
}
