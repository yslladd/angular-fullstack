using Angular16API.Models;
using Microsoft.EntityFrameworkCore;

namespace Angular16API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }     

        public DbSet<SuperHero>? SuperHeros { get; set; }
    }
}
