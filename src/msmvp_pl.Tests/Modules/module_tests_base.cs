using Nancy.Bootstrapper;
using Nancy.Testing;

namespace msmvp_pl.Tests.Modules
{
    public abstract class module_tests_base
    {
        private INancyBootstrapper _bootstrapper;
        protected Browser _browser;

        public module_tests_base()
        {
            AppDomainAssemblyTypeScanner.LoadAssemblies("*.dll");

            _bootstrapper = new ConfigurableBootstrapper(config => config.ViewFactory<TestingViewFactory>());
            _browser = new Browser(_bootstrapper);
        }

        protected BrowserResponse Get(string url)
        {
            return _browser.Get(url, with => with.HttpRequest());
        }
    }
}