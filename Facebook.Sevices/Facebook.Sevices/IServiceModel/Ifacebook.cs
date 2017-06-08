

namespace Facebook.Services.IServiceModel
{
    public interface Ifacebook
    {
        string post(string message, string accesstoken);
        string getfriends(string accesstoken);


    }
}
