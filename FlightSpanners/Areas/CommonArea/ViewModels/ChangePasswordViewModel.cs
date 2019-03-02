using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSpanners.Areas.CommonArea.ViewModels //.Home
{
    public class ChangePasswordViewModel
		{
        [Required(ErrorMessage = "Please Enter The Old Password")]
				[DataType(DataType.Password)]
        public string PasswordOld { get; set; }

        [Required(ErrorMessage = "Please Enter the New Password")]
				[DataType(DataType.Password)]
				[MinLength(3, ErrorMessage = "Min length of the New Password is 3")]
        public string PasswordNew { get; set; }

		    [Required(ErrorMessage = "please Confirm the New Password")]
				[DataType(DataType.Password)]
				[Compare(nameof(PasswordNew), ErrorMessage = "The New Password and Confirm Password do not match")]
        public string PasswordConfirm { get; set; }
    }
}
