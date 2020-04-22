using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace sff.Models
{
    
    public class PostEtikett
    {
        
        public int Id { get; set; }
        
        public string Movie { get; set; }
        
        public string City { get; set; }
        
        public DateTime Date { get; set; }
    }
}
