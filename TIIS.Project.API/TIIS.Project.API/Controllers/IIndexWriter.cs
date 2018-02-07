using Lucene.Net.Documents;

namespace TIIS.Project.API.Controllers
{
    public interface IIndexWriter
    {
        void Add(Document doc);
        void Commit();
    }
}