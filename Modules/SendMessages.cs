namespace Modules
{
    class MessageSend
    {
        /// <summary>
        /// Cleans the console and sends the new messages.
        /// </summary>
        public static void SendMessages(params string[] messages)
        {
            foreach(string message in messages) Console.WriteLine(message);
        }
        public static void SendMessages(bool clean = true, params string[] messages)
        {
            if(clean) Console.Clear();
            foreach(string message in messages) Console.WriteLine(message);
        }
    }
}