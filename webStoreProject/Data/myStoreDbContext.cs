using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webStoreProject.Models;

namespace webStoreProject.Data
{
    public class myStoreDbContext: DbContext
    {
        public myStoreDbContext(DbContextOptions<myStoreDbContext> options):base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        
    }
}
