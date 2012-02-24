namespace msmvp_pl.Modules.Home
{
    public class IndexModule : HomeModuleBase
    {
        public IndexModule()
        {
            Get["/"] = _ => View["index"];

            Get["/jakas-inna-statyczna-strona"] = _ => View["some-other-static-page"];
        }
    }
}