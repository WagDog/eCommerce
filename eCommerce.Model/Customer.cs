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
        [StringLength(255)] // MAke the maximum field string length be 255
        public string CustomerName { get; set; }

        [Required]  // Make the Customer name field be set to 'NOT NULL'
        [StringLength(50)] // Make the maximum field string length be 255
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string PostCode { get; set; }
        public bool IsSubscribedToNewsletter  { get; set; }
        public MembershipType MembershipType { get; set; }    // Navigation Property that Entity will use to map the two tables
                                                              // and bring the MembershipTypes linked to this Customer
                                                              // probably using a JOIN sql command
        public int MembershipTypeId { get; set; }  // Foreign Key using the <Other Table>Id convention

    }
}
