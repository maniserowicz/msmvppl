using System.Collections.Generic;
using System.Dynamic;

namespace System
{
    public static class ObjectExtensions
    {
        public static ExpandoObject ToExpando(this object @this)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (var prop in @this.GetType().GetProperties())
            {
                expando.Add(prop.Name, prop.GetValue(@this, null));
            }

            return (ExpandoObject)expando;
        }
    }
}