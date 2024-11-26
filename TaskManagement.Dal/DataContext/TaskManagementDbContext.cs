using Microsoft.EntityFrameworkCore;
using Task = TaskManagement.Entity.Concrete.Task;

namespace TaskManagement.Dal.DataContext
{
    public class TaskManagementDbContext : DbContext
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options) : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }
    }
}
