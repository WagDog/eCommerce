using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]  // Make the Customer name field be set to 'NOT NULL'
        [StringLength((255), ErrorMessage = "Name must be 2-255 characters long", MinimumLength = 2)] // Make the maximum field string length be 255
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Required]  // Make the Customer name field be set to 'NOT NULL'
        [StringLength(50)] // Make the maximum field string length be 50
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        public string Town { get; set; }

        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        [Display(Name = "Subscribed to Newsletter?")]
        public bool IsSubscribedToNewsletter  { get; set; }

        public MembershipType MembershipType { get; set; }    // Navigation Property that Entity will use to map the two tables
                                                              // and bring the MembershipTypes linked to this Customer
                                                              // probably using a JOIN sql command

        [Display(Name = "Membership Type")]
        public int MembershipTypeId { get; set; }  // Foreign Key using the <Other Table>Id convention

        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime BirthDate { get; set; }

    }
}
