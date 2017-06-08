using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIManager.Core
{
    #region Office 365 API Configuration 
    public class SettingsHelper
    {
        private static string _clientId = "5a67f742-7d8c-4e90-a01c-81a38d15b87f";
        private static string _clientSecret = "D/32wQIM8fU9NSocSGoOKerGLsXg/3NeYlpu6mmEiyU=";
        private static string _authorizationUri ="";
        private static string _graphResourceId = "https://graph.windows.net";
        private static string _tenantId = "d26fd680-d7d7-4c3f-9dd2-52b3ed520a0d";

        private static string _authority = "https://login.windows.net/" + _tenantId;

        private static string _discoverySvcResourceId = "https://api.office.com/discovery/";
        private static string _discoverySvcEndpointUri = "https://api.office.com/discovery/v1.0/me/";

        public static string ClientId
        {
            get
            {
                return _clientId;
            }
        }

        public static string ClientSecret
        {
            get
            {
                return _clientSecret;
            }
        }

        public static string AuthorizationUri
        {
            get
            {
                return _authorizationUri;
            }
        }

        public static string Authority
        {
            get
            {
                return _authority;
            }
        }

        public static string AADGraphResourceId
        {
            get
            {
                return _graphResourceId;
            }
        }

        public static string DiscoveryServiceResourceId
        {
            get
            {
                return _discoverySvcResourceId;
            }
        }

        public static Uri DiscoveryServiceEndpointUri
        {
            get
            {
                return new Uri(_discoverySvcEndpointUri);
            }
        }
    }

    #endregion


}
