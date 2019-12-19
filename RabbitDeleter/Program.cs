using System;
using System.Threading.Tasks;

namespace RabbitDeleter
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await RabbitDelete().ConfigureAwait(false);
        }

        private static async Task RabbitDelete()
        {
            try
            {
                var rabbitDeleter = new RabbitDeleter();
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