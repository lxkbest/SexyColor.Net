using System.Collections.Generic;
using System.Linq;

namespace SexyColor.BusinessComponents
{
    public class AnnouncementsService : IAnnouncementsService
    {
        public IAnnouncementsRepository announcementsRepository { get; set; }

        /// <summary>
        /// 编辑站点公告
        /// </summary>
        /// <param name="announcements"></param>
        /// <returns></returns>
        public bool EditAnnouncements(Announcements announcements)
        {
            return announcementsRepository.UpdateCache(announcements);
        }

        /// <summary>
        /// 获取公告实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Announcements GetAnnouncements(int id)
        {
            if (id <= 0)
                return null;
            return announcementsRepository.GetAnnouncements(id);
        }

        /// <summary>
        /// 获取公告id
        /// </summary>
        /// <returns></returns>
        public int GetGetAnnouncementsId()
        {
            IEnumerable<Announcements> lists = announcementsRepository.GetAllByCache<int>("id asc", i => i.Id);
            return lists.FirstOrDefault().Id;
        }
    }
}
