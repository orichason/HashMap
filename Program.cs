using System;
using System.ComponentModel.Design;
using System.Text;

namespace HashMap
{
    internal class Program
    {
      
        static void Main(string[] args)
        {
            HashMap<int, string> dictionary = new(EqualityComparer<int>.Default)
            {
                [6] = "Sam",
                [5] = "joe",
                [7] = "bob",
                [43] = "ori"
            };

            var enumerator = dictionary.GetEnumerator();

            while (enumerator.MoveNext())
            {
               HashMap<int, string>.Entry current = (HashMap<int, string>.Entry)enumerator.Current;
               Console.WriteLine(current.Value);
            }
        }
    }
}
