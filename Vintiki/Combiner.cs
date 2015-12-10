using System;
using System.Threading;

namespace Vintiki
{
    class Combiner : ITakeable
    {
        private SemaphoreSlim producedCount, freeSlots;
        private ITakeable m1;
        private ITakeable m2;
        private bool printResult;
        public Combiner(ITakeable m1, ITakeable m2, int limit)
        {
            this.m1 = m1;
            this.m2 = m2;
            producedCount = new SemaphoreSlim(0, limit);
            freeSlots = new SemaphoreSlim(limit, limit);
        }

        public void Combine(object message)
        {
            while (Program.keepOnGoing)
            {
                m1.Take();
                m2.Take();
                freeSlots.Wait();
                producedCount.Release();
                if (!String.IsNullOrWhiteSpace((string)message))
                    Console.WriteLine(message);
            }
        }

        public void Take()
        {
            producedCount.Wait();
            freeSlots.Release();
        }
    }
}
