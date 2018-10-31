namespace SexyColor.BusinessComponents
{
    public interface IAnnouncementsService
    {
        int GetGetAnnouncementsId();
        Announcements GetAnnouncements(int id);
        bool EditAnnouncements(Announcements announcements);
    }
}
