using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using eCommerce.Model;

namespace eCommerce.WebUI.ViewModels
{
    public class AddUpdateMovieModel
    {
        public IEnumerable<Genre> genres { get; set; }

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

        public AddUpdateMovieModel()
        {
            MovieId = 0;
        }

        public AddUpdateMovieModel(Movie movie)
        {
            MovieId = movie.MovieId;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }

    }

}