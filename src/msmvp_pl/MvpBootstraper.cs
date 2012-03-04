using System;
using System.Collections.Generic;
using System.Reflection;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using System.Linq;
using Nancy.ViewEngines;
using RssMixxxer;
using TinyIoC;

namespace msmvp_pl
{
    public class MvpBootstraper : DefaultNancyBootstrapper
    {
        private static readonly List<Type> _allModuleTypes;

        static MvpBootstraper()
        {
            _allModuleTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => typeof (NancyModule).IsAssignableFrom(x))
                .ToList();
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            var mixFeeds = container.Resolve<MixFeeds>();
            mixFeeds.ForMyNeeds();
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("assets")
            );

            conventions.ViewLocationConventions.Insert(0, CustomViewLocationConvention);
        }

        // locate view by it's base module's folder
        public static Func<string, dynamic, ViewLocationContext, string> CustomViewLocationConvention = (viewName, model, viewLocationContext) =>
        {
            Func<string, string> trimModuleNameEnd = name =>
                {
                    if (name.EndsWith("Module"))
                    {
                        return name.Substring(0, name.Length - "Module".Length);
                    }
                    if (name.EndsWith("ModuleBase"))
                    {
                        return name.Substring(0, name.Length - "ModuleBase".Length);
                    }

                    return name;
                };

            string fullModuleName = viewLocationContext.ModuleName + "Module";

            var moduleType = _allModuleTypes.SingleOrDefault(x => x.Name == fullModuleName);

            if (moduleType == null)
            {
                return null;
            }

            string baseModuleName = trimModuleNameEnd(moduleType.BaseType.Name);

            return string.Concat("views/", baseModuleName, "/", viewName);
        };
    }
}