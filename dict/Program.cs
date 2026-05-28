/**
 * @brief Entry point.
 * 
 * @author Prahlad Yeri <prahladyeri@yahoo.com>
 * @date 2024-12-31
 * @license MIT
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace dict
{
    class Program
    {
        static string wnPath = AppDomain.CurrentDomain.BaseDirectory + @"wn.dict\";
        //static string indexFilePath = wnPath + "index.noun";
        //static string dataFilePath = wnPath + "data.noun";

        static List<WordIndexEntry> indexEntries = new List<WordIndexEntry>();
        static Dictionary<int, Synset> synsets = new Dictionary<int, Synset>();

        private static void lookUp(string wordToLookup) {
            WordIndexEntry entry = indexEntries.Find(e => e.Word == wordToLookup);
            if (entry != null)
            {
                Console.WriteLine(String.Format("Word: {0}", entry.Word));
                foreach (int offset in entry.SynsetOffsets)
                {
                    if (synsets.ContainsKey(offset))
                    {
                        Synset synset = synsets[offset];
                        Console.WriteLine(String.Format("Synset: {0}", string.Join(", ", synset.Words.ToArray())));
                        Console.WriteLine(String.Format("Gloss: {0}", synset.Gloss));
                    }
                }
            }
            else
            {
                Console.WriteLine("Word not found.");
            }
        
        
        }

        static void Main(string[] args)
        {
            string attrib = "This software uses WordNet® lexical database (http://wordnet.princeton.edu/) by Princeton University.";
            //indexEntries = WordNetIndexReader.ReadIndexFile(indexFilePath);
            //synsets = WordNetDataReader.ReadDataFile(dataFilePath);

            string[] posSuffixes = { "noun", "verb", "adj", "adv" };
            // Load all parts of speech
            foreach (string pos in posSuffixes)
            {
                string indexFile = wnPath + "index." + pos;
                string dataFile = wnPath + "data." + pos;

                if (File.Exists(indexFile) && File.Exists(dataFile))
                {
                    // Assuming these return collections, append them:
                    indexEntries.AddRange(WordNetIndexReader.ReadIndexFile(indexFile));

                    // For the dictionary, merge keys safely
                    var currentSynsets = WordNetDataReader.ReadDataFile(dataFile);
                    foreach (var kvp in currentSynsets)
                    {
                        // WordNet offsets are unique within a POS file, but to be completely safe across 
                        // different POS files, you might want a composite key or just skip duplicates.
                        if (!synsets.ContainsKey(kvp.Key))
                        {
                            synsets.Add(kvp.Key, kvp.Value);
                        }
                    }
                }
            }

            Version v = Assembly.GetExecutingAssembly().GetName().Version;
            string version= string.Format("{0}.{1}", v.Major, v.Minor);
            Console.WriteLine("Dict, version " + version );
            
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: dict <word>");
                return;
            }
            lookUp(args[0]);
            //lookUp("dog");
            //Console.ReadKey();
            Console.WriteLine("");
            Console.WriteLine("Copyright (C) 2024 Prahlad Yeri <prahladyeri@yahoo.com>");
            Console.WriteLine("License: MIT");
            Console.WriteLine(attrib);
            Console.WriteLine("This is free software; you are free to change and redistribute it.");
            Console.WriteLine("There is NO WARRANTY, to the extent permitted by law.\n");
        }
    }
}
