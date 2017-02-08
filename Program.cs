using System;

namespace RabbitDeleter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var rabbitDeleter = new RabbitDeleter();
                rabbitDeleter.RemoveAllQueues();
                rabbitDeleter.RemoveAllExchanges();
            }
            catch (Exception e)
            {
                // some exchanges cannot be deleted
            }

            Console.WriteLine("Success!");
            Console.ReadLine();
        }
    }
}