using Microsoft.EntityFrameworkCore;

namespace CRUD_OPERATION.Models
{
    public class AppContext:DbContext
    {
       public AppContext(DbContextOptions<AppContext> option): base (option) { }

       public DbSet<Student> students { get; set; }

        public DbSet<Category> categories { get; set; }

        public DbSet<Product> products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.products)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
