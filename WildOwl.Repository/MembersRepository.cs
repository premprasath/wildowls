using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WildOwls.Model;
using WildOwls.Repository.Contract;
using WildOwls.Repository.Providers;

namespace WildOwls.Repository
{
    public class MembersRepository : IMembersRepository
    {
        private readonly ISqlProvider _sqlProvider;
        public MembersRepository(ISqlProvider sqlProvider)
        {
            _sqlProvider = sqlProvider;
        }

        public async Task<List<GroupInfo>> GetAllGroupInfoAsync()
        {
            string storedProcedure = "GetAllGroupInfo";
            try
            {
                var result =
                (
                    await _sqlProvider.GetDbConnection.QueryAsync<GroupInfo>(
                    storedProcedure, commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false)
                ).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _sqlProvider.Dispose();
            }
        }

        public async Task<List<MemberInfo>> GetFamilyInfobyIdAsync(string groupId)
        {
            string storedProcedure = "GetMemberInfoById";
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@groupId", groupId, DbType.String);
                var result = (
                    await _sqlProvider.GetDbConnection.QueryAsync<MemberInfo>(
                    storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false)
                    ).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _sqlProvider.Dispose();
            }
        }
        public async Task<string> InsertMemberAsync(MemberInfo memberInfo)
        {
            string storedProcedure = "InsertMember";
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@MemberId", memberInfo.MemberId, DbType.String);
                parameters.Add("@FamilyGroupId", memberInfo.FamilyId, DbType.String);
                parameters.Add("@GroupId", memberInfo.GroupId, DbType.String);
                parameters.Add("@MemberName", memberInfo.MemberName, DbType.String);
                parameters.Add("@WifeName", memberInfo.WifeName, DbType.String);
                parameters.Add("@AdditionalMember", memberInfo.AdditionalMember, DbType.String);
                parameters.Add("@Phone", memberInfo.Phone, DbType.String);
                parameters.Add("@BirthDate", memberInfo.BirthDate, DbType.DateTime);
                parameters.Add("@MarriageDate", memberInfo.MarriageDate, DbType.DateTime);
                parameters.Add("@CoupleId", memberInfo.CoupleId, DbType.String);
                parameters.Add("@IsAdditional", memberInfo.IsAdditional, DbType.Int16);
                parameters.Add("@AmountPaid", memberInfo.AmountPaid, DbType.Int32);
                parameters.Add("@IsEdit", memberInfo.IsEdit, DbType.Int32);
                parameters.Add("@Address", memberInfo.Address, DbType.String);

                var result = await _sqlProvider.GetDbConnection.QueryFirstOrDefaultAsync<int>(
                    storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _sqlProvider.Dispose();
            }
        }
    }
}
