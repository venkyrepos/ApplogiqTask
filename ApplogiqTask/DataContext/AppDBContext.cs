using ApplogiqTask.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplogiqTask.DataContext
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options) 
        {
                
        }

        public DbSet<EtherTransactionData> EtherTransactionData { get; set; }
    }
}
