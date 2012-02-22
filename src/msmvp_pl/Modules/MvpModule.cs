using Nancy;
using msmvp_pl.Core;

namespace msmvp_pl.Modules
{
    public class MvpModule : NancyModule
    {
        public MvpModule(IDbProvider dbProvider) : base("/mvp")
        {
            Get["/"] = _ =>
                {
                    var db = dbProvider.GetDb();
                    var mvps = db.mvps.WithNominations();
                    return View["mvp-list", mvps];
                };
        }
    }
}