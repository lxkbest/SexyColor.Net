using System.Collections.Generic;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public class PointTypeService : IPointTypeService
    {
        public IPointTypeRepository pointTypeRepository { get; set; }

        public bool AddPointType(PointType pointType)
        {
            return pointTypeRepository.AddPointType(pointType);
        }

        public bool EditPointType(PointType pointType)
        {
            return pointTypeRepository.UpdateCache(pointType);
        }

        public PointType GetFullPointType(string typekey)
        {
            return pointTypeRepository.GetFullPointType(typekey);
        }

        public PagingDataSet<PointType> GetPointType(int pageIndex, int pageSize)
        {
            return pointTypeRepository.GetPointType(pageIndex,pageSize);
        }

        public IEnumerable<PointType> GetPointTypeList()
        {
            return pointTypeRepository.GetAllByCache<string>("typename desc", i => i.Typekey);
        }
    }
}
