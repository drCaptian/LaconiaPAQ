using System.IO;

namespace LaconiaPAQ;

public class BitStream
{
    private readonly Stream stream;
    private int buffer;
    private int bitsInBuffer;

    public BitStream(Stream s)
    {
        stream = s;
        buffer = 0;
        bitsInBuffer = 0;
    }

    public void WriteBit(int bit)
    {
        buffer = (buffer << 1) | (bit & 1);
        bitsInBuffer++;
        if (bitsInBuffer == 8)
        {
            stream.WriteByte((byte)buffer);
            bitsInBuffer = 0;
            buffer = 0;
        }
    }

    public void Flush()
    {
        if (bitsInBuffer > 0)
        {
            buffer <<= (8 - bitsInBuffer);
            stream.WriteByte((byte)buffer);
        }
        stream.Flush();
    }

    public int ReadBit()
    {
        if (bitsInBuffer == 0)
        {
            int b = stream.ReadByte();
            if (b == -1) return -1;
            buffer = b;
            bitsInBuffer = 8;
        }

        int bit = (buffer >> (bitsInBuffer - 1)) & 1;
        bitsInBuffer--;
        return bit;
    }
}