using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppsClassification
{
    class Program
    {
        static void Main(string[] args)
        {
            Classification c = new Classification();
            c.Run();
            Console.ReadKey();
        }
    }
}
