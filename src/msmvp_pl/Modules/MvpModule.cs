using System.Collections.Generic;
using System.Linq;
using Nancy;
using msmvp_pl.Core;

namespace msmvp_pl.Modules
{
    public class MvpListModel
    {
        private readonly IEnumerable<dynamic> _mvps;

        public MvpListModel(dynamic mvps)
        {
            _mvps = mvps.ToList();
        }

        public IEnumerable<dynamic> CurrentMvps()
        {
            return _mvps.Where(
                x => ((IEnumerable<dynamic>) x.Nominations)
                    .Any(y => y.EndDate == null)
            );
        }

        public IEnumerable<dynamic> ExMvps()
        {
            return _mvps.Except(CurrentMvps());
        }
    }

    public class MvpModule : NancyModule
    {
        public MvpModule(IDbProvider dbProvider) : base("/mvp")
        {
            Get["/"] = _ =>
                {
                    var db = dbProvider.GetDb();
                    var mvps = db.mvps.WithNominations();
                    return View["mvp-list", new MvpListModel(mvps)];
                };
        }
    }
}