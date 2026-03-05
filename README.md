# LaconiaPAQ
Modern PAQ (C#)

Минимальная реализация PAQ-подобного компрессора на C#
Поддерживает побитовую обработку, контекстное прогнозирование и базовую структуру PAQ9a.


Требования

.NET 6+ (или выше)


Использование
PAQ9aCompressor compressor = new PAQ9aCompressor("input.txt");
compressor.Compress("compressed.paq");

PAQ9aCompressor decompressor = new PAQ9aCompressor("compressed.paq");
decompressor.Decompress("output.txt");
