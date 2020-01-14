using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace projekt1.net.Models
{
    public class projekt1netContext : DbContext
    {
        public projekt1netContext (DbContextOptions<projekt1netContext> options)
            : base(options)
        {
        }

        public DbSet<projekt1.net.Models.JobOffer> JobOffer { get; set; }
    }
}
