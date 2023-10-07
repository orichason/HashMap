namespace HashMap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashMap<int, string> dictionary = new(EqualityComparer<int>.Default)
            {
                 new (5, "Sam"),
                 //new(5, "Joe"),
                 new (8, "Bob"),
                 new (7, "Edden"),
                 new (20, "Pope")
            };


            string temp = dictionary[20];

            dictionary[20] = "Hi";


            dictionary.TryGetValue(7, out var yo);

        }
    }
}
