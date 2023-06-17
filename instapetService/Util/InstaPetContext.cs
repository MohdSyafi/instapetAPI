using instapetService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.Util
{
    public class InstaPetContext : DbContext
    {
        public InstaPetContext(DbContextOptions<InstaPetContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; } = default!;

        public DbSet<Image> Image { get; set; } = default!;

        public DbSet<Post> Post { get; set; } = default!;
    }
}
