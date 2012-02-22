using Nancy;
using Nancy.ViewEngines;

namespace msmvp_pl.Tests
{
    // code stolen from http://melinder.se/blog/2012/02/nancytesting-intercept-the-model-sent-to-a-view/
    public class TestingViewFactory : IViewFactory
    {
        private DefaultViewFactory _defaultViewFactory;

        public TestingViewFactory(DefaultViewFactory defaultViewFactory)
        {
            _defaultViewFactory = defaultViewFactory;
        }

        public Response RenderView(string viewName, dynamic model, ViewLocationContext viewLocationContext)
        {
            viewLocationContext.Context.Items["###ViewModel###"] = model;

            return _defaultViewFactory.RenderView(viewName, model, viewLocationContext);
        }
    }
}