using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineplexCinemas.Models
{
    public class cartItem
    {
        public int cartId { get; set; }
        public int sessionId { get; set; }
        public int movieId { get; set; }
        public int cineplxId { get; set; }
        public int numberOfAdults { get; set; }
        public int numberOfConc { get; set; }
        public string customerName { get; set; }
    }
}
