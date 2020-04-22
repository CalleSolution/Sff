using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sff.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int RentLimit { get; set; }
        

        //Rent a movie
        public void RentMovie()
        {
            this.RentLimit-=1;
        }

        //Return Movie
        public void ReturnMovie()
        {
            this.RentLimit+=1;
        }
    }
}
