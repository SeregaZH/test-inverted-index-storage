using System;
using System.IO;
using Lucene.Net.Store;
using Microsoft.AspNetCore.Hosting;
using Directory = Lucene.Net.Store.Directory;

namespace TIIS.Project.API
{
    public sealed class SimpleFSDirectoryProvider : IDirectoryProvider
    {
        private readonly IHostingEnvironment _env;
        private readonly Lazy<Directory> _directoryLazy;

        public SimpleFSDirectoryProvider(IHostingEnvironment env)
        {
            _env = env;
            _directoryLazy = new Lazy<Directory>(Initializer);
        }

        public Directory Provide()
        {
            return _directoryLazy.Value;
        }

        private Directory Initializer()
        {
            var path = new DirectoryInfo(Path.Combine(_env.ContentRootPath, "LuceneIndex"));
            if (!path.Exists)
            {
                path.Create();
                path.Refresh();
            }
            return new SimpleFSDirectory(path);
        }

    }
}
