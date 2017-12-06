using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = GetResource("ConsoleApp1.input.txt");
            input = input.Replace("\r", "");
            var inputLines = input.Split("\n");

            int q = int.Parse(inputLines[0]);
            string soughtString = "hackerrank";
            

            for (int a0 = 1; a0 < q; a0++)
            {
                string s = inputLines[a0];
                var indexesDict = new Dictionary<char, List<int>>();

                int lastIndex = -1;
                for (int i = 0; i < soughtString.Length; i++)
                {
                    var soughtChar = soughtString[i];
                    if (!indexesDict.ContainsKey(soughtChar))
                    {
                        var temp = AllIndexesOf(s, soughtChar);
                        if (temp.Count == 0)
                        {
                            // Letter not found in input.  Print NO and don't look for the next letter.
                            Console.WriteLine("NO");
                            break;
                        }

                        indexesDict.Add(soughtChar, temp);
                    }
                    
                    var indexes = indexesDict[soughtChar];
                    var valid = false;
                    foreach (var index in indexes)
                    {
                        if (index > lastIndex)
                        {
                            lastIndex = index;
                            valid = true;
                            // Found next valid index, stop searching.
                            break;
                        }
                    }

                    if (!valid)
                    {
                        // An index was found, but appeared before the index of the last found character (lastIndex).
                        Console.WriteLine("NO");
                        break;
                    }

                    if (valid && i == soughtString.Length-1)
                    {
                        Console.WriteLine("YES");
                        break;
                    }
                }
            }
        }

        public static List<int> AllIndexesOf(string input, char value)
        {
            var indexes = new List<int>();
            var index = 0;
            while (index != -1)
            {
                index = input.IndexOf(value,index);
                if (index == -1) break;
                indexes.Add(index);
                index++;
            }
            return indexes;
        }

        public static string GetResource(string resourceName)
        {
            string result = null;
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }
    }
}
