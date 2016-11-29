using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using eCommerce.DAL.Repositories;
using eCommerce.Model;

namespace eCommerce.WebUI.ViewModels
{
    public class AddUpdateMovieModel
    {
        public List<Genre> genres { get; set; }

        public int MovieId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public int GenreId { get; set; }

        [Display(Name = "Date Released")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Date Added")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Number in Stock")]
        public int NumberInStock { get; set; }

        public string PageTitle { get; set; }

        public AddUpdateMovieModel()
        {
            MovieId = 0;
            ReleaseDate = DateTime.Now;
            DateAdded = DateTime.Now;
        }

        public AddUpdateMovieModel(Movie movie)
        {
            MovieId = movie.MovieId;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            DateAdded = movie.DateAdded;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }

    }

}