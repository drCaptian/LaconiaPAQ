namespace LaconiaPAQ;

public class Squash
{
    private readonly short[] tab = new short[4096];
    public Squash()
    {
        int[] t = {1,2,3,6,10,16,27,45,73,120,194,310,488,747,1101,
            1546,2047,2549,2994,3348,3607,3785,3901,3975,4022,
            4050,4068,4079,4085,4089,4092,4093,4094};
        for (int i = -2048; i < 2048; i++)
        {
            int w = i & 127;
            int d = (i >> 7) + 16;
            tab[i + 2048] = (short)((t[d] * (128 - w) + t[d + 1] * w + 64) >> 7);
        }
    }
    public int Map(int d)
    {
        d += 2048;
        if (d < 0) return 0;
        if (d > 4095) return 4095;
        return tab[d];
    }
}