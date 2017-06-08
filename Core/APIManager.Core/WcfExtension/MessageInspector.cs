//using eBay.Services;
using APIManager.Core.Common;
using APIManager.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;


namespace APIManager.Core.WcfExtension
{
    class MessageInspector : IClientMessageInspector
    {
        #region IClientMessageInspector Members

      
        private ClientConfig config;
        private string serviceName;

        public MessageInspector(ClientConfig config, string serviceName)
        {
            this.config = config;
            this.serviceName = serviceName;
        }

        /// <summary>
        /// Called after response is received
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {

            if (this.config.HttpHeaderLoggingEnabled)
            {
                //logging http headers
                HttpResponseMessageProperty httpResponse = reply.Properties[HttpResponseMessageProperty.Name] as HttpResponseMessageProperty;
                if (httpResponse != null && httpResponse.Headers != null && httpResponse.Headers.Count > 0)
                {
                    string httpHeaderMessage = "---[HTTP Response Headers]---\r\n";
                    foreach (string headerName in httpResponse.Headers.AllKeys)
                    {
                        httpHeaderMessage += headerName + " : " + httpResponse.Headers[headerName] + "\r\n";

                    }
                  

                }
                else
                {
                   
                }
            }

            if (this.config.SoapMessageLoggingEnabled)
            {
                //logging soap message
                string soapMessage = "receiving soap request message ...\r\n" + reply.ToString();
              
            }

        }

        /// <summary>
        /// Called before request is sent
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            // Make a copy of the SOAP packet for viewing.
            MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
            Message msgCopy = buffer.CreateMessage();

            request = buffer.CreateMessage();

            // Get the SOAP XML content.
            string strMessage = msgCopy.ToString();
            if (this.config.SoapMessageLoggingEnabled)
            {
                //logging soap message
                string soapMessage = "sending soap request message ...\r\n" + strMessage;
              
            }

            HttpRequestMessageProperty httpRequest;
            if (request.Properties.ContainsKey(HttpRequestMessageProperty.Name))
            {
                httpRequest = request.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
            }
            else
            {
                httpRequest = new HttpRequestMessageProperty();
                request.Properties.Add(HttpRequestMessageProperty.Name, httpRequest);
            }

            // Get the SOAP XML body content.
            System.Xml.XmlDictionaryReader xrdr = msgCopy.GetReaderAtBodyContents();
            // Get operation name for the root element
            string opName = xrdr.LocalName;
            // Remove [Request] suffix
            opName = opName.Replace("Request", "");

            // Set soa specific headers
            // Set operation name
            httpRequest.Headers.Add(ServiceConstants.SERVICE_OPERATION_NAME, opName);

            // Set service version
            string serviceVersion = this.config.ServiceVersion;
            if (serviceVersion != null && serviceVersion.Length > 0)
            {
                httpRequest.Headers.Add(ServiceConstants.VERSION, serviceVersion);
            }

            // Set global id
            string globalId = this.config.GlobalId;
            if (globalId != null && globalId.Length > 0)
            {
                httpRequest.Headers.Add(ServiceConstants.GLOBAL_ID, globalId);
            }

            // Set appId
            string applicationId = this.config.ApplicationId;
            if (applicationId != null && applicationId.Length > 0)
            {
                httpRequest.Headers.Add(ServiceConstants.AUTH_APPNAME, applicationId);
            }


            string uaValue = ServiceConstants.USER_AGENT_VALUE;
            if (serviceName != null && serviceName.Length > 0)
            {
                uaValue = uaValue + "-" + serviceName;
            }
            httpRequest.Headers.Add(ServiceConstants.HEADER_USER_AGENT, uaValue);

            // http compression is not supported in current implementation
            /*if (config.HttpCompressionEnabled)
            {
                httpRequest.Headers.Add("Accept-Encoding", "gzip");
            }*/

            if (this.config.HttpHeaderLoggingEnabled)
            {
                //logging http headers
                string httpHeaderMessage = "---[HTTP Request Headers]---\r\n";
                foreach (string headerName in httpRequest.Headers.AllKeys)
                {
                    httpHeaderMessage += headerName + " : " + httpRequest.Headers[headerName] + "\r\n";

                }
               
            }

            return null;

        }


        #endregion
    }
}
