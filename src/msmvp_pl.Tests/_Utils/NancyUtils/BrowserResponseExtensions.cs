using Nancy.Testing;

namespace msmvp_pl.Tests
{
    public static class BrowserResponseExtensions
    {
        public static dynamic GetModel<TType>(this BrowserResponse response)
        {
            return (TType)response.Context.Items["###ViewModel###"];
        }
    }
}