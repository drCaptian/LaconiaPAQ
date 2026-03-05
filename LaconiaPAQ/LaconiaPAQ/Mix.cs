using System.Diagnostics;

namespace LaconiaPAQ;

public class Mix
{
    private readonly int N;
    private readonly int[] wt;
    private int x1, x2, cxt, pr;

    public Mix(int n)
    {
        N = n;
        wt = new int[N * 2];
        for (int i = 0; i < N * 2; i++)
            wt[i] = 1 << 23;
    }

    public int PP(int p1, int p2, int cx)
    {
        Debug.Assert(cx >= 0 && cx < N);
        cxt = cx * 2;
        x1 = p1;
        x2 = p2;
        pr = (x1 * (wt[cxt] >> 16) + x2 * (wt[cxt + 1] >> 16) + 128) >> 8;
        return pr;
    }

    public void Update(int y)
    {
        int err = ((y << 12) - pr);
        wt[cxt] += x1 * err;
        wt[cxt + 1] += x2 * err;
    }
}