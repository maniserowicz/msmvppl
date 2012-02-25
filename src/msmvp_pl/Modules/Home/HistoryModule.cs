using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using msmvp_pl.Core;

namespace msmvp_pl.Modules.Home
{
    public class HistoryModule : HomeModuleBase
    {
        public HistoryModule(IDbProvider dbProvider)
        {
            Get["/historia"] = _ =>
                {
                    var nominations = dbProvider.GetDb().nominations
                        .WithMvp()
                        .ToList();

                    return View["nominations", new NominationsListModel(nominations)];
                };
        }

        public class NominationsListModel
        {
            private readonly IEnumerable<dynamic> _nominations;

            public NominationsListModel(IEnumerable<dynamic> nominations)
            {
                _nominations = nominations;
            }

            public IEnumerable<DateTime> AllDates
            {
                get
                {
                    return _nominations
                        .Where(x => x.EndDate != null)
                        .Select(x => (DateTime) x.EndDate)
                        .Union(
                            _nominations.Select(y => (DateTime) y.StartDate)
                        );
                }
            }

            public List<int> AllYears
            {
                get
                {
                    return AllDates
                        .Select(x => x.Year)
                        .OrderByDescending(x => x)
                        .Distinct().ToList();
                }
            }

            public IEnumerable<dynamic> MonthsByYear(int year)
            {
                return AllDates.Where(x => x.Year == year)
                    .Select(x => x.Month)
                    .Distinct()
                    .OrderBy(x => x)
                    .Select(x => new
                        {
                            Number = x,
                            Name = DateTimeFormatInfo.CurrentInfo.GetMonthName(x)
                        }.ToExpando()
                    );
            }

            public IEnumerable<dynamic> StartedNominations(int year, int month)
            {
                return _nominations
                    .Where(x => x.StartDate.Year == year && x.StartDate.Month == month)
                    .OrderBy(x => x.Mvp.LastName);
            }

            public IEnumerable<dynamic> FinishedNominations(int year, int month)
            {
                return _nominations
                    .Where(x => x.EndDate != null
                        && x.EndDate.Year == year && x.EndDate.Month == month
                    )
                    .OrderBy(x => x.Mvp.LastName);
            }
        }
    }
}