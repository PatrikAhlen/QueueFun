using System.Threading;

namespace Consumer
{
    public class MessageCounter
    {
        private static MessageCounter _instance;
        private static int _count;
        private static readonly object LockObj = new object();

        public MessageCounter()
        {
            _count = 0;
        }

        public static MessageCounter Instance()
        {
            return _instance ??= new MessageCounter();
        }

        public int Inc()
        {
            lock (LockObj)
            {
                //Interlocked.Increment(ref _count);
                var x = Interlocked.Increment(ref _count);
                return x;
            }
        }

        public int Value
        {
            get
            {
                lock (LockObj)
                {
                    return _count;
                }
            }
        }

    }
}