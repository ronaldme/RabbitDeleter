using System;
using System.Threading.Tasks;

namespace RabbitDeleter
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Length == 3)
                await RabbitDelete(args[01], args[1], args[2]).ConfigureAwait(false);
            else 
                await RabbitDelete().ConfigureAwait(false);
        }

        private static async Task RabbitDelete(string host = null, string user = null, string password = null)
        {
            try
            {
                var rabbitDeleter = new RabbitDeleter(host, user, password);
                await rabbitDeleter.RemoveAllQueues();
                await rabbitDeleter.RemoveAllExchanges();
                Console.WriteLine("Success!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed:");
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}