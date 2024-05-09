using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoList.DAL.Entities;

namespace TodoList.DAL
{
    public class TodoDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<SubTask> SubTasks { get; set; }

        public DbSet<UserTask> UserTasks { get; set; }


        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {

        }
    }
}