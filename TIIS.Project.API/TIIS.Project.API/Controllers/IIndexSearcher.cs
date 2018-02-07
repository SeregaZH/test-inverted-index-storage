using System.Collections.Generic;
using TIIS.Project.API.Model;

namespace TIIS.Project.API.Controllers
{
    public interface IIndexSearcher
    {
        IEnumerable<Data> Search(string query);
    }
}
