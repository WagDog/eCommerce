using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Model
{
    public class Rental
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public Movie Movie { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        public DateTime DateRented { get; set; }

        public DateTime? DateReturned { get; set; }

    }
}
