using System;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public class PointItemsService : IPointItemsService
    {

        public IPointItemsRepository pointItemsRepository { get; set; }

        public bool AddPointItems(PointItems pointItems)
        {
            return pointItemsRepository.AddPointItems(pointItems);
        }

        public bool EditPointItems(PointItems pointItems)
        {
            return pointItemsRepository.UpdateCache(pointItems);
        }

        public PointItems GetFullPointItems(int itemskey)
        {
            return pointItemsRepository.GetFullPointType(itemskey);
        }

        public PagingDataSet<PointItems> GetPointItems(int pageIndex, int pageSize)
        {
            return pointItemsRepository.GetPointItems(pageIndex, pageSize);
        }
    }
}
