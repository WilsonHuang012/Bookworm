using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bookworm
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> wordList = new List<string>();
            LoadData(wordList);
            bool exit;
            do
            {
                Console.Clear();
                Console.Write("Please input letters ('?' for wildcard):");
                string inputLetters = UserInput();
                List<string> result = ProcesssData(inputLetters, wordList);
                Output(result);
                exit = Exit();
            } while (exit);
        }

        private static bool Exit()
        {
            ConsoleKeyInfo keyInfo;
            do
            {
                Console.Write("Press ENTER Key to retry, ESC exit program");
                Console.WriteLine();
                keyInfo = Console.ReadKey(true);
            } while (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Escape);
            return keyInfo.Key != ConsoleKey.Escape;
        }

        private static string UserInput()
        {
            string input = string.Empty;
            int n;
            do
            {
                n = Console.Read();
                char c = Convert.ToChar(n);
                if (char.IsLetter(c) || c == '?')
                {
                    input = string.Concat(input, c);
                }
            } while (n != 13);
            input = input.ToLower();
            return input;
        }

        private static void LoadData(List<string> wordList)
        {
            wordList.AddRange(File.ReadAllLines("FULL.LST").ToList());
        }

        private static List<string> ProcesssData(string inputLetters, List<string> wordList)
        {
            List<string> output = new List<string>();
            foreach (string word in wordList)
            {
                string temp = inputLetters;
                int count = 0;
                for (int i = 0; i < word.Length; i++)
                {
                    for (int j = 0; j < temp.Length; j++)
                    {
                        if (word[i] == temp[j] || temp[j] == '?')
                        {
                            count += 1;
                            temp = temp.Remove(j, 1);
                            break;
                        }
                    }
                }
                if (count == word.Length)
                {
                    output.Add(word);
                }
            }
            return output;
        }

        private static void Output(List<string> result)
        {
            result = result.OrderBy(r => r.Length).ToList();
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }
    }
}
