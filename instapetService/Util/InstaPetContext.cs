using instapetService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static Amazon.S3.Util.S3EventNotification;

namespace instapetService.Util
{
    public class InstaPetContext : DbContext
    {
        public InstaPetContext(DbContextOptions<InstaPetContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<User> User { get; set; } = default!;

        public DbSet<Image> Image { get; set; } = default!;

        public DbSet<Post> Post { get; set; } = default!;

        public DbSet<Follow> Follow { get; set; } = default!;
    }
}
