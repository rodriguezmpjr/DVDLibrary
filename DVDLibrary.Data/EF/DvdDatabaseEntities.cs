using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DVDLibrary.Data.EF 
{
    public class DvdDatabaseEntities : DbContext
    {
        public DvdDatabaseEntities() : base("DvdDatabase")
        {

        }

        public DbSet<DVD> DVD { get; set; }
        public DbSet<Director> Director { get; set; }
        public DbSet<Rating> Rating { get; set; }
    }
}
