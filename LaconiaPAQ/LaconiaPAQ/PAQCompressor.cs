using System;
using System.IO;

namespace LaconiaPAQ;

public class PAQCompressor
{
    private readonly LZP _lzp;
    private readonly Predictor _predictor;

    public PAQCompressor()
    {
        _lzp = new LZP();
        _predictor = new Predictor();
    }

    public void Compress(string inputPath,string outputPath)
    {
        if (!IsPaqFile(outputPath))
        {
            return;
        }
        using (var inFile = new FileStream(inputPath, FileMode.Open))
        using (var outFile = new FileStream(outputPath, FileMode.Create))
        {
            var encoder = new Encoder(outFile);
            int b;
            while ((b = inFile.ReadByte()) != -1)
            {
                int p = _predictor.Predict(0);
                encoder.EncodeBit(b & 1);
                _lzp.Update(b);
                _predictor.Update(0, b);
            }
            encoder.Flush();
        }
    }

    public void Decompress(string inputPath,string outputPath)
    {
        if (!IsPaqFile(inputPath))
        {
            return;
        }
        using (var inFile = new FileStream(inputPath, FileMode.Open))
        using (var outFile = new FileStream(outputPath, FileMode.Create))
        {
            int b;
            while ((b = inFile.ReadByte()) != -1)
            {
                outFile.WriteByte((byte)b);
            }
        }
    }

    private bool IsPaqFile(string inputNameFileOrPath)
    {
        return Path.GetExtension(inputNameFileOrPath)!.Equals(".paq", StringComparison.OrdinalIgnoreCase);
    }
}