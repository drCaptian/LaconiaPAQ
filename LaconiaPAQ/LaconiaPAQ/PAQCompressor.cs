using System;
using System.IO;

namespace LaconiaPAQ;

public class PAQCompressor
{
    private readonly string _toAction;
    private readonly LZP lzp;
    private readonly Predictor predictor;

    public PAQCompressor(string fileToAction, string pathToSaveResult, CompressorMode mode)
    {
        _toAction = fileToAction;
        lzp = new LZP();
        predictor = new Predictor();
        switch (mode)
        {
            case CompressorMode.Compress :
                const string allowedType = ".paq";
                if (pathToSaveResult.EndsWith(allowedType, StringComparison.OrdinalIgnoreCase))
                {
                    Compress(pathToSaveResult);
                }
                else
                {
                    throw new Exception("Wrong type in output file");
                }
                break;
            case CompressorMode.Decompress:
                if (fileToAction.EndsWith(allowedType, StringComparison.OrdinalIgnoreCase))
                {
                    Decompress(pathToSaveResult);
                }
                else
                {
                    throw new Exception("Wrong type in input file");
                }
                break;
            default:
                throw new Exception("Non-valid mode compressor");
        }
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
                int p = predictor.Predict(0);
                encoder.EncodeBit(b & 1);
                lzp.Update(b);
                predictor.Update(0, b);
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