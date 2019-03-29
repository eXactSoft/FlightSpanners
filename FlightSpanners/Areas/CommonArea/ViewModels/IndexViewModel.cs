using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSpanners.Areas.CommonArea.ViewModels //.Home
{
    public class IndexViewModel
    {
        [Required(ErrorMessage = "Please Enter your Code")]
        [MaxLength(5, ErrorMessage = "The length of your Code is 5")]
				[MinLength(5, ErrorMessage = "The length of your Code is 5")]
        [RegularExpression("[0-9]{1,}", ErrorMessage = "Your Code Only Accept numbers")]
        public string Code { get; set; }
				
				// To force the signin role to be spanner for the user who has double account (spanner & organizer)
				// If true the spanner is select to sign in as spanner (select only 7 not means it is already a spanner)
				[Required(ErrorMessage = "Please Select your Role")]
				public string SignInUserRole { get; set; } //Default value: Organizer

				public string ReturnUrl { get; set; }
	}
}
