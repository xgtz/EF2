using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Model
{
    public class Face
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        //[Timestamp]
        //public byte[] v { get; set; }

        public virtual FaceType FaceType { get; set; }

        public virtual Photo Photo { get; set; }
    }
}
