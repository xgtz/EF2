using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Model
{
    public class Teacher
    {
        public Teacher()
        {
            this.Students = new HashSet<Student>();
        }

        public string ID { get; set; }
        public string Name { get; set; }

        
        public virtual ICollection<Student> Students { get; set; }
    }
}
