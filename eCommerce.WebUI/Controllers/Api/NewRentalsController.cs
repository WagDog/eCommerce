using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using eCommerce.DAL.Data;
using eCommerce.DAL.Repositories;
using eCommerce.Model;
using eCommerce.Model.Dtos;

namespace eCommerce.WebUI.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        /****************** RESTFul Format ******************************************/
        /* Verbal           Controller                                              */
        /* Reminder         Function                    URL format                  */
        /****************************************************************************/
        /*   I              Index                       # GET /accounts
         *   Still          Show/Details                # GET /accounts/1
         *   Need           New                         # GET /accounts/new
         *   Coffee         Create                      # POST /accounts
         *   Even           Edit                        # GET /accounts/1/edit
         *   Until          Update                      # PATCH/PUT /accounts/1
         *   Dark           Destroy                     # DELETE /accounts/1
         ****************************************************************************/
        private static DataContext db = new DataContext();
        private readonly RentalRepository _rentalRepository = new RentalRepository(db);
        private readonly CustomerRepository _customerRepository = new CustomerRepository(db);
        private readonly MovieRepository _movieRepository = new MovieRepository(db);

        // GET: api/NewRentals
        public IHttpActionResult Get()
        {
            var rentalDtos = _rentalRepository.GetAll()
                            .Include(c => c.Customer)
                            .Include(m => m.Movie)
                            .ToList().Select(Mapper.Map<Rental, RentalDto>);

                
            return Ok(rentalDtos);
        }

        // GET: api/NewRentals/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/NewRentals
        [HttpPost]              // Added by PW as preferred method. Microsoft suggest calling the function Post<ControllerName>
        public IHttpActionResult PostNewRental(NewRentalDto newRental)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (newRental.MovieIds.Count == 0)
                return BadRequest("No Movie IDs have been supplied");

            Customer customer = _customerRepository.GetById(newRental.CustomerId);
            if (customer == null)
                return BadRequest("Invalid customer ID supplied");

                            
            // Get the movies where the movie ID is in the array of IDs supplied in the DTO
            var movies = db.Movies.Where(
               m => newRental.MovieIds.Contains(m.MovieId)).ToList();

            if (movies.Count != newRental.MovieIds.Count)
                return BadRequest("One or more movie IDs is incorrect.");


            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available");

                var rental = new Rental();
                rental.CustomerId = newRental.CustomerId;
                rental.MovieId = movie.MovieId;
                rental.DateRented = newRental.DateRented;

                _rentalRepository.Insert(rental);
                
                // Decrease the available number of this movie to rent
                movie.NumberAvailable--;    // Decrease the number available
                _movieRepository.Update(movie);                
            }

            _rentalRepository.Commit();
            _movieRepository.Commit();

            return Ok();
        }

        // PUT: api/NewRentals/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/NewRentals/5
        public void Delete(int id)
        {
        }
    }
}
