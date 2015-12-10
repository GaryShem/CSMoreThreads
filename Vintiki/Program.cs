using System;
using System.Threading;

namespace Vintiki
{
    class Program
    {
    public static bool keepOnGoing = true;
        static void Main(string[] args)
        {
            Producer a = new Producer(1000, 5);
            Producer b = new Producer(2000, 5);
            Producer c = new Producer(3000, 5);
            Combiner ab = new Combiner(a, b, 5);
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
