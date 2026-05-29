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
                bool headerPrinted = false;
                int definitionIndex = 1;
                foreach (int offset in entry.SynsetOffsets)
                {
                    string key = $"{entry.Pos}_{offset}";
                    if (synsets.ContainsKey(key)) // offset
                    {
                        if (!headerPrinted)
                        {
                            Console.WriteLine($"[{entry.Pos}]");
                            headerPrinted = true;
                        }
                        Synset synset = synsets[key]; // offset
                        string synonyms = string.Join(", ", synset.Words);
                        Console.WriteLine($"  {definitionIndex++}. ({synonyms}) {synset.Gloss}");
                    }
                }
                if (headerPrinted) Console.WriteLine("");
            }
        }

        static void Main(string[] args)
        {
            foreach (string pos in posSuffixes)
            {
                string indexFile = wnPath + "index." + pos;
                string dataFile = wnPath + "data." + pos;

                if (File.Exists(indexFile) && File.Exists(dataFile))
                {
                    indexEntries.AddRange(WordNetIndexReader.ReadIndexFile(indexFile, pos));
                    var currentSynsets = WordNetDataReader.ReadDataFile(dataFile);
                    foreach (var kvp in currentSynsets)
                    {
                        string uniqueKey = $"{pos}_{kvp.Key}";
                        if (!synsets.ContainsKey(uniqueKey))
                        {
                            synsets.Add(uniqueKey, kvp.Value);
                        }
                    }
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
