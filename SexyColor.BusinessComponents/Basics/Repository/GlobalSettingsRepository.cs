using System;
using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public class GlobalSettingsRepository : Repository<GlobalSettings>, IGlobalSettingsRepository
    {
        public bool EditGlobalSettings(GlobalSettings entity)
        {
            return base.UpdateByCache(entity);
        }

        public GlobalSettings GetGetGlobalSettings()
        {
            return base.GetModel(m => true);

        }
    }
}
