using System.ComponentModel.Design;

namespace HashMap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashMap<int, string> dictionary = new(EqualityComparer<int>.Default);
            try
            {
                dictionary.Add(6, "Sam");
                dictionary.Add(5, "Joe");
                dictionary.Add(7, "Bob");
                dictionary.Add(64, "Nikita");
                dictionary.Add(32, "Ori");
                dictionary.Remove(6);
                //dictionary.Add(5, "billy");
            }
            catch (Exception e)
            {
                //Console.WriteLine("Key already exists");
            }
            ;
        }

    }
}
