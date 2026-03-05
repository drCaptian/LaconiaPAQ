# LaconiaPAQ
# LaconiaPAQ

Минимальная PAQ-inspired реализация компрессора на C#.

Функциональность
- Побитовую обработку данных
- Контекстное прогнозирование
- Архитектуру в стиле PAQ9a
- Потоковую работу с файлами

> ВАЖНО: Это упрощенная реализация. Не является полной копией PAQ9a.

---

## Требования

- .NET 6.0 или выше

---

## 📦 Использование

- У основного класса PAQCompressor через конструктор передается
- string fileToAction - входной файл/также является путем
- string pathToSaveResult - выходной файл/также является путем
- Мод (Compress/Decompress), используется Enum CompressMode


## Пример
PAQCompressor paq = new PAQCompressor("output.paq","output.txt",CompressorMode.Decompress);
