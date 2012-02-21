using Nancy;

namespace msmvp_pl.Modules
{
    public class MvpModule : NancyModule
    {
        public MvpModule() : base("/mvp")
        {
            Get["/"] = _ => View["mvp-index"];
        }
    }
}