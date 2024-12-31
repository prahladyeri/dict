/**
 * @brief WordNet parser logic.
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

    class WordNetIndexReader
    {
        public static List<WordIndexEntry> ReadIndexFile(string filePath)
        {
            var entries = new List<WordIndexEntry>();
            foreach (string line in File.ReadAllLines(filePath))
            {
                if (line.StartsWith("  ")) continue; // Skip comments

                string[] parts = line.Trim().Split(' ');

                int synsetCount = int.Parse(parts[2]);
                int pointerCount = int.Parse(parts[3]);
                int synsetOffsetCount = parts.Length - (4 + pointerCount);
                //Console.WriteLine(String.Format("Line: {0}", line));
                //Console.WriteLine(String.Format("Parts: {0}", string.Join(", ", parts)));
                //Console.WriteLine(String.Format("PointerCount (Index 3): {0}", parts[3]));
                //Console.WriteLine(String.Format("SynsetOffsets Start Index: {0}",parts.Length - synsetOffsetCount));

                var entry = new WordIndexEntry
                {
                    Word = parts[0],
                    Pos = parts[1],
                    SynsetCount = synsetCount,
                    PointerCount = pointerCount,
                    Pointers = ExtractArray(parts, 4, pointerCount),
                    SynsetOffsets = Array.ConvertAll(
                        ExtractArray(parts, 4 +  pointerCount, synsetOffsetCount),
                        int.Parse
                    )
                };

                if (4 + pointerCount < parts.Length)
                {
                    entry.SynsetOffsets = Array.ConvertAll(
                        ExtractArray(parts, 4 + pointerCount, parts.Length - (4 + pointerCount)),
                        int.Parse
                    );
                }
                else
                {
                    Console.WriteLine("Synset offsets extraction is out of bounds!");
                }

                entries.Add(entry);
            }
            return entries;
        }

        private static string[] ExtractArray(string[] array, int start, int count)
        {
            string[] result = new string[count];
            Array.Copy(array, start, result, 0, count);
            return result;
        }
    }

    class WordIndexEntry
    {
        public string Word { get; set; }
        public string Pos { get; set; }
        public int SynsetCount { get; set; }
        public int PointerCount { get; set; }
        public string[] Pointers { get; set; }
        public int[] SynsetOffsets { get; set; }
    }
}
