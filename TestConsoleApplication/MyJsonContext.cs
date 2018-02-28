using LDB.Linq;
using TestConsoleApplication.Model;

namespace TestConsoleApplication
{
    public class MyJsonContext : LContext
    {
        public DbSet<Test> Tests { get; set; }
    }
}
