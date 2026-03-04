namespace LaconiaPAQ;

public class ArithmeticDecoder
{
    private ulong low = 0;
    private ulong high = ulong.MaxValue;
    private ulong code = 0;
    private readonly Stream input;

    public ArithmeticDecoder(Stream inputStream)
    {
        input = inputStream;
        for (int i = 0; i < 8; i++)
            code = (code << 8) | (uint)input.ReadByte();
    }

    public int Decode(BitPredictor predictor)
    {
        ulong range = high - low;
        ulong mid = low + (range >> 12) * (ulong)predictor.Probability;

        int bit;
        if (code <= mid)
        {
            bit = 1;
            high = mid;
        }
        else
        {
            bit = 0;
            low = mid + 1;
        }

        predictor.Update(bit);

        while ((low ^ high) >> 56 == 0)
        {
            low <<= 8;
            high = (high << 8) | 0xFF;
            code = (code << 8) | (uint)input.ReadByte();
        }

        return bit;
    }
}