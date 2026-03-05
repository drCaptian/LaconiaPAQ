namespace LaconiaPAQ;

public class Encoder
{
    private readonly BitStream bs;

    public Encoder(Stream s)
    {
        bs = new BitStream(s);
    }

    public void EncodeBit(int bit)
    {
        bs.WriteBit(bit);
    }

    public void Flush()
    {
        bs.Flush();
    }
}