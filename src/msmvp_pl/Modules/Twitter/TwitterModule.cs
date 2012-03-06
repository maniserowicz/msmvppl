using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Nancy;
using msmvp_pl.Core;
namespace msmvp_pl.Modules.Twitter
{
    public class TwitterModule : TwitterModuleBase
    {
        public TwitterModule(IDbProvider dbProvider)
        {
            Get["/{slug}"] = p =>
                {
                    var mvp = dbProvider.GetDb()
                        .mvps.FindAllBySlug(p["slug"])
                        .WithLinks()
                        .FirstOrDefault()
                        ;

                    if (mvp == null)
                    {
                        return HttpStatusCode.NotFound;
                    }

                    TwitterJson model = TwitterJson.ParseSingle(mvp);

                    return Response.AsJson(model);
                };

            Get["/"] = _ =>
                {
                    var mvps = dbProvider.GetDb().mvps.WithLinks();

                    TwitterJson model = TwitterJson.ParseList(mvps);

                    return Response.AsJson(model);
                };
        }

        public class TwitterJson
        {
            public static TwitterJson ParseSingle(dynamic mvp)
            {
                IEnumerable<dynamic> links = mvp.Links;
                TwitterJson tj = new TwitterJson();

                if (links.Count() > 0 && links.First().Value != null)
                {
                    var twitter = links.Where(x => x.Value.Contains("twitter")).FirstOrDefault();
                    tj.Twitter = GetTwitterId(twitter);
                }

                return tj;
            }

            public static TwitterJson ParseList(dynamic _mvps)
            {
                IEnumerable<dynamic> mvps = _mvps;
                var twitters = new List<string>();

                foreach (var mvp in mvps)
                {
                    var twitter = ParseSingle(mvp).Twitter;

                    if (string.IsNullOrEmpty(twitter))
                    {
                        continue;
                    }

                    twitters.Add("from:" + twitter);
                }

                TwitterJson tj = new TwitterJson();

                tj.Twitter = string.Join(" OR ", twitters);

                return tj;
            }

            private static string GetTwitterId(dynamic twitterLink)
            {
                if (twitterLink != null)
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(twitterLink.Value.ToString());
                    var link = doc.DocumentNode.SelectSingleNode("//a");
                    if (link != null)
                    {
                        var href = link.Attributes["href"].Value;
                        return href.Substring(href.LastIndexOf('/') + 1);
                    }
                }

                return null;
            }

            public string Twitter { get; set; }
        }
    }
}