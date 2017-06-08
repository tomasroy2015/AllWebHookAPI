using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using APIManager.Core.WcfExtension;
using APIManager.Core.Configuration;
//using eBay.Services;

namespace APIManager.Core.Common
{
    /// <summary>
    /// A Generic factory class to get eBay SOA service client proxy instance
    /// </summary>
    public class ClientFactory
    {
        #region Fields/Constants
        /// <summary>
        /// max receive message size constant, required by WCF framework
        /// </summary>
        private static readonly long MAX_RECEIVE_MESSAGE_SIZE = 2147483647;

        #endregion

        /// <summary>
        /// A static fractory method to get eBay SOA service client proxy instance
        /// </summary>
        /// <typeparam name="TServiceContract">Servcie contract type parameter</typeparam>
        /// <param name="config">Client configuration</param>
        /// <param name="clientType">The type of specific client</param>
        /// <param name="serviceName">The name of the service, for tracking purpose</param>
        /// <returns>ClientBase instance, need to be casted to a specific client which extends ClientBase</returns>
        public static ClientBase<TServiceContract> GetSerivceClient<TServiceContract>(ClientConfig config, Type clientType, string serviceName)
            where TServiceContract : class
        {
            // http binding setting
            BasicHttpBinding binding = new BasicHttpBinding();

            // http timeout setting
            if (config.HttpTimeout > 0)
            {
                binding.OpenTimeout = TimeSpan.FromMilliseconds(config.HttpTimeout);
                binding.ReceiveTimeout = TimeSpan.FromMilliseconds(config.HttpTimeout);
            }
            // required by WCF to support larget response message
            binding.MaxReceivedMessageSize = MAX_RECEIVE_MESSAGE_SIZE;

            // support https protocol
            string endPointAddress = config.EndPointAddress;
            if (endPointAddress.StartsWith("https"))
            {
                binding.Security.Mode = BasicHttpSecurityMode.Transport;
            }
            // build endpont address
            EndpointAddress address = new EndpointAddress(endPointAddress);

            // Use reflection to create a specific ClientBase instance
            ClientBase<TServiceContract> client = (ClientBase<TServiceContract>)Activator.CreateInstance(clientType, new object[] { binding, address });

            // add custome behaviour to the client instance
            MessageBehavior behavior = new MessageBehavior(config, serviceName);
            client.Endpoint.Behaviors.Add(behavior);

            return client;
        }
    }
}
