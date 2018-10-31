namespace SexyColor.BusinessComponents
{
    public interface IGlobalSettingsService
    {
        GlobalSettings GetGetGlobalSettings();

        GlobalSettings EditGlobalSettings(GlobalSettings entity);
    }
}
