using Nancy;
using RssMixxxer.Composition;
using RssMixxxer;

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

                    return feed.GetRssString();
                };
        }
    }
}