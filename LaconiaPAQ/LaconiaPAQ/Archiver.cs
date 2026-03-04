namespace LaconiaPAQ;

public class Archiver
{
    private readonly string filePath;

    public Archiver(string path)
    {
        filePath = path;
    }

    public void Compress(string outputPath)
    {
        using var input = File.OpenRead(filePath);
        using var output = File.Create(outputPath);

        var encoder = new ArithmeticEncoder(output);
        var predictor = new BitPredictor();

        output.Write(BitConverter.GetBytes(input.Length));

        int b;
        while ((b = input.ReadByte()) != -1)
        {
            for (int i = 7; i >= 0; i--)
            {
                int bit = (b >> i) & 1;
                encoder.Encode(bit, predictor);
            }
        }

        encoder.Finish();
    }

    public void Decompress(string outputPath)
    {
        using var input = File.OpenRead(filePath);
        using var output = File.Create(outputPath);

        long size = BitConverter.ToInt64(ReadBytes(input, 8));

        var decoder = new ArithmeticDecoder(input);
        var predictor = new BitPredictor();

        for (long i = 0; i < size; i++)
        {
            int b = 0;
            for (int j = 0; j < 8; j++)
            {
                int bit = decoder.Decode(predictor);
                b = (b << 1) | bit;
            }
            output.WriteByte((byte)b);
        }
    }

    private byte[] ReadBytes(Stream s, int n)
    {
        byte[] buffer = new byte[n];
        s.Read(buffer, 0, n);
        return buffer;
    }
}