using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Services.IServiceModel
{
    public interface Islack
    {
        string SlackAuthURL { get; set; }
        string GetAccessToken(string code);
        string GetContactList(string AccessToken);
      
    }
}
