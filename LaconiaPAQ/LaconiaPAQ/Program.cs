namespace LaconiaPAQ;

class Program
{
    static void Main(string[] args)
    {

        PAQCompressor paq = new PAQCompressor("input.txt","output.paq",CompressorMode.Compress);

        Console.WriteLine("PAQ9a compression/decompression finished!");
    }
}