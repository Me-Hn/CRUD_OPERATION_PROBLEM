using Microsoft.EntityFrameworkCore;

namespace CRUD_OPERATION.Models
{
    public class AppContext:DbContext
    {
       public AppContext(DbContextOptions<AppContext> option): base (option) { }

       public DbSet<Student> students { get; set; }
    }
}
