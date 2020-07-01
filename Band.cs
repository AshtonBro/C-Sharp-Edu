using System.Collections.Generic;

namespace AshtonBro.Code
{
    public class Band
    {
        public int BandId { get; set; }
        public string Name { get; set; }
        public int?  Year { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}
