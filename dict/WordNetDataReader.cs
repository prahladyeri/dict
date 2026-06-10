/**
 * @brief WordNet reader logic.
 * 
 * @author Prahlad Yeri <prahladyeri@yahoo.com>
 * @date 2024-12-31
 * @license MIT
 */
using System;
using System.Collections.Generic;
using System.Globalization;

namespace dict
{

    class WordNetDataReader
    {
        
        public static Synset ParseDataLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return null;

            // 1. Split the line at the pipe symbol to isolate the Gloss/Definition
            string[] mainParts = line.Split('|');
            if (mainParts.Length == 0) return null;

            string dataPart = mainParts[0].Trim();
            string gloss = mainParts.Length > 1 ? mainParts[1].Trim() : string.Empty;

            // 2. Tokenize by space, explicitly removing empty entries to handle any irregular spacing
            string[] tokens = dataPart.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length < 4) return null;

            // 3. Use TryParse to safely handle token alignment shifts
            if (!int.TryParse(tokens[3], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int wordCount))
            {
                // If it fails to parse, it means WordNet's header structure for this line is irregular.
                // We return null gracefully instead of crashing the utility.
                return null;
            }

            List<string> words = new List<string>();
            int currentTokenIndex = 4;

            // 4. Extract synonyms safely
            for (int i = 0; i < wordCount; i++)
            {
                if (currentTokenIndex >= tokens.Length) break;

                string word = tokens[currentTokenIndex];
                word = word.Replace("_", " ");
                words.Add(word);

                // Skip the next token (Lex_ID)
                currentTokenIndex += 2;
            }

            return new Synset
            {
                Words = words,
                Gloss = gloss
            };
        }
    }

    class Synset
    {
        public List<string> Words { get; set; }
        public string Gloss { get; set; }
        // TODO: To store structural relations if parsed later
        // public List<SynsetPointer> Pointers { get; set; } = new List<SynsetPointer>();
    }
}
