using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Model
{
    public class Movie
    {
        [Required]
        public int MovieId { get; set; }
        
        [Required(ErrorMessage = "Please enter a movie name")] // We have to have a name, and we overwrite the default error message if the form is saved without a name.
        [StringLength(30)]      // Maximum string length of the name       
        public string Name { get; set; }
        
        public Genre Genre { get; set; }

        public int GenreId { get; set; }

        [Display(Name = "Date Released")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Date Added")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Number in Stock")]
        public int NumberInStock { get; set; }
    }
}
