using LC4_TO_DO.Entities;
using Microsoft.EntityFrameworkCore;

namespace LC4_TO_DO.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.TodoItems)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);



            base.OnModelCreating(modelBuilder);
        }
    }
}
