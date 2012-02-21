using Nancy;
using Nancy.Conventions;

namespace msmvp_pl
{
    public class MvpBootstraper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("assets")
            );
        }
    }
}