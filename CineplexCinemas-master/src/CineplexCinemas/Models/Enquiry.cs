using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CineplexCinemas.Models
{
    public partial class Enquiry
    {
        public int EnquiryId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        public DateTime EventDate { get; set; }
    }
}
