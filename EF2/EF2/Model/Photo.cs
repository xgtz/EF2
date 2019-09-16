using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Model
{
    public class Photo
    {
        public int FaceId { get; set; }

        public string Path { get; set; }

        public virtual Face Face { get; set; }
    }
}
