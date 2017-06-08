using System;
using APIManager.Core;
using APIManager.Core.Utils;
using Aliexpress.Services.IServiceModel;
using APIManager.Core.Configuration;

namespace Aliexpress.Services.APIService
{
    public  class AliexpressAPI :DisposeObjects, IAliexpress 
    {
        #region Test Url
        /*
         * For Search Product 
         *  https://gw.api.alibaba.com/openapi/param2/2/portals.open/api.listPromotionProduct/6500?fields=productId,productTitle,productUrl,originalPrice,salePrice&keywords=apple
         *  
         *  For Get My Orders
         *  https://gw.api.alibaba.com/openapi/param2/2/portals.open/api.getCompletedOrders/6500?appSignature=z0XybWiALs&startDate=2016-01-03&endDate=2016-09-03&liveOrderStatus=pay
         *   
         *   Get Orders Start Date Format yyyy-mm-dd, eg:2016-01-03.
         *   Get Orders End Date Format yyyy-mm-dd, eg:2016-10-03.
         */
        #endregion

        #region Implement Configuration Properties

        /* AliExpress API Key */
        int ApiKey = AliexperssConfig.APIKey;

        /* AliExpress API Signature */
        string ApiSignature = AliexperssConfig.APISignature;

       /* AliExpress API BaseUrl */
        string BaseUrl = AliexperssConfig.BaseUrl;

      

        int IAliexpress.ApiKey
        {
            get
            {
                return this.ApiKey;
            }
            set
            {
                this.ApiKey = AliexperssConfig.APIKey;
            }
        }

        string IAliexpress.ApiSignature
        {
            get
            {
                return this.ApiSignature;
            }
            set
            {
                this.ApiSignature = AliexperssConfig.APISignature;
            }
        }

        string IAliexpress.BaseUrl
        {
            get
            {
                return this.BaseUrl;
            }
            set
            {
                this.BaseUrl = AliexperssConfig.BaseUrl;
            }
        }

        #endregion

        #region Implement Service Methods

        #region Search Aliexpress Product By Keyword
        public string SearchString(string Keyword)
        {
            string retVal = "";

            string url = string.Concat(BaseUrl, "api.listPromotionProduct/", ApiKey, "?fields=productId,productTitle,productUrl,originalPrice,salePrice&keywords=" + Keyword);
            System.Net.HttpWebRequest request = System.Net.WebRequest.Create(url) as System.Net.HttpWebRequest;
            System.Net.HttpWebResponse response = null;

            try
            {
                using (response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
                    retVal = reader.ReadToEnd();
                }
            }
            catch (Exception ex){    }

            return retVal;
        }
        #endregion

        #region Get My Orders 
        public string GetOrders(string StartDate, string EndDate, string OrderStatus)
        {
            
            string retVal = "";
            string url = string.Concat(BaseUrl, "api.getCompletedOrders/", ApiKey, "?appSignature=", ApiSignature, "&startDate=", StartDate, "&endDate=", EndDate, "&liveOrderStatus=", OrderStatus);


            System.Net.HttpWebRequest request = System.Net.WebRequest.Create(url) as System.Net.HttpWebRequest;
            System.Net.HttpWebResponse response = null;

            try
            {
                using (response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());


                    retVal = reader.ReadToEnd();


                }
            }
            catch (Exception ex)
            {

            }
            return retVal;
        }
        #endregion


        #endregion


    }
}
