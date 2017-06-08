using Slack.Services.IServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Services.APIService
{
    public class SlackAPI:Islack
    {
      
        #region Implement Configuration Properties
       
        /* AliExpress API Key */
        string _SlackAuthURL;

        public SlackAPI()
        {
            SlackAuthURL= this._SlackAuthURL = "https://slack.com/oauth/authorize?client_id=76256916977.84954681334&scope=users:read";
        }
        public string SlackAuthURL
        {

            get;

            set;
            
        }

        #endregion

        #region Methods Impelementation

        #region Get Access Token
        public string GetAccessToken(string code)
        {
            //Make Request To Access Token
            WebClient webClient = new WebClient();
            webClient.QueryString.Add("client_id", "76256916977.84954681334");
            webClient.QueryString.Add("client_secret", "a24fb0dcab988fdc3f2f2af3037e91fc");
            webClient.QueryString.Add("code", code);
            string result = webClient.DownloadString("https://slack.com/api/oauth.access");
            return result;
        }
        #endregion

        #region Get Contact List
        public string GetContactList(string AccessToken)
        {
            string ContactList = null;
            WebClient webClient = new WebClient();
            webClient.QueryString.Add("token", AccessToken);
            ContactList = webClient.DownloadString("https://slack.com/api/users.list");
            return ContactList;
        }
        #endregion

        #endregion


    }
}
