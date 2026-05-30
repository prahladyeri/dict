/**
 * @brief Entry point.
 * 
 * @author Prahlad Yeri <prahladyeri@yahoo.com>
 * @date 2024-12-31
 * @license MIT
 */
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Text;

namespace dict
{
    class Program
    {
        static string wnPath = AppDomain.CurrentDomain.BaseDirectory + @"wn.dict\";
        static List<WordIndexEntry> indexEntries = new List<WordIndexEntry>();
        static Dictionary<string, Synset> synsets = new Dictionary<string, Synset>();
        static string[] posSuffixes = { "noun", "verb", "adj", "adv" };

        private static void lookUp(string wordToLookup) {
            var matchingEntries = indexEntries.Where(e => e.Word == wordToLookup).ToList();
            if (!matchingEntries.Any()) {
                Console.WriteLine("Word not found.");
                return;
            }
            Console.WriteLine($"Word: {wordToLookup}\n");
            foreach (var entry in matchingEntries) {
                string dataFile = wnPath + "data." + entry.Pos;
                if (!File.Exists(dataFile)) continue;

                using (FileStream fs = new FileStream(dataFile, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                    bool headerPrinted = false;
                    int definitionIndex = 1;
                    foreach (int offset in entry.SynsetOffsets)
                    {
                        //string key = $"{entry.Pos}_{offset}";
                        fs.Seek(offset, SeekOrigin.Begin);
                        string line = ReadLineFromStream(fs);
                        if (string.IsNullOrEmpty(line)) continue;

                        Synset synset = WordNetDataReader.ParseDataLine(line);
                        if (synset == null) continue;

                        if (!headerPrinted)
                        {
                            Console.WriteLine($"[{entry.Pos}]");
                            headerPrinted = true;
                        }
                        string synonyms = string.Join(", ", synset.Words);
                        Console.WriteLine($"  {definitionIndex++}. ({synonyms}) {synset.Gloss}");
                    }
                    if (headerPrinted) Console.WriteLine("");

                }
            }
        }


        private static string ReadLineFromStream(FileStream fs)
        {
            List<byte> byteBuffer = new List<byte>();
            int nextByte;

            // Read bytes until we hit a newline character
            while ((nextByte = fs.ReadByte()) != -1)
            {
                if (nextByte == '\n' || nextByte == '\r')
                {
                    if (byteBuffer.Count > 0) break; // Skip empty leading carriage returns
                    continue;
                }
                byteBuffer.Add((byte)nextByte);
            }

            return Encoding.UTF8.GetString(byteBuffer.ToArray());
        }

        static void Main(string[] args)
        {
            foreach (string pos in posSuffixes)
            {
                string indexFile = wnPath + "index." + pos;
                if (File.Exists(indexFile))
                {
                    indexEntries.AddRange(WordNetIndexReader.ReadIndexFile(indexFile, pos));
                    //var currentSynsets = WordNetDataReader.ReadDataFile(dataFile);
                    //foreach (var kvp in currentSynsets)
                    //{
                    //    string uniqueKey = $"{pos}_{kvp.Key}";
                    //    if (!synsets.ContainsKey(uniqueKey))
                    //    {
                    //        synsets.Add(uniqueKey, kvp.Value);
                    //    }
                    //}
                }
            }
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: dict <word>");
                Version v = Assembly.GetExecutingAssembly().GetName().Version;
                string version = string.Format("{0}.{1}", v.Major, v.Minor);
                Console.WriteLine("Dict, version " + version);
                return;
            }
            lookUp(args[0]);
        }
    }
}
