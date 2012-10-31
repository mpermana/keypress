using System;
using System.Threading;

namespace Mitaru
{

    class Throttle
    {
        private DateTime last;
        public int threshold = 10;

        public Throttle(int threshold,bool lastNow = false)
        {
            this.threshold = threshold;
            if (lastNow)
                this.last = new DateTime();
            else
                this.last = new DateTime(0);
        }

        public bool isReady(bool sleepUntilReady = false)
        {
            DateTime now = DateTime.Now;
            double elapsed = (now - last).TotalSeconds;
            double sleepTime = 1000 * (threshold - elapsed);
            if (sleepTime > 0 && sleepUntilReady)
            {
                Console.WriteLine("sleeping for " + sleepTime);
                Thread.Sleep((int)sleepTime);
            }

            if (elapsed > threshold)
            {
                //Console.WriteLine("now=" + now + ",last=" + last + " this = "+this);
                reset();
                return true;
            }
            return false;
        }

        public void reset()
        {
            last = DateTime.Now;
        }

    }

}