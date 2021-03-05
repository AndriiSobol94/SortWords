using System;

namespace SortWords {
    class Program {
        private static readonly string SavePath = @"C:\TestTask\TASK\F4.txt";

        static void Main(string[] args) {
            var mostFrequentWord = new Processor().ProcessAsync(args, SavePath).Result;
            Console.WriteLine($"The most frequent word in the text is '{mostFrequentWord.Word}', count: {mostFrequentWord.Count}");
        }
    }
}