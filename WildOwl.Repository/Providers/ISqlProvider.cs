using System.Data;

namespace WildOwls.Repository.Providers
{
    public interface ISqlProvider
    {
        IDbConnection GetDbConnection { get; }

        void Dispose();
    }
}
