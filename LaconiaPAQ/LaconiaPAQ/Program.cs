namespace LaconiaPAQ;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "input.txt";
        string compressedPath = "compressed.paq";
        string decompressedPath = "output.txt";

        PAQCompressor paq = new PAQCompressor(filePath);
        paq.Compress(compressedPath);
        paq.Decompress(compressedPath);

        Console.WriteLine("PAQ9a compression/decompression finished!");
    }
}