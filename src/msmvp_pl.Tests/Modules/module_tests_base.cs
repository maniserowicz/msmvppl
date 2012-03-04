using Nancy.Bootstrapper;
using Nancy.Testing;
using RssMixxxer.LocalCache;

namespace msmvp_pl.Tests.Modules
{
    public abstract class module_tests_base
    {
        private INancyBootstrapper _bootstrapper;
        protected Browser _browser;

        private MvpBootstraper _temp__needed_to_ensure_that_web_dll_is_loaded = new MvpBootstraper();
        private LocalFeedsProvider _temp__needed_to_ensure_that_rss_dll_is_loaded = new LocalFeedsProvider();

        public module_tests_base()
        {
            _bootstrapper = new ConfigurableBootstrapper(config => config.ViewFactory<TestingViewFactory>());
            _browser = new Browser(_bootstrapper);
        }

        protected BrowserResponse Get(string url)
        {
            return _browser.Get(url, with => with.HttpRequest());
        }
    }
}