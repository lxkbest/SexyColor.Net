using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public class AnnouncementsRepository : Repository<Announcements>, IAnnouncementsRepository
    {
        /// <summary>
        /// 获取公告id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Announcements GetAnnouncements(int id)
        {
            return base.GetByCache(m => m.Id == id, id);
        }

        /// <summary>
        /// 更新实体带缓存
        /// </summary>
        /// <param name="announcements"></param>
        /// <returns></returns>
        public bool UpdateCache(Announcements announcements)
        {
            return base.UpdateByCache(announcements);
        }
    }
}
