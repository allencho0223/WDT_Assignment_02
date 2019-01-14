using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CineplexCinemas.Models
{
    public partial class Booking
    {
        public Booking()
        { }
        public Booking(int cineplxId, int movieId, int sessionId)
        {
            this.cineplxId = cineplxId;
            this.movieId = movieId;
            this.sessionId = sessionId;
        }

        public int BookingId { get; set; }
        [Range(0, 5)]
        public int numberOfAdults { get; set; }
        [Range(0, 5)]
        public int numberOfConc { get; set; }
        [Required]
        public string customerName { get; set; }
        public int sessionId { get; set; }
        public int movieId { get; set; }
        public int cineplxId { get; set; }
    }
}
