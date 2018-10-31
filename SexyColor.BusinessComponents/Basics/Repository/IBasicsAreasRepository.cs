using System.Collections.Generic;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IBasicsAreasRepository : IRepository<BasicsAreas>
    {
        IEnumerable<BasicsAreas> GetAreas(int pid);
    }
}
