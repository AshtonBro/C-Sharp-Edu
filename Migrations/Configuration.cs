namespace AshtonBro.Code.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AshtonBro.Code.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AshtonBro.Code.MyDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var bands = new List<Band>
            {
                new Band() { Name = "System of a down", Year = 2001 },
                new Band() { Name = "Nirvana", Year = 1987 },
                new Band() { Name = "Rammstain", Year = 1996 }
            };

            bands.ForEach(s => context.Bands.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var songs = new List<Song>
                {
                    new Song() { Name = "Toxicity", BandId = 16 },
                    new Song() { Name = "Smells like Teen Spirit", BandId = 17 },
                    new Song() { Name = "In bloom", BandId = 17 },
                    new Song() { Name = "Mutter", BandId = 18 }
                };

            songs.ForEach(s => context.Songs.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();


        }
    }
}
