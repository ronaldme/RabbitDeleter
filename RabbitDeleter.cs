using System;
using System.Configuration;
using System.Linq;
using EasyNetQ.Management.Client;
using EasyNetQ.Management.Client.Model;

namespace RabbitDeleter
{
    public class RabbitDeleter
    {
        private readonly ManagementClient managementClient;

        public RabbitDeleter()
        {
            managementClient = new ManagementClient(ConfigurationManager.AppSettings["host"],
                ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);
        }

        public void RemoveAllQueues()
        {
            var queues = managementClient.GetQueues();

            foreach (Queue queue in queues)
            {
                managementClient.DeleteQueue(queue);
            }
        }

        public void RemoveAllExchanges()
        {
            var exchanges = managementClient.GetExchanges();

            foreach (Exchange exchange in exchanges.Where(x => !string.IsNullOrEmpty(x.Name)))
            {
                try
                {
                    managementClient.DeleteExchange(exchange);
                }
                catch (Exception e)
                {
                    // some exchanges cannot be deleted
                }
            }
        }
    }
}