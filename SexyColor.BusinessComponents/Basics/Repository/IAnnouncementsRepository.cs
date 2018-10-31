using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IAnnouncementsRepository : IRepository<Announcements>
    {
        Announcements GetAnnouncements(int id);
        bool UpdateCache(Announcements announcements);
    }
}
