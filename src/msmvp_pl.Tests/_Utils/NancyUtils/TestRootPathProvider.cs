using Nancy;

namespace msmvp_pl.Tests
{
    public class TestRootPathProvider : IRootPathProvider
    {
        private static readonly string RootPath;

        static TestRootPathProvider()
        {
            RootPath = PathUtil.GetWebProjectLocation();
        }

        public string GetRootPath()
        {
            return RootPath;
        }
    }
}