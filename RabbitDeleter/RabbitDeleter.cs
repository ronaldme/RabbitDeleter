using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ.Management.Client;
using EasyNetQ.Management.Client.Model;

namespace RabbitDeleter
{
    public class RabbitDeleter
    {
        private readonly ManagementClient managementClient;

        public RabbitDeleter(string host, string user, string password)
        {
            managementClient = new ManagementClient(
                host ?? ConfigurationManager.AppSettings["host"],
                user ?? ConfigurationManager.AppSettings["user"],
                password ?? ConfigurationManager.AppSettings["password"]);
        }

        public async Task PurgeQueues()
        {
            var queues = await managementClient.GetQueuesAsync();

            foreach (Queue queue in queues)
            {
                await managementClient.PurgeAsync(queue);
            }
        }

        public async Task RemoveAllQueues()
        {
            var queues = await managementClient.GetQueuesAsync();

            foreach (Queue queue in queues)
            {
                await managementClient.DeleteQueueAsync(queue);
            }
        }

        public async Task RemoveAllExchanges()
        {
            var exchanges = await managementClient.GetExchangesAsync();

            foreach (Exchange exchange in exchanges.Where(x => !string.IsNullOrEmpty(x.Name)))
            {
                try
                {
                    await managementClient.DeleteExchangeAsync(exchange);
                }
                catch (Exception e)
                {
                    // some exchanges cannot be deleted
                }
            }
        }
    }
}