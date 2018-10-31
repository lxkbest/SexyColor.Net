using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public class PointTypeRepository : Repository<PointType>, IPointTypeRepository
    {
        public bool AddPointType(PointType pointType)
        {
            return bool.Parse(base.AddByCache(pointType, false).ToString());
        }

        public PointType GetFullPointType(string typekey)
        {
            return base.GetByCache(m => m.Typekey == typekey, typekey);
        }
        /// <summary>
        /// 获取积分类型分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<PointType> GetPointType(int pageIndex, int pageSize)
        {
            int totalCount,
                totalPage;
            return GetPageListByCache<string>(pageIndex, pageSize, out totalCount, out totalPage, "1", "typekey ASC", i => i.Typekey);
        }

        public bool UpdateCache(PointType pointType)
        {
            return base.UpdateByCache(pointType);
        }
    }
}
