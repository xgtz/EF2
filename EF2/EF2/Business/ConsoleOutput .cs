using EF2.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Business
{
    public class ConsoleOutput : IOutput
    {
        public void Write(string context)
        {
            Console.WriteLine(context);
        }
    }
}
