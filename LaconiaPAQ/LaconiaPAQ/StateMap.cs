namespace LaconiaPAQ;

public class StateMap
{
    private readonly int N;
    private readonly uint[] t;
    private static readonly int[] dt = new int[1024];
    private int cxt;

    static StateMap()
    {
        for (int i = 0; i < 1024; i++)
            dt[i] = 16384 / (i + i + 3);
    }

    public StateMap(int n)
    {
        N = n;
        t = new uint[N];
        for (int i = 0; i < N; i++)
            t[i] = 1u << 31;
        cxt = 0;
    }

    public void Update(int y)
    {
        int n = (int)(t[cxt] & 1023);
        int p = (int)(t[cxt] >> 10);
        if (n < 255) n++;
        int err = ((y << 22) - p) >> 3;
        t[cxt] += (uint)((err * dt[n]) & 0xfffffc00 | n);
    }

    public int P(int cx)
    {
        cxt = cx;
        return (int)(t[cxt] >> 20);
    }
}