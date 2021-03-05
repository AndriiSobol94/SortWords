using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace SortWords {
    internal class Processor {
        private readonly char[] SplitSeparators = new char[] { ' ', ',', '.', '-', ';', '?' };

        public async Task<WordFrequency> ProcessAsync(string[] paths, string savePath) {
            var tasks = new List<Task<string>>();
            foreach (var p in paths) {
                tasks.Add(File.ReadAllTextAsync(p));
            }
            await Task.WhenAll(tasks);

            return await SortAndSaveAsync(tasks.Select(t => t.Result), savePath);
        }

        private async Task<WordFrequency> SortAndSaveAsync(IEnumerable<string> texts, string path) {
            var dict = new Dictionary<string, int>();
            foreach (var word in texts.SelectMany(t => t.Split(SplitSeparators, StringSplitOptions.RemoveEmptyEntries)).Select(t => t.ToLower())) {
                if (dict.ContainsKey(word)) {
                    dict[word]++;
                } else {
                    dict.Add(word, 1);
                }
            }

            await File.WriteAllTextAsync(path, string.Join(", ", dict.Keys.OrderBy(x => x)));

            string mostFreqWord = "";
            int mostCount = 0;
            foreach (var item in dict) {
                if (item.Value > mostCount) {
                    mostFreqWord = item.Key;
                    mostCount = item.Value;
                }
            }

            return new WordFrequency(mostFreqWord, mostCount);
        }
    }
}
