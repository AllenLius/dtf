using System.Collections.Generic;

namespace Dta.Core
{
    public static class QueueExtension
    {
        public static T[] DequeueRange<T>(this Queue<T> queue, int count)
        {
            T[] ts = new T[count];
            for (int i = 0; i < count; i++)
            {
                ts[i] = queue.Dequeue();
            }
            return ts;
        }

        public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> ts)
        {
            foreach (T t in ts)
            {
                queue.Enqueue(t);
            }
        }

        //public static void EnqueueRange<T>(this Queue<T> queue, T[] ts, int count)
        //{
        //    for(int i=0;i<count;i++)
        //    {
        //        queue.Enqueue(ts[i]);
        //    }
        //}

        public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> ts, int count)
        {
            foreach (T t in ts)
            {
                if (count == 0)
                {
                    break;
                }
                queue.Enqueue(t);
                count--;
            }
        }

        public static T[] PickRange<T>(this Queue<T> queue, int count)
        {
            T[] ts = new T[count];
            queue.CopyTo(ts, 0);
            return ts;
        }
    }
}
