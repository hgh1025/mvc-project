using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HuniMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HuniMVC.Data
{
    public class HuniMVCContext : IdentityDbContext //Dbcontext를 사용하면 login 컨트롤러에서 찾지를 못함
    {
        public HuniMVCContext (DbContextOptions<HuniMVCContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Message> Messages { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;




    }
}
