using SexyColor.Infrastructure;
using System.Collections.Generic;

namespace SexyColor.BusinessComponents
{
    public interface IBasicsProvincesRepository : IRepository<BasicsProvinces>
    {
       IEnumerable<BasicsProvinces> GetProvinces();
    }
}
