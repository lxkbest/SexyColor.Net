using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public class PointItemsRepository : Repository<PointItems>, IPointItemsRepository
    {
        public bool AddPointItems(PointItems pointItems)
        {
            object result = base.AddByCache(pointItems, true);
            if (result != null) return true;
            return false;
        }

        public PointItems GetFullPointType(int itemskey)
        {
            return base.GetByCache(m => m.Itemskey == itemskey, itemskey);
        }

        public PagingDataSet<PointItems> GetPointItems(int pageIndex, int pageSize)
        {
            int totalCount,
               totalPage;
            return GetPageListByCache<int>(pageIndex, pageSize, out totalCount, out totalPage, "1", "display_order ASC", m=>m.Itemskey);
        }

        public bool UpdateCache(PointItems pointItems)
        {
            return base.UpdateByCache(pointItems);
        }
    }
}
