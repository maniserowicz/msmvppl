using Nancy;

namespace msmvp_pl.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => View["index"];
            Get["/o-programie"] = _ => View["about"];
        }
    }
}