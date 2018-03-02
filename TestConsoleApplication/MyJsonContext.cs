using LDB.Linq;
using LDB.Linq.Enums;
using TestConsoleApplication.Model;

namespace TestConsoleApplication
{
    public class MyJsonContext : LContext
    {
        public MyJsonContext()
        {
        }

        public MyJsonContext(string connectionString) : base(connectionString)
        {
        }

        public MyJsonContext(DataTypeEnum dataType, string path, PositionTypeEnum positionType, bool isReadOnly) : base(dataType, path, positionType, isReadOnly)
        {
        }

        public DbSet<Test> Tests { get; set; }
        
    }
}
