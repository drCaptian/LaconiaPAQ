using System;
using System.IO;

namespace LaconiaPAQ;

public class PAQCompressor
{
    private readonly string _toAction;
    private readonly LZP _lzp;
    private readonly Predictor _predictor;

    public PAQCompressor(string fileToAction)
    {
        _toAction = fileToAction;
        _lzp = new LZP();
        _predictor = new Predictor();
        
    }

    public void Compress(string output)
    {
        using (var inFile = new FileStream(_toAction, FileMode.Open))
        using (var outFile = new FileStream(output, FileMode.Create))
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

    public void Decompress(string output)
    {
        using (var inFile = new FileStream(_toAction, FileMode.Open))
        using (var outFile = new FileStream(output, FileMode.Create))
        {
            int b;
            while ((b = inFile.ReadByte()) != -1)
            {
                outFile.WriteByte((byte)b);
            }
        }
    }
}