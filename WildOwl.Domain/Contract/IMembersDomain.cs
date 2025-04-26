using WildOwls.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WildOwls.Domain.Contract
{
    public interface IMembersDomain
    {
        Task<List<GroupInfo>> GetAllGroupInfoAsync();
        Task<List<MemberInfo>> GetFamilyInfobyIdAsync(string groupId);
        Task<string> InsertMemberAsync(MemberInfo memberInfo);
    }
}
