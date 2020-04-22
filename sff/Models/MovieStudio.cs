using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sff.Models
{
    public class MovieStudio
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int StudioId { get; set; }
        public Studio Studio { get; set; }

        

    }
}
