namespace Modules
{
    class Msg
    {
        /// <summary>
        /// Sends a console message with some extra operations. Optionally, cleans console.
        /// </summary>
        public static void SendMessages(bool clean = true, params string[] messages)
        {
            if(clean) Console.Clear();
            foreach(string message in messages) Console.WriteLine(message);
        }
    }
}