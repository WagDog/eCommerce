using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using eCommerce.DAL.Data;
using eCommerce.DAL.Repositories;
using eCommerce.Model;
using eCommerce.Model.Dtos;

namespace eCommerce.WebUI.Controllers.Api
{
    public class MovieController : ApiController
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
        readonly MovieRepository _movieRepository = new MovieRepository(db);

        // Note how Microsoft scaffolding differs from the CustomersController which 
        // was done as per the Udemy course. This controller uses ResponseType and Models
        // as well as using the Http verb as part of the function name
        
        // GET: api/Movie
        public IHttpActionResult GetMovies()
        {
            return Ok(_movieRepository.GetAll().ToList().Select(Mapper.Map<Movie, MovieDto>));
        }

        // GET: api/Movie/5
        // [ResponseType(typeof(Movie))]
        public IHttpActionResult GetMovie(int id)
        {
            Movie movie = _movieRepository.GetById(id);
            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // PUT: api/Movie/5
        //[ResponseType(typeof(void))]
        [HttpPut]              // Added by PW as preferred method. Microsoft suggest calling the function Put<Controller>
        public IHttpActionResult PutMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movieInDb = _movieRepository.GetById(id);
            if (movieInDb == null)
                return BadRequest();
            //throw new HttpResponseException(HttpStatusCode.NotFound);   // Old Code

            // Map the Movie DTO to the found Movie entity in the database.
            // Because we are supplying both souce and destination objects, AutoMapper
            // knows what types we are using, so instead of the following:-
            // Mapper.Map<MovieDto, Movie>(movieDto, movieInDb);
            // We can write as follows:-
            Mapper.Map(movieDto, movieInDb);

            _movieRepository.Update(movieInDb);
            _movieRepository.Commit();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Movie
        //[ResponseType(typeof(Movie))]
        [HttpPost]              // Added by PW as preferred method. Microsoft suggest calling the function Post<Controller>
        public IHttpActionResult PostMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map the MovieDto to the Model entity
            Movie movie = Mapper.Map<MovieDto, Movie>(movieDto);

            _movieRepository.Insert(movie);
            _movieRepository.Commit();

            return CreatedAtRoute("DefaultApi", new { id = movie.MovieId }, movie);
        }

        // DELETE: api/Movie/5
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            Movie movie = _movieRepository.GetById(id);
            if (movie == null)
                return NotFound();

            _movieRepository.Delete(movie);
            _movieRepository.Commit();

            return Ok(movie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovieExists(int id)
        {
            return db.Movies.Count(e => e.MovieId == id) > 0;
        }
    }
}