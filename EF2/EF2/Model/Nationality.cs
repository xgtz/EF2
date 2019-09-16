using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Model
{
    public class Nationality
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
    }
}
