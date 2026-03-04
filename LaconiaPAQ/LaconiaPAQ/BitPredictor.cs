namespace LaconiaPAQ;

public class BitPredictor
{
    private int count0 = 1;
    private int count1 = 1;

    public int Probability
    {
        get
        {
            int total = count0 + count1;
            return (count1 << 12) / total;
        }
    }

    public void Update(int bit)
    {
        if (bit == 1)
            count1++;
        else
            count0++;

        if (count0 + count1 > 10000)
        {
            count0 >>= 1;
            count1 >>= 1;
        }
    }
}