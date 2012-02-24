using Nancy;
using msmvp_pl.Core;

namespace msmvp_pl.Modules.Mvp
{
    public class MvpDetailsModule : MvpModuleBase
    {
        public MvpDetailsModule(IDbProvider dbProvider)
        {
            Get["/{slug}"] = p =>
                {
                    var mvp = dbProvider.GetDb()
                        .mvps.FindAllBySlug(p["slug"])
                        .WithContents()
                        .WithLinks()
                        .FirstOrDefault()
                        ;

                    if (mvp == null)
                    {
                        return HttpStatusCode.NotFound;
                    }

                    return View["mvp-details", mvp];
                };
        }
    }
}