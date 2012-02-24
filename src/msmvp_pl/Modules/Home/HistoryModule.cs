namespace msmvp_pl.Modules.Home
{
    public class HistoryModule : HomeModuleBase
    {
        public HistoryModule()
        {
            Get["/historia"] = _ => View["nominations"];
        }
    }
}