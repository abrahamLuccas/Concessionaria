using Concessionaria.Models;
using Microsoft.EntityFrameworkCore;

namespace Concessionaria.Data
{
    public class AppContext : DbContext
    {

        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {

        }

        public DbSet<Carros> Carros { get; set; }
    }
}
