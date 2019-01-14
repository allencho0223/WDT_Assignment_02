using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CineplexCinemas.Models
{
    public class CheckOut
    {
        [RegularExpression("^([a-zA-z ,.-]+)$", ErrorMessage = "Invalid Name")]
        [Required]
        public string FullName { get; set; }

        [RegularExpression("^([a-z0-9_.-]+[@]{1}[a-z0-9]{1,15}[.]{1}[a-z]{1,5}[.]{0,1}[a-z]{0,5}[.]{0,1}[a-z]{0,5})$", ErrorMessage = "Invalid Email")]
        [Required]     
        public string Email { get; set; }

        [RegularExpression("^(([+]?)([0-9]{3,4})([- ]?)([0-9]{3,4})([- ]?)([0-9]{3,4}))$", ErrorMessage = "Invalid Phone")]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
