namespace LaconiaPAQ;

public class LZP
{
    private const int MEM = 1 << 22;
    private readonly byte[] buf;
    private readonly uint[] t;
    private int pos;

    public LZP()
    {
        buf = new byte[MEM / 8];
        t = new uint[MEM / 32];
        pos = 0;
    }

    public int Predict()
    {
        return 0; // minimal placeholder
    }

    public void Update(int ch)
    {
        buf[pos % buf.Length] = (byte)ch;
        pos++;
    }
}