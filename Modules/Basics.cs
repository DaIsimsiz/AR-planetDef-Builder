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
        /// A better way to get user input. WIP
        /// </summary>
        public static string ReadLine()
        {
            //Do stuff
            return "";
        }
    }
}