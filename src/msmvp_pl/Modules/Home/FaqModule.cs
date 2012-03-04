namespace msmvp_pl.Modules.Home
{
    public class FaqModule : HomeModuleBase
    {
        public FaqModule()
        {
            Get["/faq"] = _ => View["faq"];
        }
    }
}