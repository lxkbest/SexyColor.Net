using SexyColor.Infrastructure;

namespace SexyColor.BusinessComponents
{
    public interface IPermissionItemsRepository : IRepository<PermissionItems>
    {
        string GetJsonStringByParentsid(string parentsid);

        bool AddPermissionItems(PermissionItems entity, bool isIdentity = true);

        bool UpdatePermissionItems(PermissionItems entity, string oldParentsid);

        bool DeletePermissionItems(PermissionItems entity);
    }
}
