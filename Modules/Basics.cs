namespace Modules
{
    /// <summary>
    /// A class with very basic methods, this prevents me from having to use the same lines multiple times, and polluting the code.
    /// </summary>
    class Basics
    {
        /// <summary>
        /// Sends a console message with some extra operations. Optionally, cleans console.
        /// </summary>
        public static void SendMessages(bool clean = true, params string[] messages)
        {
            if(clean) Console.Clear();
            foreach(string message in messages) Console.WriteLine(message);
        }

        /// <summary>
        /// For debugging purposes only. There should be 0 references of this method once the publish is made.
        /// </summary>
        public static void DEBUG(object message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }
}