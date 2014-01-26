using Nancy.Testing;
using Xunit;

namespace msmvp_pl.Tests.Modules.home
{
    public class index_tests
        : module_tests_base
    {
        private BrowserResponse execute()
        {
            return Get("/");
        }

        [Fact]
        public void renders_view()
        {
            var response = execute();

            var body = response.Body.AsString();

            Assert.False(body.Contains("errorText"));
        }

        [Fact]
        public void does_not_pass_any_model()
        {
            var response = execute();

            var model = response.GetModel<object>();

            Assert.Null(model);
        }

        [Fact]
        public void sets_title_with_MVP_text()
        {
            var response = execute();

            response.Body["title"]
                .ShouldExistOnce()
                .And.ShouldContain("MVP");
        }
    }
}
