namespace LaconiaPAQ;

public class ArithmeticEncoder
{
    private ulong low = 0;
    private ulong high = ulong.MaxValue;
    private readonly Stream output;

    public ArithmeticEncoder(Stream outputStream)
    {
        output = outputStream;
    }

    public void Encode(int bit, BitPredictor predictor)
    {
        ulong range = high - low;
        ulong mid = low + (range >> 12) * (ulong)predictor.Probability;

        if (bit == 1)
            high = mid;
        else
            low = mid + 1;

        predictor.Update(bit);

        while ((low ^ high) >> 56 == 0)
        {
            output.WriteByte((byte)(high >> 56));
            low <<= 8;
            high = (high << 8) | 0xFF;
        }
    }

    public void Finish()
    {
        for (int i = 0; i < 8; i++)
        {
            output.WriteByte((byte)(low >> 56));
            low <<= 8;
        }
    }
}