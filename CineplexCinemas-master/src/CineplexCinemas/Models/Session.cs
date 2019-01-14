using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineplexCinemas.Models
{
    public partial class Session
    {
        public Session()
        {
            CineplexMovie = new HashSet<CineplexMovie>();
        }

        public int SessionId { get; set; }
        public int SeatsAvailable { get; set; }
        public int SeatsTotal { get; set; }
        public DateTime SessionDateTime { get; set; }
        public int? FilmMovieId { get; set; }

        public virtual ICollection<CineplexMovie> CineplexMovie { get; set; }
        public virtual Movie FilmMovie { get; set; }
    }
}
