using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace TIIS.Project.API.Extensions
{
    public static class LuceneConfigExtensions
    {
        public static IApplicationBuilder ConfigureLucene(this IApplicationBuilder builder, IHostingEnvironment env)
        {
            return builder;
        }
    }
}
