using LinkConverterApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkConverterApi.Repository
{
    public interface ILinkRepository
    {
        void SaveLinks(Link link);
    }
}
