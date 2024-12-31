/**
 * @brief WordNet reader logic.
 * 
 * @author Prahlad Yeri <prahladyeri@yahoo.com>
 * @date 2024-12-31
 * @license MIT
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace dict
{

    class WordNetDataReader
    {
        public static Dictionary<int, Synset> ReadDataFile(string filePath)
        {
            var synsets = new Dictionary<int, Synset>();
            foreach (string line in File.ReadAllLines(filePath))
            {
                if (line.StartsWith("  ")) continue; // Skip comments

                string[] parts = line.Split('|');
                string[] metadata = parts[0].Trim().Split(' ');

                int wordCount = ConvertHexToInt(metadata[3]);

                var synset = new Synset
                {
                    Offset = int.Parse(metadata[0]),
                    LexicalFileNumber = int.Parse(metadata[1]),
                    Pos = metadata[2],
                    WordCount = wordCount,
                    Words = ExtractWords(metadata, 4, wordCount),
                    Gloss = parts[1].Trim()
                };

                synsets[synset.Offset] = synset;
            }
            return synsets;
        }

        private static List<string> ExtractWords(string[] array, int start, int wordCount)
        {
            var words = new List<string>();
            for (int i = 0; i < wordCount; i++)
            {
                words.Add(array[start + (i * 2)]);
            }
            return words;
        }

        private static int ConvertHexToInt(string hex)
        {
            return int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        }
    }

    class Synset
    {
        public int Offset { get; set; }
        public int LexicalFileNumber { get; set; }
        public string Pos { get; set; }
        public int WordCount { get; set; }
        public List<string> Words { get; set; }
        public string Gloss { get; set; }
    }
}
