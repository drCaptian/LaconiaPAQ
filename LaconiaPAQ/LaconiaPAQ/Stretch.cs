using System.Diagnostics;

namespace LaconiaPAQ;

public class Stretch
{
    private readonly short[] t = new short[4096];
    public Stretch(Squash squash)
    {
        int pi = 0;
        for (int x = -2047; x <= 2047; x++)
        {
            int i = squash.Map(x);
            for (int j = pi; j <= i; j++)
                t[j] = (short)x;
            pi = i + 1;
        }
        t[4095] = 2047;
    }

    public int Map(int p)
    {
        Debug.Assert(p >= 0 && p < 4096);
        return t[p];
    }
}