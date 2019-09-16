using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Model
{
    public class Student
    {
        public Student()
        { 
           this.Teachers = new HashSet<Teacher>();
        }

        public string ID { get; set; }

        public string Name { get; set; }

        public int StudentNumber { get; set; }

        //[NotMapped]
        public bool IsMale { get; set; }

       public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
