using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineplexApi.Models
{
    [Table("Enquiry")]
    public class Enquiry
    {
        /**
         * Delcare variables
         * Note : In order to read data from database,
         * variable names should match database column names
         */
        [Key]
        public int EnquiryID { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        public DateTime EventDate { get; set; }
    }
}
