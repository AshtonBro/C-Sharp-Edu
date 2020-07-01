using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace AshtonBro.Code
{
    public class MyDbContext : DbContext
    {
        protected MyDbContext() : base("DbConnectionString")
        {
        }
    }
}
