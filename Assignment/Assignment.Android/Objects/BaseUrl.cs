using Xamarin.Forms;
using Assignment.Droid.Objects;
using Assignment.Interfaces;

[assembly: Dependency(typeof(BaseUrl))]
namespace Assignment.Droid.Objects
{
    public class BaseUrl : IBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}

