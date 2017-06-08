using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using APIManager.Core.Common;
//using eBay.Services;
using APIManager.Core.WcfExtension;
using APIManager.Core.Configuration;

namespace APIManager.Core
{
    class MessageBehavior : IEndpointBehavior
    {
        #region IEndpointBehavior Members

        private ClientConfig clientConfig;
        private string serviceName;

        public MessageBehavior(ClientConfig config, string serviceName)
        {
            this.clientConfig = config;
            this.serviceName = serviceName;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Enable custom message inspector
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="clientRuntime"></param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            MessageInspector inspector = new MessageInspector(this.clientConfig, serviceName);
            clientRuntime.MessageInspectors.Add(inspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            throw new Exception("Behavior not supported on the consumer side!");
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion
    }
}
