using ChessApi.Model;
using Microsoft.EntityFrameworkCore;

namespace ChessApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Game>? Games { get; set; }
    }
}
