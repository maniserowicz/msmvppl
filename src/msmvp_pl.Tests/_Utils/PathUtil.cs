using System;
using System.IO;
using System.Reflection;
using Nancy.Testing;

namespace msmvp_pl.Tests
{
    public static class PathUtil
    {
        public static string GetAbsoluteTestLocation()
        {
            // some test runner might shadow copy the tested assemblies, making Assembly.Location unsuitable
            // for the purpose of finding current assembly's physical location on disk
            string codeBaseLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            return new Uri(codeBaseLocation).AbsolutePath;
        }

        public static string GetWebProjectLocation()
        {
            string testsLocation = GetAbsoluteTestLocation().Replace('/', Path.DirectorySeparatorChar);

            string srcLocation = PathHelper.GetParent(testsLocation, 3);

            var rootNamespace = typeof (MvpBootstraper).Namespace.Split('.')[0];

            var webProjectLocation = Path.Combine(srcLocation, rootNamespace);

            return webProjectLocation;
        }
    }
}