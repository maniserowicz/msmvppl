using System;
using System.Collections.Generic;
using System.Reflection;
using Nancy;
using Nancy.Conventions;
using System.Linq;

namespace msmvp_pl
{
    public class MvpBootstraper : DefaultNancyBootstrapper
    {
        private List<Type> _allModuleTypes;

        public MvpBootstraper()
        {
            _allModuleTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => typeof(NancyModule).IsAssignableFrom(x))
                .ToList();
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("assets")
            );

            // locate view by it's base module's folder
            conventions.ViewLocationConventions.Insert(0, (viewName, model, viewLocationContext) =>
                {
                    Func<string, string> trimModuleNameEnd = name =>
                        {
                            if (name.EndsWith("Module"))
                            {
                                return name.Substring(0, name.Length - "Module".Length);
                            }
                            else if (name.EndsWith("ModuleBase"))
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
                });
        }
    }
}