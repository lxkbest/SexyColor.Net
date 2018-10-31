using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IGlobalSettingsRepository : IRepository<GlobalSettings>
    {
        GlobalSettings GetGetGlobalSettings();

        bool EditGlobalSettings(GlobalSettings entity);
    }
}
