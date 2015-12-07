using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vintiki
{
    class Producer : ITakeable
    {
        private SemaphoreSlim producedCount, freeSlots;
        private int delay;
        public Producer(int milliseconds, int limit)
        {
            if (milliseconds < 0)
                throw new Exception();
            delay = milliseconds;
            producedCount = new SemaphoreSlim(0, limit);
            freeSlots = new SemaphoreSlim(limit, limit);
        }

        public void Produce(object message)
        {
            while (Program.keepOnGoing)
            {
                Thread.Sleep(delay);
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
