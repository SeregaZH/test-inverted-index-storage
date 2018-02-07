using Lucene.Net.Store;

namespace TIIS.Project.API
{
    public interface IDirectoryProvider
    {
        Directory Provide();
    }
}