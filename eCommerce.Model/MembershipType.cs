using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Model
{
    public class MembershipType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Membership Type")]

        public string Name { get; set; }

        public short SignUpFee { get; set; }

        public byte DurationInMonths { get; set; }

        [Display(Name = "Discount Rate")]
        public byte DiscountRate { get; set; }

        // Define some static types for our default rates, so we can use the types in our 
        // code, instead of 'Magic Numbers'. This helps other developers understand the code better.
        // For example, see the Min18YearsIfMember custom validation.
        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;
    }
}
