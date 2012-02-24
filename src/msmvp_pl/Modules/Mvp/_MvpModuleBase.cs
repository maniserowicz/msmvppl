using Nancy;

namespace msmvp_pl.Modules.Mvp
{
    public abstract class MvpModuleBase : NancyModule
    {
        public MvpModuleBase() : base("/mvp")
        {
            
        }
    }
}