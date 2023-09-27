namespace Modules.MessageSend
{
    class Temp
    {
        /// <summary>
        /// Cleans the console and sends the new messages.
        /// </summary>
        public static void SendMessages(params string[] messages)
        {
            Console.Clear();
            foreach(string message in messages) Console.WriteLine(message);
        }
    }
}