using System.Collections.Generic;
using msmvp_pl.Core;
using System.Linq;

namespace msmvp_pl.Modules.Mvp
{
    public class MvpListModule : MvpModuleBase
    {
        public MvpListModule(IDbProvider dbProvider)
        {
            Get["/"] = _ =>
                {
                    var mvps = dbProvider.GetDb().mvps.WithNominations();

                    return View["mvp-list", new MvpListModel(mvps)];
                };
        }

        public class MvpListModel
        {
            private readonly IEnumerable<dynamic> _mvps;

            public MvpListModel(dynamic mvps)
            {
                _mvps = mvps;
            }

            private bool IsCurrent(dynamic mvp)
            {
                IEnumerable<dynamic> nominations = mvp.Nominations;
                
                return nominations.Any(y => y.EndDate == null);
            }

            public IEnumerable<dynamic> Current
            {
                get { return _mvps.Where(x => IsCurrent(x)); }
            }

            public IEnumerable<dynamic> Ex
            {
                get { return _mvps.Where(x => !IsCurrent(x)); }
            }
        }
    }
}