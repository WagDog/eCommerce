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
        
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        public Genre Genre { get; set; }

        public int GenreId { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

        public int NumberInStock { get; set; }
    }
}
