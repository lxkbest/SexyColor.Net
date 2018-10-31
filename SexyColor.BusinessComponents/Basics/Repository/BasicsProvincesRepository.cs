using System.Collections.Generic;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public class BasicsProvincesRepository : Repository<BasicsProvinces>, IBasicsProvincesRepository
    {
        public IEnumerable<BasicsProvinces> GetProvinces()
        {
            return GetAllByCache("provinceid asc", i => i.Provinceid);
        }

    }
}
