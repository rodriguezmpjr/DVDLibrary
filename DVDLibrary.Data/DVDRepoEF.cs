using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVDLibrary.Data.EF;
using DVDLibrary.Models;
using System.Data.Entity;

namespace DVDLibrary.Data
{
    public class DVDRepoEF : IDVDLibraryRepo
    {
        public Models.DVD AddDvd(Models.DVD dvdToAdd)
        {
            var myRepo = new DvdDatabaseEntities();
            var director = myRepo.Director.FirstOrDefault(d => d.Name == dvdToAdd.Director);
            if (director == null)
            {
                director = myRepo.Director.Add(new Director { Name = dvdToAdd.Director });
                myRepo.SaveChanges();
            }

            var rating = myRepo.Rating.FirstOrDefault(r => r.Name == dvdToAdd.Rating);
            if (rating == null)
            {
                rating = myRepo.Rating.Add(new Rating { Name = dvdToAdd.Rating });
                myRepo.SaveChanges();
            }

            var dvd = myRepo.DVD.Add(
                new EF.DVD
                {
                    Title = dvdToAdd.Title,
                    ReleaseYear = dvdToAdd.ReleaseYear,
                    DirectorId = director.DirectorId,
                    RatingId = rating.RatingId,
                    Notes = dvdToAdd.Notes
                });

            myRepo.SaveChanges();
            dvdToAdd.DVDId = dvd.DVDId; 
            return dvdToAdd;
        }

        public Models.DVD DeleteDvd(int dvdId)
        {
            var myRepo = new DvdDatabaseEntities();

            var dvd = myRepo.DVD.Find(dvdId);

            Models.DVD dvdToBeDeleted = new Models.DVD
            {
                Title = dvd.Title,
                ReleaseYear = dvd.ReleaseYear,
                Director = dvd.Director.Name,
                Rating = dvd.Rating.Name,
                Notes = dvd.Notes
            };

            myRepo.DVD.Remove(dvd);
            myRepo.SaveChanges();
            return dvdToBeDeleted;

        }

        public Models.DVD EditDvd(Models.DVD current, Models.DVD updated)
        {
            var myRepo = new DvdDatabaseEntities();
            var director = myRepo.Director.FirstOrDefault(d => d.Name == updated.Director);
            if (director == null)
            {
                //director.Name = updated.Director;
                director = myRepo.Director.Add(new Director { Name = updated.Director });
                myRepo.SaveChanges();
            }        

            var rating = myRepo.Rating.FirstOrDefault(r => r.Name == updated.Rating);
            if (rating == null)
            {
                rating = myRepo.Rating.Add(new Rating { Name = updated.Rating });
                myRepo.SaveChanges();
            }   

            EF.DVD dvdEntry = new EF.DVD
            {
                DVDId = current.DVDId,
                Title = updated.Title,
                ReleaseYear = updated.ReleaseYear,
                DirectorId = director.DirectorId,
                RatingId = rating.RatingId,
                Notes = updated.Notes
            };

            myRepo.Entry(dvdEntry).State = EntityState.Modified;
            myRepo.SaveChanges();
            return current;

        }

        public List<Models.DVD> GetAllDvds()
        {
            var myRepo = new DvdDatabaseEntities();           

            var repoToList = myRepo.DVD.ToList();

            List<Models.DVD> returnList= new List<Models.DVD>();

            foreach (EF.DVD d in repoToList)             
            {
                returnList.Add(new Models.DVD
                {
                    DVDId = d.DVDId,
                    Title = d.Title,
                    ReleaseYear = d.ReleaseYear,
                    Director = d.Director.Name,
                    Rating = d.Rating.Name,
                    Notes = d.Notes
                });
            } 

            return returnList;
        }

        public Models.DVD GetDvdById(int dvdId)
        {
            var myRepo = new DvdDatabaseEntities();
            var dvd = myRepo.DVD.Find(dvdId);
            Models.DVD dvdToBeReturned = new Models.DVD();

            if (dvd != null)
            {
                dvdToBeReturned.DVDId = dvdId;
                dvdToBeReturned.Title = dvd.Title;
                dvdToBeReturned.ReleaseYear = dvd.ReleaseYear;
                dvdToBeReturned.Director = dvd.Director.Name;
                dvdToBeReturned.Rating = dvd.Rating.Name;
                dvdToBeReturned.Notes = dvd.Notes;
                return dvdToBeReturned;
            }
            else
                return null;

        }
    }
}
