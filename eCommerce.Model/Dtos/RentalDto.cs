using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Model.Dtos
{
    public class RentalDto
    {
        public int Id { get; set; }

        public CustomerDto CustomerDto { get; set; }

        public int CustomerId { get; set; }

        public MovieDto MovieDto { get; set; }

        public int MovieId { get; set; }

        public DateTime DateRented { get; set; }

        public DateTime? DateReturned { get; set; }

    }
}
