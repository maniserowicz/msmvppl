using System;
using Nancy;
using msmvp_pl.Core;
using System.Linq;

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
                    .WithNominations()
                    .FirstOrDefault()
                    ;

                mvp.StringSlug = new Func<string, string>(
                    title => new string(title.Where(x => char.IsLetterOrDigit(x)).ToArray())
                );

                if (mvp == null)
                {
                    return HttpStatusCode.NotFound;
                }

                return View["mvp-details", mvp];
            };
        }
    }
}