using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
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
                        .WithNominations()
                        .FirstOrDefault()
                        ;

                    if(mvp == null)
                    {
                        return HttpStatusCode.NotFound;
                    }

                    var model = new MvpDetailsViewModel(mvp);

                    return View["mvp-details", model];
                };
        }

        public class MvpDetailsViewModel
        {
            private readonly dynamic _mvp;

            public MvpDetailsViewModel(dynamic mvp)
            {
                _mvp = mvp;

                SetTwitterData();
            }

            private void SetTwitterData()
            {
                IEnumerable<dynamic> links = _mvp.Links;

                var twitter = links.Where(x => x.Value.Contains("twitter")).FirstOrDefault();

                HasTwitter = false;

                if(twitter != null)
                {

                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(twitter.Value.ToString());
                    var link = doc.DocumentNode.SelectSingleNode("//a");
                    if(link != null)
                    {
                        HasTwitter = true;
                        var twitterLink = link.Attributes["href"].Value;
                        Twitter = twitterLink.Substring(twitterLink.LastIndexOf('/') + 1);
                    }
                }
            }

            public dynamic Mvp
            {
                get { return _mvp; }
            }

            public bool HasTwitter { get; set; }
            public string Twitter { get; set; }
        }
    }
}