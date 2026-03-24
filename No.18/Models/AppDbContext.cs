using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace No._18.Models
{
    public class AppDbContext : DbContext
    {
        //DI
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<CaseModel> Cases { get; set; }
    }
}
