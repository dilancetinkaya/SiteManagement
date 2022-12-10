using System.Threading.Tasks;

namespace SiteManagement.Infrastructure.IServices
{
    public interface IRoleService
    {
        Task AddRole(string name);
    }
}
