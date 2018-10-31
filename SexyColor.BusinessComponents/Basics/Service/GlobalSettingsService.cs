using System;

namespace SexyColor.BusinessComponents
{
    public class GlobalSettingsService : IGlobalSettingsService
    {

        public IGlobalSettingsRepository globalSettingsRepository { get; set; }

        public GlobalSettings EditGlobalSettings(GlobalSettings entity)
        {
            if (entity == null)
                return null;

            globalSettingsRepository.EditGlobalSettings(entity);

            return entity;
        }

        public GlobalSettings GetGetGlobalSettings()
        {
            return globalSettingsRepository.GetGetGlobalSettings();
        }
    }
}
