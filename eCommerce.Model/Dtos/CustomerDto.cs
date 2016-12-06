using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Model.CustomValidations;

namespace eCommerce.Model.Dtos
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }

        [Required]  // Make the Customer name field be set to 'NOT NULL'
        [StringLength((255), ErrorMessage = "Name must be 2-255 characters long", MinimumLength = 2)] // Make the maximum field string length be 255
        public string CustomerName { get; set; }

        [Required]  // Make the Customer name field be set to 'NOT NULL'
        [StringLength(50)] // Make the maximum field string length be 50
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Town { get; set; }

        public string PostCode { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public int MembershipTypeId { get; set; }  // Foreign Key using the <Other Table>Id convention

        public DateTime? BirthDate { get; set; }
    }
}
