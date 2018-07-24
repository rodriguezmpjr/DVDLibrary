using DVDLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDLibrary.Data
{
    public static class DvdStaticList
    {
        public static List<DVD> MyDvdList { get; set; }

        static DvdStaticList()
        {
            MyDvdList = new List<DVD>()
            {
                new DVD()
                {
                    DVDId = 1,
                    Title = "Sling Blade",
                    ReleaseYear = 1996,
                    Director = "Billy Bod Thornton",
                    Rating = "R",
                    Notes = "One of my favs..."
                },
                new DVD()
                {
                    DVDId = 2,
                    Title = "Caddie Shack",
                    ReleaseYear = 1980,
                    Director = "Harold Ramis",
                    Rating = "R",
                    Notes = "Another one of my favs..."
                },
                new DVD()
                {
                    DVDId = 3,
                    Title = "E.T.",
                    ReleaseYear = 1982,
                    Director = "Steven Spielberg",
                    Rating = "PG",
                    Notes = "Childhood fav..."
                }

            };
        }


    }
}