namespace DVDLibrary.Data.Migrations
{
    using DVDLibrary.Data.EF;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DVDLibrary.Data.EF.DvdDatabaseEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DVDLibrary.Data.EF.DvdDatabaseEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Rating.AddOrUpdate(
            r => r.Name,
            new Rating { Name = "G" },
            new Rating { Name = "PG" },
            new Rating { Name = "PG-13" },
            new Rating { Name = "R" }
            );

            context.Director.AddOrUpdate
            (
                d => d.Name,
                new Director { Name = "Billy Bob Thornton" },
                new Director { Name = "Harold Ramis" },
                new Director { Name = "Steven Spielberg" }
            );

            context.SaveChanges();

            context.DVD.AddOrUpdate
            (
                m => m.Title,
                
                new DVD
                {
                    Title = "Caddie Shack",
                    ReleaseYear = 1980,
                    DirectorId = context.Director.First(d => d.Name == "Harold Ramis").DirectorId,
                    RatingId = context.Rating.First(r => r.Name == "R").RatingId,
                    Notes = "EF notes CS"
                },
                new DVD
                {
                    Title = "E.T.",
                    ReleaseYear = 1996,
                    DirectorId = context.Director.First(d => d.Name == "Steven Spielberg").DirectorId,
                    RatingId = context.Rating.First(r => r.Name == "G").RatingId,
                    Notes = "EF notes ET"
                }
                
            );
        }
    }
}
