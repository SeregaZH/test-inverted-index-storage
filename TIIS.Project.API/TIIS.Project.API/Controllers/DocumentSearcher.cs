using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using TIIS.Project.API.Model;
using Version = Lucene.Net.Util.Version;

namespace TIIS.Project.API.Controllers
{
    public sealed class DocumentSearcher: IIndexSearcher
    {
        private readonly IndexSearcher _searcher;

        public DocumentSearcher(IndexSearcher searcher)
        {
            _searcher = searcher;
        }

        public IEnumerable<Data> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return Enumerable.Empty<Data>();
            }

            var props = new[]
            {
                nameof(Data.Name),
                nameof(Data.Type),
                nameof(Data.Url),
                nameof(Data.Content)
            };

            var queryParser = new MultiFieldQueryParser(Version.LUCENE_29, props,
                new StandardAnalyzer(Version.LUCENE_29));
            var boolQuery = new BooleanQuery { {queryParser.Parse(query), Occur.MUST} };
            var result = _searcher.Search(boolQuery, null, _searcher.MaxDoc);
            return result.ScoreDocs.Select(d =>
            {
                var doc = _searcher.Doc(d.Doc);
                return new Data
                {
                    Name = doc.Get("Name"),
                    Url = doc.Get("Url"),
                    Content = doc.Get("Content"),
                    Type = doc.Get("Type")
                };
            });
        }
    }
}
