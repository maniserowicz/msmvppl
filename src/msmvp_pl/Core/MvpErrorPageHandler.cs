using Nancy;
using Nancy.ErrorHandling;
using Nancy.ViewEngines;

namespace msmvp_pl.Core
{
    public class MvpErrorPageHandler : IErrorHandler
    {
        private readonly IViewFactory _viewFactory;

        public MvpErrorPageHandler(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode)
        {
            return statusCode == HttpStatusCode.NotFound;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var viewLocationContext = new ViewLocationContext();
            viewLocationContext.Context = context;
            context.Response = _viewFactory.RenderView("_errors/not-found", new object(), viewLocationContext);
        }
    }
}