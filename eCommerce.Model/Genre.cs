using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Model
{
    public class Genre
    {
        [Required]
        public int GenreId { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Genre Type")]
        public string Name { get; set; }
    }
}
