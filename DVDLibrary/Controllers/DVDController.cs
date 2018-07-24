using DVDLibrary.Data;
using DVDLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DVDLibrary.Controllers
{
    public class DVDController : ApiController
    {
        [Route("dvds")]
        [AcceptVerbs("GET")]
        public IHttpActionResult All()
        {
            IDVDLibraryRepo myRepo = DVDRepoFactory.GetRepository();
            var displayDvds = myRepo.GetAllDvds();
            return Ok(displayDvds);
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("dvd/{dvdId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDvd(int dvdId)
        {
            IDVDLibraryRepo myRepo = DVDRepoFactory.GetRepository();
            DVD selectedDvd = myRepo.GetDvdById(dvdId);
            if (selectedDvd == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(selectedDvd);
            }            
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("dvd")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AddDvd(AddDvdRequest request)
        {
            IDVDLibraryRepo myRepo = DVDRepoFactory.GetRepository();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DVD dvd = new DVD()
            {
                Title = request.Title,
                ReleaseYear = request.ReleaseYear,
                Director = request.Director,
                Rating = request.Rating,
                Notes = request.Notes,
            };

            myRepo.AddDvd(dvd);
            return Created($"dvd/get/{dvd.DVDId}", dvd);
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("dvd/{Id}")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult Edit(int Id, EditDvdRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IDVDLibraryRepo myRepo = DVDRepoFactory.GetRepository();

            DVD dvd = new DVD();
            DVD current = myRepo.GetDvdById(Id);
            //if (dvd == null)
            //{
            //    return NotFound();
            //}
            

            dvd.DVDId = Id;
            dvd.Title = request.Title;
            dvd.ReleaseYear = request.ReleaseYear;
            dvd.Rating = request.Rating;
            dvd.Director = request.Director;
            dvd.Notes = request.Notes;

            
            //add DVDId to current here????
            
            myRepo.EditDvd(current, dvd);

            return Ok(dvd);
        }
        
        [Route("dvd/{dvdId}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteDvd(int dvdId)
        {
            IDVDLibraryRepo myRepo = DVDRepoFactory.GetRepository();

            DVD dvd = myRepo.GetDvdById(dvdId);

            if (dvd == null)
            {
                return NotFound();
            }

            myRepo.DeleteDvd(dvdId);
            return Ok();
        }
    }
    
}
