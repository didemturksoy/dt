using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkConverterApi.Model
{
    public class LinkDbContext :DbContext
    {
        public LinkDbContext()
        {

        }
        public LinkDbContext(DbContextOptions<LinkDbContext> options)
            : base(options)
        {

        }
        public DbSet<Link> Links { get; set; }
      

    }
}
