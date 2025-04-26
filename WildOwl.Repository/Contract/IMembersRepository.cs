using System.Collections.Generic;
using System.Threading.Tasks;
using WildOwls.Model;

namespace WildOwls.Repository.Contract
{
    public interface IMembersRepository
    {
        Task<List<GroupInfo>> GetAllGroupInfoAsync();
        Task<List<MemberInfo>> GetFamilyInfobyIdAsync(string groupId);

        Task<string> InsertMemberAsync(MemberInfo memberInfo);
    }
}
