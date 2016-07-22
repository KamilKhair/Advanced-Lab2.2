using System;

namespace GenericApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var dictionary = new MultiDictionary<MyInt, MyString>
                {
                    {1, new MyString("one")},
                    {2, new MyString("two")},
                    {3, new MyString("three")},
                    {1, new MyString("ich")},
                    {2, new MyString("nee")},
                    {3, new MyString("sun")}
                };

                Console.WriteLine("Initial state:");
                DisplayDictionary(dictionary);

                Console.WriteLine("After Removing key = 3, value = sun:");
                dictionary.Remove(3, new MyString("sun"));
                DisplayDictionary(dictionary);

                Console.WriteLine("After Removing key = 3:");
                dictionary.Remove(3);
                DisplayDictionary(dictionary);

                Console.WriteLine("After Removing key = 1:");
                dictionary.Remove(1);
                DisplayDictionary(dictionary);

                Console.WriteLine("After Creating a new value with the key = 4:");
                dictionary.CreateNewValue(4);
                DisplayDictionary(dictionary);

                Console.WriteLine("After Clearing the dictionary:");
                dictionary.Clear();
                DisplayDictionary(dictionary);

                Console.WriteLine("Returning to Initial state:");
                dictionary.Add(1, new MyString("one"));
                dictionary.Add(1, new MyString("ich"));
                dictionary.Add(2, new MyString("two"));
                dictionary.Add(2, new MyString("nee"));
                dictionary.Add(3, new MyString("three"));
                dictionary.Add(3, new MyString("sun"));
                DisplayDictionary(dictionary);

                Console.WriteLine("The values are:");
                foreach (var value in dictionary.Values)
                {
                    Console.WriteLine(value);
                }

                Console.WriteLine();
                Console.WriteLine("The Keys are:");
                foreach (var key in dictionary.Keys)
                {
                    Console.WriteLine(key);
                }

                Console.WriteLine();
                Console.WriteLine(dictionary.Contains(1, new MyString("one"))
                    ? "The Multi Dictionary contains key = 1, value = one"
                    : "The Multi Dictionary does not contain key = 1, value = one");

                Console.WriteLine(dictionary.ContainsKey(3)
                    ? "The Multi Dictionary contains key = 3"
                    : "The Multi Dictionary does not contain key = 3");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void DisplayDictionary<TKey, TValue>(MultiDictionary<TKey, TValue> dictionary) where TKey : struct where TValue : struct
        {
            foreach (var key in dictionary)
            {
                foreach (var value in key.Value)
                {
                    Console.WriteLine($"Key = {key.Key}, Value = {value}");
                }
            }
            Console.WriteLine($"Count = {dictionary.Count}");
            Console.WriteLine();
        }
    }
}
