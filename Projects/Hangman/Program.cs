using System;
using System.Text;
using System.Xml;

namespace Hangman
{
    internal class Program
    {
        public static void HangMan(string randomWord)
        {
            StringBuilder sb = new StringBuilder();
            List<char> words = new List<char>();
            Dictionary<char, List<int>> dict = new Dictionary<char, List<int>>();
            for (int i = 0; i < randomWord.Length; i++)
            {
                char ch = randomWord[i];

                if (!dict.ContainsKey(ch))
                {
                    dict[ch] = new List<int>();
                }

                dict[ch].Add(i);
                sb.Append("_ ");
            }
            int lives = 6;
            while (lives > 0)
            {
                string result = sb.ToString();
                Console.WriteLine(result);
                Console.WriteLine($"Lives left: {lives}");
                Console.WriteLine("Guessed: " + string.Join(",", words));
                Console.WriteLine("Make your guess: ");
                char guess = char.ToUpper(char.Parse(Console.ReadLine()));

                words.Add(guess);
                if (!(char.IsLetter(guess)) || char.IsWhiteSpace(guess))
                {
                    Console.WriteLine("Please enter a valid letter.");
                }
                else
                {

                    if (dict.ContainsKey(guess))
                    {
                        foreach (int index in dict[guess])
                        {
                            sb[index * 2] = guess;
                        }
                        dict.Remove(guess);
                        Console.WriteLine("Good Catch!");
                        if (!sb.ToString().Contains("_"))
                        {
                            Console.WriteLine("YOU WON!");
                            break;
                        }
                    }



                    else
                    {
                        Console.WriteLine("Nope! Thats not in the word.");
                        lives--;
                    }
                }


            }

        }
        public static string GetRandomWord(string[] wordList)
        {
            Random random = new Random();
            int index = random.Next(0, wordList.Length);


            return wordList[index];
        }
        static void Main(string[] args)
        {
            string[] words = { "apple", "banana", "cherry", "date", "elderberry", "fig", "classroom", "freedon", "Travel destination" }; ;
            string randomWord = GetRandomWord(words);
            randomWord = randomWord.ToUpper();

            HangMan(randomWord);
        }
    }
}

