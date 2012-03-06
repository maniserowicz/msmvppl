using Nancy;

namespace msmvp_pl.Modules.Twitter
{
    public abstract class TwitterModuleBase : NancyModule
    {
        public TwitterModuleBase()
            : base("/twitter")
        {

        }
    }
}