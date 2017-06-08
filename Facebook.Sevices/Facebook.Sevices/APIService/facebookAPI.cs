using APIManager.Core.Utils;
using Facebook.Services.IServiceModel;
using System;


namespace Facebook.Services.APIService
{
    public class facebookAPI: DisposeObjects, Ifacebook
    {

        string API_BaseUrl = "https://graph.facebook.com";

        public string post(string message, string accesstoken)
        {
            string retVal = "";
            string url = API_BaseUrl+"/me/feed?message="+message+"&access_token=" + accesstoken;
            System.Net.HttpWebRequest request = System.Net.WebRequest.Create(url) as System.Net.HttpWebRequest;
            request.Method = "POST";
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
        public  string getfriends(string accesstoken)
        {
            string retVal = "";
            
            string url = API_BaseUrl + "/me/friends?&access_token=" + accesstoken;
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
    }
}
