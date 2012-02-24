namespace msmvp_pl.Modules.Home
{
    public class AboutModule : HomeModuleBase
    {
        public AboutModule()
        {
            Get["/o-programie"] = _ => View["about"];
        }
    }
}