using Simple.Data;

namespace msmvp_pl.Core
{
    public interface IDbProvider
    {
        dynamic GetDb();
    }

    public class SqlCeDbProvider : IDbProvider
    {
        public dynamic GetDb()
        {
            return Database.OpenNamedConnection("msmvp_pl");
        }
    }
}