namespace TPinfo
{
    class Suite
    {
        public long U0;
        public long M;
        public long Fac;
        public long Terme;

        public Suite(long u0, long m, long fac = 1, long terme = 0)
        {
            Terme = terme;
            U0 = u0;
            M = m;
            Fac = fac;
        }

        public long[] List(long k)
        {
            long[] ulist = new long[k + 1];
            ulist[0] = U0;
            for (int i = 1; i <= k; i++)
            {
                ulist[i] = (Fac * ulist[i - 1] + Terme) % M;
            }
            return ulist;
        }

        public long this[long k] { get { return List(k)[k]; } }

        static public long Mod(long x, long n)
        {
            while (x < 0) x += n;
            while (x >= 100000 * n) x -= 100000 * n;
            while (x >= 10000 * n) x -= 10000 * n;
            while (x >= 1000 * n) x -= 1000 * n;
            while (x >= 100 * n) x -= 100 * n;
            while (x >= 10 * n) x -= 10 * n;
            while (x >= n) x -= n;
            return x;
        }
    }
}
