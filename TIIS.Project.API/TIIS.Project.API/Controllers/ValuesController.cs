using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lucene.Net.Documents;
using Microsoft.AspNetCore.Mvc;
using TIIS.Project.API.Model;

namespace TIIS.Project.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IIndexWriter _writer;
        private readonly IIndexSearcher _searcher;

        public ValuesController(IIndexWriter writer, IIndexSearcher searcher)
        {
            _writer = writer;
            _searcher = searcher;
        }
        
        // GET api/values
        [HttpGet]
        public IEnumerable<Data> Get([FromQuery]string query)
        {
            return _searcher.Search(query);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Data data)
        {
            var doc = new Document();
            doc.Add(new Field(nameof(data.Name), data.Name, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field(nameof(data.Type), data.Type, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field(nameof(data.Url), data.Url, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field(nameof(data.Content), data.Content, Field.Store.YES, Field.Index.ANALYZED));
            _writer.Add(doc);
            _writer.Commit();
            return Created("", data);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
