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
    }
}
