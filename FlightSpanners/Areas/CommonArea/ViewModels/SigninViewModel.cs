using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSpanners.Areas.CommonArea.ViewModels //.Home
{
    public class SigninViewModel
    {
        [Required(ErrorMessage = "Please Enter your Code")]
        [MaxLength(5, ErrorMessage = "Max length of your Code is 5")]
        [RegularExpression("[0-9]{1,}", ErrorMessage = "Your Code Only Accept numbers")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please Enter the Password")]
        public string Password { get; set; }
				
				// To force the signin role to be spanner for the user who has double account (spanner & organizer)
				// If true the spanner is select to sign in as spanner (select only 7 not means it is already a spanner)
				public bool IsSigninAsSpanner { get; set; } //Default value: false
	}
}
