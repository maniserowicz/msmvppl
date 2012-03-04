using NLog;
using Nancy;
using RssMixxxer.Composition;
using RssMixxxer;
using System.Linq;

namespace msmvp_pl.Modules.Rss
{
    public class RssModule : NancyModule
    {
        private readonly IFeedComposer _feedComposer;

        public RssModule(IFeedComposer feedComposer)
        {
            _feedComposer = feedComposer;
            Get["/mvp-rss"] = _ =>
                {
                    var feed = _feedComposer.ComposeFeed();

                    _log.Debug("RSS request with headers: IfModifiedSince ({0}), IfNoneMatch ({1})"
                        , Request.Headers.IfModifiedSince
                        , string.Join(",", Request.Headers.IfNoneMatch.ToArray())
                    );

                    return feed.GetRssString();
                };
        }

        private static readonly Logger _log = LogManager.GetCurrentClassLogger();
    }
}