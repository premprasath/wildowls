using WildOwls.Repository.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;
using WildOwls.Domain.Contract;
using WildOwls.Model;

namespace WildOwls.Domain
{
    public class MembersDomain : IMembersDomain
    {

        private readonly IMembersRepository _membersRepository;
        public MembersDomain(IMembersRepository membersRepository)
        {
            _membersRepository = membersRepository;
        }

        public async Task<List<GroupInfo>> GetAllGroupInfoAsync()
        {
            return await _membersRepository.GetAllGroupInfoAsync();
        }

        public async Task<List<MemberInfo>> GetFamilyInfobyIdAsync(string groupId)
        {
            return await _membersRepository.GetFamilyInfobyIdAsync(groupId);
        }
        public async Task<string> InsertMemberAsync(MemberInfo memberInfo)
        {
            return await _membersRepository.InsertMemberAsync(memberInfo);
        }
    }
}
