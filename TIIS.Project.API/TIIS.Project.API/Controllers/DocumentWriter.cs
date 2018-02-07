using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace TIIS.Project.API.Controllers
{
    public sealed class DocumentWriter : IIndexWriter
    {
        private readonly IndexWriter _writer;

        public DocumentWriter(IndexWriter writer)
        {
            _writer = writer;
        }

        public void Add(Document doc)
        {
            _writer.AddDocument(doc);
        }

        public void Commit()
        {
            _writer.Commit();
        }
    }
}
