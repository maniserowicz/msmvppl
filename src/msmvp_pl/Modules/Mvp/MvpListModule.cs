using System.Collections.Generic;
using msmvp_pl.Core;
using System.Linq;
using System;

namespace msmvp_pl.Modules.Mvp
{
    public class MvpListModule : MvpModuleBase
    {
        public MvpListModule(IDbProvider dbProvider)
        {
            Get["/"] = _ =>
                {
                    var mvps = dbProvider.GetDb().mvps.WithNominations();

                    var model = new MvpListModel(mvps);
                    return View["mvp-list", model];
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

                return nominations.Any(y => y.StartDate != null && y.EndDate == null);
            }

            private IEnumerable<dynamic> GroupByCategory(IEnumerable<dynamic> mvps)
            {
                return mvps.GroupBy(x => LastCategory(x))
                    .Select(x => new
                        {
                            Category = x.Key,
                            Mvps = x.OrderBy(y => y.LastName)
                        }.ToExpando()
                    );
            }

            public IEnumerable<dynamic> Current
            {
                get
                {
                    return GroupByCategory(_mvps.Where(x => IsCurrent(x)));
                }
            }

            public IEnumerable<dynamic> Ex
            {
                get
                {
                    return GroupByCategory(_mvps.Where(x => !IsCurrent(x)));
                }
            }

            public string LastCategory(dynamic mvp)
            {
                IEnumerable<dynamic> nominations = mvp.Nominations;

                var lastNomination = nominations.OrderByDescending(x => x.StartDate)
                    .FirstOrDefault();

                return lastNomination == null
                    ? "?"
                    : lastNomination.Category ?? "?";
            }
        }
    }
}