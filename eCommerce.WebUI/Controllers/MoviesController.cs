using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eCommerce.DAL.Data;
using eCommerce.DAL.Repositories;
using eCommerce.Model;
using eCommerce.WebUI.Models;
using eCommerce.WebUI.ViewModels;

namespace eCommerce.WebUI.Controllers
{
    // Below specifies that Users must be of role type 'canManage' to 
    // Access the REST functions in this controller
    // To specify roles for each action, decorate the individual action
    // To switch to different views, dependant on the role of the logged
    // in user, use the code User.IsInRole("RoleNameToTest") to branch off
    // to different views. Different views can be used to different role 
    // types, which is often more maintainable if there are many role
    // types.
    [Authorize(Roles = RoleName.CanManage)]
    public class MoviesController : Controller
    {
        private DataContext db = new DataContext();        

        // GET: Movies
        public ActionResult Index()
        {
            //MovieRepository movieRepository = new MovieRepository(db);
            //var movies = movieRepository.GetAll().Include(m => m.Genre);
            //return View(movies.ToList());
        
            // JQuery Datatable is handleing the retrieval of dat in the View called Index.cshtml
            return View();
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MovieRepository movieRepository = new MovieRepository(db);
            var movie = movieRepository.GetAll().Include(m => m.Genre).SingleOrDefault(m => m.MovieId == id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            GenreRepository genreRepository = new GenreRepository(db);
            var genres = genreRepository.GetAll().ToList();

            AddUpdateMovieModel addUpdateMovieModel = new AddUpdateMovieModel();
            addUpdateMovieModel.genres = genres;
            addUpdateMovieModel.PageTitle = "Create Movie";
        
            return View(addUpdateMovieModel);
        }


        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            if (movie.MovieId == 0)
            {
                MovieRepository movieRepository = new MovieRepository(db);
                movie.DateAdded = DateTime.Now;
                movieRepository.Insert(movie);  // Add the movie to the repository
                movieRepository.Commit();       // SaveChanges to the repository
                return RedirectToAction("Index");
            }


            // Got here, so show the Create form again with details input so far
            GenreRepository genreRepository = new GenreRepository(db);
            var genres = genreRepository.GetAll().ToList();

            AddUpdateMovieModel addUpdateMovieModel = new AddUpdateMovieModel(movie);   // Create the model from details supplied into the controller
            addUpdateMovieModel.genres = genres;
            addUpdateMovieModel.PageTitle = "Create Movie";

            return View(addUpdateMovieModel);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GenreRepository genreRepository = new GenreRepository(db);
            var genres = genreRepository.GetAll().ToList();
            MovieRepository movieRepository = new MovieRepository(db);
            Movie movie = movieRepository.GetById(id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            AddUpdateMovieModel addUpdateMovieModel = new AddUpdateMovieModel(movie);   // Create the model from details 
                                                                                        // supplied into the controller
            addUpdateMovieModel.genres = genres;
            addUpdateMovieModel.PageTitle = "Edit Movie";

            return View(addUpdateMovieModel);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                MovieRepository movieRepository = new MovieRepository(db);
                movie.DateAdded = DateTime.Now;
                movieRepository.SetState(movie, EntityState.Modified);
                movieRepository.Update(movie);
                movieRepository.Commit();
                return RedirectToAction("Index");
            }

            // Got here, so show the edit form again.
            AddUpdateMovieModel addUpdateMovieModel = new AddUpdateMovieModel(movie);   // Create the model from details 
                                                                                        // supplied into the controller
            GenreRepository genreRepository = new GenreRepository(db);
            var genres = genreRepository.GetAll().ToList();
            addUpdateMovieModel.genres = genres;
            addUpdateMovieModel.PageTitle = "Edit Movie";
            return View(addUpdateMovieModel);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieRepository movieRepository = new MovieRepository(db);
            Movie movie = movieRepository.GetById(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovieRepository movieRepository = new MovieRepository(db);
            Movie movie = movieRepository.GetById(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            movieRepository.Delete(movie);
            movieRepository.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
