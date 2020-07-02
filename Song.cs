using System.Collections.Generic;

namespace AshtonBro.Code
{
    public class Song
    {
        public int SongId { get; set; }
        public string Name { get; set; }
        public int BandId { get; set; }
        public virtual Band Band { get; set; }
    }
}
