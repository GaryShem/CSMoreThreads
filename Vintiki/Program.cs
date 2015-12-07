using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vintiki
{
    class Program
    {
    public static bool keepOnGoing = true;
        static void Main(string[] args)
        {
            Producer a = new Producer(1000, 1);
            Producer b = new Producer(2000, 1);
            Producer c = new Producer(3000, 1);
            Combiner ab = new Combiner(a, b, 1);
            Combiner widget = new Combiner(ab, c, 50);

            Thread t1 = new Thread(a.Produce);
            Thread t2 = new Thread(b.Produce);
            Thread t3 = new Thread(c.Produce);
            Thread t4 = new Thread(ab.Combine);
            Thread t5 = new Thread(widget.Combine);
            t1.Start("A produced");
            t2.Start("B produced");
            t3.Start("C produced");
            t4.Start("A&B combined");
            t5.Start("Widget produced");
            Console.ReadLine();
            keepOnGoing = false;
        }
    }
}
