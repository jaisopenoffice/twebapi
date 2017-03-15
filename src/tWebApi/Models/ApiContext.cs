using Microsoft.EntityFrameworkCore;
using tWebApi.Models;

namespace tWebApi
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Article> Articles { get; set; }
    }
}