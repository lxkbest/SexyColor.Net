using System.Collections.Generic;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IBasicsCitysRepository : IRepository<BasicsCitys>
    {

        IEnumerable<BasicsCitys> GetCitys(int pid);
    }
}
