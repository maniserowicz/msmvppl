using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Nancy;
using Nancy.Bootstrapper;

namespace msmvp_pl.Core
{
    /// <summary>
    /// Finds favicon.ico file and provides it for further usage
    /// Partially copied from https://github.com/NancyFx/Nancy/issues/535
    /// (done when on Nancy v0.10, to remove when upgraded to v0.11
    /// </summary>
    public class FavIconStartup : IStartup
    {
        private readonly IRootPathProvider _rootPathProvider;

        public FavIconStartup(IRootPathProvider rootPathProvider)
        {
            _rootPathProvider = rootPathProvider;
            FavIcon = this.LoadFavicon();
        }

        public void Initialize(IPipelines pipelines)
        {
        }

        private const string faviconFileName = "favicon.ico";
        public static byte[] FavIcon { get; private set; }

        private byte[] LoadFavicon()
        {
            string faviconPath = Path.Combine(_rootPathProvider.GetRootPath(), faviconFileName);

            if (File.Exists(faviconPath) == false)
            {
                return null;
            }

            var converter = new ImageConverter();

            var image = Image.FromFile(faviconPath);

            return (byte[])converter.ConvertTo(image, typeof(byte[]));
        }

        public IEnumerable<TypeRegistration> TypeRegistrations { get; private set; }
        public IEnumerable<CollectionTypeRegistration> CollectionTypeRegistrations { get; private set; }
        public IEnumerable<InstanceRegistration> InstanceRegistrations { get; private set; }
    }
}