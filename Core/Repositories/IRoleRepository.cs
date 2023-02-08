using Board.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Board.Core.Repositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
