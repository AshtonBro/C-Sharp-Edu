using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace AshtonBro.Code
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("DbConnectionString")
        {
        }

        public DbSet<Band> Bands { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
}
