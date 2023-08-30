using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HuniMVC.Models;

namespace HuniMVC.Data
{
    public class HuniMVCContext : DbContext
    {
        public HuniMVCContext (DbContextOptions<HuniMVCContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Food> Foods { get; set; } = default!;
        public DbSet<Message> Messages { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;




    }
}
